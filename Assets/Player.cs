using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using diagnostics = System.Diagnostics;
using Assets.Scripts;
using System.Timers;

public enum PlayerMode
{
    ACTIVE,
    PASSIVE
}

public class Player : NetworkBehaviour
{

    public const float lookDistance = 1f;
    public const KeyCode interactionKey = KeyCode.F;
    public diagnostics.Stopwatch throwSpeedWatch;

    private int drunknessLevel;

    private PlayerSkill playerSkill;

    private NetworkIdentity objNetId;

    public int ThrowingStrength { get { return playerSkill.ThrowingStrength; } }

    public int RunningSpeed { get { return playerSkill.RunningSpeed; } }

    [SerializeField]
    public PlayerMode mode = PlayerMode.PASSIVE;

    [SerializeField]
    public GameObject ball = null;

    private Camera camera;

    public BeerTarget ownBeer;

    [HideInInspector]
    public AbstractCollectable CollectedObject = null;

    [HideInInspector]
    public AbstractCollectable lastLookAtObject = null;

    private bool drinking = false;
    private diagnostics.Stopwatch drinkWatch;
    private Timer drinkingTimer = null;

    // Use this for initialization
    void Start()
    {
        camera = GetComponent<PlayerController>().cam;
        throwSpeedWatch = new System.Diagnostics.Stopwatch();
        playerSkill = new PlayerSkill();
	}   

    // Update is called once per frame
    void Update () {

        if (!isLocalPlayer || playerSkill.Dead)
        {
            return;
        }

        if (this.mode == PlayerMode.ACTIVE)
        {
            if (GameManager.instance.GameState == GameManager.State.INITIAL)
            {
                ball.transform.position = camera.transform.forward * 2.0f + camera.transform.position;

            }


            if (Input.GetMouseButtonDown(0))
            {
                throwSpeedWatch.Start();
            }


            if (Input.GetMouseButtonUp(0) && GameManager.instance.GameState == GameManager.State.INITIAL)
            {
                var projectile = ball.GetComponent<Assets.Scripts.Projectile>();
                throwSpeedWatch.Stop();
                float thrust = Mathf.Max(7, (throwSpeedWatch.Elapsed.Milliseconds + throwSpeedWatch.Elapsed.Seconds * 1000) / ThrowingStrength);
                var transformVector = camera.transform.forward.normalized * thrust;
                projectile.FireShot(transformVector);

            }
        }


        LookAt();
        

        if (Input.GetKeyDown(interactionKey) && lastLookAtObject != null && lastLookAtObject.AllowedToCollect(this))
        {
            this.CollectedObject = lastLookAtObject;
            CollectedObject.OnCollect(this);
        }

        if (CollectedObject != null)
        {
            var position = camera.transform.forward;
            position.x *= 2f;
            position.z *= 2f;
            CollectedObject.transform.position = position + camera.transform.position;
            
            if (CollectedObject.CanDrop(this))
            {
                CollectedObject.OnDrop(this);
                CollectedObject = null;
            }
        }

       if (drinking && GameManager.instance.GameState == GameManager.State.FINISHED)
        {
            CollectedObject.OnDrop(this);
        }

    }

    public void StartDrinkingTimer()
    {
        drinking = true;
        if (drinkingTimer == null)
        {
            drinkingTimer = new Timer(1000);
            drinkingTimer.Elapsed += Copping;
            drinkingTimer.Enabled = false;
            drinkingTimer.Start();
        }    
    }

    void LookAt()
    {
        // object that stores the results of a raycast
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;
        RaycastHit rayCastHit;


        if (Physics.Raycast(rayOrigin, rayDirection, out rayCastHit, lookDistance))
        {
            // See if we have hit an object that carries a ILookAtHandler component
            AbstractCollectable objectLookedAt = rayCastHit.collider.GetComponent<AbstractCollectable>();

            // if we start looking at a valid object, call its "start" mehtod
            // if we stop looking at a valid object, call its "end" method
            if (objectLookedAt != null)
            {
                if (lastLookAtObject == null)
                {
                    lastLookAtObject = objectLookedAt;
                }
                else if (objectLookedAt != lastLookAtObject)
                {
                    lastLookAtObject = objectLookedAt;
                }
            }
            else if (lastLookAtObject != null)
            {
                lastLookAtObject = null;
            }
        }
        else if (lastLookAtObject != null)
        {
            lastLookAtObject = null;
        }

       
    }

    private void Copping(object source, System.Timers.ElapsedEventArgs e)
    {


        if (!CollectedObject is BeerTarget)
        {
            return;
        }
        var beerTarget = (BeerTarget)CollectedObject;

        if (playerSkill.Dead)
            return;

        drunknessLevel++;
        
        // Update Fill Level depending on Drining Speed

        if (drunknessLevel >= playerSkill.DrinkingCapacty)
        {
            ThrowUp();
            // Throwing up
            // Player Died, Hide
        }

        beerTarget.Drink(playerSkill.DrinkingSpeed);
        if (beerTarget.CanDrop(this))
        {
            this.drinkingTimer.Enabled = false;
            drinkingTimer.Stop();
            // empty stuff
        }
    }

    private void ThrowUp()
    {
        playerSkill.Dead = true;
        // Hide Player
        // animate dead
    }

    public void FinishBeer()
    {
        var rb = CollectedObject.GetComponent<Rigidbody>();
        var force = camera.transform.forward.normalized * 3;
        force.y = 3f;
        rb.AddForce(force);
    }



}

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

    public int ThrowingStrength { get { return playerSkill.ThrowingStrength; } }

    public int RunningSpeed { get { return playerSkill.RunningSpeed; } }

    [SerializeField]
    public PlayerMode mode = PlayerMode.PASSIVE;

    [SerializeField]
    public GameObject ball = null;

    private Camera camera;

    public BeerTarget ownBeer;

    public BeerTarget beerInHand = null;

    [HideInInspector]
    public BeerTarget lastLookAtObject = null;

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
            if (GameManager.instance.gameState == GameManager.State.INITIAL)
            {
                ball.transform.position = camera.transform.forward * 2.0f + camera.transform.position;

            }


            if (Input.GetMouseButtonDown(0))
            {
                throwSpeedWatch.Start();
            }


            if (Input.GetMouseButtonUp(0) && GameManager.instance.gameState == GameManager.State.INITIAL)
            {
                var projectile = ball.GetComponent<Assets.Scripts.Projectile>();
                throwSpeedWatch.Stop();
                float thrust = Mathf.Max(7, (throwSpeedWatch.Elapsed.Milliseconds + throwSpeedWatch.Elapsed.Seconds * 1000) / ThrowingStrength);
                var transformVector = camera.transform.forward.normalized * thrust;
                projectile.FireShot(transformVector);

            }
        }

        // TODO uncomment additional condition
        if (mode == PlayerMode.ACTIVE 
            // && GameManager.instance.gameState == GameManager.State.HIT
            )
        {
            LookAt();
        }

        if (Input.GetKeyDown(interactionKey) && lastLookAtObject != null)
        {
            beerInHand = lastLookAtObject;
            beerInHand.TakeBeer();
            startDrinkingTimer();
        }

        if (beerInHand != null)
        {
            beerInHand.transform.position = camera.transform.forward * 1.2f + camera.transform.position;
            if (beerInHand.Empty)
            {
                FinishBeer();
                beerInHand = null;
            }
        }

       

    }

    void startDrinkingTimer()
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
            BeerTarget beerLookedAt = rayCastHit.collider.GetComponent<BeerTarget>();

            // if we start looking at a valid object, call its "start" mehtod
            // if we stop looking at a valid object, call its "end" method
            if (beerLookedAt != null)
            {
                
                if (lastLookAtObject == null)
                {
                    lastLookAtObject = beerLookedAt;
                }
                else if (beerLookedAt != lastLookAtObject)
                {
                    lastLookAtObject = beerLookedAt;
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

        Debug.Log("Drinking Speed: " + playerSkill.DrinkingSpeed);
        Debug.Log(beerInHand == null);
        beerInHand.Drink(playerSkill.DrinkingSpeed);
        if (beerInHand.Empty)
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

    private void FinishBeer()
    {
        var rb = beerInHand.GetComponent<Rigidbody>();
        var force = camera.transform.forward.normalized * 3;
        force.y = 3f;
        rb.AddForce(force);
    }

}

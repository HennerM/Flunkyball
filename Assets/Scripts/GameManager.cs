using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour 
{

    public enum State
    {
        INITIAL,
        SHOT,
        HIT,
        MISSED,
        FINISHED
    }

    private NetworkIdentity objNetId;
    public IDictionary<int, Beer> beerMap;

    [SyncVar]
    private State gameState;

    [SerializeField]
    public State GameState
    {
        get
        {
            return this.gameState;
        }
        set
        {
            this.CmdPushState(value);
        }
    }

   
    public TextMesh textMeshPrefab;

    private TextMesh shoutout;

    void Start()
    {
        GameState = State.INITIAL;
    }

    private Assets.Scripts.Target target;

    public Assets.Scripts.Target Target
    {
        get
        {
            return target;
        }
        set
        {
            if (target == null)
            {
                target = value;
                target.targetFellDown += OnTargetDown;
            }
        }
    }

    private GameObject ball;

    public GameObject Ball
    {
        get
        {
            return this.ball;
        }
        set
        {
            if (this.ball == null)
            {
                this.ball = value;
            }
            var projectile = ball.GetComponent<Assets.Scripts.Projectile>();
            if (projectile != null)
            {
                projectile.OnShotFired += () =>
                {

                    this.GameState = State.SHOT;
                };
            }
        }
    }



    void OnTargetDown()
    {
        if (GameState == State.SHOT)    
        {
            GameState = State.HIT; ;
            Vector3 spawnPosition = new Vector3(0.0f, 1.0f, 1.0f);
            Quaternion spawnRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            TextMesh wallTxt= (TextMesh)Instantiate(textMeshPrefab, Vector3.up * 10, spawnRotation);
            wallTxt.transform.position = Camera.main.transform.forward * 2.0f + Camera.main.transform.position;
            wallTxt.transform.Rotate(new Vector3(0f, 90f, 0f));
            wallTxt.text = "DRINK";
            StartCoroutine(DoTheDance());
            shoutout = wallTxt;
        }
       
    }

    public IEnumerator DoTheDance()
    {
        yield return new WaitForSeconds(3f); // waits 3 seconds
        shoutout.gameObject.SetActive(false);
    }

    void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

    public


	#region Main Gameloop


	#endregion

	#region Accessing the object

	// Kind of like a Singleton. Easy way to access MonoBehaviors that only exist once in a game. 
	static GameManager instance;

	// Awake() is like Start() but will be called earlier. See https://docs.unity3d.com/Manual/ExecutionOrder.html
	void Awake ()
	{
		GameManager.instance = this;
	}

    #endregion



    [Command]
    void CmdPushState(State newState)
    {
        objNetId = GetComponent<NetworkIdentity>();        // get the object's network ID
        objNetId.AssignClientAuthority(connectionToClient);    // assign authority to the player who is changing the color
        RpcPushState(newState);                                    // usse a Client RPC function to "paint" the object on all clients
        objNetId.RemoveClientAuthority(connectionToClient);    // remove the authority from the player who changed the color
    }

    [ClientRpc]
    void RpcPushState(State newState)
    {
        this.gameState = newState;
    }

}

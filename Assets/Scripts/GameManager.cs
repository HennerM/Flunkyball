using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour 
{

    public enum State
    {
        INITIAL,
        SHOT,
        HIT,
        MISSED,
        FINISHED
    }


    public IDictionary<int, Beer> beerMap;

	public CharacterController character;

    public State gameState;

    public TextMesh textMeshPrefab;

    private TextMesh shoutout;

    public Assets.Scripts.Target target;
    public Assets.Scripts.Projectile projectile;

    void Start()
    {
        gameState = State.INITIAL;
        if (target != null)
        {
            target.targetFellDown += OnTargetDown;

        }
        if (this.projectile != null)
        {
            projectile.OnShotFired += () =>
            {
                this.gameState = State.SHOT;
            };
        }
       
    }

    void OnTargetDown()
    {
        Debug.Log("Game State:" + gameState);
        if (gameState == State.SHOT)    
        {
            Debug.Log("bottle down");
            gameState = State.HIT; ;
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

    
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using diagnostics = System.Diagnostics;

public enum PlayerMode
{
    ACTIVE,
    PASSIVE
}

public class Player : NetworkBehaviour {

    public diagnostics.Stopwatch watch;

    [SerializeField]
    public PlayerMode mode = PlayerMode.PASSIVE;

    [SerializeField]
    public GameObject ball = null;

    private Camera camera;

	// Use this for initialization
	void Start () {
        camera = GetComponent<PlayerController>().cam;
        watch = new System.Diagnostics.Stopwatch();
	}
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
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
                watch.Start();
            }


            if (Input.GetMouseButtonUp(0) && GameManager.instance.gameState == GameManager.State.INITIAL)
            {
                var projectile = ball.GetComponent<Assets.Scripts.Projectile>();
                watch.Stop();
                float thrust = Mathf.Max(7, (watch.Elapsed.Milliseconds + watch.Elapsed.Seconds * 1000) / 10);
                var transformVector = camera.transform.forward.normalized * thrust;
                Debug.Log(transformVector);
                projectile.FireShot(transformVector);

            }
        }

    }
}

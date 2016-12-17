using UnityEngine;
using System.Collections;
using diagnostics = System.Diagnostics;

public class ShootBall : MonoBehaviour {


    public Rigidbody ball;
    public diagnostics.Stopwatch watch;
    private Camera m_Camera;

    bool shooted = false;

    // Use this for initialization
    void Start () {
        watch = new System.Diagnostics.Stopwatch();
    }

    // Update is called once per frame
    void Update () {

        if (!shooted) {
            var forward = transform.forward * 3;
            var position = forward + transform.position;
            ball.transform.position = position;
        }

        if (Input.GetMouseButtonDown(0))
        {
            watch.Start();
        }

        if (Input.GetMouseButtonUp(0))
        {

            watch.Stop();
            float thrust = watch.Elapsed.Milliseconds / 75;
            shooted = true;
            ball.useGravity = true;
            Debug.Log("Stopwatch stopped: " + thrust);
            var transformVector = transform.forward *thrust;
            ball.AddForce(transformVector, ForceMode.Impulse);
        }

    }

    void FixedUpdate()
    {
        
    }
}

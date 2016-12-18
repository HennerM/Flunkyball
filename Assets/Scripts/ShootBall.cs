using UnityEngine;
using System.Collections;
using diagnostics = System.Diagnostics;

public class ShootBall : MonoBehaviour {


    public Assets.Scripts.Projectile ball;
    public diagnostics.Stopwatch watch;
    private Camera m_Camera;

    bool shooted = false;

    // Use this for initialization
    void Start () {
        watch = new System.Diagnostics.Stopwatch();
        m_Camera = Camera.main;
    }

    // Update is called once per frame
    void Update () {


        if (ball == null)
        {
        //    Debug.Log("no ballls fucktard");
            return;
        } else
        {
            //Debug.Log("such balls, much wow");
        }

        if (!shooted) {
            ball.transform.position = m_Camera.transform.forward * 2.0f + m_Camera.transform.position;
        }

        if (Input.GetMouseButtonDown(0))
        {
            watch.Start();
        }

        if (Input.GetMouseButtonUp(0) && !shooted)
        {
            var rb = ball.GetComponent<Rigidbody>();
            watch.Stop();
            float thrust = Mathf.Max(7,(watch.Elapsed.Milliseconds + watch.Elapsed.Seconds * 1000) / 10);
            shooted = true;
            rb.useGravity = true;
            var transformVector = m_Camera.transform.forward.normalized * thrust;
            rb.AddForce(transformVector, ForceMode.Force);
            ball.ShotFired();
            
        }



    }

    void FixedUpdate()
    {
        
    }
}

using UnityEngine;
using System.Collections;

public class ShootBall : MonoBehaviour {


    public Rigidbody ball;
    public float thrust;
    private Camera m_Camera;

    bool shooted = false;

    // Use this for initialization
    void Start () {

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
            shooted = true;
            var transformVector = transform.forward * thrust;
            transformVector.y += 5;
            ball.AddForce(transformVector, ForceMode.Impulse);
            ball.useGravity = true;
        }

    }

    void FixedUpdate()
    {
        
    }
}

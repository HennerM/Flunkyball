using UnityEngine;
using System.Collections;

public class ShootBall : MonoBehaviour {


    public Rigidbody rb;
    public float thrust;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void StaticUpdate () {
	
        if (Input.GetKeyDown("space"))
        {
            throw new System.Exception("asdfasd");
            Vector3 force = new Vector3(0, 0, thrust);
            rb.AddForce(0, 0, thrust, ForceMode.Impulse);
            rb.useGravity = true;
        }
	}
}

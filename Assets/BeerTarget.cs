using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BeerTarget : NetworkBehaviour {


    const float BEER_DEFAULT_MASS = 0.5f;

    private Rigidbody rb;



    public float fill = 1f;

    public bool opened = false;

    public int Id;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
        rb.mass = BEER_DEFAULT_MASS;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeBeer()
    {
        if (opened == false)
        {
            var audio = GetComponent<AudioSource>();
            audio.Play();
            opened = true;
        }
    }

    public bool Drink(float amount)
    {

    }
}

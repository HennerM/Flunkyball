using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BeerTarget : NetworkBehaviour {


    const float BEER_DEFAULT_MASS = 0.5f;

    private Rigidbody rb;



    public float fill = 500;

    public bool opened = false;

    public int Id;

    private bool destroyed = false;

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

    public void Drink(float amount)
    {
        Debug.Log("fill state: " + fill);
        this.fill -= amount;
    }

    public bool Empty
    {
        get
        {
            return this.fill < 0;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (Empty && !destroyed)
        {
            var audio = GetComponent<AudioSource>();
            var audioClip = (AudioClip)Resources.Load("Assets/Sound/beer_fall");
            audio.clip = audioClip;
            audio.Play();
            destroyed = true;
        }
    }
}

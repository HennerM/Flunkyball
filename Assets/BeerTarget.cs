using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class BeerTarget : Assets.Scripts.AbstractCollectable
{


    const float BEER_DEFAULT_MASS = 1f;
    const float MAX_FILL = 500;

    private Rigidbody rb;


    [SyncVar(hook = "OnChangeFill")]
    public float fill = 500;

    public bool opened = false;

    public int Id;

    private bool destroyed = false;



    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.mass = BEER_DEFAULT_MASS;
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

    void OnChangeFill(float newFilll)
    {
        rb.mass = BEER_DEFAULT_MASS * Mathf.Max(0, MAX_FILL - fill);
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

    public override void OnCollect(Player player)
    {
        player.StartDrinkingTimer();

    }

    public override void OnDrop(Player player)
    {
        Debug.Log("drop se beer");
        player.FinishBeer();
    }

    public override bool AllowedToCollect(Player player)
    {
        return player.ownBeer == this && player.mode == PlayerMode.ACTIVE && GameManager.instance.GameState == GameManager.State.HIT;
    }


    public override bool CanDrop(Player player)
    {
        return this.Empty;
    }

}



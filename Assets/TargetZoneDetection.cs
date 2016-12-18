using UnityEngine;
using System.Collections;

public class TargetZoneDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<Assets.Scripts.Target>();
        if (target != null)
        {
            GameManager.instance.GameState = GameManager.State.FINISHED;
        }
    }
}

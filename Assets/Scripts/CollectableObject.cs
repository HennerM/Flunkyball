using UnityEngine;
using System.Collections;

public class CollectableObject : MonoBehaviour, ILookAtHandler {

	public int objectID;

	public void OnLookatEnter()
	{
		GetComponent<Renderer>().material.SetFloat("_Emission", 1f);
	}

	public void OnLookatExit()
	{
		GetComponent<Renderer>().material.SetFloat("_Emission", 0);
	}

	public void OnLookatInteraction()
	{
		//GameManager.instance.CollectObject(objectID);
		GetComponent<AudioSource>().Play();
//		gameObject.SetActive(false);
		GetComponent<Collider>().enabled = false;
		GetComponent<Renderer>().enabled = false;
	}
}

using UnityEngine;
using System.Collections;

public class LookAtBox : MonoBehaviour, ILookAtHandler {

	public void OnLookatEnter()
	{
		this.GetComponent<Renderer>().material.color = Color.yellow;
	}

	public void OnLookatExit()
	{
		this.GetComponent<Renderer>().material.color = Color.green;
	}

	public void OnLookatInteraction()
	{
		Debug.Log("Interaction!");
	}
}

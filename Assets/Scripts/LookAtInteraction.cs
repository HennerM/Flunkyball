using UnityEngine;
using System.Collections;

// Attach this to a FirstPersonGameController's Camer in order to interact with objects by looking at them.
// Works together with ILookAtHandler
public class LookAtInteraction : MonoBehaviour 
{
	public float lookDistance = 10f;
	public KeyCode interactionKey = KeyCode.F;
	public Texture crossHair;
	public Texture crossHairActive;


	[HideInInspector]
	public ILookAtHandler lastLookAtObject = null;

	// Provided by Unity, executed every frame
	void Update ()
	{
		// object that stores the results of a raycast
		Vector3 rayOrigin = transform.position;
		Vector3 rayDirection = transform.forward;
		RaycastHit rayCastHit;

		// Perform a raycast. The physics engine traverses the ray in the scene and looks if it hits any colliders.
		// Returns true if anyhting is hit.
		// This is very expensive to calculate.
		if(Physics.Raycast(rayOrigin, rayDirection, out rayCastHit, lookDistance)) 
		{
			// See if we have hit an object that carries a ILookAtHandler component
			ILookAtHandler currentLookAtObject = rayCastHit.collider.GetComponent<ILookAtHandler>();

			// if we start looking at a valid object, call its "start" mehtod
			// if we stop looking at a valid object, call its "end" method
			if(currentLookAtObject != null)
			{
				if(lastLookAtObject == null)
				{
					currentLookAtObject.OnLookatEnter();
					lastLookAtObject = currentLookAtObject;
				}
				else if(currentLookAtObject != lastLookAtObject)
				{
					lastLookAtObject.OnLookatExit();
					currentLookAtObject.OnLookatEnter();

					lastLookAtObject = currentLookAtObject;
				}
			}
			else if (lastLookAtObject != null)
			{
				lastLookAtObject.OnLookatExit();
				lastLookAtObject = null;
			}
		}
		else if (lastLookAtObject != null)
		{
			lastLookAtObject.OnLookatExit();
			lastLookAtObject = null;
		}


		if (Input.GetKeyDown(interactionKey) && lastLookAtObject != null)
		{
			lastLookAtObject.OnLookatInteraction();
		}
	}

	// Provided by Unity to draw helper objects in the scene
	void OnDrawGizmos ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + transform.forward * lookDistance);
	}

	// Provided by Unity to draw simple GUI from scripts
	// for more complex GUI, use the UGUI system https://unity3d.com/learn/tutorials/topics/user-interface-ui
	void OnGUI ()
	{
		Vector2 crossHairPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Vector2 crossHairSize = new Vector2(10f, 10f);

		if(lastLookAtObject == null)
			GUI.DrawTexture(new Rect(crossHairPosition, crossHairSize), crossHair);
		else 
			GUI.DrawTexture(new Rect(crossHairPosition, crossHairSize), crossHairActive);
	}
}

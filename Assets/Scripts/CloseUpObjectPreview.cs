using UnityEngine;
using System.Collections;

public class CloseUpObjectPreview : MonoBehaviour {

	public Transform rotateObject;
	public Camera camera;
	public GameObject [] objectsToLookAt;

	Vector3 lastMousePos;
	int visible = -1;

	public void ShowPreview (int objectID)
	{
		camera.enabled = true;
		rotateObject.localEulerAngles = Vector3.zero;
		objectsToLookAt[objectID].SetActive(true);
		visible = objectID;
	}

	public void HidePreview ()
	{
		camera.enabled = false;
		objectsToLookAt[visible].SetActive(false);
		visible = -1;
	}

	void Update ()
	{
		if(visible != -1)
		{
			Vector3 delta = Input.mousePosition - lastMousePos;
	
			rotateObject.Rotate(Vector3.up, -delta.x);
			rotateObject.Rotate(Vector3.right, delta.y);
	
			lastMousePos = Input.mousePosition;

			if(Input.GetMouseButtonDown(0))
			{
				HidePreview();
			}
		}
	}

	#region Accessing the object

	public static CloseUpObjectPreview instance;

	void Awake ()
	{
		CloseUpObjectPreview.instance = this;
	}

	#endregion
}

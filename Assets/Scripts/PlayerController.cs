using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
    public float mouseSensitivity = 180f;
    public Camera cam; // Drag camera into here
    float leftRightRot;
    float verticalRot;

    void Start()
    {
        // IF I'M THE PLAYER, STOP HERE (DON'T TURN MY OWN CAMERA OFF)
        if (isLocalPlayer) return;

        // DISABLE CAMERA AND CONTROLS HERE (BECAUSE THEY ARE NOT ME)
        cam.enabled = false;
    }


    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //wasd moves
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;

        transform.Translate(x, 0, z);


        //mouse moves
        leftRightRot = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        verticalRot -= Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        transform.Rotate(new Vector3(0, leftRightRot));

        cam.transform.localEulerAngles = new Vector3(verticalRot, 0);
    }
}

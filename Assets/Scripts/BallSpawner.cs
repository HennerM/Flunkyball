using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BallSpawner : NetworkBehaviour{

    public GameObject ballPrefab;

    public override void OnStartServer()
    {
        Vector3 spawnPosition = new Vector3(0.0f, 1.0f, 1.0f);
        Quaternion spawnRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        GameObject ball = (GameObject)Instantiate(ballPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(ball);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EntitySpawner : NetworkBehaviour {

    public GameObject bottlePrefab;
    public GameObject ballPrefab;

    public override void OnStartServer()

    {
        Vector3 spawnPosition = new Vector3(0.0f, 1.0f, 1.0f);
        Quaternion spawnRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        GameObject bottle = (GameObject)Instantiate(bottlePrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(bottle);
    }
}

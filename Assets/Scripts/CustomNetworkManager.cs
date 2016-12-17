using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class CustomNetworkManager : NetworkManager {


    public Assets.Scripts.Projectile projectile;

    private int nrOfPlayers = 0;

    public virtual void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        //PlayerSpawnPoint spawnPoint = PlayerSpawnPoint.Next();
        //Vector3 position = Vector3.zero;
        //Quaternion rotation = Quaternion.identity;
        //if (spawnPoint != null)
        //{
        //    position = spawnPoint.position;
        //    rotation = spawnPoint.rotation;
        //    Debug.Log(rotation);
        //}
        var position = new Vector3(0, 1, 0);
        Quaternion spawnRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        var tempPlayer = playerPrefab;
        var shootBall = tempPlayer.GetComponent<ShootBall>();
        if (nrOfPlayers == 0)
        {
            shootBall.ball = projectile;
        }
        nrOfPlayers++;
        var player = (GameObject)GameObject.Instantiate(playerPrefab, position, spawnRotation);
        NetworkServer.AddPlayerForConnection(conn, tempPlayer, playerControllerId);
    }
}

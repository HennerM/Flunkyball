using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class CustomNetworkManager : NetworkManager {

   
    private int nrOfPlayers = 0;

    public GameObject projectilePrefab;

    //public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    //{
    //    base(conn, playerControllerId);
    //    //PlayerSpawnPoint spawnPoint = PlayerSpawnPoint.Next();
    //    //Vector3 position = Vector3.zero;
    //    //Quaternion rotation = Quaternion.identity;
    //    //if (spawnPoint != null)
    //    //{
    //    //    position = spawnPoint.position;
    //    //    rotation = spawnPoint.rotation;
    //    //    Debug.Log(rotation);
    //    //}
    //    var position = new Vector3(10, 1, 0);
    //    Quaternion spawnRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    //    var tempPlayer = playerPrefab;
    //    if (nrOfPlayers == 0)
    //    {
    //        var ball = Instantiate(projectilePrefab);
    //        NetworkServer.Spawn(ball);
    //        tempPlayer.GetComponent<ShootBall>().ball = ball.GetComponent<Assets.Scripts.Projectile>();
    //    }
    //    nrOfPlayers++;
    //    var player = (GameObject)GameObject.Instantiate(tempPlayer, position, spawnRotation);
    //    NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    //}
}

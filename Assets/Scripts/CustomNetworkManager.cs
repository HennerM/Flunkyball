using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
public class CustomNetworkManager : NetworkManager {

   
    private int nrOfPlayers = 0;

    public GameObject beerPrefab;


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
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
        Vector3 position = nrOfPlayers == 0 ? new Vector3(10, 1, 0) : new Vector3(-10, 1, 0);
        var rotationy =(nrOfPlayers == 0) ? -90 : 90; 
        Quaternion spawnRotation = Quaternion.Euler(0.0f, rotationy, 0.0f);
        var tempPlayer = playerPrefab;
        
        var player = (GameObject)GameObject.Instantiate(tempPlayer, position, spawnRotation);

        if (nrOfPlayers == 0)
        {
            player.GetComponent<Player>().mode = PlayerMode.ACTIVE;
            player.GetComponent<Player>().ball = GameManager.instance.Ball;
        }
        nrOfPlayers++;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        SpawnBeer(player);
    }

    void SpawnBeer(GameObject player)
    {
        var beerPosition = player.transform.position;
        beerPosition.x -= 3f;
        var beer = (GameObject)GameObject.Instantiate(beerPrefab, beerPosition, player.transform.rotation);
        NetworkServer.SpawnWithClientAuthority(beer, player);
        player.GetComponent<Player>().ownBeer = beer.GetComponent<BeerTarget>();
    }
}

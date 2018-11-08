using UnityEngine;
using UnityEngine.Networking;

public class CreateBoard : NetworkBehaviour
{
    public GameObject boardPrefab;

    public void Start()
    {
        var enemy = (GameObject)Instantiate(boardPrefab, Vector3.zero, Quaternion.Euler(0,0,0));
        NetworkServer.Spawn(enemy);
    }
}
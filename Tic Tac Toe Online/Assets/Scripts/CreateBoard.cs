using UnityEngine;
using UnityEngine.Networking;

public class CreateBoard : NetworkBehaviour
{
    public GameObject boardPrefab;

    public override void OnStartServer()
    {
        var enemy = (GameObject)Instantiate(boardPrefab, Vector3.zero, Quaternion.EulerAngles(0,0,0));
        NetworkServer.Spawn(enemy);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour
{
    [SyncVar]
    public int playerType = 0;

    public void Start()
    {
        if (isLocalPlayer)
        {
            gameObject.name = "PlayerLocal";
            BoardManager.Instance.playerConnection = this;
        }
        else
        {
            gameObject.name = "Player";
        }

        if (isServer)
        {
            gameObject.name += "_Server";

            ServerData.Instance.playersCount++;
            playerType = ServerData.Instance.playersCount;
        }
        else
        {
            gameObject.name += "_Client";
        }
    }




    // MakePlay
    [Command]
    public void CmdMakePlay(int line, int column)
    {
        Debug.Log("PlayerConnectionObject::CmdMakePlay");
        RpcMakePlay(line, column);
    }

    [ClientRpc]
    public void RpcMakePlay(int line, int column)
    {
        Debug.Log("PlayerConnectionObject::RpcMakePlay");
        BoardManager.Instance.MakePlay(line, column);
    }


    // RestartGame
    [Command]
    public void CmdRestartGame()
    {
        Debug.Log("PlayerConnectionObject::CmdRestartGame");
        RpcRestartGame();
    }

    [ClientRpc]
    public void RpcRestartGame()
    {
        Debug.Log("PlayerConnectionObject::RpcRestartGame");
        BoardManager.Instance.RestartGame();
    }
}

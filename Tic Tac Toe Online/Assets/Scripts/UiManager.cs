﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public void Restart()
    {
        BoardView.Instance.boardManager.playerConnection.CmdRestartGame();
    }
}

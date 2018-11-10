using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellClickDetector : MonoBehaviour
{

    public int line = 0;
    public int column = 0;

    private void Awake()
    {

    }

    void OnMouseDown()
    {
        Debug.Log("CellClickDetector::OnMouseDown");

        if (BoardManager.Instance.CanMakePlay())
        {
            BoardManager.Instance.playerConnection.CmdMakePlay(line, column);
        }
    }

    public void SetCoordinates(int line, int column)
    {
        Debug.Log("CellClickDetector::OnMouseDown");

        this.line = line;
        this.column = column;
    }
}

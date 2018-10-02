using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellClickDetector : MonoBehaviour {

    public int line = 0;
    public int column = 0;

    void OnMouseDown()
    {
        BoardManager.Instance.MakePlay(line, column);
    }

    public void SetCoordinates(int line, int column)
    {
        this.line = line;
        this.column = column;
    }
}

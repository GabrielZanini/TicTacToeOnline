using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellClickDetector : MonoBehaviour
{

    public int line = 0;
    public int column = 0;

    private BoardView _boardView;

    private void Awake()
    {
        _boardView = GetComponentInParent<BoardView>();
    }

    void OnMouseDown()
    {
        Debug.Log("CellClickDetector::OnMouseDown");
        _boardView.boardManager.CmdMakePlay(line, column);
    }

    public void SetCoordinates(int line, int column)
    {
        Debug.Log("CellClickDetector::OnMouseDown");

        this.line = line;
        this.column = column;
    }
}

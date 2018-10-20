using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BoardView : NetworkBehaviour {

    public Sprite Cross;
    public Sprite Circle;

    private SpriteRenderer[,] cells;

    public override void OnStartServer()
    {
        cells = new SpriteRenderer[BoardManager.Instance.bordSize, BoardManager.Instance.bordSize];

        for (int l = 0; l < BoardManager.Instance.bordSize; l++)
        {
            for (int c = 0; c < BoardManager.Instance.bordSize; c++)
            {
                var spriteCell = transform.GetChild(l * BoardManager.Instance.bordSize + c);
                //Debug.Log(spriteCell);
                spriteCell.GetComponent<CellClickDetector>().SetCoordinates(l, c);
                cells[l, c] = spriteCell.GetComponent<SpriteRenderer>();
            }
        }
    }

    public void UpdateBoard(CircleOrCross[,] board)
    {
        if (cells == null)
        {
            return;
        }

        for (int l = 0; l < BoardManager.Instance.bordSize; l++)
        {
            for (int c = 0; c < BoardManager.Instance.bordSize; c++)
            {
                if (board[l,c] == CircleOrCross.None)
                {
                    cells[l, c].sprite = null;
                }
                else if (board[l, c] == CircleOrCross.Circle)
                {
                    cells[l, c].sprite = Circle;
                }
                else if (board[l, c] == CircleOrCross.Cross)
                {
                    cells[l, c].sprite = Cross;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BoardView : NetworkBehaviour {

    public static BoardView Instance { get; private set; }
    public BoardManager boardManager = null;

    public Sprite Cross;
    public Sprite Circle;

    private SpriteRenderer[,] cells;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    public void Start()
    {
        StartCoroutine(CheckForBoardManager());
    }

    IEnumerator CheckForBoardManager()
    {
        while (boardManager == null)
        {
            yield return null;
        }

        StartBoardView();
    }

    void StartBoardView()
    {
        cells = new SpriteRenderer[boardManager.bordSize, boardManager.bordSize];

        for (int l = 0; l < boardManager.bordSize; l++)
        {
            for (int c = 0; c < boardManager.bordSize; c++)
            {
                var spriteCell = transform.GetChild(l * boardManager.bordSize + c);
                //Debug.Log(spriteCell);
                spriteCell.GetComponent<CellClickDetector>().SetCoordinates(l, c);
                cells[l, c] = spriteCell.GetComponent<SpriteRenderer>();
            }
        }
    }
    
    public void UpdateBoard(int[,] board)
    {
        if (cells == null)
        {
            return;
        }

        for (int l = 0; l < boardManager.bordSize; l++)
        {
            for (int c = 0; c < boardManager.bordSize; c++)
            {
                if (board[l,c] == (int)CircleOrCross.None)
                {
                    cells[l, c].sprite = null;
                }
                else if (board[l, c] == (int)CircleOrCross.Circle)
                {
                    cells[l, c].sprite = Circle;
                }
                else if (board[l, c] == (int)CircleOrCross.Cross)
                {
                    cells[l, c].sprite = Cross;
                }
            }
        }
    }
}

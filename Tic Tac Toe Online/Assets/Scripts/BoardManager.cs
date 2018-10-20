using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BoardManager : NetworkBehaviour {

    public static BoardManager Instance { get; private set; }

    [Range(2,5)]
    public int bordSize = 3;

    public CircleOrCross[,] board;
    public CircleOrCross CurrentPlayer;
    public CircleOrCross winner = CircleOrCross.None;

    public PlayerType PlayerCircle;
    public PlayerType PlayerCross;

    private CircleOrCross _firstPlayer = CircleOrCross.Cross;
    public BoardView boardView;


    void Awake ()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        board = new CircleOrCross[bordSize, bordSize];
        boardView = GetComponent<BoardView>();
    }

    void Start()
    {
        ClearBoard();
        boardView.UpdateBoard(board);
    }
	
	void Update () {
		
	}
    
    void ClearBoard()
    {
        for (int l = 0; l < BoardManager.Instance.bordSize; l++)
        {
            for (int c = 0; c < BoardManager.Instance.bordSize; c++)
            {
                board[l, c] = CircleOrCross.None;
            }
        }
    }

    public void MakePlay (int line, int column)
    {
        if (winner != CircleOrCross.None)
        {
            Debug.Log("The Game is Over!");
            return;
        }

        if (board[line, column] != CircleOrCross.None)
        {
            Debug.Log("Choose another place!!!");
            return;
        }

        board[line, column] = CurrentPlayer;

        CurrentPlayer = GetOppositePlayer(CurrentPlayer);
        winner = GetFinishState(board);

        boardView.UpdateBoard(board);
    }

    public static CircleOrCross GetFinishState(CircleOrCross[,] board)
    {        
        //Check lines
        for (int l = 0; l < BoardManager.Instance.bordSize; l++)
        {
            CircleOrCross firstCellLine = board[l, 0];
            for (int c = 1; c < BoardManager.Instance.bordSize; c++)
            {
                if (firstCellLine != board[l, c])
                {
                    break;
                }
                else if (c == BoardManager.Instance.bordSize - 1)
                {
                    return firstCellLine;
                }
            }
        }

        //Check Columns
        for (int c = 0; c < BoardManager.Instance.bordSize; c++)
        {
            CircleOrCross firstCellColumn = board[0, c];
            for (int l = 1; c < BoardManager.Instance.bordSize; l++)
            {
                if (firstCellColumn != board[l, c])
                {
                    break;
                }
                else if (l == BoardManager.Instance.bordSize - 1)
                {
                    return firstCellColumn;
                }
            }
        }

        //Check Diagoanl (Same)
        CircleOrCross firstCellDiagonal = board[0, 0];
        for (int lc = 1; lc < BoardManager.Instance.bordSize; lc++)
        {
            if (firstCellDiagonal != board[lc, lc])
            {
                break;
            }
            else if (lc == BoardManager.Instance.bordSize - 1)
            {
                return firstCellDiagonal;
            }
        }

        //Check Diagoanl (Opposite)
        firstCellDiagonal = board[0, BoardManager.Instance.bordSize - 1];
        for (int lc = 1; lc < BoardManager.Instance.bordSize; lc++)
        {
            if (firstCellDiagonal != board[lc, BoardManager.Instance.bordSize - lc - 1])
            {
                break;
            }
            else if (lc == BoardManager.Instance.bordSize - 1)
            {
                return firstCellDiagonal;
            }
        }

        //Check for Draw
        bool isDraw = true;
        for (int l = 0; l < BoardManager.Instance.bordSize; l++)
        {
            if (!isDraw)
            {
                break;
            }
            for (int c = 0; c < BoardManager.Instance.bordSize; c++)
            {
                if (board[l, c] == CircleOrCross.None)
                {
                    isDraw = false;
                    break;
                } else if (l == BoardManager.Instance.bordSize - 1 && c == BoardManager.Instance.bordSize - 1)
                {
                    return CircleOrCross.Draw;
                }
            }
        }


        return CircleOrCross.None;
    }
    
    public static CircleOrCross GetOppositePlayer(CircleOrCross player)
    {
        if (player == CircleOrCross.Circle)
        {
            return CircleOrCross.Cross;
        }
        else
        {
            return CircleOrCross.Circle;
        }
    }

    public void RestartGame()
    {
        ClearBoard();
        boardView.UpdateBoard(board);

        CurrentPlayer = _firstPlayer;

        winner = CircleOrCross.None;
    }

    public void SetFirstPlayer(int value)
    {
        if (value == 0)
        {
            _firstPlayer = CircleOrCross.Circle;
        }
        else
        {
            _firstPlayer = CircleOrCross.Cross;
        }

        CurrentPlayer = _firstPlayer;
    }

    public void SetCirclePlayerType(int value)
    {
        if (value == 0)
        {
            PlayerCircle = PlayerType.Human;
        }
        else
        {
            PlayerCircle = PlayerType.AI;
        }
        
    }

    public void SetCrossPlayerType(int value)
    {
        if (value == 0)
        {
            PlayerCross = PlayerType.Human;
        }
        else
        {
            PlayerCross = PlayerType.AI;
        }

    }
}

public class Point
{
    int x = 0;
    int y = 0;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public enum CircleOrCross
{
    None,
    Circle,
    Cross,
    Draw
}

public enum PlayerType
{
    Human,
    AI
}

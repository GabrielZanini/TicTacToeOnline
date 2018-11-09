using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BoardManager : NetworkBehaviour {

    public PlayerConnectionObject playerConnection;

    [Range(2,5)]
    public int bordSize = 3;

    public int[,] board;
    [SyncVar]
    public int CurrentPlayer;
    [SyncVar]
    public int winner = (int)CircleOrCross.None;
    
    private int _firstPlayer = (int)CircleOrCross.Cross;
    public BoardView boardView;


    void Awake ()
    {
        
    }

    void Start()
    {
        board = new int[bordSize, bordSize];
        boardView = BoardView.Instance;
        boardView.boardManager = this;

        if (isServer)
        {
            ClearBoard();
            UpdateBoardView();
        }
    }
	
	void Update () {
		
	}
    
    void ClearBoard()
    {
        for (int l = 0; l < bordSize; l++)
        {
            for (int c = 0; c < bordSize; c++)
            {
                board[l, c] = (int)CircleOrCross.None;
            }
        }
    }
    

    [Command]
    public void CmdMakePlay(int line, int column)
    {
        Debug.Log("BoardManager::CmdMakePlay");

        if ((CircleOrCross)winner != CircleOrCross.None)
        {
            Debug.Log("The Game is Over!");
            return;
        }

        if (board[line, column] != (int)CircleOrCross.None)
        {
            Debug.Log("Choose another place!!!");
            return;
        }

        RpcUpdateBoardData(line, column, CurrentPlayer);

        CurrentPlayer = GetOppositePlayer(CurrentPlayer);
        winner = GetFinishState(board);

        RpcUpdateBoardView();
    }

    [ClientRpc]
    public void RpcUpdateBoardData(int line, int column, int player)
    {
        Debug.Log("PlayerConnectionObject::RpcUpdateBoardData");
        board[line, column] = player;
    }

    [ClientRpc]
    public void RpcUpdateBoardView()
    {
        Debug.Log("PlayerConnectionObject::RpcUpdateBoardView");
        UpdateBoardView();
    }


    public void UpdateBoardView()
    {
        boardView.UpdateBoard(board);
    }


    public int GetFinishState(int[,] board)
    {        
        //Check lines
        for (int l = 0; l < bordSize; l++)
        {
            int firstCellLine = board[l, 0];
            for (int c = 1; c < bordSize; c++)
            {
                if (firstCellLine != board[l, c])
                {
                    break;
                }
                else if (c == bordSize - 1)
                {
                    return firstCellLine;
                }
            }
        }

        //Check Columns
        for (int c = 0; c < bordSize; c++)
        {
            int firstCellColumn = board[0, c];
            for (int l = 1; c < bordSize; l++)
            {
                if (firstCellColumn != board[l, c])
                {
                    break;
                }
                else if (l == bordSize - 1)
                {
                    return firstCellColumn;
                }
            }
        }

        //Check Diagoanl (Same)
        int firstCellDiagonal = board[0, 0];
        for (int lc = 1; lc < bordSize; lc++)
        {
            if (firstCellDiagonal != board[lc, lc])
            {
                break;
            }
            else if (lc == bordSize - 1)
            {
                return firstCellDiagonal;
            }
        }

        //Check Diagoanl (Opposite)
        firstCellDiagonal = board[0, bordSize - 1];
        for (int lc = 1; lc < bordSize; lc++)
        {
            if (firstCellDiagonal != board[lc, bordSize - lc - 1])
            {
                break;
            }
            else if (lc == bordSize - 1)
            {
                return firstCellDiagonal;
            }
        }

        //Check for Draw
        bool isDraw = true;
        for (int l = 0; l < bordSize; l++)
        {
            if (!isDraw)
            {
                break;
            }
            for (int c = 0; c < bordSize; c++)
            {
                if (board[l, c] == (int)CircleOrCross.None)
                {
                    isDraw = false;
                    break;
                } else if (l == bordSize - 1 && c == bordSize - 1)
                {
                    return (int)CircleOrCross.Draw;
                }
            }
        }


        return (int)CircleOrCross.None;
    }
    
    public int GetOppositePlayer(int player)
    {
        if (player == (int)CircleOrCross.Circle)
        {
            return (int)CircleOrCross.Cross;
        }
        else
        {
            return (int)CircleOrCross.Circle;
        }
    }

    [Command]
    public void CmdRestartGame()
    {
        ClearBoard();
        UpdateBoardView();

        CurrentPlayer = _firstPlayer;

        winner = (int)CircleOrCross.None;
    }

    public void SetFirstPlayer(int value)
    {
        if (value == 0)
        {
            _firstPlayer = (int)CircleOrCross.Circle;
        }
        else
        {
            _firstPlayer = (int)CircleOrCross.Cross;
        }

        CurrentPlayer = _firstPlayer;
    }
}


public enum CircleOrCross
{
    None = 0,
    Circle = 1,
    Cross = 2,
    Draw = 3
}


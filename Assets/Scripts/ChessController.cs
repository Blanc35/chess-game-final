using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessController : MonoBehaviour
{
    public chessBoard chessBoardCtrl;
    public GameObject highlightGridPrefab;

    private GameObject highlightGrid;

    private chess movingChess;

    // Start is called before the first frame update
    void Start()
    {
        chessBoardCtrl.startGame();

        highlightGrid = Instantiate(highlightGridPrefab, grid2ToPoint(new Vector2Int(0, 0)), Quaternion.identity, gameObject.transform);
        highlightGrid.SetActive(false);
    }

    // Update is called once per frame
    void Update () 
    {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;
        if (Physics.Raycast (ray, out hit)) 
        {
            Vector3 point = hit.point;
            // Debug.Log(point);
            Vector2Int grid2 = pointToGrid2(point);
            // Debug.Log(grid2);
            bool checkValid = chessBoard.checkValidGrid2(grid2);
            highlightGrid.SetActive(checkValid);
            HighlightBoard(chessBoard.getGrid(grid2));

            if (checkValid) 
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if(movingChess == null) EnterState(chessBoardCtrl.getChess(grid2));
                    else 
                    {
                        // TODO: Check chess rules and move


                        if (chessBoardCtrl.getChess(grid2) == null)
                        {
                            Move(movingChess, grid2);
                        }
                        else
                        {
                            // TODO: CapturePieceAt
                    
                            Move(movingChess, grid2);
                        }
                        ExitState();
                    }
                }
            }
		}
        else
        {
            highlightGrid.SetActive(false);
        }

	}

    static public Vector2Int getGrid2(int col, int row)
    {
        return new Vector2Int(col, row);
    }

    static public Vector3 grid2ToPoint(Vector2Int gridPoint)
    {
        float x = chessBoard.gridSize.x * gridPoint.x;
        float z = chessBoard.gridSize.y * gridPoint.y;
        return chessBoard.originPosition + new Vector3(x, 0, z);
    }

    static public Vector2Int pointToGrid2(Vector3 point)
    {
        int col = Mathf.FloorToInt((chessBoard.gridSize.x / 2 + point.x) / chessBoard.gridSize.x);
        int row = Mathf.FloorToInt((chessBoard.gridSize.y / 2 + point.z) / chessBoard.gridSize.y);
        return chessBoard.getValidGrid2(new Vector2Int(col, row));
    }

    public void LogInfo(Vector2Int grid2)
    {
        LogInfo(chessBoardCtrl.getChess(grid2));
    }

    public string LogInfo(chess piece)
    {
        return piece ? $"ChessColor: {piece.mChesspPieces} \n"
        + $"ChessType: {piece.mChessType} \n" : "Chess is null !";
    }

    void HighlightBoard (int index)
	{
		highlightGrid.transform.position = grid2ToPoint(chessBoard.getGrid2(index));
	}

    private void CancelMove()
    {
        // this.enabled = false;

        // TODO: cancel moveable highlights


        // TODO: cancel selected effect


        // TODO: can select


        // Debug.Log("CancelMove - " + LogInfo(movingChess));
    }

    public void EnterState(chess piece)
    {
        movingChess = piece;
        // this.enabled = true;

        // TODO: show moveable highlights


        // Debug.Log("EnterState - " + LogInfo(movingChess));
    }

    void ExitState()
    {
        // this.enabled = false;
        movingChess = null;
        highlightGrid.SetActive(false);

        // TODO: cancel selected effect
        

        // TODO: NextPlayer

        
        // TODO: can select


        // TODO: cancel moveable highlights


        // Debug.Log("ExitState - " + LogInfo(movingChess));
    }


    public void Move(chess piece, Vector2Int grid2)
    {
        // TODO: check pawn moved
        // if (piece.mChessType == chess.chessType.Pawn && !determineMove(piece))
        // {
        //     historyMoved(piece);
        // }

        chessBoardCtrl.movements(piece, grid2);
    }
}

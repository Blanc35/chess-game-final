using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessController : MonoBehaviour
{
    static public ChessController instance;
    public chessBoard chessBoardCtrl;
    public GameObject highlightGridPrefab;
    public GameObject highlightGridMovePrefab;

    GameObject highlightSelectGrid;
    List<GameObject> highlightMoveGrid;

    chess movingChess;
    List<Vector2Int> moveableGrid2;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize();
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
            highlightSelectGrid.SetActive(checkValid);
            HighlightBoard(highlightSelectGrid, grid2);

            if (checkValid) 
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if(movingChess == null) EnterState(grid2);
                    else if (movingChess == chessBoardCtrl.getChess(grid2)) CancelMove();
                    else 
                    {
                        // TODO: Check chess rules and move
                        if (!moveableGrid2.Contains(grid2))
                        {
                            return;
                        }


                        if (chessBoardCtrl.getChess(grid2) == null)
                        {
                            Move(movingChess, grid2);
                        }
                        else
                        {
                            // TODO: CapturePieceAt
                            Move(movingChess, grid2);
                            Gamedev.instance.eat(chessBoardCtrl.getChess(grid2));
                        }
                        ExitState();
                    }
                }
            }
		}
        else
        {
            highlightSelectGrid.SetActive(false);
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

    public void Initialize()
    {
        // chessBoardCtrl.startGame();

        highlightSelectGrid = Instantiate(highlightGridPrefab, grid2ToPoint(new Vector2Int(0, 0)), Quaternion.identity, gameObject.transform);
        highlightSelectGrid.SetActive(false);

        highlightMoveGrid = new List<GameObject>();

        moveableGrid2 = new List<Vector2Int>();
    }

    void HighlightBoard (GameObject highlightObject, Vector2Int grid2)
	{
        if(highlightObject == null) return;
		highlightObject.transform.position = grid2ToPoint(grid2);
	}

    void HighlightBoard (GameObject prefab, List<GameObject> listHighlightObject, List<Vector2Int> listGrid2)
	{
        if(prefab == null || listHighlightObject == null) return;
// Debug.Log(listGrid2.Count);
        CancelHighlightBoard(listHighlightObject);
        foreach (Vector2Int grid2 in listGrid2)
        {
            GameObject obj;
            obj = Instantiate(prefab, grid2ToPoint(new Vector2Int(0, 0)), Quaternion.identity, gameObject.transform);    
            listHighlightObject.Add(obj);
            HighlightBoard(obj, grid2);
            // Debug.Log(grid2);
        }
	}

    void CancelHighlightBoard (List<GameObject> listHighlightObject)
	{
        foreach (GameObject obj in listHighlightObject)
        {
            Destroy(obj);
        }
	}

    private void CancelMove()
    {
        // this.enabled = false;
        // clear current moving chess
        movingChess = null;

        // cancel moveable highlights
        CancelHighlightBoard(highlightMoveGrid);

        // cancel selected effect
        highlightSelectGrid.SetActive(false);


        // TODO: can select


        // Debug.Log("CancelMove - " + LogInfo(movingChess));
    }

    public void EnterState(Vector2Int grid2)
    {
        movingChess = chessBoardCtrl.getChess(grid2);
        // this.enabled = true;

        // get moveable
        moveableGrid2 = movingChess.getMoveable(grid2);
        moveableGrid2.RemoveAll(x => !chessBoard.checkValidGrid2(x));
        moveableGrid2.RemoveAll(x => Gamedev.instance.isFriendlyChess(x));

        // show moveable highlights
        HighlightBoard(highlightGridMovePrefab, highlightMoveGrid, moveableGrid2);
        
        // check is any moveable
        if (moveableGrid2.Count == 0)
        {
            CancelMove();
        }


        // Debug.Log("EnterState - " + LogInfo(movingChess));
    }

    void ExitState()
    {
        // this.enabled = false;

        // clear current moving chess
        movingChess = null;

        // cancel selected effect
        highlightSelectGrid.SetActive(false);
        

        // TODO: NextPlayer

        
        // TODO: can select


        // cancel moveable highlights
        CancelHighlightBoard(highlightMoveGrid);


        // Debug.Log("ExitState - " + LogInfo(movingChess));
    }


    public void Move(chess piece, Vector2Int grid2)
    {
        // check pawn moved
        if (piece.mChessType == chess.chessType.Pawn && !Gamedev.instance.determineMove(piece))
        {
            Gamedev.instance.historyMoved(piece);
        }

        chessBoardCtrl.movements(piece, grid2);
    }
}

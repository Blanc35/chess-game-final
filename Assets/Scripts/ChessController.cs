using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    Dictionary<Vector2Int, List<Vector2Int>> moveAttackGrid2;

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
            HighlightBoard(highlightSelectGrid, grid2);
            highlightSelectGrid.SetActive(checkValid);

            if (checkValid) 
            {
                if (Input.GetMouseButtonDown(0))
                {
                    chess targetChess = chessBoardCtrl.getChess(grid2);
                    Debug.Log($"movingChess: {Gamedev.instance.getInfo(movingChess)}, targetChess: {Gamedev.instance.getInfo(targetChess)}");
                    
                    if(movingChess == null) EnterState(grid2);
                    else if (movingChess == targetChess) CancelMove();
                    else 
                    {
                        // TODO: Check chess rules and move
                        if (!moveAttackGrid2.ContainsKey(grid2))
                        {
                            return;
                        }

                        // get attack targets
                        List<Vector2Int> attackGrid2List = new List<Vector2Int>();
                        moveAttackGrid2.TryGetValue(grid2, out attackGrid2List);

                            // TODO: Check checkmate rules and history
                            Gamedev.instance.AddMoveHistory(new HalfMove(movingChess, new Movement(chessBoardCtrl.getGrid2(movingChess), grid2), true, false));

                            // TODO: CapturePieceAt
                        foreach(var attackGrid2 in attackGrid2List) Gamedev.instance.eat(chessBoardCtrl.getChess(attackGrid2));

                            Move(movingChess, grid2);

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
        float x = Gamedev.instance.ba.gridSize.x * gridPoint.x;
        float z = Gamedev.instance.ba.gridSize.y * gridPoint.y;
        return Gamedev.instance.ba.originPosition + new Vector3(x, 0, z);
    }

    static public Vector2Int pointToGrid2(Vector3 point)
    {
        int col = Mathf.FloorToInt((Gamedev.instance.ba.gridSize.x / 2 + point.x) / Gamedev.instance.ba.gridSize.x);
        int row = Mathf.FloorToInt((Gamedev.instance.ba.gridSize.y / 2 + point.z) / Gamedev.instance.ba.gridSize.y);
        return chessBoard.getValidGrid2(new Vector2Int(col, row));
    }

    public void Initialize()
    {
        // chessBoardCtrl.startGame();

        Vector2Int highlightGrid2 = new Vector2Int(0, 0);
        highlightSelectGrid = Instantiate(highlightGridPrefab, grid2ToPoint(highlightGrid2), Quaternion.identity, gameObject.transform);
        HighlightBoard(highlightSelectGrid, highlightGrid2);
        highlightSelectGrid.SetActive(false);

        highlightMoveGrid = new List<GameObject>();

        moveAttackGrid2 = new Dictionary<Vector2Int, List<Vector2Int>>();
    }

    void HighlightBoard (GameObject highlightObject, Vector2Int grid2)
	{
        if(highlightObject == null) return;
		highlightObject.transform.position = grid2ToPoint(grid2) + new Vector3(0, -0.1f, 0);
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
        chess selectedChess= chessBoardCtrl.getChess(grid2);
        if(selectedChess == null || !Gamedev.instance.isChessBelongToCurrentPlayer(selectedChess)) return;

        movingChess = selectedChess;
        // this.enabled = true;

        // get moveable
        moveAttackGrid2 = movingChess.getMoveable(grid2);
        // foreach(var tmp in moveAttackGrid2.Keys) Debug.LogError($"@@@EnterState1 {Gamedev.instance.getInfo(tmp)}");
        moveAttackGrid2 = moveAttackGrid2.Where(x => chessBoard.checkValidGrid2(x.Key)).ToDictionary(x => x.Key, x => x.Value);
        // foreach(var tmp in moveAttackGrid2.Keys) Debug.LogError($"@@@EnterState2 {Gamedev.instance.getInfo(tmp)}");
        moveAttackGrid2 = moveAttackGrid2.Where(x => !Gamedev.instance.isFriendlyChess(x.Key)).ToDictionary(x => x.Key, x => x.Value);
        // foreach(var tmp in moveAttackGrid2.Keys) Debug.LogError($"@@@EnterState3 {Gamedev.instance.getInfo(tmp)}");

        List<Vector2Int> moveableGrid2 = moveAttackGrid2.Keys.ToList();
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
        Gamedev.instance.changePlayer();

        
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

        // check promotion
        if(piece.mChessType == chess.chessType.Pawn 
        && chessBoardCtrl.getGrid2(piece).y == 0
        )
        {
            Gamedev.instance.SelectPromotion(grid2);
        }
        if(piece.mChessType == chess.chessType.Pawn 
        && chessBoardCtrl.getGrid2(piece).y == 7
        )
        {
            Gamedev.instance.SelectPromotion(grid2);
        }
    }
}

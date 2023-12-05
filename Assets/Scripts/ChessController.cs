using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessController : MonoBehaviour
{
    public GameObject highlightGridPrefab;

    private GameObject highlightGrid;

    // Start is called before the first frame update
    void Start()
    {
        highlightGrid = Instantiate(highlightGridPrefab, grid2ToPoint(new Vector2Int(0, 0)), Quaternion.identity, gameObject.transform);
        highlightGrid.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate () 
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
            highlightGrid.transform.position = grid2ToPoint(grid2);

            if (checkValid) 
            {
                // TODO: Check chess rules and move
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

    void HighlightBoard (int index)
	{
		highlightGrid.transform.position = grid2ToPoint(chessBoard.getGrid2(index));
	}

}

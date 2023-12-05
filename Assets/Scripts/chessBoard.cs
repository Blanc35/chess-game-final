using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessBoard : MonoBehaviour
{
    public chess whitePawn; 
    public chess whiteRook; 
    public chess whiteKnight; 
    public chess whiteBishop;
    public chess whiteQueen;
    public chess whiteKing; 
    public chess blackRook; 
    public chess blackKnight; 
    public chess blackBishop; 
    public chess blackQueen;    
    public chess blackKing; 
    public chess blackPawn; 

    Vector2[,] boardSetup = new Vector2[8, 8];
    static readonly public Vector2 gridSize = new Vector2(4.2f, 4.2f);
    static readonly public Vector3 originPosition = new Vector3(0.0f, 2.1f, 0.0f);
    static readonly public Vector3 chessAngle = new Vector3(180.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<8;i++)
        {
            for(int k=0;k<8;k++)
            {
                boardSetup[k, i] = new Vector2(k, i) * gridSize;
            }
        }

        startGame();
    }
    void startGame()
    {
        for(int i=0;i<8;i++)
        {
            chess tmpChess = Instantiate(whitePawn.gameObject, Vector3.zero, Quaternion.identity).GetComponent<chess>();
            placement(tmpChess, i, 1);
        }
        for(int i=0;i<8;i++)
        {
            chess tmpChess = Instantiate(blackPawn.gameObject, Vector3.zero, Quaternion.identity).GetComponent<chess>();
            placement(tmpChess, i, 6);
        }

        chess wr1Chess = Instantiate(whiteRook.gameObject, Vector3.zero, Quaternion.identity).GetComponent<chess>();
        placement(wr1Chess, 0,0);
        chess wr2Chess = Instantiate(whiteRook.gameObject, Vector3.zero, Quaternion.identity).GetComponent<chess>();
        placement(wr2Chess, 7,0);
        chess wn1Chess = Instantiate(whiteKnight.gameObject, Vector3.zero, Quaternion.identity).GetComponent<chess>();
        placement(wn1Chess, 1,0);
        chess wn2Chess = Instantiate(whiteKnight.gameObject, Vector3.zero, Quaternion.identity).GetComponent<chess>();
        placement(wn2Chess, 6,0);
        chess wb1Chess = Instantiate(whiteBishop.gameObject, Vector3.zero, Quaternion.identity).GetComponent<chess>();
        placement(wb1Chess, 2,0);
        chess wb2Chess = Instantiate(whiteBishop.gameObject, Vector3.zero, Quaternion.identity).GetComponent<chess>();
        placement(wb2Chess, 5,0);
        chess wkChess = Instantiate(whiteKing.gameObject, Vector3.zero, Quaternion.identity).GetComponent<chess>();
        placement(wkChess, 4,0);
        chess wqChess = Instantiate(whiteQueen.gameObject, Vector3.zero, Quaternion.identity).GetComponent<chess>();
        placement(wqChess, 3,0);
    }   
    // Update is called once per frame
    void Update()
    {
        
    }
    void placement(chess piece, int x, int y)
    {
        Vector2 position = boardSetup[x, y];
        piece.transform.position = originPosition + new Vector3(position.x, 0.0f, position.y);
        piece.transform.localEulerAngles = chessAngle;
    }
    void movements()
    {

    }
    void remove()
    {
        
    }
    void check()
    {

    }

    static public bool checkValidGrid2(Vector2Int grid2)
    {
       return (0 <= grid2.x && grid2.x < 8)
       && (0 <= grid2.y && grid2.y < 8);
    }

    static public Vector2Int getValidGrid2(Vector2Int grid2)
    {
       int x = (grid2.x < 0 ? 0 : grid2.x);
       x = (x >= 8 ? 7 : x);
       int y = (grid2.y < 0 ? 0 : grid2.y);
       y = (y >= 8 ? 7 : y);

       return new Vector2Int(x, y);
    }

	static public string getBoardGridString (int index)
	{
		Vector2Int grid2 = getGrid2(index);
		return char.ConvertFromUtf32 (grid2.y + 65) + "" + (grid2.x + 1);
	}

    static public int getGrid(Vector2Int grid2)
    {
        return grid2.x * 8 + grid2.y;
    }

    static public Vector2Int getGrid2(int index)
    {
        int x = index / 8;
		int y = index % 8;
        return new Vector2Int(x, y);
    }

}

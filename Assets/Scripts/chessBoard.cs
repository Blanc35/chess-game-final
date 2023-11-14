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
    Vector2 gridSize = new Vector2(4.2f, 4.2f);
    Vector3 originPosition = new Vector3(0.0f, 2.1f, 0.0f);
    Vector3 chessAngle = new Vector3(180.0f, 0.0f, 0.0f);

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
}

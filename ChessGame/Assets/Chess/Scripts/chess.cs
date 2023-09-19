using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chess : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    enum chesspPieces
    {
        White,
        Black,
    }
    chesspPieces mChesspPieces;
    enum chessType{

        Pawn,
        Bishop,

        Knight,
        Rook,
        King,
        Queen,

    }
    chessType mChessType;
    int[][] chessMove;
    
    int[][] rook = 
    {
        new int[]{1,0},
        new int[]{-1,0},
        new int[]{0,1},
        new int[]{0,-1}
    };
    int[][] bishop = 
    {
        new int[]{1,1},
        new int[]{1,-1},
        new int[]{-1,-1},
        new int[]{-1,1}
    };
}


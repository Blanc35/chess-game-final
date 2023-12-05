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
    public virtual bool isvalid(int x,int y)
    {
        return false;
    }

   public enum chesspPieces
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
        int[][] knight = 
    {
        new int[]{2,1},
        new int[]{-2,1},
        new int[]{-2,-1},
        new int[]{2,-1},
        new int[]{-1,2},
        new int[]{-1,-2},
        new int[]{1,-2},
        new int[]{1,2}
    };
        int[][] pawn = 
    {
        new int[]{0,1},
    };
            int[][] queen = 
    {
        new int[]{1,1},
        new int[]{0,1},
        new int[]{0,-1},
        new int[]{-1,0},
        new int[]{-1,-1},
        new int[]{-1,1},
        new int[]{1,-1},
        new int[]{1,0},
        
    };
                int[][] king = 
    {
        new int[]{1,1},
        new int[]{0,1},
        new int[]{0,-1},
        new int[]{-1,0},
        new int[]{-1,-1},
        new int[]{-1,1},
        new int[]{1,-1},
        new int[]{1,0},
        
    };
}


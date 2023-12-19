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
    
    public virtual List<Vector2Int> getMoveable(Vector2Int grid)
    {
        return new List<Vector2Int>();
    }

   public enum chesspPieces
    {
        White,
        Black,
    }
    public chesspPieces mChesspPieces;
    public enum chessType{

        Pawn,
        Bishop,

        Knight,
        Rook,
        King,
        Queen,

    }
    public chessType mChessType;
    int[][] chessMove;
}


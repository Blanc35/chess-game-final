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

    public string getNickname()
    {
        List<string> listName = new List<string>();
        switch(mChesspPieces)
        {
            case chess.chesspPieces.White:
                listName.Add("W");
                break;
            case chess.chesspPieces.Black:
                listName.Add("B");
                break;
            default:
                listName.Add("");
                break;
        }
        switch(mChessType)
        {
            case chess.chessType.Pawn:
                listName.Add("P");
                break;
            case chess.chessType.Rook:
                listName.Add("R");
                break;
            case chess.chessType.Knight:
                listName.Add("N");
                break;
            case chess.chessType.Bishop:
                listName.Add("B");
                break;
            case chess.chessType.King:
                listName.Add("K");
                break;
            case chess.chessType.Queen:
                listName.Add("Q");
                break;
            default:
                listName.Add("");
                break;
        }

        return string.Join("-", listName);
    }
}


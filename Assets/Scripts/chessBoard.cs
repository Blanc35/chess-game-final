using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessBoard : MonoBehaviour
{
    public GameObject whitePawn; 
    public GameObject whiteRook; 
    public GameObject whiteKnight; 
    public GameObject whiteBishop;
    public GameObject whiteQueen;
    public GameObject whiteKing; 
    public GameObject blackRook; 
    public GameObject blackKnight; 
    public GameObject blackBishop; 
    public GameObject blackQueen;    
    public GameObject blackKing; 
    public GameObject blackPawn; 
    GameObject[,] prefabs = new GameObject[2, 6];

    chess[,] boardChesses = new chess[8, 8];
    static readonly public Vector2 gridSize = new Vector2(4.2f, 4.2f);
    static readonly public Vector3 originPosition = new Vector3(0.0f, 2.1f, 0.0f);
    static readonly public Vector3 chessAngle = new Vector3(90.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {

    }
    public void startGame()
    {
        for(int i=0;i<8;i++)
        {
            createChess(chess.chesspPieces.White, chess.chessType.Pawn, i, 1);
        }
        createChess(chess.chesspPieces.White, chess.chessType.Rook, 0,0);
        createChess(chess.chesspPieces.White, chess.chessType.Rook, 7,0);
        createChess(chess.chesspPieces.White, chess.chessType.Knight, 1,0);
        createChess(chess.chesspPieces.White, chess.chessType.Knight, 6,0);
        createChess(chess.chesspPieces.White, chess.chessType.Bishop, 2,0);
        createChess(chess.chesspPieces.White, chess.chessType.Bishop, 5,0);
        createChess(chess.chesspPieces.White, chess.chessType.King, 4,0);
        createChess(chess.chesspPieces.White, chess.chessType.Queen, 3,0);

        for(int i=0;i<8;i++)
        {
            createChess(chess.chesspPieces.Black, chess.chessType.Pawn, i, 6);
        }
        createChess(chess.chesspPieces.Black, chess.chessType.Rook, 0,7);
        createChess(chess.chesspPieces.Black, chess.chessType.Rook, 7,7);
        createChess(chess.chesspPieces.Black, chess.chessType.Knight, 1,7);
        createChess(chess.chesspPieces.Black, chess.chessType.Knight, 6,7);
        createChess(chess.chesspPieces.Black, chess.chessType.Bishop, 2,7);
        createChess(chess.chesspPieces.Black, chess.chessType.Bishop, 5,7);
        createChess(chess.chesspPieces.Black, chess.chessType.King, 4,7);
        createChess(chess.chesspPieces.Black, chess.chessType.Queen, 3,7);
    }   

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        prefabs[(int)chess.chesspPieces.White, (int)chess.chessType.Pawn] = whitePawn;
        prefabs[(int)chess.chesspPieces.White, (int)chess.chessType.Bishop] = whiteBishop;
        prefabs[(int)chess.chesspPieces.White, (int)chess.chessType.Knight] = whiteKnight;
        prefabs[(int)chess.chesspPieces.White, (int)chess.chessType.Rook] = whiteRook;
        prefabs[(int)chess.chesspPieces.White, (int)chess.chessType.King] = whiteKing;
        prefabs[(int)chess.chesspPieces.White, (int)chess.chessType.Queen] = whiteQueen;
        prefabs[(int)chess.chesspPieces.Black, (int)chess.chessType.Pawn] = blackPawn;
        prefabs[(int)chess.chesspPieces.Black, (int)chess.chessType.Bishop] = blackBishop;
        prefabs[(int)chess.chesspPieces.Black, (int)chess.chessType.Knight] = blackKnight;
        prefabs[(int)chess.chesspPieces.Black, (int)chess.chessType.Rook] = blackRook;
        prefabs[(int)chess.chesspPieces.Black, (int)chess.chessType.King] = blackKing;
        prefabs[(int)chess.chesspPieces.Black, (int)chess.chessType.Queen] = blackQueen;
    }

    public chess createChess(chess.chesspPieces color, chess.chessType type, int x, int y)
    {
        GameObject createObject = Instantiate(prefabs[(int)color, (int)type], Vector3.zero, Quaternion.identity);
        chess tmpChess = null;
        switch (type)
        {
            case chess.chessType.Pawn: tmpChess = createObject.AddComponent<Pawn>(); break;
            case chess.chessType.Bishop: tmpChess = createObject.AddComponent<Bishop>(); break;
            case chess.chessType.Knight: tmpChess = createObject.AddComponent<Knight>(); break;
            case chess.chessType.Rook: tmpChess = createObject.AddComponent<Rook>(); break;
            case chess.chessType.King: tmpChess = createObject.AddComponent<King>(); break;
            case chess.chessType.Queen: tmpChess = createObject.AddComponent<Queen>(); break;
            default: break;
        }
        if(tmpChess != null)
        {
            tmpChess.mChesspPieces = color;
            tmpChess.mChessType = type;
            placement(tmpChess, x, y);
        }

        return tmpChess;
    }


    void placement(chess piece, int x, int y)
    {
        Vector2 position = new Vector2(x, y) * gridSize;
        piece.transform.position = originPosition + new Vector3(position.x, 0.0f, position.y);
        piece.transform.localEulerAngles = chessAngle;

        boardChesses[x, y] = piece;
    }

    public void movements(chess piece, Vector2Int grid2)
    {
        Vector2Int startGrid2 = getGrid2(piece);
        if(checkValidGrid2(startGrid2)) boardChesses[startGrid2.x, startGrid2.y] = null;
        if(checkValidGrid2(grid2)) boardChesses[grid2.x, grid2.y] = piece;
        placement(piece, grid2.x, grid2.y);
    }

    void remove()
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

    public chess getChess(Vector2Int grid2)
    {
        if (!checkValidGrid2(grid2))
        {
            return null;
        }
        return boardChesses[grid2.x, grid2.y];
    }

    public Vector2Int getGrid2(chess piece)
    {
        for (int i = 0; i < boardChesses.GetLength(0); i++) 
        {
            for (int j = 0; j < boardChesses.GetLength(1); j++)
            {
                if (boardChesses[i, j] == piece)
                {
                    return new Vector2Int(i, j);
                }
            }
        }

        return new Vector2Int(-1, -1);
    }
}

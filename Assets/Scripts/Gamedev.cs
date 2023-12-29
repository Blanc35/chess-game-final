using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamedev : MonoBehaviour
{
    static public Gamedev instance;

    public List<chess> moved;

    public Player self;
    public Player other;

    public Player turns;
    public Player[] playerColor;
    public chessBoard ba;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize game system
        moved = new List<chess>();

        playerColor = new Player[2];
        playerColor[(int)chess.chesspPieces.White] = new Player("playerName", chess.chesspPieces.White);
        playerColor[(int)chess.chesspPieces.Black] = new Player("playerName", chess.chesspPieces.Black);
        
        self=new Player("Bob", chess.chesspPieces.White);
        other=new Player("Joe", chess.chesspPieces.Black);
        turns=self;

        playerColor[(int)chess.chesspPieces.White] = self;
        playerColor[(int)chess.chesspPieces.Black] = other;

        ChessController.instance.Initialize();

        ba.Initialize();

        // Create white chess
        chess.chesspPieces chessColor = chess.chesspPieces.White;
        for(int i=0;i<8;i++)
        {
            createChess(chessColor, chess.chessType.Pawn, i, 1);
        }
        createChess(chessColor, chess.chessType.Rook, 0,0);
        createChess(chessColor, chess.chessType.Rook, 7,0);
        createChess(chessColor, chess.chessType.Knight, 1,0);
        createChess(chessColor, chess.chessType.Knight, 6,0);
        createChess(chessColor, chess.chessType.Bishop, 2,0);
        createChess(chessColor, chess.chessType.Bishop, 5,0);
        createChess(chessColor, chess.chessType.King, 4,0);
        createChess(chessColor, chess.chessType.Queen, 3,0);

        // Create black chess
        chessColor = chess.chesspPieces.Black;
        for(int i=0;i<8;i++)
        {
            createChess(chessColor, chess.chessType.Pawn, i, 6);
        }
        createChess(chessColor, chess.chessType.Rook, 0,7);
        createChess(chessColor, chess.chessType.Rook, 7,7);
        createChess(chessColor, chess.chessType.Knight, 1,7);
        createChess(chessColor, chess.chessType.Knight, 6,7);
        createChess(chessColor, chess.chessType.Bishop, 2,7);
        createChess(chessColor, chess.chessType.Bishop, 5,7);
        createChess(chessColor, chess.chessType.King, 4,7);
        createChess(chessColor, chess.chessType.Queen, 3,7);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(getInfo(self));
            Debug.Log(getInfo(other));
            Debug.Log(getInfo(ba));
        }
    }

    public void createChess(chess.chesspPieces color, chess.chessType type, int x, int y)
    {
        chess chessObject = ba.createChess(color, type, x, y);
        playerColor[(int)color].recordHave(chessObject);
    }

    public bool isFriendlyChess(Vector2Int grid2)
    {
        chess chessObject = ba.getChess(grid2);
        if (chessObject == null) 
        {
            return false;
        }
        if (other.getHave(chessObject))
        {
            return false;
        }

        return true;
    }

    public bool determineMove(chess a)
    {
        for(int i=0;i<moved.Count;i++)
        {
            if(a.gameObject == moved[i].gameObject)
            {
                return true;
            }

        }
        return false;

    }
    public void historyMoved(chess a)
    {
        moved.Add(a);
    } 


    public string getInfo(Player player)
    {
        string log = $"Name: {player.userName} \n";
        log += $"rating: {player.rating} \n";
        log += $"color: {player.color} \n";
        for(int i = 0;i < player.eatan.GetLength(0);i++) log += $"eatan: [i]: {player.eatan[i]} \n";
        for(int i = 0;i < player.have.GetLength(0);i++) log += $"have: [i]: {player.have[i]} \n";
        return log;
    }
    public void changePlayer()
    {
        if(turns==self)
        {
            turns=other;
        }
        else
        {
        
            turns=self;
        }

    }

    public string getInfo(chessBoard chessBoardCtrl)
    {
        string log = $"chessBoard: all board info \n";
        for(int x = 0;x < 8; x++) 
        {
            for(int y = 0;y < 8; y++) 
            {
                Vector2Int grid2 = new Vector2Int(x, y);
                log += $"{grid2}: {chessBoardCtrl.getChess(grid2)} \n";
            }
        }
        
        return log;
    }

    public string LogInfo(chess piece)
    {
        if(!piece) return "Chess is null !";

        string log = $"ChessColor: {piece.mChesspPieces} \n";
        log += $"ChessType: {piece.mChessType} \n";
        return log;
    }
    public void eat(chess piece)
    {
        turns.getEatan(piece);
        Destroy(piece);
    }

}

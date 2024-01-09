using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamedev : MonoBehaviour
{
    static public Gamedev instance;

    float fixedTimer = 0.0f;

    public float[] considerSec = { 60.0f, 5*60.0f, 10*60.0f }; 

    public List<chess> moved;

    public Player self;
    public Player other;

    public Player turns;
    public Player[] playerColor;
    public chessBoard ba;
    public UIController uiController;

    [Tooltip("DebugSection")]
    public bool debugMode = false;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        initialize();
        startGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(debugMode)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(getInfo(self));
                Debug.Log(getInfo(other));
                Debug.Log(getInfo(ba));
            }
        }
    }

    void FixedUpdate()
    {
        countDownTimer(ref fixedTimer, 1.0f, countDownConsiderTime);
    }

    void initialize()
    {
        // Initialize game system
        fixedTimer = 0.0f;

        moved = new List<chess>();

        playerColor = new Player[2];
        playerColor[(int)chess.chesspPieces.White] = new Player("playerName", chess.chesspPieces.White);
        playerColor[(int)chess.chesspPieces.Black] = new Player("playerName", chess.chesspPieces.Black);

        self = null;
        other = null;
        turns = null;

        ChessController.instance.Initialize();

        ba.Initialize();
        // ba.gridSize = new Vector2(4.2f, 4.2f);
        // ba.originPosition = new Vector3(0.0f, 2.1f, 0.0f);
        // ba.chessAngle = new Vector3(90.0f, 0.0f, 0.0f);

        // UI initialize
        uiController.Initialize();
        // TODO: set consider time ui menu

    }

    void startGame()
    {
        // configure player color
        chess.chesspPieces chessColor = chess.chesspPieces.White;
        self = playerColor[(int)chessColor] = new Player("Bob", chessColor);
        chessColor = chess.chesspPieces.Black;
        other = playerColor[(int)chessColor] = new Player("Joe", chessColor);
        
        // TODO: get ui consider timer selected
        int tempIndex = 0;
        // configure considerTime
        self.considerTime = considerSec[tempIndex];
        other.considerTime = considerSec[tempIndex];

        // configure chessboard
        // Create white chess
        chessColor = chess.chesspPieces.White;
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

        // configure player info
        setPlayerName(self);
        setPlayerTurn(self, false);
        setPlayerConsiderTime(self);
        setPlayerName(other);
        setPlayerTurn(other, false);
        setPlayerConsiderTime(other);

        // first player
        changePlayer();
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
        if (!turns.getHave(chessObject))
        {
            return false;
        }

        return true;
    }

    public bool determineMove(chess a)
    {
        for(int i=0;i<moved.Count;i++)
        {
            if(!moved[i]) continue;

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

    public bool isChessBelongToCurrentPlayer(chess piece)
    {
        return piece && turns.color == piece.mChesspPieces && turns.getHave(piece);
    }

    public string getConsiderTimeFormate(float seconds)
    {       
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        return timeSpan.ToString("mm':'ss");
    }

    public string getInfo(Player player)
    {
        string log = $"Name: {player.userName} \n";
        log += $"rating: {player.rating} \n";
        log += $"color: {player.color} \n";
        for(int i = 0;i < player.eatan.GetLength(0);i++) log += $"eatan: [{i}]: {player.eatan[i]} \n";
        for(int i = 0;i < player.have.GetLength(0);i++) log += $"have: [{i}]: {player.have[i]} \n";
        return log;
    }

    public string getInfo(chessBoard chessBoardCtrl)
    {
        string log = $"chessBoard: all board info \n";
        for(int x = 0;x < 8; x++) 
        {
            for(int y = 0;y < 8; y++) 
            {
                Vector2Int grid2 = new Vector2Int(x, y);
                chess piece = chessBoardCtrl.getChess(grid2);
                log += $"{grid2}: {getInfo(piece)} \n";
            }
        }
        
        return log;
    }

    public string getInfo(chess piece)
    {
        if(!piece) return "Chess is null !";
        string log = $"Nickname: {piece.getNickname()}";
        return log;
    }

    public void checkWinner()
    {       
        if(turns == null) return;

        // TODO: check current player capture king, then current player win

        // TODO: check current player time up, then current player lose
    }

    public void changePlayer()
    {
        if(turns == null) 
        {
            turns = playerColor[(int)chess.chesspPieces.White];
        }
        else 
        {
            setPlayerTurn(turns, false);

            if(turns==self)
            {
                turns=other;
            }
            else
            {
            
                turns=self;
            }
        }

        setPlayerTurn(turns, true);
    }

    public void eat(chess piece)
    {
        Debug.Log(getInfo(turns));
        Debug.Log(getInfo(piece));

        turns.getEatan(piece);
        Destroy(piece.gameObject);
    }

    int getPlayerIndex(Player player)
    {
        int playerIndex = -1;
        if(player == self) playerIndex = 0;
        else if(player == other) playerIndex = 1;

        return playerIndex;
    }

    void setPlayerName(Player player)
    {
        UiPlayerInfo uiPlayerInfo = uiController.getSelfPlayer(getPlayerIndex(player));
        if(uiPlayerInfo == null) return;
        
        uiPlayerInfo.name.text = $"{player.color} ({player.userName})";
    }

    void setPlayerTurn(Player player, bool isTurn)
    {
        UiPlayerInfo uiPlayerInfo = uiController.getSelfPlayer(getPlayerIndex(player));
        if(uiPlayerInfo == null) return;

        uiPlayerInfo.turnIndicator.enabled = isTurn;
    }

    void setPlayerConsiderTime(Player player)
    {
        UiPlayerInfo uiPlayerInfo = uiController.getSelfPlayer(getPlayerIndex(player));
        if(uiPlayerInfo == null) return;
        
        uiPlayerInfo.considerTime.text = getConsiderTimeFormate(player.considerTime);
    }

    void countDownTimer(ref float timer, float seconds, Action<float> action = null)
    {       
        float now = Time.fixedTime;
        if(now - timer >= seconds) 
        {
            timer = now;
            action?.Invoke(seconds);
        }
    }

    void countDownConsiderTime(float delay)
    {       
        if(turns == null) return;
        turns.considerTime -= delay;
        turns.considerTime = turns.considerTime < 0 ? 0 : turns.considerTime;
        setPlayerConsiderTime(turns);
    }

    public void AddMoveHistory(HalfMove halfMove)
    {
        string msg = halfMove.ToAlgebraicNotation();
        Debug.LogWarning(msg);
        if(uiController.debugMoveHistory) 
        {
            uiController.debugMoveHistory.text += msg + "\n";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamedev : MonoBehaviour
{
    public chess[] moved;
    int moves=0;

    public Player self;
    public Player other;

    public Player turns;
    public chessBoard ba;

    // Start is called before the first frame update
    void Start()
    {
        self=new Player("Bob", chess.chesspPieces.White);
        other=new Player("Joe", chess.chesspPieces.Black);
        turns=self;

        ChessController.instance.Initialize();

        ba.Initialize();
        ba.startGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool determineMove(chess a)
    {
        for(int i=0;i<moved.GetLength(0);i++)
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
        moved[moves]=a;
        moves++;

    } 

}

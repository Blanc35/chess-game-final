using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    // Start is called before the first frame update
    public string userName;
    public int rating;
    public float considerTime;

    public chess.chesspPieces color;


    public chess[] eatan;



    public chess[] have;

    public int way;

    public Player(string a, chess.chesspPieces c)
    {
        userName=a;
        color=c;
        rating = 0;
        eatan = new chess[16];
        have =new chess[16] ;
        way = 1;

    }
    public void getEatan(chess a)
    {
        for(int i=0;i<eatan.GetLength(0);i++)
        {
            if(eatan[i]==null)
            {
                eatan[i]=a;
            }
        }
    }
    public void recordHave(chess a)
    {
        for(int i=0;i<have.GetLength(0);i++)
        {
            if(have[i]==null)
            {
                have[i]=a;
                break;
            }
        }
    }

    public bool getHave(chess a)
    {
        for(int i=0;i<have.GetLength(0);i++)
        {
            if(!have[i]) continue;

            if(a == have[i] && have[i].mChesspPieces == a.mChesspPieces && have[i].mChessType == a.mChessType)
            {
                return true;
            }
        }
        return false;
    }

    // public bool getHave(Vector2Int grid2)
    // {
    //     for(int i=0;i<have.GetLength(0);i++)
    //     {
    //         if(have[i].mChesspPieces == a.mChesspPieces && have[i].mChessType == a.mChessType)
    //         {
    //             return true;
    //         }
    //     }
    //     return true;
    // }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

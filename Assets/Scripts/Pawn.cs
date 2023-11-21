using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : chess
{

        int[][] pawn = 
    {
        new int[]{0,1},
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool isvalid(int x,int y)
    {
        for(int i=0;i<pawn.GetLength(0);i++)
        {
            for(int a=0;a<pawn.GetLength(1);a++)
            {
                if(pawn[i][a] == pawn[x][y])
                {
                    return true;
                }

            }

        }
        return false;

    }
}
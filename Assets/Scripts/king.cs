using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : chess
{

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
        for(int i=0;i<king.GetLength(0);i++)
        {
            for(int a=0;a<king.GetLength(1);a++)
            {
                if(king[i][a] == king[x][y])
                {
                    return true;
                }

            }

        }
        return false;

    }
}

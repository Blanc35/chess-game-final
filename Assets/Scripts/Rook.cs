using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : chess
{
    // Start is called before the first frame update
        int[][] rook = 
    {
        new int[]{1,0},
        new int[]{-1,0},
        new int[]{0,1},
        new int[]{0,-1}
    };
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        public override bool isvalid(int x,int y)
    {
        for(int c=1;c<8;c++)
        {
        for(int i=0;i<rook.GetLength(0);i++)
        {
            for(int a=0;a<rook.GetLength(1);a++)
            {
                if(rook[i][a]*c == rook[x][y])
                {
                    return true;
                }

            }

        }
        }
        return false;

    }
}

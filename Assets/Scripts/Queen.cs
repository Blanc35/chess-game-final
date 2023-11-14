using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : chess
{
    // Start is called before the first frame update
            int[][] queen = 
    {
        new int[]{1,1},
        new int[]{0,1},
        new int[]{0,-1},
        new int[]{-1,0},
        new int[]{-1,-1},
        new int[]{-1,1},
        new int[]{1,-1},
        
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
        for(int i=0;i<queen.GetLength(0);i++)
        {
            for(int a=0;a<queen.GetLength(1);a++)
            {
                if(queen[i][a]*8 == queen[x][y])
                {
                    return true;
                }

            }

        }
        }
        return false;

    }
}

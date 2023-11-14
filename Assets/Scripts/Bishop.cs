using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : chess
{
    // Start is called before the first frame update
    int[][] bishop = 
    {
        new int[]{1,1},
        new int[]{1,-1},
        new int[]{-1,-1},
        new int[]{-1,1}
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
        for(int i=0;i<bishop.GetLength(0);i++)
        {
            for(int a=0;a<bishop.GetLength(1);a++)
            {
                if(bishop[i][a] == bishop[x][y])
                {
                    return true;
                }

            }

        }
        return false;

    }
}

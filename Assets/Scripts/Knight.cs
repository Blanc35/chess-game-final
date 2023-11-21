using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : chess
{
    // Start is called before the first frame update
    int[][] knight = 
    {
        new int[]{2,1},
        new int[]{-2,1},
        new int[]{-2,-1},
        new int[]{2,-1},
        new int[]{-1,2},
        new int[]{-1,-2},
        new int[]{1,-2},
        new int[]{1,2}
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
        for(int i=0;i<knight.GetLength(0);i++)
        {
            for(int a=0;a<knight.GetLength(1);a++)
            {
                if(knight[i][a] == knight[x][y])
                {
                    return true;
                }

            }

        }
        return false;

    }
}

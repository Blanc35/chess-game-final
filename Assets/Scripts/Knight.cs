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
    public override Dictionary<Vector2Int, List<Vector2Int>> getMoveable(Vector2Int grid2)
    {
        Dictionary<Vector2Int, List<Vector2Int>> moveAttacks = new Dictionary<Vector2Int, List<Vector2Int>>();

        int[][] directions = knight;
        for(int dir = 0; dir < directions.GetLength(0); dir++)
        {
            for (int i = 1; i < 2; i++)
            {
                Vector2Int moveableGrid2 = new Vector2Int(grid2.x + i * directions[dir][0], grid2.y + i * directions[dir][1]);
                moveAttacks.Add(moveableGrid2, new List<Vector2Int>(){moveableGrid2});
            }
        }

        return moveAttacks;
    }
}

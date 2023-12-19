using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : chess
{
    Gamedev gamedev;
    int[][] pawn =
{
        new int[]{0,1},
        new int[]{0,2},
    };
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override bool isvalid(int x, int y)
    {

        for (int i = 0; i < pawn.GetLength(0); i++)
        {
            for (int a = 0; a < pawn.GetLength(1); a++)
            {
                if (pawn[i][a] == pawn[x][y])
                {
                    if (gamedev.determineMove(this))
                    {
                        return true;
                    }
                    else
                    {
                        for (int s = 1; s < 2; s++)
                        {
                            if (pawn[i][a] * s == pawn[x][y])
                            {
                                return true;
                            }
                        }
                    }


                }

            }

        }
        return false;
    }
           public override List<Vector2Int> getMoveable(Vector2Int grid2)
    {
        List<Vector2Int> moveable = new List<Vector2Int>();

        int[][] directions = pawn;
        for(int dir = 0; dir < directions.GetLength(0); dir++)
        {
            for (int i = 1; i < 2; i++)
            {
                if(mChesspPieces== chesspPieces.Black)
                {
                Vector2Int moveableGrid2 = new Vector2Int(grid2.x + i* directions[dir][0], grid2.y + i * -1 * directions[dir][1]);
                moveable.Add(moveableGrid2);
                }
                else{
                                    Vector2Int moveableGrid2 = new Vector2Int(grid2.x + i * directions[dir][0], grid2.y + i * directions[dir][1]);
                moveable.Add(moveableGrid2);
                }
            }
        }

        return moveable;
    }

}

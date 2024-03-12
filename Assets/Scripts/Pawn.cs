using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : chess
{
    int[][] pawn =
{
        new int[]{0,1},
        new int[]{0,2},
         new int[]{1,1},
          new int[]{-1,1},
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
                    if (Gamedev.instance.determineMove(this))
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
    public override Dictionary<Vector2Int, List<Vector2Int>> getMoveable(Vector2Int grid2)
    {
        Dictionary<Vector2Int, List<Vector2Int>> moveAttacks = new Dictionary<Vector2Int, List<Vector2Int>>();


        int[][] directions = pawn;
        // forward
        int dirLength = (Gamedev.instance.determineMove(this) ? 1 : 2);
        for (int dir = 0; dir < dirLength; dir++)
        {
            for (int i = 1; i < 2; i++)
            {
                if (mChesspPieces == chesspPieces.Black)
                {
                    Vector2Int moveableGrid2 = new Vector2Int(grid2.x + i * directions[dir][0], grid2.y + i * -1 * directions[dir][1]);
                    if (Gamedev.instance.ba.getChess(moveableGrid2) == null)
                    {
                        moveAttacks.Add(moveableGrid2, new List<Vector2Int>(){});
                    }
                }
                else
                {
                    Vector2Int moveableGrid2 = new Vector2Int(grid2.x + i * directions[dir][0], grid2.y + i * directions[dir][1]);
                    if (Gamedev.instance.ba.getChess(moveableGrid2) == null)
                    {
                        moveAttacks.Add(moveableGrid2, new List<Vector2Int>(){});
                    }
                }
            }
            
        }

        // attack
        for (int i = 2; i < 4; i++)
        {

            if (mChesspPieces == chesspPieces.Black)
            {
                
                // normal attack
                Vector2Int moveableGrid2 = new Vector2Int(grid2.x + directions[i][0], grid2.y + -1 * directions[i][1]);
                if (!Gamedev.instance.isFriendlyChess(moveableGrid2) && Gamedev.instance.ba.getChess(moveableGrid2) != null)
                {
                    moveAttacks.Add(moveableGrid2, new List<Vector2Int>(){moveableGrid2});
                }

                // en passant attack
                Vector2Int moveableGrid2Left = new Vector2Int(grid2.x -1, grid2.y);
                Vector2Int moveableGrid2Right = new Vector2Int(grid2.x +1, grid2.y);
                if(
                    grid2.y==3 
                    &&
                    (
                        (!Gamedev.instance.isFriendlyChess(moveableGrid2Left) && Gamedev.instance.ba.getChess(moveableGrid2Left) != null && moveableGrid2Left.x == moveableGrid2.x)
                        || (!Gamedev.instance.isFriendlyChess(moveableGrid2Right) && Gamedev.instance.ba.getChess(moveableGrid2Right) != null && moveableGrid2Right.x == moveableGrid2.x)
                    )
                )
                {
                    if ((!Gamedev.instance.isFriendlyChess(moveableGrid2Left) && Gamedev.instance.ba.getChess(moveableGrid2Left) != null && moveableGrid2Left.x == moveableGrid2.x) ) moveAttacks.Add(moveableGrid2, new List<Vector2Int>(){moveableGrid2Left});
                    else if ((!Gamedev.instance.isFriendlyChess(moveableGrid2Right) && Gamedev.instance.ba.getChess(moveableGrid2Right) != null && moveableGrid2Right.x == moveableGrid2.x))  moveAttacks.Add(moveableGrid2, new List<Vector2Int>(){moveableGrid2Right});

                }

            }
            else
            {
                // normal attack
                Vector2Int moveableGrid2 = new Vector2Int(grid2.x + directions[i][0], grid2.y + directions[i][1]);
                if (!Gamedev.instance.isFriendlyChess(moveableGrid2) && Gamedev.instance.ba.getChess(moveableGrid2) != null)
                {
                    moveAttacks.Add(moveableGrid2, new List<Vector2Int>(){moveableGrid2});
                }

                // en passant attack
                Vector2Int moveableGrid2Left = new Vector2Int(grid2.x -1, grid2.y);
                Vector2Int moveableGrid2Right = new Vector2Int(grid2.x +1, grid2.y);
                if(
                    grid2.y==4 
                    &&
                    (
                        (!Gamedev.instance.isFriendlyChess(moveableGrid2Left) && Gamedev.instance.ba.getChess(moveableGrid2Left) != null && moveableGrid2Left.x == moveableGrid2.x)
                        || (!Gamedev.instance.isFriendlyChess(moveableGrid2Right) && Gamedev.instance.ba.getChess(moveableGrid2Right) != null && moveableGrid2Right.x == moveableGrid2.x)
                    )
                )
                {
                    if( (!Gamedev.instance.isFriendlyChess(moveableGrid2Left) && Gamedev.instance.ba.getChess(moveableGrid2Left) != null && moveableGrid2Left.x == moveableGrid2.x)) moveAttacks.Add(moveableGrid2, new List<Vector2Int>(){moveableGrid2Left});
                    else if((!Gamedev.instance.isFriendlyChess(moveableGrid2Right) && Gamedev.instance.ba.getChess(moveableGrid2Right) != null && moveableGrid2Right.x == moveableGrid2.x)) moveAttacks.Add(moveableGrid2, new List<Vector2Int>(){moveableGrid2Right});
                }

            
            }

        }

        // TODO: check EnPassant attack
    return moveAttacks;
    }
    
    

}

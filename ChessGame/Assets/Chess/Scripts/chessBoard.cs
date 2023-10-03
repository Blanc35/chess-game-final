using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessBoard : MonoBehaviour
{
     Vector2[,] boardSetup = new Vector2[8, 8];
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<8;i++)
        {
            for(int k=0;k<8;k++)
            {
                boardSetup[k, i] = new Vector2(k, i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void placement(chess piece, int x, int y)
    {
        Vector2 position = boardSetup[x, y];
        piece.transform.position = new Vector3(position.x, position.y, 0.0f);
    }
    void movements()
    {

    }
    void remove()
    {
        
    }
    void check()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public string userName;
    public int rating;

    public chess.chesspPieces color;


    public chess[] eatan;


    public chess[] have;

    public int way;

    public Player(string a, chess.chesspPieces c)
    {
        userName=a;
        color=c;
        rating = 0;
        eatan = new chess[16];
        have =new chess[16] ;
        way = 1;

    }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

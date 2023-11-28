using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamedev : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public chess[] moved;
    int moves=0;
    public bool determineMove(chess a)
    {
        for(int i=0;i<moved.GetLength(0);i++)
        {
            if(a.gameObject == moved[i].gameObject)
            {
                return true;
            }

        }
        return false;

    }
    public void historyMoved(chess a)
    {
        moved[moves]=a;
        moves++;

    }

}

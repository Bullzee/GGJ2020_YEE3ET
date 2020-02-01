using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private int repairsFinished = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: keep track of boss's progress
        // if boss gets to town, end game in failure.
    }

    public void repairFinished()
    {
        repairsFinished++;

        //TODO: 
        //if (repairsFinished >= NUM_REPAIRS)
            // end the game in success
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public MiscManager theMiscManager;

    private int repairsFinished = 0;
    int NUM_REPAIRS = 5;
    bool gameOver = false; 

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
        theMiscManager.UpdateHealth(0.2f);
        //TODO: 
        if (repairsFinished >= NUM_REPAIRS)
        {
            if (gameOver == false)
            {
                SceneManager.LoadScene("Win", LoadSceneMode.Additive);
                gameOver = true; 
            }

        }
    }
}

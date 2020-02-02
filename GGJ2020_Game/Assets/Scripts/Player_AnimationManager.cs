using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimationManager : MonoBehaviour
{
    string move = "Move", jump = "Jump", repair = "Repair", idle = "Idle";
    Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setToJump()
    {
        playerAnimator.SetBool(idle, false);
        playerAnimator.SetBool(move, false);
        playerAnimator.SetBool(jump, true);
    }

    public void setToMove()
    {
        playerAnimator.SetBool(idle, false);
        playerAnimator.SetBool(repair, false);
        playerAnimator.SetBool(jump, false);
        playerAnimator.SetBool(move, true);
    }

    public void setToRepair()
    {
        playerAnimator.SetBool(idle, false);
        playerAnimator.SetBool(move, false);
        playerAnimator.SetBool(repair, true);
    }

    public void setToIdle()
    {
        playerAnimator.SetBool(repair, false);
        playerAnimator.SetBool(jump, false);
        playerAnimator.SetBool(idle, true);
        playerAnimator.SetBool(move, false);
    }
}

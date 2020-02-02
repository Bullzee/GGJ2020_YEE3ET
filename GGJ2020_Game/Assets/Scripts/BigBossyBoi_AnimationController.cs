using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossyBoi_AnimationController : MonoBehaviour
{
    string Attacking = "Attack", Idle = "Idle";
    Animator bossyBoi_Anim;
    // Start is called before the first frame update
    void Start()
    {
        bossyBoi_Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void setToAttack()
    {
        bossyBoi_Anim.SetBool(Attacking, true);
        bossyBoi_Anim.SetBool(Idle, false);
    }

    public void setToIdle()
    {
        bossyBoi_Anim.SetBool(Idle, true);
        bossyBoi_Anim.SetBool(Attacking, false);
    }


}

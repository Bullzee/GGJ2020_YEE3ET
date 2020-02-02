using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum BossState {Targeting,LockedOn,Attacking,Stuck,Shaking,CoolingDown};
/*public delegate void UpdateLogic();
public delegate void OnEnterState();
public delegate void OnExitSate();
public delegate bool ExitConditions();*/
public class BigBossyBoi : MonoBehaviour
{
    public float StuckTime = 15.0f;
    public float SpinTime = 1.0f;
    public float CoolDownTime = 1.5f;
    public float angleThreshold = 5.0f;
    public float fastTurnThreshold = 120.0f;
    public float turnSpeed = 35.0f;
    public float quickTurnSpeed =90.0f;
    public Transform lockOnTarget;
    public float spinSpeed = 720.0f;
    public int stuckOnAttackX = 3;
    public RocketLauncher launcher;
    public float rocketCD = 7.0f;
    float _rocketTimer = 0;
    int attackCounter = 0;
    bool _playerOn = false;
    bool _hitPlayer = false;
    bool _finishedAttack = false;
    bool _KockOff = false;
    float _timer = 0.0f;
    
    BossState state;
    BigBossyBoi_AnimationController animationController;

    // Start is called before the first frame update
    void Start()
    {
        state = BossState.CoolingDown;
        animationController = GetComponent<BigBossyBoi_AnimationController>();
    }

    // Update is called once per frame
    void Update()
    {

        switch(state){
            case BossState.Targeting:
                TargetLogic();
                break;
            case BossState.LockedOn:
                LockedOnLogic();
                break;
            case BossState.Attacking:
                AttackingLogic();
                break;
            case BossState.Stuck:
                StuckLogic();
                break;
            case BossState.Shaking:
                ShakeLogic();
                break;
            case BossState.CoolingDown:
                CoolDownLogic();
                break;

        }
        if (state != BossState.Stuck && state != BossState.Shaking&&_KockOff)
        {
            exitState();
            state = BossState.Shaking;
        }

        if(state != BossState.Stuck)
        {
            _rocketTimer += Time.deltaTime;
            if (_rocketTimer>=rocketCD) {
                _rocketTimer = 0;
                launcher.Fire(lockOnTarget);
            }
        }
        _playerOn = false;
    }
    void exitState()
    {
        _hitPlayer = false;
        _finishedAttack = false;
        _timer = 0;
        switch (state)
        {
            case BossState.Targeting:
               
                break;
            case BossState.LockedOn:
                
                break;
            case BossState.Attacking:
             
                break;
            case BossState.Stuck:
              
                break;
            case BossState.Shaking:
              
                break;
            case BossState.CoolingDown:
            
                break;

        }

    }

    void TargetLogic()
    {
        Debug.Log("Targeting");
        if (LockOnPlayer())
        {
            state = BossState.LockedOn;
        }
    }
    bool LockOnPlayer()
    {
        Vector3 robotToPlayer = new Vector3(lockOnTarget.position.x-transform.position.x,0, lockOnTarget.position.z - transform.position.z);
        Debug.DrawRay(transform.position,robotToPlayer,Color.red);
        Debug.DrawRay(transform.position, transform.forward*10.0f, Color.red);
        
        float angleBetween = Vector3.Angle(transform.forward, robotToPlayer);
        Vector3 newDirection;
        //Debug.Log(angleBetween);
        if (angleBetween < angleThreshold)
        {
            return true;
        }
        if (angleBetween > fastTurnThreshold)
        {

            Debug.Log("GottaGOFast");
            /*    Vector3 newDir = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, angleBetween, 0), quickTurnSpeed * Time.deltaTime);
                 Quaternion.RotateTowards();*/
             newDirection = Vector3.RotateTowards(transform.forward, robotToPlayer, Mathf.Deg2Rad*quickTurnSpeed*Time.deltaTime, 0.0f);

            // Draw a ray pointing at our target in
         //   Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
            return false;
        }
        Debug.Log("SlowTurn");

        //  transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,angleBetween,0), turnSpeed * Time.deltaTime);
         newDirection = Vector3.RotateTowards(transform.forward, robotToPlayer, Mathf.Deg2Rad * turnSpeed * Time.deltaTime, 0.0f);

        // Draw a ray pointing at our target in
        //   Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
        return false;
        
       
    }
    void LockedOnLogic()
    {
        Debug.Log("Locked");
        /*
         Start animation
         */
        animationController.setToAttack();
        _hitPlayer = false;
        _finishedAttack = false;
        state = BossState.Attacking;
    }
    void AttackingLogic()
    {
        Debug.Log("Attacking");
        if (_finishedAttack)
        {
            
            attackCounter++;

               
            if(attackCounter>=stuckOnAttackX&&!_hitPlayer)
            {
              
                    state = BossState.Stuck;
                    attackCounter = 0;
                
               
            }
            else
            {
            state = BossState.CoolingDown;
                PlayUprightAnimation();
            }

        }
      //  state = BossState.Stuck;
    }
    void StuckLogic()
    {
        Debug.Log("Help I have fallen and I can't get up");
        if (_timer < StuckTime)
        {
            _timer += Time.deltaTime;
        }
        if (_timer>=StuckTime)
        {
            _timer = 0;
            if (_playerOn)
            {
                state = BossState.Shaking;
            }
            else
                state = BossState.CoolingDown;
            PlayUprightAnimation();
        }

    }
    void ShakeLogic()
    {
        Debug.Log("Get of you filthy heathen!");
       
        if (_timer < SpinTime)
        {
            _timer += Time.deltaTime;
        }
        if (_timer >= SpinTime)
        {
            _timer = 0;
          
            state = BossState.CoolingDown; 
        }
        transform.Rotate(new Vector3(0 ,720.0f*Time.deltaTime ,0));

    }

    void CoolDownLogic()
    {
        Debug.Log("Cooling Down");
        if (_timer < CoolDownTime)
        {
            _timer += Time.deltaTime;
        }
        if (_timer >= CoolDownTime)
        {
            _timer = 0;
            state = BossState.Targeting;
        }
    }

    public void HitSomething()
    {
        _hitPlayer = true;
    }
    public void FinishAttack()
    {
        _finishedAttack = true;
    }

    void PlayUprightAnimation()
    {
        //play animation to stand up
        animationController.setToIdle();
    }
    public void FreeBossyBoi()
    {
        if (state == BossState.Stuck)
        {
            PlayUprightAnimation();
            state = BossState.CoolingDown;
            _timer = 0;
        }

    }
    /*void OnCollisionEnter(Collision col)
    {
      if (col.gameObject.tag == "Player")
        {
            _playerOn = true;
        }
    }*/
}
/*public class BossStateMachine:ScriptableObject
{
   BossState state;
   Dictionary<BossState, OnEnterState> EnterState = new Dictionary<BossState, OnEnterState>();
    Dictionary<BossState, OnEnterState> EnterState = new Dictionary<BossState, OnEnterState>();







}*/

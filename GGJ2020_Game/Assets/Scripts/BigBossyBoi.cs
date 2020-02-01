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
    public float StuckTime = 30.0f;
    public float SpinTime = 1.0f;
    public float CoolDownTime = 1.5f;
    public float angleThreshold = 5.0f;
    public float fastTurnThreshold = 120.0f;
    public float turnSpeed = 15.0f;
    public float quickTurnSpeed = 30.0f;
    public Transform lockOnTarget;
    bool _playerOn = false;
    bool _hitPlayer = false;
    bool _finishedAttack = false;
    bool _KockOff = false;
    float _timer = 0.0f;
    
    BossState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BossState.CoolingDown;
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
        if (LockOnPlayer())
        {
            state = BossState.Attacking;
        }
    }
    bool LockOnPlayer()
    {
        Vector3 robotToPlayer = new Vector3(lockOnTarget.position.x-transform.position.x,0, lockOnTarget.position.z - transform.position.z);
        float angleBetween = Vector3.Angle(transform.forward, robotToPlayer);
        if (angleBetween < angleThreshold)
        {
            return true;
        }
        if (angleBetween > fastTurnThreshold)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, angleBetween, 0), quickTurnSpeed * Time.deltaTime);
            return false;
        }
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,angleBetween,0), turnSpeed * Time.deltaTime);
            return false;
        
       
    }
    void LockedOnLogic()
    {
        /*
         Start animation
         */
        _hitPlayer = false;
        _finishedAttack = false;
        state = BossState.Attacking;
    }
    void AttackingLogic()
    {
        if (_finishedAttack)
        {
            if (_hitPlayer)
            {
                PlayUprightAnimation();
                state = BossState.CoolingDown;
            }
            else
            {
                state = BossState.Stuck;
            }

        }

    }
    void StuckLogic()
    {
        
        if (_timer < StuckTime)
        {
            _timer += Time.deltaTime;
        }
        if (_timer>=StuckTime)
        {
            _timer = 0;
            state = BossState.Shaking;
        }

    }
    void ShakeLogic()
    {

        PlayUprightAnimation();
        if (_timer < SpinTime)
        {
            _timer += Time.deltaTime;
        }
        if (_timer >= SpinTime)
        {
            _timer = 0;
            state = BossState.CoolingDown;
        }
    }

    void CoolDownLogic()
    {
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
    }
}
/*public class BossStateMachine:ScriptableObject
{
   BossState state;
   Dictionary<BossState, OnEnterState> EnterState = new Dictionary<BossState, OnEnterState>();
    Dictionary<BossState, OnEnterState> EnterState = new Dictionary<BossState, OnEnterState>();







}*/

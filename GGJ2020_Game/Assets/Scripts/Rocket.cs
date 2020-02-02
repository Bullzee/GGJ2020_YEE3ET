using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MissileState {StageOne,StageTwo,StageThree };
public class Rocket : MonoBehaviour
{
    Transform _targetPos;
    bool _launched = false;
    MissileState state;
    float _speed = 25.0f;
    float _stageOneTimer = 0.75f;
    float _timer = 0.0f;
    private float rotationSpeed = 1000;
    private float focusDistance = 10;
    private bool isLookingAtObject = true;
     float stageTwoDistance = 5.0f;
    public void Launch(Transform target)
    {
        _targetPos = target;
        _launched = true;
        transform.forward = transform.up;
    }
    // Start is called before the first frame update
    void Start()
    {
        state = MissileState.StageOne;
        Destroy(gameObject,15);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position,transform.forward * 5,Color.red );
        Debug.DrawRay(transform.position, transform.up * 5, Color.blue);
        if (_launched)
        {
            //  y = mx+b
            switch (state)
            {
                case MissileState.StageOne:
                    StageOneLogic();
                    break;
                case MissileState.StageTwo:
                    StageTwoLogic();
                    break;
                case MissileState.StageThree:
                    StageThreeLogic();
                    break;


            }
        }
    }

    void StageOneLogic()
    {
        Vector3 newDirection = Vector3.RotateTowards(transform.forward,_targetPos.position, Mathf.Deg2Rad * 30.0f * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.position += transform.forward * (_speed*0.5f) * Time.deltaTime;
        _timer += Time.deltaTime;
        if (_timer >= _stageOneTimer)
        {
            state = MissileState.StageTwo;
        }
    }
    void StageTwoLogic()
    {
        Vector3 targetDirection = _targetPos.position - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0F);

        transform.Translate(Vector3.forward * Time.deltaTime * _speed, Space.Self);

        if (Vector3.Distance(transform.position, _targetPos.position) < stageTwoDistance)
        {
            state = MissileState.StageThree;
        }

     
            transform.rotation = Quaternion.LookRotation(newDirection);
        Debug.DrawRay(transform.position, targetDirection * 5, Color.black);
        Debug.DrawRay(transform.position, newDirection * 5, Color.cyan);

    }

    
    void StageThreeLogic()
    {
        Vector3 targetDirection = _targetPos.position - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0F);

        transform.Translate(Vector3.forward * Time.deltaTime * _speed, Space.Self);

        if (Vector3.Distance(transform.position, _targetPos.position) < focusDistance)
        {
            isLookingAtObject = false;
        }

        if (isLookingAtObject)
        {
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        Debug.DrawRay(transform.position, targetDirection * 5, Color.black);
        Debug.DrawRay(transform.position, newDirection * 5, Color.cyan);

    }
    void OnCollisionEnter(Collision col)
    {
    if(col.gameObject.tag == "Player")
        {
            Debug.Log("here");
            col.gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * 10.0f, ForceMode.VelocityChange);
        }

        Destroy(gameObject);
    }

}

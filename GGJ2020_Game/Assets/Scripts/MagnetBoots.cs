using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Referenced https://answers.unity.com/questions/155907/basic-movement-walking-on-walls.html?_ga=2.216328085.2006187933.1580522026-1664004522.1569421874

public class MagnetBoots : MonoBehaviour
{
    [SerializeField]
    public string RobotMask;
    Vector3 playerNormal, groundNormal, playerForward;
    Quaternion gravRotation;
    [SerializeField]
    float worldGravity = 9.8f, rotationSpeed = 1f, forwardOffset = 2f, groundOffset = 2f, rotationTime = 7f, height = 0.5f;
    [SerializeField]
    int layerMask;

    Rigidbody playerRigidbody;
    Ray playerRay, playerForwardRay;
    RaycastHit groundPoint;

    Vector3 rotation;
    Vector3 highTransform;

    public PlayerMovement player;

    public GameObject sparkParticles;

    //for sounds
    public SoundManager theSoundManager;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask(RobotMask);
        groundNormal = Vector3.up;
        playerNormal = transform.up;
        //highTransform = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        playerRigidbody = GetComponentInChildren<Rigidbody>();
    }

    void FixedUpdate()
    {
        playerRigidbody.AddForce(-worldGravity * playerRigidbody.mass * playerNormal);
    }

    // Update is called once per frame
    void Update()
    {
        highTransform = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        playerRay = new Ray(transform.position, -transform.up);
        playerForwardRay = new Ray(transform.position, transform.forward);

        switch (Physics.Raycast(playerForwardRay, out groundPoint, forwardOffset, layerMask))
        {
            case true:
                groundNormal = groundPoint.normal;
                sparkParticles.SetActive(true);
                break;
            case false:
                if (Physics.Raycast(playerRay, out groundPoint, groundOffset, layerMask))
                {
                    groundNormal = groundPoint.normal;
                    theSoundManager.PlayMagnet();
                    //playerNormal = groundNormal;
                    sparkParticles.SetActive(true);
                }
                else
                {
                    groundNormal = Vector3.up;
                    theSoundManager.StopMagnet(); 
                    sparkParticles.SetActive(false);
                }
                break;   
        }

        //if (Physics.Raycast(playerRay, out groundPoint, groundOffset, layerMask))
        //{
        //    groundNormal = groundPoint.normal;
         
        //    sparkParticles.SetActive(true);
        //}
        //else if (Physics.Raycast(playerForwardRay, out groundPoint, forwardOffset, layerMask))
        //{
        //    groundNormal = groundPoint.normal;
        //    //playerNormal = groundNormal;
        //    sparkParticles.SetActive(true);
        //}
        //else
        //{
        //    groundNormal = Vector3.up;
        //    sparkParticles.SetActive(false);
        //}


        playerNormal = groundNormal;
        Debug.DrawRay(transform.position, playerNormal);
        rotation = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        
        playerForward = Vector3.Cross(transform.right, groundNormal);
        gravRotation = Quaternion.LookRotation(playerForward, playerNormal);
        transform.rotation = Quaternion.Lerp(transform.rotation, gravRotation, rotationTime * Time.deltaTime) * Quaternion.Euler(rotation * rotationSpeed);
    }
}

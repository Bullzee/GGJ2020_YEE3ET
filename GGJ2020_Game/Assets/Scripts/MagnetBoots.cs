using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Referenced https://answers.unity.com/questions/155907/basic-movement-walking-on-walls.html?_ga=2.216328085.2006187933.1580522026-1664004522.1569421874

public class MagnetBoots : MonoBehaviour
{
    Vector3 playerNormal, groundNormal, playerForward;
    Quaternion gravRotation;
    [SerializeField]
    float worldGravity = 9.8f, rotationSpeed = 1f, groundOffset = 2f;
    int layerMask = ~(1 << 8);

    Rigidbody playerRigidbody;
    Ray playerRay;
    RaycastHit groundPoint;

    Vector3 rotation;

    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        groundNormal = Vector3.up;
        playerNormal = transform.up;
        playerRigidbody = GetComponentInChildren<Rigidbody>();
    }

    void FixedUpdate()
    {
        playerRigidbody.AddForce(-worldGravity * playerRigidbody.mass * playerNormal);
    }
    // Update is called once per frame
    void Update()
    {
        playerRay = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(playerRay, out groundPoint, groundOffset, layerMask))
        {
            groundNormal = groundPoint.normal;
            playerNormal = groundNormal;
        }
        else
        {
            groundNormal = Vector3.up;
            playerNormal = groundNormal;
        }

        Debug.DrawRay(transform.position, playerNormal);
        rotation = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        
        playerForward = Vector3.Cross(transform.right, groundNormal);
        gravRotation = Quaternion.LookRotation(playerForward, playerNormal);
        transform.rotation = Quaternion.Lerp(transform.rotation, gravRotation, 10 * Time.deltaTime) * Quaternion.Euler(rotation * rotationSpeed);
    }

}

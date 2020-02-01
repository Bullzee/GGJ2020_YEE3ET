using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed, jumpForce, jumpOffset;

    Rigidbody playerRigidbody;
    RaycastHit groundRaycast;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 5f;
        jumpForce = 1f;
        jumpOffset = 1.5f;
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        playerMove();
        playerJump();
    }

    public void playerMove()
    {
        //limit angles of camera to 0 so player doesn't try to move up or down
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * movementSpeed * Time.deltaTime);
    }

    public void playerJump()
    {
        if (Physics.Raycast(transform.position, -transform.up, out groundRaycast, jumpOffset) && Input.GetAxis("Jump") > 0)
        {
            playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        }
    }



}

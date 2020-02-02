using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 5f, jumpForce = 1f, jumpOffset = 1.5f;

    Rigidbody playerRigidbody;
    RaycastHit groundRaycast;

    MagnetBoots magnet;

    //for the sounds
    public SoundManager theSoundManager; 

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        magnet = GetComponent<MagnetBoots>();
    }

    void Update()
    {
        playerMove();
        playerJump();
    }

    public void playerMove()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(move * movementSpeed * Time.deltaTime);
    }

    public void playerJump()
    {
        float jump = Input.GetAxis("Jump");
        if (Physics.Raycast(transform.position, -transform.up, out groundRaycast, jumpOffset) && jump > 0)
        {
            playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
            theSoundManager.PlayJump(); 
        }
    }



}

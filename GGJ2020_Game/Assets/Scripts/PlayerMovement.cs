using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 5f, jumpForce = 1f, jumpOffset = 1.5f, knockBackForce = 5f;

    Rigidbody playerRigidbody;
    RaycastHit groundRaycast;

    MagnetBoots magnet;

    //for the sounds
    public SoundManager theSoundManager;

    Player_AnimationManager animator;

    public bool repairing = false;
    bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        magnet = GetComponent<MagnetBoots>();
        animator = GetComponent<Player_AnimationManager>();
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
        if (Physics.Raycast(transform.position, -transform.up, out groundRaycast, jumpOffset))
        {
            if (jump > 0)
            {
                playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
                theSoundManager.PlayJump();
            }

            if (!repairing)
            {
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                    animator.setToMove();
                else
                {
                    animator.setToIdle();
                }
            }
            else
                animator.setToRepair();

        }
        else
            animator.setToJump();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KnockBack"))
        {
            KnockBack();
        }
    }

    void KnockBack()
    {
        playerRigidbody.AddForce(-transform.forward * jumpForce, ForceMode.VelocityChange);

    }


}

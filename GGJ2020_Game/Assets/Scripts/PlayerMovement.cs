using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 10f;
    }

    void Update()
    {
        playerMove();
    }

    public void playerMove()
    {
        //limit angles of camera to 0 so player doesn't try to move up or down
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * movementSpeed * Time.deltaTime);
    }



}

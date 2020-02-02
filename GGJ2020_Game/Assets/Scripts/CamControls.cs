using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControls : MonoBehaviour
{
    Vector3 rotation;
    [SerializeField]
    float rotationSpeed, minAngle, maxAngle;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 1f;
        minAngle = -40f;
        maxAngle = 80f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        lookAround();
    }

    public void lookAround()
    {
        rotation += new Vector3(-Input.GetAxis("Mouse Y"), 0, 0) * rotationSpeed;
        rotation.x = Mathf.Clamp(rotation.x, minAngle, maxAngle);
        transform.localRotation = Quaternion.Euler(rotation);
    }
}

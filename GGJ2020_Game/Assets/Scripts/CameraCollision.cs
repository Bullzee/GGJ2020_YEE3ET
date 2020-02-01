using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [SerializeField]
    Vector3 normalPosition;
    [SerializeField]
    float rayDistance, groundOffset, transistionTime;

    public Transform rayTransform;

    RaycastHit cameraHit;
    // Start is called before the first frame update
    void Start()
    {
        normalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(rayTransform.position, -rayTransform.forward, out cameraHit, rayDistance))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(cameraHit.point.x, cameraHit.point.y + groundOffset, cameraHit.point.z), transistionTime * Time.deltaTime);
        }
        else
            transform.localPosition = Vector3.Lerp(transform.localPosition, normalPosition, transistionTime * Time.deltaTime);
    }
}

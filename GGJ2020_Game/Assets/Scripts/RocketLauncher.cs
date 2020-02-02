using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
   public GameObject RocketPrefab;
 //   public Transform RocketTransform;
    public void Fire(Transform player)
    {
      GameObject temp = Instantiate(RocketPrefab, transform.position,transform.rotation);
        Rocket rocket = temp.GetComponentInChildren<Rocket>();
        rocket.Launch(player);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

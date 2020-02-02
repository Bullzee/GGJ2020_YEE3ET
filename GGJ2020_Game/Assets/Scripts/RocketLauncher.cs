using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
   public GameObject RocketPrefab;
    public Transform RocketTransform;
    public void Fire()
    {
      GameObject temp = Instantiate(RocketPrefab, RocketTransform.position,RocketTransform.rotation);
        Rocket rocket = temp.GetComponent<Rocket>();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public Transform throughPoint;
    public GameObject repairPrompt;
    public GameObject damageBar;
    public Transform healthBar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, throughPoint.position - transform.position);
        //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f));
        int layerMask = ~(1 << 8);

        repairPrompt.SetActive(false);
        damageBar.SetActive(false);

        if (Physics.Raycast(ray, out hit, 10f, layerMask))
        {
            if (hit.transform.tag == "Repairable")
            {
                Repairable r = hit.transform.gameObject.GetComponent<Repairable>();
                damageBar.SetActive(true);
                if (r.getCurrentDamage() > 0)
                {
                    repairPrompt.SetActive(true);

                    if (Input.GetKey(KeyCode.E))
                    {
                        r.repairDamage(Time.deltaTime);

                        healthBar.localScale = new Vector3(1f - (r.getCurrentDamage() / r.getInitialDamage()), healthBar.localScale.y, healthBar.localScale.z);
                    }
                }
            }
        }
    }
}

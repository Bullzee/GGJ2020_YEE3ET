using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiscManager : MonoBehaviour
{
    //stuff for the timer
    public Slider timerSlider;
    public Image boss, village;

    //the slider for the health
    public Slider healthTimer;
    public Image fill;

    //the city 
    public GameObject city;
    public Vector3 cityPosition;
    int cityLimit = 490;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer(0.1f * Time.deltaTime);
        UpdateHealth(0.1f * Time.deltaTime);
        MoveCity();
    }

    public void UpdateTimer(float amount)
    {
        timerSlider.value += amount;
        boss.color = new Color(1, 1, 1, timerSlider.value);
        village.color = new Color(1, 1, 1, timerSlider.value);
    }

    public void UpdateHealth(float amount)
    {
        healthTimer.value += amount;
        fill.color = Color.Lerp(Color.red, Color.green, healthTimer.value / 1);
    }

    void MoveCity()
    {
        if (cityPosition.x < cityLimit)
        {
            cityPosition.x += (10*Time.deltaTime);
            city.transform.position = new Vector3(cityPosition.x, cityPosition.y, cityPosition.z);
        }
    }
}

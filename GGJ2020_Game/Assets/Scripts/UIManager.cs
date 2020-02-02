using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //stuff for the timer
    public Slider timerSlider;
    public Image boss, village;

    //the slider for the health
    public Slider healthTimer;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer(0.2f * Time.deltaTime);
        UpdateHealth(0.2f * Time.deltaTime);
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
}

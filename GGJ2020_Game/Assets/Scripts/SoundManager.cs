using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgMusic, jumpSound, magnetSound, repairSound;

    // Start is called before the first frame update
    void Start()
    {
        bgMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayJump()
    {
        jumpSound.Play();
    }

    public void PlayMagnet()
    {
        if (!magnetSound.isPlaying)
        magnetSound.Play();
    }

    public void StopMagnet()
    {
        magnetSound.Stop();
    }

    public void PlayRepair()
    {
        if (!repairSound.isPlaying)
        repairSound.Play();
    }
}

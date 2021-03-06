﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Number of seconds it will take to repair this object.")]
    private float damage = 5;

    private float currentDamage;

    //vfx
    public ParticleSystem smokeParticles, cloudParticles;

    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        currentDamage = damage;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void repairDamage(float amount)
    {
        if (smokeParticles.isPlaying) smokeParticles.Stop();

        if (currentDamage > 0)
        {
            player.repairing = true;
            currentDamage -= amount;
            if (!cloudParticles.isPlaying) cloudParticles.Play();

            if (currentDamage <= 0)
            {
                player.repairing = false;
                currentDamage = 0;
                cloudParticles.Stop();
                GameController.instance.repairFinished();
            }
        }


    }

    public float getInitialDamage()
    {
        return damage;
    }

    public float getCurrentDamage()
    {
        return currentDamage;
    }
}

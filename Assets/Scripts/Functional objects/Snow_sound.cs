﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow_sound : MonoBehaviour
{
    public AudioSource snow_sound;
    public float min_pause;

    private float since_last = 10f, start_time;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        start_time = Time.time;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (((tag == "Player") || (tag == "Meteor")) && (since_last >= min_pause))
        {
            snow_sound.Play();
            since_last = 0;
        }
    }

    void FixedUpdate()
    {
        since_last += Time.fixedDeltaTime;
        animator.SetFloat("time", Time.time - start_time);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyBehaviour : GhostBehaviour
{
    private float pinkySpeed;
    [SerializeField] private Transform pellets;    
    [SerializeField] private GameObject pacman;

    private void Start()
    {
        pinkySpeed = pacman.GetComponent<Movement>().speed * 0.75f;
        ghostscr.movementscr.speed = pinkySpeed;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.pelletCount<=20)
        {
            ghostscr.movementscr.speed = pinkySpeed * 1.3f;
        }
        else
        {
            pinkySpeed = pacman.GetComponent<Movement>().speed * 0.75f;
            ghostscr.movementscr.speed = pinkySpeed;
        }
    }
}

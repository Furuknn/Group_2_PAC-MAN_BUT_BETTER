using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyBehaviour : GhostBehaviour
{
    private float blinkySpeed;
    [SerializeField] private GameObject pacman;

    private void Start()
    {
        blinkySpeed = pacman.GetComponent<Movement>().speed / 2;
        ghostscr.movementscr.speed = blinkySpeed;
    }
    private void BlinkyRageOn(float duration)
    {


        Invoke(nameof(BlinkyRageOff), duration);
    }

    private void BlinkyRageOff()
    {

    }
}

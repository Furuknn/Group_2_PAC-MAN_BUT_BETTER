using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyBehaviour : GhostBehaviour
{
    private float blinkySpeed;
    private const float rageDuration = 3.0f;
    [SerializeField] private GameObject pacman;

    private void Start()
    {
        ghostscr.blinkyChasescr.Enable();
        blinkySpeed = pacman.GetComponent<Movement>().speed / 2;
        ghostscr.movementscr.speed = blinkySpeed;
    }
    public void Update()
    {
        if (ghostscr.blinkyChasescr.enabled && !ghostscr.spawnscr.enabled && !ghostscr.vulnerablescr.enabled)
        {
            InvokeRepeating(nameof(BlinkyRageSetup), 17f,17f);
        }
        if (ghostscr.vulnerablescr.enabled)
        {
            CancelInvoke();
            blinkySpeed = pacman.GetComponent<Movement>().speed / 2;
            ghostscr.movementscr.speed = blinkySpeed;
        }
    }

    private void BlinkyRageSetup()
    {
        ghostscr.movementscr.speed = 0f;

        AudioManager.Instance.PlaySFX("Rage");
        AudioManager.Instance.PlaySFX("RageMusic");

        CancelInvoke();

        Invoke(nameof(BlinkyRageOn), 2f);
    }

    private void BlinkyRageOn()
    {
        blinkySpeed = pacman.GetComponent<Movement>().speed * 2;
        ghostscr.movementscr.speed = blinkySpeed;

        CancelInvoke();

        Invoke(nameof(BlinkyRageOff), rageDuration);
    }

    private void BlinkyRageOff()
    {
        blinkySpeed = pacman.GetComponent<Movement>().speed / 2;
        ghostscr.movementscr.speed = blinkySpeed;

        

        CancelInvoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeBehaviour : GhostBehaviour
{
    public Transform pacman;
    public float proximityDistance = 5f;
    private float clydeSpeed;

    private void Start()
    {
        ghostscr.clydeChasescr.Disable();

        clydeSpeed = pacman.GetComponent<Movement>().speed * 0.8f;
        ghostscr.movementscr.speed = clydeSpeed;
    }
    private void FixedUpdate()
    {
        Invoke(nameof(CheckPacManProximity),0.1f);
    }

    private void CheckPacManProximity() {

        float distanceToPacMan = Vector3.Distance(transform.position, pacman.position);

        if (distanceToPacMan < proximityDistance)
        {
            ghostscr.clydeScatterscr.Disable();
        }
        else
        {
            ghostscr.clydeChasescr.Disable();
        }
    }
}

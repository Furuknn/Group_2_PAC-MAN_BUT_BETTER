using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeScatter : GhostBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !ghostscr.vulnerablescr.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections[index] == -ghostscr.movementscr.direction && node.availableDirections.Count > 1)
            {
                index++;

                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }
            ghostscr.movementscr.SetDirection(node.availableDirections[index]);
        }
    }

    private void OnDisable()
    {
        ghostscr.clydeChasescr.Enable();
    }
}

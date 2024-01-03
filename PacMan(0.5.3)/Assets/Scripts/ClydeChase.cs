using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeChase : GhostBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !ghostscr.vulnerablescr.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (ghostscr.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }

            }
            ghostscr.movementscr.SetDirection(direction);
        }

    }
    private void OnDisable()
    {
        ghostscr.clydeScatterscr.Enable();
    }
}

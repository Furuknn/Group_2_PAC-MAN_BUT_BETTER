using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyChase : GhostBehaviour
{
    private float inkySpeed;
    public Transform pacman;
    public float targetingOffset=2f;

    private void Start()
    {
        inkySpeed = pacman.GetComponent<Movement>().speed;
        ghostscr.movementscr.speed = inkySpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !ghostscr.vulnerablescr.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;
            Vector3 targetPosition = CalculateTargetPosition();

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (targetPosition - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }

            }
            ghostscr.movementscr.SetDirection(direction);
        }

    }

    private Vector3 CalculateTargetPosition()
    {
        if (pacman != null)
        {
            // Pacman'in baktýðý yönden belirli bir mesafe ilerisinin vektörünü alýr.
            Vector3 pacManDirection = pacman.forward;
            Vector3 targetPosition = pacman.position + pacManDirection * targetingOffset;
            targetPosition.z=ghostscr.target.transform.position.z;

            return targetPosition;
        }

        // Pacman mapte yoksa þimdiki pozisyonu return et
        return transform.position;
    }
}

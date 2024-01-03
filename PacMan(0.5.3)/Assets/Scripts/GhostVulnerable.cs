using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostVulnerable : GhostBehaviour
{
    public SpriteRenderer body, eyes, blueMode, whiteMode;
    public bool eaten {  get; private set; }


    public override void TimedEnable(float duration)
    {
        base.TimedEnable(duration);

        body.enabled = false;
        eyes.enabled = false;
        blueMode.enabled = true;
        whiteMode.enabled = false;

        Invoke(nameof(Flash), duration / 2.0f);
    }
    

    public override void Disable()
    {
        base.Disable();

        body.enabled = true;
        eyes.enabled = true;
        blueMode.enabled = false;
        whiteMode.enabled = false;
    }

    private void Flash()
    {
        if (!eaten)
        {
            blueMode.enabled = false;
            whiteMode.enabled = true;
            whiteMode.GetComponent<AnimatedSprite>().Restart();
        }
        
    }

    private void Eaten()
    {
        eaten= true;

        Vector3 position = this.ghostscr.spawnscr.inside.position;
        position.z = ghostscr.transform.position.z;
        ghostscr.transform.position = position;

        ghostscr.spawnscr.TimedEnable(this.duration);

        body.enabled = false;
        eyes.enabled = true;
        blueMode.enabled = false;
        whiteMode.enabled = false;
    }

    private void OnEnable()
    {
        ghostscr.movementscr.speedMultiplier = 0.5f;
        eaten = false;
    }

    private void OnDisable()
    {
        ghostscr.movementscr.speedMultiplier = 1f;
        eaten = false;

        ghostscr.blinkyChasescr.Enable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (enabled)
            {
                Eaten();
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (ghostscr.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }

            }
            ghostscr.movementscr.SetDirection(direction);
        }

    }
}

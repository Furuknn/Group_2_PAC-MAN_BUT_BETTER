using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class GhostSpawn : GhostBehaviour
{
    public Transform inside,outside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }
    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")){
            ghostscr.movementscr.SetDirection(-ghostscr.movementscr.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        ghostscr.movementscr.SetDirection(Vector2.up, true);
        ghostscr.movementscr.rigidbody.isKinematic = true;
        ghostscr.movementscr.enabled = false;

        Vector3 position =transform.position;

        float duration = 0.5f, elapsed = 0.0f;

        while (elapsed<duration)
        {
            Vector3 newPosition =Vector3.Lerp(position,inside.position, elapsed/duration);
            newPosition.z=position.z;
            ghostscr.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(inside.position, outside.position, elapsed / duration);
            newPosition.z = position.z;
            ghostscr.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        ghostscr.movementscr.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        ghostscr.movementscr.rigidbody.isKinematic = false;
        ghostscr.movementscr.enabled = true;
    }
}

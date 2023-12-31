using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public SpriteRenderer spriteRenderer {  get; private set; }
    public Sprite upEye, downEye, leftEye, rightEye;
    public Movement movementscr {  get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementscr=GetComponentInParent<Movement>();
    }

    private void Update()
    {
        if (movementscr.direction==Vector2.up)
        {
            spriteRenderer.sprite = upEye;
        }
        if (movementscr.direction == Vector2.down)
        {
            spriteRenderer.sprite = downEye;
        }
        if (movementscr.direction == Vector2.left)
        {
            spriteRenderer.sprite = leftEye;
        }
        if (movementscr.direction == Vector2.right)
        {
            spriteRenderer.sprite = rightEye;
        }
    }
}

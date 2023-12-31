using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> availableDirections {  get; private set; }
    public LayerMask obstacleLayer;

    private void Start()
    {
        availableDirections = new List<Vector2> ();

        //oyun ba�lad���nda t�m node'lar�n hangi yon�n�n bo� oldu�una bakar.
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, 1.0f, obstacleLayer);

        if (hit.collider==null)
        {
            this.availableDirections.Add(direction);
        }
    }
}

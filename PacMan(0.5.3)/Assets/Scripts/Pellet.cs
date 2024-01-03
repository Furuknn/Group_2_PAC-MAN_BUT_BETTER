using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pellet : MonoBehaviour
{

    
    protected virtual void Eat()
    {
        GameManager.Instance.PelletEaten(this);
        GameManager.Instance.pelletCount -= 1;
        Debug.Log("pellet count: " + GameManager.Instance.pelletCount);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman")) 
        {
            Eat();
        }
    }

}

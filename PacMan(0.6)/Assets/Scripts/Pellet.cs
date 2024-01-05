using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pellet : MonoBehaviour
{

    
    protected virtual void Eat()
    {
        GameManager.Instance.PelletEaten(this);
        GameManager.Instance.pelletCount -= 1;
        if (!AudioManager.Instance.sfxSource.isPlaying)
        {
            AudioManager.Instance.PlaySFX(PlayerPrefs.GetString("pacmanVersion") + "PelletEat");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman")) 
        {
            Eat();
        }
    }

}

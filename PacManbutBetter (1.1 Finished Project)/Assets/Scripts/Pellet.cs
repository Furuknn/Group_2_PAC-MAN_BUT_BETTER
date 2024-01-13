using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pellet : MonoBehaviour
{
    [SerializeField] private GameObject classicPellet, betterPellet;

    private void Start()
    {
        if (PlayerPrefs.GetString("pacmanVersion") == "classic")
        {
            classicPellet.SetActive(true);
            betterPellet.SetActive(false);
        }
        else
        {
            classicPellet.SetActive(false);
            betterPellet.SetActive(true);
        }
    }
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

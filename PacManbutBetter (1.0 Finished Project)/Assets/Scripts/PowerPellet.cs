using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration = 8.0f;

    [SerializeField] private GameObject classicPowerPellet, betterPowerPellet;
    protected override void Eat()
    {
        GameManager.Instance.PowerPelletEaten(this);
        GameManager.Instance.pelletCount -= 1;

        AudioManager.Instance.PlaySFX(PlayerPrefs.GetString("pacmanVersion") + "PowerUp");
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("pacmanVersion") == "classic")
        {
            classicPowerPellet.SetActive(true);
            betterPowerPellet.SetActive(false);
        }
        else
        {
            classicPowerPellet.SetActive(false);
            betterPowerPellet.SetActive(true);
        }
    }

}

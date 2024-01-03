using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration = 8.0f;
    protected override void Eat()
    {
        GameManager.Instance.PowerPelletEaten(this);
        GameManager.Instance.pelletCount -= 1;
    }

}

using UnityEngine;

public class PowerPellet : Pellet
{

    protected override void Eat()
    {
        GameManager.Instance.PowerPelletEaten(this);
    }

}

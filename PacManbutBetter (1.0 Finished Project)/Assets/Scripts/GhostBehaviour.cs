using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ghost))] //objede Ghost component'� olmas� �art

public abstract class GhostBehaviour : MonoBehaviour //"abstract" kodu, Scriptin bir gameobject'e component olarak eklenmesini engeller.
{

    public Ghost ghostscr { get; private set; }
    public float duration;

    private void Awake()
    {
        ghostscr = GetComponent<Ghost>();
        this.enabled = false;
    }

    public void Enable()
    {
        this.enabled = true;
    }

    public void TimedEnable()
    {
        TimedEnable(duration);
    }
    public virtual void TimedEnable(float duration) //"Virtual" bu metodun ba�ka classlarda nas�l davrand���n� de�i�tirmeye yarayacak.
    {
        this.enabled=true;

        CancelInvoke();
        Invoke(nameof(Disable),duration);
    }

    public virtual void Disable()
    {
        this.enabled = false; 
        
        CancelInvoke();
    }
}

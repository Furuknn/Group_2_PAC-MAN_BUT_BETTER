using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : MonoBehaviour
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
        Enable(duration);
    }
    public virtual void Enable(float duration) //"Virtual" bu metodun baþka classlarda nasýl davrandýðýný deðiþtirmeye yarayacak.
    {
        this.enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        this.enabled = false;

        CancelInvoke();
    }
}

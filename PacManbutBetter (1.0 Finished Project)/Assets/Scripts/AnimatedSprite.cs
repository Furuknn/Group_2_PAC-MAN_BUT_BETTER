using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[0];
    public float animationTime = 0.25f;
    public bool loop = true;

    private SpriteRenderer spriteRenderer;
    private int animationFrame;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);
    }

    private void Advance()
    {
        if (!this.spriteRenderer.enabled) {
            return;
        }

        this.animationFrame++;

        if (this.animationFrame >= this.sprites.Length && this.loop) {
            this.animationFrame = 0;
        }

        if (animationFrame >= 0 && this.animationFrame < this.sprites.Length) {
            this.spriteRenderer.sprite = this.sprites[animationFrame];
        }
    }

    public void Restart()
    {
        this.animationFrame = -1;

        Advance();
    }

}

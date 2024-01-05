using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    [SerializeField]
    private AnimatedSprite deathSequence;
    private SpriteRenderer spriteRenderer;
    private Movement movementscr;
    private new Collider2D collider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementscr = GetComponent<Movement>();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // Geçerli giriþe göre yeni yönü ayarlayýn
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            movementscr.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            movementscr.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            movementscr.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            movementscr.SetDirection(Vector2.right);
        }

        // Pacman'i hareket yönüne bakacak þekilde döndürün
        float angle = Mathf.Atan2(movementscr.direction.y, movementscr.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        collider.enabled = true;
        movementscr.enabled = true;

        movementscr.ResetState();
        gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
        AudioManager.Instance.PlaySFX(PlayerPrefs.GetString("pacmanVersion") + "Death");

        enabled = false;
        spriteRenderer.enabled = false;
        collider.enabled = false;
        movementscr.enabled = false;
        deathSequence.enabled = true;

        deathSequence.Restart();
    }

}

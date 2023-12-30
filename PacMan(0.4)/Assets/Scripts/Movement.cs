using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //Gameobject'de bu component'�n mutlaka olmas� gerekti�ini bildirir. E�er bu component zaten yoksa bu kodla otomatik olarak olu�turulur.
public class Movement : MonoBehaviour
{
    public float speed = 8f;
    public float speedMultiplier = 1f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        speedMultiplier = 1f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        rigidbody.isKinematic = false;
        enabled = true;
    }

    private void Update()
    {
        // Hareket yapmak i�in s�raya al�nm��ken bir sonraki y�ne do�ru hareket etmeye �al���n
        if (nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        // Y�n� yaln�zca o y�ndeki d��eme mevcutsa ayarlay�n
        // aksi takdirde onu bir sonraki y�n olarak ayarlayaca��z, b�ylece otomatik olarak olacak
        // ne zaman kullan�labilir olaca��n� ayarla
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }else{
              nextDirection = direction;
             }
    }

    public bool Occupied(Vector2 direction)
    {
        // E�er herhangi bir �arp��t�r�c�ya �arp�lmazsa o y�nde hi�bir engel yoktur
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }

}

using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int points = 100;

    public Movement movementscr {  get; private set; }
    public GhostSpawn spawnscr { get; private set; }
    public GhostVulnerable vulnerablescr { get; private set; }
    public BlinkyBehaviour blinkyBehaviourscr { get; private set; }
    public BlinkyChase blinkyChasescr { get; private set; }
    public PinkyBehaviour pinkyBehaviourscr { get; private set; }
    public PinkyChase pinkyChasescr { get; private set; }
    public ClydeBehaviour clydeBehaviourscr { get; private set; }
    public ClydeChase clydeChasescr { get; private set; }
    public ClydeScatter clydeScatterscr { get; private set; }
    public InkyChase inkyChasescr { get; private set; }

    public GhostBehaviour initialBehaviour;

    public Transform target;


    public void Awake()
    {
        movementscr = GetComponent<Movement>();
        spawnscr= GetComponent<GhostSpawn>();
        vulnerablescr = GetComponent<GhostVulnerable>();
        blinkyBehaviourscr=FindObjectOfType<BlinkyBehaviour>();
        blinkyChasescr=FindObjectOfType<BlinkyChase>();
        pinkyBehaviourscr = FindObjectOfType<PinkyBehaviour>();
        pinkyChasescr = FindObjectOfType<PinkyChase>();
        clydeBehaviourscr = FindObjectOfType<ClydeBehaviour>();
        clydeChasescr = FindObjectOfType<ClydeChase>();
        clydeScatterscr = FindObjectOfType<ClydeScatter>();
        inkyChasescr = FindObjectOfType<InkyChase>();
    }

    private void Start()
    {
        blinkyChasescr.Enable();
        blinkyBehaviourscr.Enable();
        pinkyChasescr.Enable();
        pinkyBehaviourscr.Enable();
        clydeBehaviourscr.Enable();
        inkyChasescr.Enable();
        ResetState();
    }

    public void ResetState()
    {
        movementscr.ResetState();
        gameObject.SetActive(true);

        vulnerablescr.Disable();
        
        

        if (spawnscr!=initialBehaviour)
        {
            spawnscr.Disable();
        }
        if (initialBehaviour!=null)
        {
            initialBehaviour.TimedEnable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer==LayerMask.NameToLayer("Pacman"))
        {
            if (vulnerablescr.enabled) //Ghost'lar maviyse ghost yendi, deðilse pacman yendi metodunu çaðýrýr.
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}

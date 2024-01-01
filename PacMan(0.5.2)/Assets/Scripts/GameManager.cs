using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Ghost[] ghosts;
    [SerializeField] private Pacman pacmanscr;
    [SerializeField] private Transform pellets;
    [SerializeField] private GameObject twoLife,oneLife;
    //[SerializeField] private Text gameOverText;
    [SerializeField] private TextMeshProUGUI scoreText,highScoreText;
    //[SerializeField] private Text livesText;

    private int ghostMultiplier = 1;
    private int lives = 3;
    private int score = 0;

    public int Lives => lives;
    public int Score => score;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }

        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        NewGame();
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    private void Update()
    {
        scoreText.text=score.ToString();

        if (PlayerPrefs.GetInt("HighScore")<score)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }

        if (lives <= 0 && Input.anyKeyDown) {
            NewGame();
        }
        if (lives == 3 && !twoLife.activeSelf && !oneLife.activeSelf) 
        {
            twoLife.SetActive(true);
            oneLife.SetActive(true);
        }
        if (lives == 2 && twoLife.activeSelf)
        {
            twoLife.SetActive(false);
            oneLife.SetActive(true);
        }
        if (lives == 1)
        {
            twoLife.SetActive(false);
            oneLife.SetActive(false);
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {

        foreach (Transform pellet in pellets) 
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < ghosts.Length; i++) {
            this.ghosts[i].ResetState();
        }

        pacmanscr.ResetState();
    }

    private void GameOver()
    {

        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].gameObject.SetActive(false);
        }

        pacmanscr.gameObject.SetActive(false); //Oyun sona erdiðinde bu pacman scriptini deaktive ederek Pacman'in hareket etmesini engeller.
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    public void PacmanEaten()
    {
        pacmanscr.DeathSequence();

        SetLives(lives - 1);

        if (lives > 0) {
            Invoke(nameof(ResetState), 3f);
        } else {
            GameOver();
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostMultiplier;
        SetScore(score + points);

        ghostMultiplier++;
    }

    public void PelletEaten(Pellet pellet) //yenilen noktalarý yok eder. Hepsi yendiðinde yeni rounda geçer.
    {
        pellet.gameObject.SetActive(false);

        if (!HasRemainingPellets())
        {
            pacmanscr.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].vulnerablescr.TimedEnable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier),pellet.duration);
    }

    private bool HasRemainingPellets() //haritada nokta kaldý mý kalmadý mý kontrol eder.
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf) {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }

}

using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public int pelletCount = 0;

    public static GameManager Instance { get; private set; }

    [SerializeField] private Ghost[] ghosts;
    [SerializeField] private Pacman pacmanscr;
    [SerializeField] private Transform pellets;
    [SerializeField] private GameObject twoLife,oneLife;
    [SerializeField] private TextMeshProUGUI scoreText,highScoreText, tipText;
    [SerializeField] private GameObject gameOverPanel;

    private int ghostMultiplier = 1;
    private int lives = 3;
    private int score = 0;
    private const string pacmanVersion = "pacmanVersion";
    public string[] tips;

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
        gameOverPanel.SetActive(false);
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
        pelletCount = 0;

        SetScore(0);
        SetLives(3);
        NewRound();

        gameOverPanel.SetActive(false);

        AudioManager.Instance.PlaySFX(PlayerPrefs.GetString(pacmanVersion) + "GameStart");
        Debug.Log(PlayerPrefs.GetString(pacmanVersion));
    }

    private void NewRound()
    {
        
        foreach (Transform pellet in pellets) 
        {
            pellet.gameObject.SetActive(true);
            pelletCount++;
        }

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < ghosts.Length; i++) {
            this.ghosts[i].ResetState();
        }

        if (lives < 3) 
        {
            AudioManager.Instance.sfxSource.Stop();
            AudioManager.Instance.PlaySFX(PlayerPrefs.GetString("pacmanVersion") + "HereWeGoAgain");
        }

        pacmanscr.ResetState();
    }

    private void GameOver()
    {

        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].gameObject.SetActive(false);
        }

        gameOverPanel.SetActive(true);
        AudioManager.Instance.PlaySFX(PlayerPrefs.GetString("pacmanVersion") + "GameOver");

        if (score==0)
        {
            tipText.text = "0 score? Lol you have to eat the ghosts to earn points. It's ok just play again";
        }
        else
        {
            int rng = Random.Range(0,tips.Length);
            tipText.text = tips[rng];
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
        AudioManager.Instance.PlaySFX(PlayerPrefs.GetString("pacmanVersion") + "GhostEat");

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

            AudioManager.Instance.PlaySFX(PlayerPrefs.GetString("pacmanVersion") + "StageCleared");
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

    public void MainMenuButtonClick()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

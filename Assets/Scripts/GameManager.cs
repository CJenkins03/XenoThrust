
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [Header("Game")]
    public bool gameActive;
    public int score;
    bool canStart;
    public int highScore;
    bool gotHighScore;

    public List<ColourChanger> backgroundList;


    [Header("UI")]
    public TextMeshProUGUI text;
    public GameObject startText;
    public GameOverSign gameOverSign;
    public GameObject newHighScore;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;
    public AudioClip explosion;
    public AudioClip scoreSound;


    public AlienMovement alien;
    public bool clearPrefs;

    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        canStart = true;
        audioSource.clip = mainMenuMusic;
        if (clearPrefs) PlayerPrefs.DeleteAll();
        highScore = PlayerPrefs.GetInt("HighScore");

    }

    private void Update()
    {
        if (!canStart) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        startText.SetActive(false);
        alien.SetGravity();
        PipeManager.Instance.StartGame();
        gameActive = true;
        audioSource.clip = gameMusic;
        audioSource.Play();
        canStart = false;
    }

    public void AddScore()
    {
        AudioManager.Instance.PlayScoreSound();
        score ++;
        AdjustBackground();
        text.text = score.ToString("0");
        if (highScore == 0) return;
        if (score == (highScore + 1))
        {
            gotHighScore = true;
            newHighScore.transform.position = Vector3.zero;
            newHighScore.SetActive(true);
        }
    }

    public void EndGame()
    {
        Save();
        PipeManager.Instance.StopPipes();
        gameActive = false;
        gameOverSign.gameObject.SetActive(true);
        gameOverSign.SetUp(score, PlayerPrefs.GetInt("HighScore"), gotHighScore);
        audioSource.Stop();
        audioSource.PlayOneShot(explosion);
    }

    public void Retry()
    {
        SceneManager.LoadScene("Game");
        
    }

    public void AdjustBackground()
    {
        switch (score)
        {
            case 100:
                ChangeBackgroundColour();
                break;
            case 200:
                ChangeBackgroundColour();
                break;
            case 300:
                ChangeBackgroundColour();
                break;
            case 400:
                ChangeBackgroundColour();
                break;
            case 1000:
                ChangeBackgroundColour();
                break;
            default:
                break;
        }
    }

    private void ChangeBackgroundColour()
    {
        foreach (ColourChanger background in backgroundList)
        {
            background.ChangeColour();
        }
    }

    public void Save()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            Debug.Log("No save");
        }
    }


}

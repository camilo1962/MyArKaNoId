using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;
    public static GameManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion

    public bool isPlayingNow { get; set; }
    public int currentLevel;

    public int availableLives = 4;
    private int score = 0;
    public int lives { get; set; }

    [SerializeField]
    private TMP_Text livesCountTMP;
    [SerializeField]
    private TMP_Text scoreTMP;
    [SerializeField]
    private TMP_Text currentLevelTMP;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private TMP_Text highScoreTMP;

    private void Start()
    {
        this.lives = availableLives;
        livesCountTMP.text = "VIDAS: " + Environment.NewLine + lives;
        currentLevelTMP.text = currentLevel.ToString();
        Ball.onBallDestroy += OnBallDestroy;
        Brick.onBrickDestroy += OnBrickDestroy;
    }

    private void OnBrickDestroy(Brick brick)
    {
        score += 10;
        scoreTMP.text = "PUNTOS: " + Environment.NewLine + score.ToString().PadLeft(6, '0');

        if (BricksManager.Instance.remainingBricks.Count <= 0)
        {
            isPlayingNow = false;
            currentLevel++;
            if (currentLevel >= BricksManager.Instance.levelsData.Count)
                ShowPanelUI(winScreen);
            else
            {
                BricksManager.Instance.LoadLevel();
                AudioManager.Instance.PlayRandomMusic();
                currentLevelTMP.text = currentLevel.ToString();
            }
            BuffsManager.Instance.ClearSpawnedBuffs();
            BallsManager.Instance.ResetBalls();
        }
    }
    private void OnBallDestroy(Ball ball)
    {
        if (BallsManager.Instance.balls.Count <= 0)
        {
            lives--;
            livesCountTMP.text = "VIDAS: " + Environment.NewLine + lives;
            if (lives <= 0)
            {
                ShowPanelUI(gameOverScreen);
            }
            else
            {
                BuffsManager.Instance.ClearSpawnedBuffs();
                BallsManager.Instance.ResetBalls();
                isPlayingNow = false;
                BricksManager.Instance.LoadLevel();
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ShowPanelUI(GameObject panel)
    {
        panel.SetActive(true);
        UpdateHighScore(score);
        panel.transform.Find("ScoreText").GetComponent<TMP_Text>().text = "TUS PUNTOS: " + Environment.NewLine + score.ToString().PadLeft(6, '0');
        panel.transform.Find("HighScoreText").GetComponent<TMP_Text>().text = "RECORD: " + Environment.NewLine + PlayerPrefs.GetInt("HighScore").ToString().PadLeft(6, '0');
    }

    private void UpdateHighScore(int score)
    {
        int oldHighScore = PlayerPrefs.GetInt("HighScore");
        if (score > oldHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    private void OnDisable()
    {
        Ball.onBallDestroy -= OnBallDestroy;
        Brick.onBrickDestroy -= OnBrickDestroy;
    }
}

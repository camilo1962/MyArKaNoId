using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text highScoreTMP;
    [SerializeField]
    private GameObject menuUI, loadingUI;

    private int highScore;

    void Start()
    {
          highScore = PlayerPrefs.GetInt("HighScore");
        highScoreTMP.text = "RECORD: " + Environment.NewLine + highScore;  
    }

    public void PlayGame()
    {
        menuUI.SetActive(false);
        loadingUI.SetActive(true);
        SceneManager.LoadScene("Game");
    }
    public void ExitApp()
    {
        Application.Quit();
    }
}

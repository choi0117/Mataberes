using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    public static GameManager Instance
    {
        get { return gameManager; }
    }

    private int currentScore = 0;
    UIManager uiManager;

    public UIManager UIManager
    {
        get { return uiManager; }
    }
    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        uiManager.UpdateScore(0);
        StartCoroutine(StartGameAfterDelay(2f));
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        uiManager.SetRestart();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        uiManager.UpdateScore(currentScore);
        Debug.Log("Score: " + currentScore);
    }

    private IEnumerator StartGameAfterDelay(float delay)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
    }

}
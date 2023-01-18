using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    #region Variables
    
    // Public Variables
    
    // Private Variables
    [SerializeField] private GameObject timer, score;
    [SerializeField] private GameObject loseScreen;
    
    #endregion Variables
    
    private void Awake()
    {
        timer.SetActive(true);
        score.SetActive(true);
        loseScreen.SetActive(false);
    }

    private void Start()
    {
        GameManager.LoseGame += OnLoseGame;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnLoseGame()
    {
        timer.SetActive(false);
        score.SetActive(false);
        loseScreen.SetActive(true);
    }
}

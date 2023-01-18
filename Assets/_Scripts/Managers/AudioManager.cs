using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Variables
    
    // Public Variables
    
    // Private Variables
    [Header("Audio Groups")]
    [SerializeField] private AudioGroup collectedCollectable;
    [SerializeField] private AudioGroup jump;
    [SerializeField] private AudioGroup landing;
    [SerializeField] private AudioGroup newWaveStarted;
    
    [Header("Audio Clips")]
    [SerializeField] private AudioClip sliding;
    [SerializeField] private AudioClip gameOver;
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource timerLastSeconds;
    
    #endregion Variables
    
    private void Start()
    {
        PlayerMovement.JumpEvent += () => AudioReactor.PlaySfx(jump);
        PlayerMovement.LandEvent += () => AudioReactor.PlaySfx(landing);
        CollectableGenerator.OnCollectableCollected += () => AudioReactor.PlaySfx(collectedCollectable);
        CollectableGenerator.OnNewWaveStarted += () => AudioReactor.PlaySfx(newWaveStarted);
        TimerController.OnLastSeconds += StartTimerLastSeconds;
        TimerController.OnTimeDelaySetted += StopTimerLastSeconds;
        GameManager.LoseGame += StopTimerLastSeconds;
        GameManager.LoseGame += () => AudioReactor.PlaySfx(gameOver);
    }

    private void StartTimerLastSeconds()
    {
        timerLastSeconds.Play();
    }

    private void StopTimerLastSeconds()
    {
        timerLastSeconds.Stop();
    }
}

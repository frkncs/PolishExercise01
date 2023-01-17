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
    
    [Header("Audio Clips")]
    [SerializeField] private AudioClip sliding;
    
    #endregion Variables
    
    private void Start()
    {
        PlayerMovement.JumpEvent += () => AudioReactor.PlaySfx(jump);
        PlayerMovement.LandEvent += () => AudioReactor.PlaySfx(landing);
        CollectableGenerator.OnCollectableCollected += () => AudioReactor.PlaySfx(collectedCollectable);
    }

    private void Update()
    {
    }
}

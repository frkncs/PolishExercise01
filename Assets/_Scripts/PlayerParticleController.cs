using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleController : MonoBehaviour
{
    private enum RunParticleType
    {
        None,
        RightRun,
        LeftRun,
    }

    #region Variables

    // Public Variables

    // Private Variables
    [SerializeField] private GameObject rightRunParticle, leftRunParticle, jumpParticle, landParticle;

    private RunParticleType _currentPlayingRunParticle;

    #endregion Variables

    private void Awake()
    {
        SignUpEvents();
    }

    private void SignUpEvents()
    {
        PlayerMovement.PlayMoveRightEvent += PlayRightRunParticle;
        PlayerMovement.PlayMoveLeftEvent += PlayLeftRunParticle;
        PlayerMovement.PlayJumpEvent += PlayJumpParticle;
        PlayerMovement.PlayLandEvent += PlayLandParticle;
        PlayerMovement.PlayMoveIdleEvent += StopAllParticles;
    }

    private void StopAllParticles()
    {
        rightRunParticle.SetActive(false);
        leftRunParticle.SetActive(false);
        jumpParticle.SetActive(false);
        landParticle.SetActive(false);

        _currentPlayingRunParticle = RunParticleType.None;
    }

    private void PlayRightRunParticle()
    {
        if (_currentPlayingRunParticle == RunParticleType.RightRun)
        {
            return;
        }

        _currentPlayingRunParticle = RunParticleType.RightRun;

        leftRunParticle.SetActive(false);
        rightRunParticle.SetActive(true);
    }

    private void PlayLeftRunParticle()
    {
        if (_currentPlayingRunParticle == RunParticleType.LeftRun)
        {
            return;
        }

        _currentPlayingRunParticle = RunParticleType.LeftRun;

        rightRunParticle.SetActive(false);
        leftRunParticle.SetActive(true);
    }

    private void PlayJumpParticle()
    {
        _currentPlayingRunParticle = RunParticleType.None;
        jumpParticle.SetActive(true);
    }

    private void PlayLandParticle()
    {
        _currentPlayingRunParticle = RunParticleType.None;
        jumpParticle.SetActive(true);
    }
}
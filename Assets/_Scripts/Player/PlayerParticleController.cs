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
    [SerializeField] private ParticleSystem rightRunParticle, leftRunParticle, jumpParticle, landParticle;

    private RunParticleType _currentPlayingRunParticle;

    #endregion Variables

    private void Awake()
    {
        SignUpEvents();
    }

    private void SignUpEvents()
    {
        PlayerMovement.MoveRightEvent += PlayRightRunParticle;
        PlayerMovement.MoveLeftEvent += PlayLeftRunParticle;
        PlayerMovement.JumpEvent += PlayJumpParticle;
        PlayerMovement.LandEvent += PlayLandParticle;
        PlayerMovement.IdleEvent += StopAllParticles;
        GameManager.LoseGame += StopAllParticles;
    }

    private void StopAllParticles()
    {
        rightRunParticle.Stop();
        leftRunParticle.Stop();
        jumpParticle.Stop();
        landParticle.Stop();

        _currentPlayingRunParticle = RunParticleType.None;
    }

    private void PlayRightRunParticle()
    {
        if (_currentPlayingRunParticle == RunParticleType.RightRun)
        {
            return;
        }

        _currentPlayingRunParticle = RunParticleType.RightRun;

        leftRunParticle.Stop();
        rightRunParticle.Play();
    }

    private void PlayLeftRunParticle()
    {
        if (_currentPlayingRunParticle == RunParticleType.LeftRun)
        {
            return;
        }

        _currentPlayingRunParticle = RunParticleType.LeftRun;

        rightRunParticle.Stop();
        leftRunParticle.Play();
    }

    private void PlayJumpParticle()
    {
        _currentPlayingRunParticle = RunParticleType.None;
        jumpParticle.Emit(30);
    }

    private void PlayLandParticle()
    {
        _currentPlayingRunParticle = RunParticleType.None;
        landParticle.Emit(30);
    }
}
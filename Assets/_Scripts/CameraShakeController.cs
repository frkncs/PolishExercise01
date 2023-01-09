using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    #region Variables
    
    // Public Variables
    
    // Private Variables
    
    #endregion Variables
    
    private void Start()
    {
        PlayerMovement.PlayLandEvent += Shake;
    }

    private void Shake()
    {
        var cameraShaker = CameraShaker.Instance;
        cameraShaker.ShakeOnce(.5f, 7f, 0, 1, new Vector3(0, 1, 0), new Vector3(1, 1, 1));
    }
}

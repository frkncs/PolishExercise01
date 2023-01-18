using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    #region Variables
    
    // Public Variables
    
    // Private Variables
    [SerializeField] private ParticleSystem collectedParticle;
    
    #endregion Variables

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collectedParticle.transform.SetParent(null);
            collectedParticle.Play();
            
            CollectableGenerator.OnCollectableCollected?.Invoke();
            
            Destroy(gameObject);
        }
    }
}

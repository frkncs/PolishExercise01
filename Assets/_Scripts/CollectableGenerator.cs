using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectableGenerator : MonoBehaviour
{
    #region Variables
    
    // Public Variables
    public static Action OnCollectableCollected;
    
    // Private Variables
    [SerializeField] private Vector2 horizontalBoundary, verticalBoundary;
    [SerializeField] private CollectableController collectableController;

    private Vector3 _targetSpawnPos;

    private int _sameTimeCollectableCount;
    private int _generatedCollectableCount;
    private int _collectedCollectableCount;
    private int _sameTimeCollectableCountIncreaseValue;

    #endregion Variables
    
    private void Awake()
    {
        _sameTimeCollectableCount = 1;
        _sameTimeCollectableCountIncreaseValue = 1;
        
        GenerateCollectable();

        OnCollectableCollected += () =>
        {
            _collectedCollectableCount++;

            if (_collectedCollectableCount < _sameTimeCollectableCount)
            {
                return;
            }
            
            for (int i = 0; i < _sameTimeCollectableCount; i++)
            {
                GenerateCollectable();
            }

            _collectedCollectableCount = 0;
        };
    }

    private void OnDestroy()
    {
        OnCollectableCollected = null;
    }

    private async void GenerateCollectable()
    {
        await FindCorrectPosition();
        
        Instantiate(collectableController, _targetSpawnPos, Quaternion.identity);

        _generatedCollectableCount++;

        if (_generatedCollectableCount >= _sameTimeCollectableCountIncreaseValue * 7)
        {
            _sameTimeCollectableCount++;
            _sameTimeCollectableCountIncreaseValue++;
            _generatedCollectableCount = 0;
        }
    }

    private Task FindCorrectPosition()
    {
        _targetSpawnPos = Vector3.zero;
        _targetSpawnPos.x = Random.Range(horizontalBoundary.x, horizontalBoundary.y);
        _targetSpawnPos.y = Random.Range(verticalBoundary.x, verticalBoundary.y);
        
        Collider[] nearByObjects = new Collider[1];
        Physics.OverlapSphereNonAlloc(_targetSpawnPos, .5f, nearByObjects);
        
        while (nearByObjects[0] != null)
        {
            _targetSpawnPos.x = Random.Range(horizontalBoundary.x, horizontalBoundary.y);
            _targetSpawnPos.y = Random.Range(verticalBoundary.x, verticalBoundary.y);
            
            nearByObjects = new Collider[1];
            Physics.OverlapSphereNonAlloc(_targetSpawnPos, .5f, nearByObjects);

            Task.Yield();
        }
        
        return Task.CompletedTask;
    }
}

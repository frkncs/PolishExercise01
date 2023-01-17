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
    public static Action OnCollectableCollected, OnNewWaveStarted;
    public static int IncreaseWaveCollectableTime => st_increaseWaveCollectableTime;
    
    // Private Variables
    [SerializeField] private Vector2 horizontalBoundary, verticalBoundary;
    [SerializeField] private CollectableController collectableController;
    [Tooltip("It increasing collectable count per wave after reached this amount of wave"), SerializeField] private int increaseWaveCollectableTime = 7;

    private Vector3 _targetSpawnPos;

    private int _sameTimeCollectableCount;
    private int _generatedCollectableCount;
    private int _collectedCollectableCount;
    private int _sameTimeCollectableCountIncreaseValue;
    
    private static int st_increaseWaveCollectableTime;

    #endregion Variables
    
    private void Awake()
    {
        _sameTimeCollectableCount = 1;
        _sameTimeCollectableCountIncreaseValue = 1;

        st_increaseWaveCollectableTime = increaseWaveCollectableTime;
        
        GenerateCollectable();
        
        OnCollectableCollected += () =>
        {
            _collectedCollectableCount++;

            if (_collectedCollectableCount < _sameTimeCollectableCount)
            {
                return;
            }
            
            OnNewWaveStarted?.Invoke();
            
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
        OnNewWaveStarted = null;
    }

    private async void GenerateCollectable()
    {
        await FindCorrectPosition();
        
        Instantiate(collectableController, _targetSpawnPos, Quaternion.identity);

        _generatedCollectableCount++;

        if (_generatedCollectableCount >= _sameTimeCollectableCountIncreaseValue * increaseWaveCollectableTime + 1)
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

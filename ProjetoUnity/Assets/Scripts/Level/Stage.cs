using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private float timeInterval;
    [SerializeField] private Obstacle obstaclePrefab;
    [SerializeField] private Transform spawnPoint;


    private bool IsRunning => isRunning;
    private bool isRunning;
    private GenericPool<Obstacle> obstaclePool;
    private float lastInstantiated;
    
    private void Awake()
    {
        obstaclePool = new GenericPool<Obstacle>(obstaclePrefab);
    }

    private void Update()
    {
        Run();
    }

    private void Run()
    {
        if (Time.time < lastInstantiated + timeInterval)
            return;

        var obstacle = obstaclePool.Get();
        obstacle.transform.position = spawnPoint.position;

        obstacle.onLeftScreen.AddListener(OnObstacleLeftScreen);

        lastInstantiated = Time.time;
    }


    private void OnObstacleLeftScreen(Obstacle obstacle)
    {
        obstaclePool.Release(obstacle);
    }
}

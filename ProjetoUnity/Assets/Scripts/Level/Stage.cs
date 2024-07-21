using AYellowpaper;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour, IStage, IStartable
{
    [SerializeField] private float timeInterval;
    [RequireInterface(typeof(IStartable))]
    [SerializeField] private MonoBehaviour backgroundObject;
    [RequireInterface(typeof(IStartable))]
    [SerializeField] private MonoBehaviour floorObject;
    [SerializeField] private Obstacle obstaclePrefab;
    [SerializeField] private Transform spawnPoint;

    private bool isRunning;
    private GenericPool<Obstacle> obstaclePool;
    private List<Obstacle> runningObstacles;
    private float lastInstantiated;
    private IStartable background;
    private IStartable floor;

    private void Awake()
    {
        obstaclePool = new GenericPool<Obstacle>(obstaclePrefab);
        runningObstacles = new List<Obstacle>();

        background = (IStartable)backgroundObject;
        floor = (IStartable)floorObject;
    }
    private void Update()
    {
        CheckInstantiation();
    }
    private void CheckInstantiation()
    {
        if (!isRunning)
            return;

        if (Time.time < lastInstantiated + timeInterval)
            return;

        var obstacle = obstaclePool.Get();
        obstacle.Setup(spawnPoint.position);

        runningObstacles.Add(obstacle);
        obstacle.onLeftScreen.AddListener(OnObstacleLeftScreen);

        lastInstantiated = Time.time;
    }
    private void OnObstacleLeftScreen(Obstacle obstacle)
    {
        obstaclePool.Release(obstacle);
        runningObstacles.Remove(obstacle);
    }
    public void Setup()
    {
    }
    public void Run()
    {
        isRunning = true;
        background.Run();
        floor.Run();

        foreach (var obstacle in runningObstacles)
            obstacle.Run();
    }
    public void Stop()
    {
        isRunning = false;
        background.Stop();
        floor.Stop();

        foreach (var obstacle in runningObstacles)
            obstacle.Stop();
    }
    public void Destroy()
    {
        
    }
}

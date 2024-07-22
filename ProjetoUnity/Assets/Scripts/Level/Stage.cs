using AYellowpaper;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour, IStage, IStartable
{
    [RequireInterface(typeof(ITexturable))]
    [SerializeField] public MonoBehaviour backgroundObject;
    [RequireInterface(typeof(ITexturable))]
    [SerializeField] public MonoBehaviour floorObject;
    [SerializeField] private Transform spawnPoint;

    private StageData stageData;
    private bool isRunning;
    private GenericPool<Obstacle> obstaclePool;
    private List<Obstacle> runningObstacles;
    private float lastInstantiated;
    private ITexturable background;
    private ITexturable floor;

    private void Awake()
    {
        runningObstacles = new List<Obstacle>();

        background = (ITexturable)backgroundObject;
        floor = (ITexturable)floorObject;
    }
    private void Update()
    {
        CheckInstantiation();
    }
    private void CheckInstantiation()
    {
        if (!isRunning)
            return;

        if (Time.time < lastInstantiated + stageData.spawnTimeInterval)
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
    public void Setup(StageData stageData)
    {
        this.stageData = stageData;

        background.SetTexture(stageData.backgroundTexture);
        floor.SetTexture(stageData.floorTexture);

        obstaclePool = new GenericPool<Obstacle>(stageData.obstaclePrefab);
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
        obstaclePool.Reset();
    }

    public StageData GetStageData()
    {
        return stageData;
    }
}

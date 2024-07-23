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
    private Obstacle lastInstantiatedObject;
    private ITexturable background;
    private ITexturable floor;
    private float lastPauseTime;
    private float totalPausedTime;
    private Vector3 distanceCalculation;

    private void Awake()
    {
        runningObstacles = new List<Obstacle>();

        background = (ITexturable)backgroundObject;
        floor = (ITexturable)floorObject;
    }
    private void FixedUpdate()
    {
        CheckInstantiation();
    }
    int number = 0;
    private void CheckInstantiation()
    {
        if (!isRunning)
            return;

        var time = GetTime();

        if (lastInstantiatedObject != null)
        {
            var normalizedObstablePosition = new Vector3(lastInstantiatedObject.transform.position.x, spawnPoint.transform.position.y, lastInstantiatedObject.transform.position.z);

            var distance = Vector3.Distance(normalizedObstablePosition, spawnPoint.transform.position);

            if (distance < stageData.spawnDistance)
            {
                Debug.Log("Obstacle " + lastInstantiatedObject.name + " is on position " + lastInstantiatedObject.transform.position + ", " + distance + " units away from spawn " + spawnPoint.transform.position + ". The required is " + stageData.spawnDistance);
                return;
            }
        }

        var obstacle = obstaclePool.Get();
        obstacle.Setup(spawnPoint.position);

        runningObstacles.Add(obstacle);
        obstacle.onLeftScreen.AddListener(OnObstacleLeftScreen);
        obstacle.name = number.ToString();

        Debug.Log("Creating obstable " + obstacle.name + " at position " + obstacle.transform.position);

        lastInstantiatedObject = obstacle;

        number++;
    }
    private float GetTime()
    {
        return Time.time;
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

    public void TogglePause(bool pause)
    {
        var time = GetTime();
        if (pause)
            lastPauseTime = time;
        else
            totalPausedTime = time - lastPauseTime;

        isRunning = !pause;

        foreach(var obstacle in runningObstacles)
        {
            obstacle.TogglePause(pause);
        }

        background.TogglePause(pause);
        floor.TogglePause(pause);
    }
}

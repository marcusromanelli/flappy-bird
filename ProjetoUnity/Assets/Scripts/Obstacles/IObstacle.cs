using UnityEngine;
using UnityEngine.Events;

public interface IObstacle : IPoolable
{
    public void Setup(Vector3 startPosition);
}

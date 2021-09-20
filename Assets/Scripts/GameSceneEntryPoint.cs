using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    private void Start()
    {
        _zombieSpawner.Enable();

        _eventsPool = new EventsPool();
        _eventsPool.Init();
    }

    [SerializeField] ZombieSpawner _zombieSpawner;
    [SerializeField] EventsPool _eventsPool;
    [SerializeField] GameSceneUI _gameSceneUI;
}
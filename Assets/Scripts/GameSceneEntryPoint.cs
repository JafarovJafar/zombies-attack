using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    private void Start()
    {
        _playerTouchInput.Init(_player);

        _zombieSpawner.Enable();

        _eventsPool = new EventsPool();
        _eventsPool.Init();
    }

    [SerializeField] PlayerTouchInput _playerTouchInput;
    [SerializeField] PlayerController _player;
    [SerializeField] ZombieSpawner _zombieSpawner;
    [SerializeField] EventsPool _eventsPool;
    [SerializeField] GameSceneUI _gameSceneUI;
}
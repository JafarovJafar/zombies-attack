using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    private void Start()
    {
        _playerTouchInput.Init(_player);

        _zombieSpawner.Enable();
        _eventsPool.Init();


    }

    [SerializeField] PlayerTouchInput _playerTouchInput;
    [SerializeField] PlayerController _player;
    [SerializeField] ZombieSpawner _zombieSpawner;
    [SerializeField] EventsPool _eventsPool;
}
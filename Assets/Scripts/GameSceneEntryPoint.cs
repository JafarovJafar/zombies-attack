using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    private void Start()
    {
        _playerTouchInput.Init(_player);

        _zombieSpawner.Enable();
    }

    [SerializeField] PlayerTouchInput _playerTouchInput;
    [SerializeField] PlayerController _player;
    [SerializeField] ZombieSpawner _zombieSpawner;
}
using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    private void Start()
    {
        _playerTouchInput.Init(_player);
    }

    [SerializeField] PlayerTouchInput _playerTouchInput;
    [SerializeField] Player _player;
}
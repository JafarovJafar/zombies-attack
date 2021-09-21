using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneEntryPoint : MonoBehaviour
{
    private void Start()
    {
        _zombieSpawner.Enable();

        _eventsPool = new EventsPool();
        _eventsPool.Init();

        _eventsPool.PlayerDead += ProcessPlayerDeath;
        _eventsPool.ZombieDead += ProcessZombieDeath;

        _gameSceneUI.Init();
        _gameSceneUI.ExitButtonClicked += GoToMainMenu;

        AdaptSpawnPoints();
    }

    [SerializeField] ZombieSpawner _zombieSpawner;
    [SerializeField] EventsPool _eventsPool;
    [SerializeField] GameSceneUI _gameSceneUI;

    private float _score;

    [SerializeField] private Camera _camera;

    [SerializeField] private PlayerController _player;

    private void ProcessPlayerDeath()
    {
        PlayerPrefs.SetFloat(CONSTANTS.SCORE_PREF_NAME, _score);

        _gameSceneUI.SetScore(_score);
        _gameSceneUI.ShowGameOver();
    }

    private void ProcessZombieDeath(ZombieModel zombieModel)
    {
        _score += zombieModel.Cost;
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(CONSTANTS.MAIN_MENU_SCENE_NAME);
    }

    private void AdaptSpawnPoints()
    {
        float goalAspectRatio = 2048f / 1536f;
        float currentAspectRatio = Screen.width / (float)Screen.height;

        float goalScreenHalfInUnits = _camera.orthographicSize * goalAspectRatio;
        float currentScreenHalfInUnits = _camera.orthographicSize * currentAspectRatio;

        float increaser = Mathf.Abs(_zombieSpawner.SpawnPoints[0].position.x) - goalScreenHalfInUnits;

        Vector3 goalPosition;
        Vector3 goalRotation = new Vector3();
        float sign;

        foreach (Transform point in _zombieSpawner.SpawnPoints)
        {
            goalPosition = point.position;
            sign = Mathf.Sign(goalPosition.x);
            goalPosition.x = (currentScreenHalfInUnits + increaser) * sign;
            point.position = goalPosition;

            goalRotation.z = Vector2.SignedAngle(point.up, _player.transform.position - point.position);
            point.eulerAngles += goalRotation;
        }
    }
}
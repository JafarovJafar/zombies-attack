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
    }

    [SerializeField] ZombieSpawner _zombieSpawner;
    [SerializeField] EventsPool _eventsPool;
    [SerializeField] GameSceneUI _gameSceneUI;

    private float _score;

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
}
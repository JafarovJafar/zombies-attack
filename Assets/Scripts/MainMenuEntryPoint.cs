using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEntryPoint : MonoBehaviour
{
    private void Start()
    {
        _mainMenuUI.Init();

        _mainMenuUI.StartButtonClicked += LoadGameScene;

        _mainMenuUI.SetScore(PlayerPrefs.GetFloat(CONSTANTS.SCORE_PREF_NAME, 0));
    }

    [SerializeField] MainMenuUI _mainMenuUI;

    private void LoadGameScene()
    {
        _mainMenuUI.Disable();
        SceneManager.LoadScene(CONSTANTS.GAME_SCENE_NAME);
    }
}
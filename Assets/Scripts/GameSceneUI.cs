using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _gameOverCanvasGroup;

    private CanvasGroup _lastCanvasGroup;

    [SerializeField] private Button _exitButton;
    [SerializeField] private Text _scoreText;

    public UnityAction ExitButtonClicked;

    public void Init()
    {
        _exitButton.onClick.AddListener(() => ExitButtonClicked?.Invoke());
    }

    public void ShowGameOver()
    {
        EnableCanvasGroup(_gameOverCanvasGroup);
    }

    private void EnableCanvasGroup(CanvasGroup canvasGroup)
    {
        if (_lastCanvasGroup != null)
        {
            DisableCanvasGroup(_lastCanvasGroup);
        }

        canvasGroup.interactable = true;
        canvasGroup.alpha = 1f;

        _lastCanvasGroup = canvasGroup;
    }

    private void DisableCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0f;
    }

    public void SetScore(float score)
    {
        _scoreText.text = $"Score: {score}";
    }
}
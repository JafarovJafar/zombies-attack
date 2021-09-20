using UnityEngine;

public class GameSceneUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _gameOverCanvasGroup;

    private CanvasGroup _lastCanvasGroup;

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
}
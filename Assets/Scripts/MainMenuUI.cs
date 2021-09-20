using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button _startGameButton;
    [SerializeField] Text _scoreText;

    public UnityAction StartButtonClicked;

    public void Init()
    {
        _startGameButton.onClick.AddListener(() => StartButtonClicked?.Invoke());
    }

    public void Enable()
    {
        ToggleUI(true);
    }

    public void Disable()
    {
        ToggleUI(false);
    }

    private void ToggleUI(bool enabled)
    {
        _startGameButton.interactable = enabled;
    }

    public void SetScore(float score)
    {
        _scoreText.text = $"Score: {score}";
    }
}
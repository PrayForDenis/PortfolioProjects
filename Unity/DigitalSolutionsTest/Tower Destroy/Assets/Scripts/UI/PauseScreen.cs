using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour, IScreen
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _resumeButton;

    public event UnityAction RestartButtonClicked;
    public event UnityAction ResumeButtonClicked;

    public void Open()
    {
        gameObject.SetActive(true);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _resumeButton.onClick.AddListener(OnResumeButtonClick);
    }

    public void Close()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        gameObject.SetActive(false);
    }

    private void OnRestartButtonClick()
    {
        RestartButtonClicked?.Invoke();
    }

    private void OnResumeButtonClick()
    {
        ResumeButtonClicked?.Invoke();
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartScreen : Screen
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _recordsButton;
    [SerializeField] private Button _titlesButton;
    [SerializeField] private Button _quitButton;

    public event UnityAction PlayButtonClick;
    public event UnityAction RecordButtonClick;
    public event UnityAction TitleButtonClick;
    public event UnityAction QuitButtonClick;

    private void OnPlayButtonClick()
    {
        PlayButtonClick?.Invoke();
    }

    private void OnRecordButtonClick()
    {
        RecordButtonClick?.Invoke();
    }

    private void OnTitleButtonClick()
    {
        TitleButtonClick?.Invoke();
    }

    private void OnExitButtonClick()
    {
        QuitButtonClick?.Invoke();
    }

    protected override void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnPlayButtonClick);
        _recordsButton.onClick.RemoveListener(OnRecordButtonClick);
        _titlesButton.onClick.RemoveListener(OnTitleButtonClick);
        _quitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    protected override void OnEnable()
    {
        _startButton.onClick.AddListener(OnPlayButtonClick);
        _recordsButton.onClick.AddListener(OnRecordButtonClick);
        _titlesButton.onClick.AddListener(OnTitleButtonClick);
        _quitButton.onClick.AddListener(OnExitButtonClick);
    }
}

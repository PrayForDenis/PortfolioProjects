using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverScreen : Screen
{
    [SerializeField] protected Button _backButton;
    [SerializeField] protected Button _quitButton;

    public event UnityAction BackButtonClick;
    public event UnityAction QuitButtonClick;

    private void OnBackButtonClick()
    {
        BackButtonClick?.Invoke();
    }

    private void OnExitButtonCLick()
    {
        QuitButtonClick?.Invoke();
    }

    protected override void OnDisable()
    {
        _backButton.onClick.RemoveListener(OnBackButtonClick);
        _quitButton.onClick.RemoveListener(OnExitButtonCLick);
    }

    protected override void OnEnable()
    {
        _backButton.onClick.AddListener(OnBackButtonClick);
        _quitButton.onClick.AddListener(OnExitButtonCLick);
    }
}

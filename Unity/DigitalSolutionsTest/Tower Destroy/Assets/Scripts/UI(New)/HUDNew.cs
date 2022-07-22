using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class HUDNew : MonoBehaviour
{
    private Button _pauseButton;
    private Button _boostButton;
    private VisualElement _enemyHealth;
    private VisualElement _playerHealth;

    public event UnityAction PauseButtonClicked;
    public event UnityAction BoostButtonClicked;

    private void Awake() 
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _pauseButton = root.Q<Button>("PauseButton");
        _boostButton = root.Q<Button>("BoostButton");
        _enemyHealth = root.Q<VisualElement>("EnemyHealthBar").Q<VisualElement>();
        _playerHealth = root.Q<VisualElement>("PlayerHealthBar").Q<VisualElement>();
    }

    private void Start() 
    {
        RegisterEvents();    
    }

    private void OnDisable() 
    {
        _pauseButton.clicked -= OnPauseButtonClicked;    
        _boostButton.clicked -= OnBoostButtonClicked;
    }

    private void RegisterEvents()
    {
        _pauseButton.clicked += OnPauseButtonClicked;
        _boostButton.clicked += OnBoostButtonClicked;
    }

    private void OnPauseButtonClicked()
    {
        PauseButtonClicked?.Invoke();
    }

    private void OnBoostButtonClicked()
    {
        BoostButtonClicked?.Invoke();
    }
}

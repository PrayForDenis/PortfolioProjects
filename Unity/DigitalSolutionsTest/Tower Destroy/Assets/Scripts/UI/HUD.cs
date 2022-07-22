using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;
using TMPro;

public class HUD : MonoBehaviour, IScreen
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _boostButton;
    [SerializeField] private Slider _enemyHealth;
    [SerializeField] private Slider _playerHealth;
    [SerializeField] private TMP_Text _timer;
    
    private const float FillDuration = 0.5f;
    private EnemyTower _enemyTower;
    private PlayerTower _playerTower;

    public event UnityAction PauseButtonClicked;
    public event UnityAction BoostButtonClicked;

    [Inject]
    private void Construct(EnemyTower enemyTower, PlayerTower playerTower)
    {
        _enemyTower = enemyTower;
        _playerTower = playerTower;
    }

    public void Open()
    {
        gameObject.SetActive(true);
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
        _boostButton.onClick.AddListener(OnBoostButtonClick);
        _enemyTower.HealthChanged += OnEnemyHealthChanged;
        _playerTower.HealthChanged += OnPlayerHealthChanged;
        _playerTower.TimerFinished += OnTimerFinished;
        _playerTower.TimeUpdated += OnTimeUpdated;
    }

    public void Close()
    {
        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
        _boostButton.onClick.RemoveListener(OnBoostButtonClick); 
        _enemyTower.HealthChanged -= OnEnemyHealthChanged;
        _playerTower.HealthChanged -= OnPlayerHealthChanged;  
        _playerTower.TimerFinished -= OnTimerFinished;
        _playerTower.TimeUpdated -= OnTimeUpdated;             
        gameObject.SetActive(false);       
    }

    private void OnPlayerHealthChanged(int health, int maxHealth)
    {
        _playerHealth.DOValue((float) health / maxHealth, FillDuration);
    }

    private void OnEnemyHealthChanged(int health, int maxHealth)
    {
        _enemyHealth.DOValue((float) health / maxHealth, FillDuration);
    }

    private void OnPauseButtonClick()
    {
        PauseButtonClicked?.Invoke();
    }

    private void OnBoostButtonClick()
    {
        BoostButtonClicked?.Invoke();
        _boostButton.image.sprite = Resources.Load<Sprite>(AssetPath.NonActiveBoostButton);
        _boostButton.interactable = false;
    }

    private void OnTimerFinished()
    {
        _boostButton.image.sprite = Resources.Load<Sprite>(AssetPath.ActiveBoostButton);
        _boostButton.interactable = true;
    }

    private void OnTimeUpdated(float seconds)
    {
        _timer.text = string.Format("0 : {0:00}", seconds);
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class GameOverScreen : MonoBehaviour, IScreen
{
    private const string VictoryMessage = "Вы победили!";
    private const string LoseMessage = "Вы проиграли!";

    [SerializeField] private Button _restartButton;
    [SerializeField] private TMP_Text _gameOverResult;

    private EnemyTower _enemyTower;
    private PlayerTower _playerTower;

    public event UnityAction RestartButtonClicked;

    [Inject]
    private void Construct(EnemyTower enemyTower, PlayerTower playerTower) 
    {
        _enemyTower = enemyTower;
        _playerTower = playerTower;
    }

    private void Awake() 
    {
        _enemyTower.Died += OnEnemyDied;
        _playerTower.Died += OnPlayerDied;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _enemyTower.Died -= OnEnemyDied;
        _playerTower.Died -= OnPlayerDied;   
    }

    public void Open()
    {
        gameObject.SetActive(true);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    public void Close()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        gameObject.SetActive(false);
    }

    private void OnEnemyDied()
    {
        _gameOverResult.text = VictoryMessage;
    }

    private void OnPlayerDied()
    {
        _gameOverResult.text = LoseMessage;
    }

    private void OnRestartButtonClick()
    {
        RestartButtonClicked?.Invoke();
    }
}

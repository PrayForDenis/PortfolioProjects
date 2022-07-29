public class GameState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IInputService _input;
    private readonly Game _game;
    private readonly PlayerTower _playerTower; 
    private readonly EnemyTower _enemyTower;
    private readonly HUD _hud;

    public GameState(GameStateMachine stateMachine, 
                    IInputService input, Game game,
                    PlayerTower playerTower, EnemyTower enemyTower, HUD hud) 
    {
        _gameStateMachine = stateMachine;
        _input = input;
        _game = game;
        _playerTower = playerTower;
        _enemyTower = enemyTower;
        _hud = hud;
    }

    public void Enter()
    {
        _input.Enable();
        _hud.Open();

        RegisterEvents();
    }

    public void Exit()
    {
        _hud.PauseButtonClicked -= OnPauseButtonClicked;
        _hud.BoostButtonClicked -= OnBoostButtonClicked;
        _playerTower.Died -= OnPlayerDied;
        _enemyTower.Died -= OnEnemyDied;
    }

    private void RegisterEvents()
    {
        _hud.PauseButtonClicked += OnPauseButtonClicked;
        _hud.BoostButtonClicked += OnBoostButtonClicked;
        _playerTower.Died += OnPlayerDied;
        _enemyTower.Died += OnEnemyDied;
    }

    private void OnPauseButtonClicked()
    {
        _gameStateMachine.Enter<PauseState>();
    }

    private void OnBoostButtonClicked()
    {
        _playerTower.ActiveBoost();
    }

    private void OnPlayerDied()
    {
        _gameStateMachine.Enter<GameOverState>();
    }

    private void OnEnemyDied()
    {
        _gameStateMachine.Enter<GameOverState>();
    }
}

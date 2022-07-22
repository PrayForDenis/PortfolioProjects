using UnityEngine;

public class GameOverState : IState
{
    private GameStateMachine _gameStateMachine;
    private IInputService _input;
    private Game _game;
    private GameOverScreen _gameOverScreen;
    private HUD _hud;

    public GameOverState(GameStateMachine stateMachine, IInputService input, 
                        Game game, GameOverScreen gameOverScreen, HUD hud)
    {
        _gameStateMachine = stateMachine;
        _input = input;
        _game = game;
        _gameOverScreen = gameOverScreen;
        _hud = hud;
    }

    public void Enter()
    {
        _input.Disable();
        Time.timeScale = 0;
        _gameOverScreen.Open();
        _gameOverScreen.RestartButtonClicked += OnRestartButtonClicked;
    }

    public void Exit()
    {
        _gameOverScreen.RestartButtonClicked -= OnRestartButtonClicked;
        _gameOverScreen.Close();
    }

    private void OnRestartButtonClicked()
    {
        _game.Reset();
        _hud.Close();
        Time.timeScale = 1;
        _gameStateMachine.Enter<GameState>();
    }
}

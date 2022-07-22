using UnityEngine;

public class PauseState : IState
{
    private GameStateMachine _gameStateMachine;
    private IInputService _input;
    private Game _game;
    private PauseScreen _pauseScreen;
    private HUD _hud;

    public PauseState(GameStateMachine stateMachine, IInputService input, 
                    Game game, PauseScreen pauseScreen, HUD hud)
    {
        _gameStateMachine = stateMachine;
        _input = input;
        _game = game;
        _pauseScreen = pauseScreen;
        _hud = hud;
    }

    public void Enter()
    {
        _input.Disable();
        Time.timeScale = 0;
        _pauseScreen.Open();
        RegisterEvents();
    }

    public void Exit()
    {
        _pauseScreen.ResumeButtonClicked -= OnResumeButtonClicked;
        _pauseScreen.RestartButtonClicked -= OnRestartButtonClicked;
        _pauseScreen.Close();
    }

    private void RegisterEvents() 
    {
        _pauseScreen.ResumeButtonClicked += OnResumeButtonClicked;
        _pauseScreen.RestartButtonClicked += OnRestartButtonClicked;        
    }

    private void OnResumeButtonClicked()
    {
        _hud.Close();
        Time.timeScale = 1;
        _gameStateMachine.Enter<GameState>();
    }

    private void OnRestartButtonClicked()
    {
        _game.Reset();
        _hud.Close();
        Time.timeScale = 1;
        _gameStateMachine.Enter<GameState>();
    }
}

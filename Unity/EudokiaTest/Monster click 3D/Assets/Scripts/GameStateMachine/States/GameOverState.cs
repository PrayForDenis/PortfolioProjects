using UnityEngine;

public class GameOverState : IState
{
    private IAssetProvider _assetProvider;
    private GameOverScreen _gameOverScreen;
    private GameStateMachine _gameStateMachine;

    public GameOverState(GameStateMachine gameStateMachine, IAssetProvider assetProvider) 
    {
        _gameStateMachine = gameStateMachine;
        _assetProvider = assetProvider;
    }

    public void Enter()
    {
        _gameOverScreen = _assetProvider.Instantiate(AssetPath.GameOverScreenPath).GetComponent<GameOverScreen>();
        RegisterEvents();
    }

    public void Exit()
    {
        _gameOverScreen.BackButtonClick -= OnBackButtonCLick;
        _gameOverScreen.QuitButtonClick -= OnExitButtonCLick;

        _gameOverScreen.Close();
    }

    private void RegisterEvents()
    {
        _gameOverScreen.BackButtonClick += OnBackButtonCLick;
        _gameOverScreen.QuitButtonClick += OnExitButtonCLick;
    }

    private void OnBackButtonCLick()
    {
        _gameStateMachine.Enter<MainMenuState>();
    }

    private void OnExitButtonCLick()
    {
        Application.Quit();
    }
}

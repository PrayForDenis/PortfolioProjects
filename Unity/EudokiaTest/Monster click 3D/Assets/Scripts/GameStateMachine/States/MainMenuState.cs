using UnityEngine;

public class MainMenuState : IState
{
    private IAssetProvider _assetProvider;
    private StartScreen _startScreen;
    private GameStateMachine _gameStateMachine;

    public MainMenuState(GameStateMachine gameStateMachine, IAssetProvider assetProvider) 
    {
        _gameStateMachine = gameStateMachine;
        _assetProvider = assetProvider;
    }

    public void Enter()
    {
        _startScreen = _assetProvider.Instantiate(AssetPath.StartScreenPath).GetComponent<StartScreen>();
        RegisterEvents();
    }

    public void Exit()
    {
        _startScreen.PlayButtonClick -= OnPlayButtonCLick;
        _startScreen.RecordButtonClick -= OnRecordButtonCLick;
        _startScreen.TitleButtonClick -= OnTitleButtonCLick;
        _startScreen.QuitButtonClick -= OnExitButtonCLick;

        _startScreen.Close();
    }

    private void RegisterEvents()
    {
        _startScreen.PlayButtonClick += OnPlayButtonCLick;
        _startScreen.RecordButtonClick += OnRecordButtonCLick;
        _startScreen.TitleButtonClick += OnTitleButtonCLick;
        _startScreen.QuitButtonClick += OnExitButtonCLick;
    }

    private void OnPlayButtonCLick()
    {
        _gameStateMachine.Enter<GameState>();
    }

    private void OnRecordButtonCLick()
    {
        Debug.Log("Record");
    }

    private void OnTitleButtonCLick()
    {
        Debug.Log("Title");
    }

    private void OnExitButtonCLick()
    {
        Application.Quit();
    }
}

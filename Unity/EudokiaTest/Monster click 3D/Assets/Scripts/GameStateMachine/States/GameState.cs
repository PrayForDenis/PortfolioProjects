public class GameState : IState
{
    private IAssetProvider _assetProvider;
    private GameStateMachine _gameStateMachine;
    private EnemyGenerator _enemyGenerator;

    private Platform _platform;
    private InputService _input;
    private Hud _hud;

    public GameState(GameStateMachine gameStateMachine, IAssetProvider assetProvider) 
    {
        _gameStateMachine = gameStateMachine;
        _assetProvider = assetProvider;
    }

    public void Enter()
    {
        _platform = _assetProvider.Instantiate(AssetPath.PlatformPath).GetComponent<Platform>();
        _enemyGenerator = _assetProvider.Instantiate(AssetPath.ObjectPoolPath).GetComponent<EnemyGenerator>();
        _input = _assetProvider.Instantiate(AssetPath.InputServicePath).GetComponent<InputService>();
        _hud = _assetProvider.Instantiate(AssetPath.HudPath).GetComponent<Hud>();

        RegisterEvents();
    }

    public void Exit()
    {
        _enemyGenerator.MaxActiveEnemiesSpawned -= OnMaxActiveEnemiesSpawned;
        DestroyObjects();
    }

    private void RegisterEvents()
    {
        _enemyGenerator.MaxActiveEnemiesSpawned += OnMaxActiveEnemiesSpawned;
    }

    private void OnMaxActiveEnemiesSpawned()
    {
        _gameStateMachine.Enter<GameOverState>();
    }

    private void DestroyObjects()
    {
        _platform.gameObject.SetActive(false);
        _enemyGenerator.gameObject.SetActive(false);
        _input.gameObject.SetActive(false);
        _hud.gameObject.SetActive(false);
    }
}

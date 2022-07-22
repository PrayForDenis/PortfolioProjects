using System;
using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoInstaller
{
    [SerializeField] private PlayerTower _playerTower;
    [SerializeField] private EnemyTower _enemyTower;
    [SerializeField] private HUD _hud;
    [SerializeField] private PauseScreen _pauseScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
   
    private Game _game;
    private IInputService _inputService;

    public override void InstallBindings()
    {
        _inputService = new InputService();
        _game = new Game(_inputService, _playerTower, _enemyTower, _hud, _pauseScreen, _gameOverScreen);  

        BindInstanceSingle<IInputService>(_inputService);
        BindInstanceSingle<PlayerTower>(_playerTower);
        BindInstanceSingle<EnemyTower>(_enemyTower);
        BindInstanceSingle<HUD>(_hud);
        BindInstanceSingle<PauseScreen>(_pauseScreen);
        BindInstanceSingle<GameOverScreen>(_gameOverScreen);
    }

    private void Awake() 
    { 
        _game.StateMachine.Enter<GameState>();
    }

    private void OnDestroy() 
    {
        (_inputService as IDisposable).Dispose();
    }

    private T BindInstanceSingle<T>(T instance)
    {
        Container
            .BindInstance<T>(instance)
            .AsSingle()
            .NonLazy();

        return instance;
    }
}

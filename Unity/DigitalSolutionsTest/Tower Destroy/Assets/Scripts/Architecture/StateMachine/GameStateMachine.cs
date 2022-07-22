using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private Dictionary<Type, IState> _states;
    private IState _currentState;

    public GameStateMachine(IInputService input, Game game, PlayerTower playerTower, 
                            EnemyTower enemyTower, HUD hud, PauseScreen pauseScreen,
                            GameOverScreen gameOverScreen)
    {
        _states = new Dictionary<Type, IState>
        {
            [typeof(GameState)] = new GameState(this, input, game, playerTower, enemyTower, hud),
            [typeof(PauseState)] = new PauseState(this, input, game, pauseScreen, hud),
            [typeof(GameOverState)] = new GameOverState(this, input, game, gameOverScreen, hud)
        };
    }

    public void Enter<TState>() where TState : class, IState 
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }

    private TState ChangeState<TState>() where TState : class, IState
    {
        _currentState?.Exit();

        TState state = GetState<TState>();
        _currentState = state;

        return state;
    }

    private TState GetState<TState>() where TState : class, IState
    {
        return _states[typeof(TState)] as TState;
    }
}

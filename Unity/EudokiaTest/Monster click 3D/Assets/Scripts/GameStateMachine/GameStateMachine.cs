using System;
using System.Collections.Generic;

public class GameStateMachine 
{
    private Dictionary<Type, IState> _states;

    private IState _currentState;

    public GameStateMachine()
    {
        _states = new Dictionary<Type, IState>
        {
            [typeof(MainMenuState)] = new MainMenuState(this, new AssetProvider()),
            [typeof(GameState)] = new GameState(this, new AssetProvider()),
            [typeof(GameOverState)] = new GameOverState(this, new AssetProvider())
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

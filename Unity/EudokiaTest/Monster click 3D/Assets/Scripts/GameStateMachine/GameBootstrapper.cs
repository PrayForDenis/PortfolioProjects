using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    private Game _game;

    private void Awake() 
    {
        _game = new Game();
        _game.StateMachine.Enter<MainMenuState>();

        DontDestroyOnLoad(this);
    }
}

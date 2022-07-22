public class Game
{
    private EnemyTower _enemyTower;
    private PlayerTower _playerTower;

    public GameStateMachine StateMachine;

    public Game(IInputService input, PlayerTower playerTower,
                EnemyTower enemyTower, HUD hud, PauseScreen pauseScreen, 
                GameOverScreen gameOverScreen)
    {
        StateMachine = new GameStateMachine(input, this, playerTower, enemyTower, hud, pauseScreen, gameOverScreen);
        _enemyTower = enemyTower;
        _playerTower = playerTower;
    }

    public void Reset()
    {
        _enemyTower.Reset();
        _playerTower.Reset();
    }
}

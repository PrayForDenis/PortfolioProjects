public class MonsterScore : Score
{
    private EnemyGenerator _enemyGenerator;

    private void Awake() 
    {
        _enemyGenerator = FindObjectOfType<EnemyGenerator>();
    }

    protected override void OnEnable()
    {
        _enemyGenerator.EnemyCountChanged += OnScoreChanged;
    }

    protected override void OnDisable()
    {
        _enemyGenerator.EnemyCountChanged -= OnScoreChanged;
    }
}

public class TapScore : Score
{
    private InputService _inputService;

    private void Awake() 
    {
        _inputService = FindObjectOfType<InputService>();
    }

    protected override void OnEnable()
    {
        _inputService.ScoreChanged += OnScoreChanged;
    }
    
    protected override void OnDisable()
    {
        _inputService.ScoreChanged -= OnScoreChanged;
    }
}

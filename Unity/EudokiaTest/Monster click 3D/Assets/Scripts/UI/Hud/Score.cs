using TMPro;
using UnityEngine;

public abstract class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;

    protected abstract void OnEnable();

    protected abstract void OnDisable();

    protected void OnScoreChanged(int score)
    {
        _score.text = score.ToString();
    }
}

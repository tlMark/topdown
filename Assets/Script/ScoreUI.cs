using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private TMP_Text _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();

        ScoreController scoreController = FindFirstObjectByType<ScoreController>();

        if (scoreController != null)
        {
            UpdateScore(scoreController); 
        }
        else
        {
            _scoreText.text = "Error: Score Not Found"; 
        }
    }

    public void UpdateScore(ScoreController scoreController)
    {
        _scoreText.text = $"Score : {scoreController.Score}";
    }
}

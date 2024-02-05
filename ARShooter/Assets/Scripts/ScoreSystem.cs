using TMPro;
using UnityEngine;

//This class is responsible for the score system and displaying the score on the screen
public class ScoreSystem : MonoBehaviour
{
    private int score = 0;
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        if(scoreText == null)
        {
            Debug.LogError("Score Text is null");
        }
    }
    private void OnEnable()
    {
        Enemy.OnEnemyDestroyed += HandleEnemyDestroyed;
    }
    private void OnDisable()
    {
        Enemy.OnEnemyDestroyed -= HandleEnemyDestroyed;
    }

    void HandleEnemyDestroyed(int score)
    {
        //Add the score to the current score
        this.score += score;

        //Display the score on the screen
        scoreText.text = "Score: " + this.score;
    }
}

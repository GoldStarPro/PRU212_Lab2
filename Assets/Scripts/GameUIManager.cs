using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;

    void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
        UpdateScoreDisplay();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGameBGMusic();
        }
    }
    public void ShowGameOver()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopBGMusic();
        }
        Time.timeScale = 0f;
        gameOverCanvas.SetActive(true);

        gameOverScoreText.text = "Score: " + GameManager.Instance.Score.ToString();
    }

    public void BackToMenu()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopBGMusic();
        }
        Time.timeScale = 1f;
        GameManager.Instance.ResetScore();
        SceneManager.LoadScene("Menu");
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.Score.ToString();
        }
    }
}

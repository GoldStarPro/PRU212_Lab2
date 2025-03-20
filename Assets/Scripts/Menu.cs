using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMenuBGMusic();
        }
    }

    public void PlayGame()
    {
        // Reset score khi bắt đầu một lần chơi mới
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetScore();
        }
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}

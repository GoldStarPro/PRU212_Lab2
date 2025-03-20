using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] private GameUIManager gameUIManager;
    [SerializeField] float timeDelays = 2f;
    [SerializeField] ParticleSystem particleFlag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            particleFlag.Play();
            Debug.Log("Nuh uh");
            Invoke("LoadScene", timeDelays);
        }
    }
    
    void LoadScene()
    {
        gameUIManager.ShowGameOver();
    }
}

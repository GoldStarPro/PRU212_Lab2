using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int score = 0;

    private const int POINTS_PER_SECOND = 10;
    private float tempCooldown;
    private float timeCooldown = 1f;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if(tempCooldown <= 0)
        {
            AddScore(10);
            tempCooldown = timeCooldown;
        }
        tempCooldown -= Time.deltaTime;
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public void ResetScore()
    {
        score = 0;
        tempCooldown = timeCooldown;
    }
}
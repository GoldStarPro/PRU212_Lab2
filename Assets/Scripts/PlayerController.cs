using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torque = 1;
    [SerializeField] float timeDelays = 2f;
    [SerializeField] ParticleSystem particleHit;

    [SerializeField] ParticleSystem trickParticle;
    [SerializeField] TextMeshProUGUI trickText;
    [SerializeField] int trickPoints = 100;
    Rigidbody2D rb;

    SurfaceEffector2D se;
    [SerializeField] float boostedSpeed = 30f;
    [SerializeField] float normalSpeed = 15f;

    [SerializeField] ParticleSystem Dust;
    [SerializeField] ParticleSystem Snowflake;

    bool check = true;
    bool isGrounded = true;
    private Animator animator;

    private float groundExitDelay = 0.3f;
    private float timeSinceLeftGround = 0f;
    private bool isCheckingGroundExit = false;

    private float previousRotation;
    private float totalRotation;
    private int flipCount;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        se = FindFirstObjectByType<SurfaceEffector2D>();
        previousRotation = rb.rotation;
        totalRotation = 0f;
        flipCount = 0;

        // Hide trick text initially
        if (trickText != null) trickText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (check)
        {
            Rotate();
            Boost();
            UpdateAnimation();
            CheckForTricks();
        }

        if (isCheckingGroundExit)
        {
            timeSinceLeftGround += Time.deltaTime;
            if (timeSinceLeftGround >= groundExitDelay)
            {
                isGrounded = false;
                isCheckingGroundExit = false;
            }
        }
    }

    void Stopmove()
    {
        check = false;
    }

    void Boost()
    {
        if (Input.GetKey(KeyCode.Space))
            se.speed = boostedSpeed;
        else
            se.speed = normalSpeed;
    }
    void CheckForTricks()
    {
        if (!isGrounded)
        {
            float currentRotation = rb.rotation;
            float rotationDelta = Mathf.DeltaAngle(previousRotation, currentRotation);
            totalRotation += Mathf.Abs(rotationDelta);
            previousRotation = currentRotation;

            if (totalRotation >= 360f)
            {
                flipCount++;
                totalRotation -= 360f;
                PerformTrickEffects();
                GameManager.Instance.AddScore(trickPoints * flipCount);
            }
        }
    }

    void PerformTrickEffects()
    {
        if (trickParticle != null) trickParticle.Play();
        if (trickText != null)
        {
            trickText.gameObject.SetActive(true);
            trickText.text = "Amazing!";
            Invoke("HideTrickText", 1f);
        }
    }

    void HideTrickText()
    {
        if (trickText != null) trickText.gameObject.SetActive(false);
    }


    void Rotate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            rb.AddTorque(torque);
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            rb.AddTorque(-torque);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            se.speed = 0f;
            particleHit.Play();
            Stopmove();
            Debug.Log("Ouch!");
            Invoke("LoadScene", timeDelays);

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayHitSFX();
            }
        }
        else if (collision.CompareTag("Snowflake"))
        {
            Snowflake.Play();
            Destroy(collision.gameObject);
            GameManager.Instance.AddScore(100);

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySnowflakeSFX();
            }
            Invoke("SnowflakeStop", 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            trickParticle.Stop();
            isGrounded = true;
            isCheckingGroundExit = false;
            timeSinceLeftGround = 0f;
            Dust.Play();

            totalRotation = 0f;
            flipCount = 0;
            previousRotation = rb.rotation;

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySkiingSFX();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isCheckingGroundExit = true;
            timeSinceLeftGround = 0f;
            Dust.Stop();
        }
    }

    private void UpdateAnimation()
    {
        animator.SetBool("IsGrounded", isGrounded);
    }

    void SnowflakeStop()
    {
        Snowflake.Stop();
    }

    void LoadScene()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.ResetScore();
    }
}
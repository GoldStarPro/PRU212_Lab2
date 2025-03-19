using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torque = 1;
    [SerializeField] float timeDelays = 2f;
    [SerializeField] ParticleSystem particleHit;
    Rigidbody2D rb;

    SurfaceEffector2D se;
    [SerializeField] float boostedSpeed = 30f;
    [SerializeField] float normalSpeed = 15f;

    [SerializeField] ParticleSystem Dust;

    bool check = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        se = FindObjectOfType<SurfaceEffector2D>();
    }
    void Update()
    {
        if (check)
        {
            Rotate();
            Boost();
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
    void Rotate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            rb.AddTorque(torque);
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            rb.AddTorque(-torque);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            se.speed = 0f;
            particleHit.Play();
            Stopmove();
            Debug.Log("Ouch!");
            Invoke("LoadScene", timeDelays);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Dust.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Dust.Stop();
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}

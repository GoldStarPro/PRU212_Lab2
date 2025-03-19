using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torque = 1;
    [SerializeField] float timeDelays = 2f;
    [SerializeField] ParticleSystem particleHit;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            rb.AddTorque(torque);
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            rb.AddTorque(-torque);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            particleHit.Play();
            Debug.Log("Ouch!");
            Invoke("LoadScene", timeDelays);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}

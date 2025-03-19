using UnityEngine;

public class Flag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Nuh uh");
        }
    }
}

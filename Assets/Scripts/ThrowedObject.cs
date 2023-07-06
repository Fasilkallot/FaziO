
using UnityEngine;

public class ThrowedObject : MonoBehaviour
{
    private void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 8);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);

        if (collision.CompareTag("Enemi"))
        {
            collision.gameObject.GetComponent<AudioSource>().Play(); 
            Destroy(collision.gameObject);
        }
    }
}


using UnityEngine;

public class GaurdScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Dead();
        }
    }

    private void Dead()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<WayPointFollower>().enabled = false;
        GetComponent<AudioSource>().Play();
        GetComponent<Animator>().SetTrigger("Dead");
        Destroy(gameObject, 2);
    }
 
}

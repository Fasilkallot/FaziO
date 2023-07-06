
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.lastCheckPoint = gameObject.transform.position;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
           
        }
    }
}

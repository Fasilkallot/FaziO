using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class PlayerLife : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    [SerializeField] AudioSource deathAudio;

    public int playerHelth;


    private void Awake()
    {
        playerHelth = 3;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(transform.position.y < -2.5)  GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))  Hurt();
        if (collision.gameObject.CompareTag("Enemi")) Die();
    }

    void Hurt()
    {
        --playerHelth;
        if (playerHelth > 0)
        {
            GameManager.Instance.sceneControler.HelthUpdate(playerHelth);
            StartCoroutine(GetHurt());
        }
        else
        {
            GameManager.Instance.sceneControler.HelthUpdate(playerHelth);
            deathAudio.Play();
            animator.SetTrigger("death");
        }
     }
    void Die()
    {
        deathAudio.Play();
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
    }
    private IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(3,7);
        animator.SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(3, 7 , false);
        animator.SetLayerWeight(1, 0);
    }
    void GameOver()
    {
        rb.bodyType = RigidbodyType2D.Static;
        GameManager.Instance.sceneControler?.GameOver();
    }
}

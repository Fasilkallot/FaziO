using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private AudioSource winnerAudio;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        winnerAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && GameManager.Instance.currentState == GameState.playing)
        {
            StartCoroutine(GameCompleted());

        }
    }

    private IEnumerator GameCompleted()
    {
        winnerAudio.Play();
        anim.SetTrigger("isWin");
        GameManager.Instance.currentState = GameState.pause;
        yield return new WaitForSeconds(1);
        GameManager.Instance.sceneControler?.Winner();
    }

}

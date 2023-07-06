
using UnityEngine;
using UnityEngine.UI;

public class ItemsCollector : MonoBehaviour
{
   
    [SerializeField] Text KiwiText;
    [SerializeField] AudioSource collectAudio;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kiwi"))
        {
            collectAudio.Play();
            Destroy(collision.gameObject);
            GameManager.Instance.kiwis++;
            TextUpdate();
        }
    }
    public void TextUpdate()
    {
        KiwiText.text = "Kiwi : " + GameManager.Instance.kiwis;
    }
}

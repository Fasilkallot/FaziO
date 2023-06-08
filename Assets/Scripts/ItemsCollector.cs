
using UnityEngine;
using UnityEngine.UI;

public class ItemsCollector : MonoBehaviour
{
    int kiwi = 0;

    [SerializeField] Text KiwiText; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kiwi"))
        {
            Destroy(collision.gameObject);
            kiwi++;
            KiwiText.text = "Kiwi : " + kiwi; 
        }
    }
}


using UnityEngine;

public class Mace : MonoBehaviour
{
    [SerializeField] float range = 3f;
    [SerializeField] float speed = 2f;

    private int dir = 1;
    private float startPosY;
    
    void Start()
    {
        startPosY = transform.position.y;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime * dir);
        if (transform.position.y < startPosY || transform.position.y > startPosY + range) dir *= -1; 
    }
}

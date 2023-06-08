using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float jumpForce = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        { 
            rb.velocity = new Vector2(transform.position.x, transform.position.y+ jumpForce);
            
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}

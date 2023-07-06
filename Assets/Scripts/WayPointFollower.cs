using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float speed = 2f;

    int currentindex = 0;


    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(wayPoints[currentindex].position,transform.position) < 0.1f)
        {
            GetComponent<SpriteRenderer>().flipX = !(GetComponent<SpriteRenderer>().flipX);
            currentindex++;
            if (currentindex >= wayPoints.Length)
            {
                currentindex = 0;
            }
        }

        transform.position =  Vector2.MoveTowards(transform.position, wayPoints[currentindex].position, Time.deltaTime * speed);
    }
}

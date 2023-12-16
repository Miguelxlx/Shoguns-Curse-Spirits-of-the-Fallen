using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sideways_enemy : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private bool movingHorizontally;
    [SerializeField] private bool startRightOrUp;


    private bool movingRightOrUp;
    private Vector2 v_speed;

    private Vector2 edges;


    private void Awake()
    {
        movingRightOrUp = startRightOrUp;

        //Adjust edges depending on rotation of the object
        if (movingHorizontally)
        {
            edges.x = transform.position.x - movementDistance;
            edges.y = transform.position.x + movementDistance;

            v_speed = new Vector2(speed, 0);
        }
        else
        {
            edges.x = transform.position.y - movementDistance;
            edges.y = transform.position.y + movementDistance;

            v_speed = new Vector2(0, speed);
        }

    }

    private void Update()
    {

        if (movingRightOrUp)
        {
            if ((movingHorizontally && transform.position.x < edges.y )|| (!movingHorizontally && transform.position.y < edges.y ))
            {
                transform.position = new Vector3(transform.position.x + v_speed.x * Time.deltaTime, transform.position.y + v_speed.y * Time.deltaTime, transform.position.z);
            }
            else
                movingRightOrUp = false;
        }
        else
        {
            if ((movingHorizontally && transform.position.x > edges.x) || (!movingHorizontally && transform.position.y > edges.x))
            {
                transform.position = new Vector3(transform.position.x - v_speed.x * Time.deltaTime, transform.position.y - v_speed.y * Time.deltaTime, transform.position.z);
            }
            else
                movingRightOrUp = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().takeDamage(damage);
        }
    }
}

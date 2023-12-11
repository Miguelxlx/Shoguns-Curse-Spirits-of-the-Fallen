using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEye : MonoBehaviour
{
    private EnemyPatrol enemyPatrol;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    private Vector3 objectScale;

    private void Awake()
    {
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        objectScale = transform.localScale;
    }

    private void Update()
    {

        if (playerInSight())
        {
            FollowPlayer();
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !playerInSight();
        }
    }

    private bool playerInSight()
    {
        return (playerPosition.position.x > leftEdge.position.x && playerPosition.position.x < rightEdge.position.x) ;
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPosition.position.x,transform.position.y,playerPosition.position.z), enemyPatrol.getSpeed() * Time.deltaTime);

        if (playerPosition.position.x > transform.position.x)
            transform.localScale = new Vector3(-objectScale.x, objectScale.y, objectScale.z);
        else if((playerPosition.position.x < transform.position.x))
            transform.localScale = objectScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
           collision.GetComponent<Health>().takeDamage(1);
    }
}

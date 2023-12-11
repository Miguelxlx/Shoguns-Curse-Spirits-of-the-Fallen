using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform rigthEdge;
    [SerializeField] private Transform leftEdge;


    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behavior")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;


    private void Awake()
    {
        initScale = enemy.localScale;
        movingLeft = true;
    }

    private void OnDisable()
    {
        anim.SetBool("Moving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rigthEdge.position.x)
                MoveInDirection(1);
            else
            {
                DirectionChange();
            }
        }

    }

    private void DirectionChange() //Idle until timer stops and then change direction
    {

        anim.SetBool("Moving", false);

        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;

        anim.SetBool("Moving", true);

        //Reverse the sprite
        enemy.localScale = new Vector3(-initScale.x * _direction, initScale.y, initScale.z);

        //Move towards direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }

    public bool isMovingLeft()
    {
        return movingLeft;
    }

    public float getSpeed()
    {
        return speed;
    }
}

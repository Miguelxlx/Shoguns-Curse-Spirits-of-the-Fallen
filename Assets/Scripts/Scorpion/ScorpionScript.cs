using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionScript : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float shootingRange;
    public float fireRate = 1f;
    private float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;

    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.position.x,transform.position.y), speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer>shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.position.x, transform.position.y), speed * Time.deltaTime);
        }
        //else if (distanceFromPlayer <= shootingRange && nextFireTime <Time.time)
        //{
        //    Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
        //    nextFireTime = Time.time + fireRate;
        //}

        float dirX = transform.position.x - player.position.x;

        if (dirX > 0f)
        {
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            sprite.flipX = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}

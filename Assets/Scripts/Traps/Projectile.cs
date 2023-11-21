using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;

    private float lifetime;


    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    public void Update()
    {
        float movementSpeed = speed * Time.deltaTime * -1;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        gameObject.SetActive(false);
    }
}

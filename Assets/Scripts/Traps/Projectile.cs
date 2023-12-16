using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    [SerializeField] private float lifetime;


    public void ActivateProjectile()
    {
        gameObject.SetActive(true);
        StartCoroutine(DeactivateAfterLifetime());
    }

    private IEnumerator DeactivateAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
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

        if (collision.gameObject.layer == 7 || collision.gameObject.tag =="Player")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            gameObject.SetActive(false);
        }
    }
}

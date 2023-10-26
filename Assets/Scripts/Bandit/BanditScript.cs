using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditScript : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public Transform player;
    public float attackRange = 3f;


    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        animator.SetTrigger("Hurt");

        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died");
        animator.SetBool("IsDead", true);

        //GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(rb.position, attackRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamouraiScript : MonoBehaviour
{
    public float moveSpeed = 5f;

    private float dirX;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer sprite;

    public Transform attackPointRight;
    public Transform attackPointLeft;

    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 40;

    private bool isRight = true;

    public int maxHealth = 100;
    int currentHealth;

    public bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(dirX, 14f);
        }

        updateAnimation();
    }

    void updateAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            animator.SetTrigger("Attack2");
        }


        if (dirX > 0f )
        {
            animator.SetBool("Running", true);
            sprite.flipX = false;
            isRight = true;
        }
        else if(dirX < 0f)
        {
            animator.SetBool("Running", true);
            sprite.flipX = true;
            isRight = false;
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack1");

        if (isRight)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Enemy hurt");
                enemy.GetComponent<BanditScript>().takeDamage(attackDamage);

            }
        }
        else
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Enemy hurt");
                enemy.GetComponent<BanditScript>().takeDamage(attackDamage);

            }
        }

    }

    public void takeDamage(int damage)
    {
        animator.SetTrigger("Hurt");

        currentHealth -= damage;

        Debug.Log(currentHealth + " health");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died");
        animator.SetBool("IsDead", true);

        //GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        alive = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointRight == null)
            return;

        Gizmos.DrawWireSphere(attackPointRight.position, attackRange);

        if (attackPointLeft == null)
            return;

        Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
    }
}

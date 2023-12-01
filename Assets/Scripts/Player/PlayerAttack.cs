using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private int damage;


    [Header("Cooldown Time")]
    [SerializeField] private float attackCoolDown;

    [Header("Range of Attack")]
    [SerializeField] private Transform attackPointLeft;
    [SerializeField] private Transform attackPointRight;
    [SerializeField] private float attackRangeX;
    [SerializeField] private float attackRangeY;


    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private AudioClip swordSound;


    private Animator anim;
    private PlayerMovement playerMovement;
    private float coolDownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return) && coolDownTimer > attackCoolDown && playerMovement.canAttack())
            Attack();

        coolDownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(swordSound);
        anim.SetTrigger("Attack");
        coolDownTimer = 0;
    }

    private void DamageEnemy()
    {
        if (playerMovement.isLookingRight())
        {
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPointLeft.position, new Vector2(attackRangeX, attackRangeY), 0, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Health>().takeDamage(damage);
                enemy.GetComponent<Bandit>().resetCoolDown();
            }
        }
        else
        {
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPointRight.position, new Vector2(attackRangeX, attackRangeY), 0, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Health>().takeDamage(damage);
                enemy.GetComponent<Bandit>().resetCoolDown();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointRight == null)
            return;

        Gizmos.DrawWireCube(attackPointRight.position, new Vector2(attackRangeX, attackRangeY));
        if (attackPointLeft == null)
            return;

        Gizmos.DrawWireCube(attackPointLeft.position, new Vector2(attackRangeX, attackRangeY));
    }

    public void resetCoolDown()
    {
       coolDownTimer = 0;
    }
}


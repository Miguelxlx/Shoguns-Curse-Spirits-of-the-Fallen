﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit: MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Animator anim;
    [SerializeField] private EnemyPatrol enemyPatrol;

    [Header("Range of Attack")]
    [SerializeField] private Transform attackPointLeft;
    [SerializeField] private Transform attackPointRight;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attackSound;

    private Health playerHealth;
    private PlayerAttack playerAttack;

    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }


    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (playerInSight())
        {
            if (cooldownTimer >= attackCooldown && !playerHealth.isDead())
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attack");
                PlayAttackSound(); // Play the attack sound here
            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !playerInSight();
        }
    }

    private bool playerInSight()
    {
        if (!enemyPatrol.isMovingLeft())
        {
            Collider2D collider = Physics2D.OverlapBox(attackPointLeft.position, new Vector2(.5f, 1), 0, playerLayer);

            if (collider != null)
            {
                playerHealth = collider.GetComponent<Health>();
                playerAttack = collider.GetComponent<PlayerAttack>();
            }

            return collider != null;
        }
        else if (enemyPatrol.isMovingLeft())
        {
            Collider2D collider = Physics2D.OverlapBox(attackPointRight.position, new Vector2(.5f, 1), 0, playerLayer);
            if (collider != null)
            {
                playerHealth = collider.GetComponent<Health>();
                playerAttack = collider.GetComponent<PlayerAttack>();
            }

            return collider != null;
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointRight == null)
            return;

        Gizmos.DrawWireCube(attackPointRight.position, new Vector2(.5f, 1));
        if (attackPointLeft == null)
            return;

        Gizmos.DrawWireCube(attackPointLeft.position, new Vector2(.5f, 1));
    }

    private void DamagePlayer()
    {
        if (playerInSight())
        {
            playerHealth.takeDamage(damage);
            playerAttack.resetCoolDown();
            //PlayAttackSound(); // would be better to put sword hit and find another sound for the attack
        }
    }

    private void PlayAttackSound()
    {
        if (attackSound != null)
        {
            SoundManager.instance.PlaySound(attackSound);
        }
    }


    public void resetCoolDown()
    {
        cooldownTimer = 0;
    }
}
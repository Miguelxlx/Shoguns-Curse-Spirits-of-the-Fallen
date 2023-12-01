using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform arrowpoint;
    [SerializeField] private GameObject[] arrows;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attackSound;

    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0;
        SoundManager.instance.PlaySound(attackSound);
        arrows[FindArrow()].transform.position = arrowpoint.position;
        arrows[FindArrow()].GetComponent<Projectile>().ActivateProjectile();
    }

    private int FindArrow()
    {
        for(int i=0; i<arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }

        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCooldown)
            Attack();
    }

}

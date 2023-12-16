using System.Collections;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [Header("Arrow Details")]
    [SerializeField] private float resetTime;
    [SerializeField] private float lifetime;
    [SerializeField] private float speed;

    [SerializeField] private Transform arrowpoint;
    [SerializeField] private GameObject[] arrows;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attackSound;


    private float cooldownTimer;
    private float coolDown;
    private bool canStart;

    private void Awake()
    {
        canStart = false;
        coolDown = Random.Range(3, 5);

        StartCoroutine(StartAfterDelay());
    }

    private IEnumerator StartAfterDelay()
    {
        float delay = Random.Range(0f, 3f);
        yield return new WaitForSeconds(delay);

        canStart = true;
    }

    private void FireArrow()
    {
        cooldownTimer = 0;
        SoundManager.instance.PlaySound(attackSound);
        arrows[FindArrow()].transform.position = arrowpoint.position;
        arrows[FindArrow()].GetComponent<Projectile>().ActivateProjectile(speed,resetTime,lifetime);
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }

        return 0;
    }

    private void Update()
    {
        if (canStart)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= coolDown)
                FireArrow();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timer")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer sprite;

    private bool triggered;
    private bool active;

    private Health playerhealth;

    private void Update()
    {
        if (playerhealth != null && active)
        {
            playerhealth.takeDamage(damage);
        }
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        triggered = false;
        active = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerhealth = collision.GetComponent<Health>();
            if (!triggered)
                StartActivation();

            if (active)
                collision.GetComponent<Health>().takeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerhealth = null;

    }

    private void StartActivation()
    {
        triggered = true;
        anim.SetTrigger("Hit");
    }

    public void activateTrap()
    {
        StartCoroutine(ActivateFiretrap());
    }
    private IEnumerator ActivateFiretrap()
    {
        //triggered = true;
        //sprite.color = Color.red;

        //yield return new WaitForSeconds(activationDelay);

        sprite.color = Color.white;
        active = true;
        anim.SetBool("Active", true);

        yield return new WaitForSeconds(activeTime);

        active = false;
        triggered = false;
        anim.SetBool("Active", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim;
    private bool activated = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!activated && collision.tag == "Player")
        {
            activated = true;
            anim.SetTrigger("Activated");
        }
    }
}

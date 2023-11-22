using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    [SerializeField] private int nextScene;
    [SerializeField] private Health[] enemiesToKill;
    private Animator anim;
    private bool active;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        active = false;
    }
    private void Update()
    {
        if (enemiesToKill.Length != 0)
        {
            //Checks that all enemies have been deactivated (killed)
            for (int i = 0; i < enemiesToKill.Length; i++)
            {
                if (!enemiesToKill[i].isDead())
                {
                    return;
                }
            }
        }

        activatePortal();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(active && collision.tag == "Player")
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
    }

    private void activatePortal()
    {
        active = true;
        anim.SetTrigger("Activate");
    }
}

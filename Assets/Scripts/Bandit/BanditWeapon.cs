using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWeapon : MonoBehaviour
{
    public int attackDamage = 20;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D collider = Physics2D.OverlapCircle(pos, attackRange, attackMask);

        if (collider != null)
        {
            collider.GetComponent<SamouraiScript>().takeDamage(attackDamage);
        }
    }
}

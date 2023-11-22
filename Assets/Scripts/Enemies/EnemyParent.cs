using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    protected bool alive;

    private void Awake()
    {
        alive = true;
    }

    public bool isAlive()
    {
        return alive;
    }
}

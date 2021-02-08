using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MainHealth
{
    public void Kill()
    {
        base.Kill();

        Destroy(gameObject);
    }
}

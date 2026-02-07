using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(RangedWeapons.currentTotalAmmo < 28)
            {
                RangedWeapons.rw.AddAmmo(28);
                Destroy(gameObject);
            }
        }
    }
}

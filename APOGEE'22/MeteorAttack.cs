using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorAttack : MonoBehaviour
{
    public float damage = 0.08f;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MeteorShadow"))
        {

            Destroy(other.gameObject, 1f);
            Destroy(gameObject, 1f);
            
            
        }

        if (other.CompareTag("Player"))
        {
            Health hp = other.gameObject.GetComponent<Health>();
            hp.TakeDamage(damage);
            FindObjectOfType<HapticsController>().SendHaptics();
        }
    }
}

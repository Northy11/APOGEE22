using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalShard : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float damage = 0.004f;

    public GameObject impactCrystal;
    public bool useGravity = true;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigidbody.useGravity = false;
        if (useGravity) rigidbody.AddForce(Physics.gravity * (rigidbody.mass * rigidbody.mass));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Damge
            Health hp = other.gameObject.GetComponent<Health>();
            hp.TakeDamage(damage);
            FindObjectOfType<HapticsController>().SendHaptics(0.2f,0.01f);
        }
        
        if(other.CompareTag("Floor"))
        {
            Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
            if ((int)Random.RandomRange(0, 10) > 6)
            {
                GameObject crys = Instantiate(impactCrystal, pos, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                Destroy(crys, 10f);
            }
            Destroy(gameObject, 10f);
        }
        
    }
    
}

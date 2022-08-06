using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAway : MonoBehaviour
{
    public GameObject center;
    public float speed;
    public float maxDist;
    public float distance;
    public float damage = 0.12f;

    void Update()
    {
        Vector3 dir = (gameObject.transform.position - center.transform.position);
        distance = dir.magnitude;
        if (dir.magnitude < maxDist)
        {
            gameObject.GetComponent<Rigidbody>().velocity = dir.normalized * speed;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Health hp = collision.gameObject.GetComponent<Health>();
            hp.TakeDamage(damage);
            FindObjectOfType<HapticsController>().SendHaptics();

            this.gameObject.GetComponent<VFX>().enabled = true;
            StartCoroutine(DoVFX());
        }
    }
    
    public IEnumerator DoVFX()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.GetComponent<VFX>().enabled = false;
        this.gameObject.SetActive(false);
    }
}

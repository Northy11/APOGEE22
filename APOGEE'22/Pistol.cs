using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public float speed = 40;
    public GameObject bullet;
    public Transform barrel;
    public AudioManager am;

    // configure shots per second
    public float rate = 1;

    private Coroutine _current;

    public void BeginFire()
    {
        if (_current != null) StopCoroutine(_current);

        _current = StartCoroutine(FireRoutine());
    }

    public void StopFire()
    {
        if (_current != null) StopCoroutine(_current);
    }

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
            am.PlaySound("RifleShoot");

            yield return new WaitForSeconds(1f / rate);
        }
    }
}

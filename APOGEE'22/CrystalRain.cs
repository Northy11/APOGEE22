using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRain : MonoBehaviour
{
    public GameObject[] crystals;

    public GameObject player;

    public float speed;

    Vector2 playerI;
    Vector2 playerF;
    Vector2 v1;
    Vector2 v2;

    public float spawnTime;
    float timeLeft1;
    float timeLeft2;
    float timeLeft3;
    float timeLeft4;
    public float spawnDist;

    float timeLimit;
    public float destroyIn;

    VFX vfx;

    Rigidbody rb;

    private void Start()
    {
        vfx = gameObject.GetComponent<VFX>();
        timeLimit = destroyIn;
        timeLeft1 = spawnTime / 4;
        timeLeft2 = timeLeft1 * 2;
        timeLeft3 = timeLeft1 * 3;
        timeLeft4 = timeLeft1 * 4;
        rb = gameObject.GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        timeLimit -= Time.deltaTime;
        timeLeft1 -= Time.deltaTime;
        timeLeft2 -= Time.deltaTime;
        timeLeft3 -= Time.deltaTime;
        timeLeft4 -= Time.deltaTime;

        if (timeLimit < 0)
        {
            StartCoroutine("Destroy");
        }
        if (timeLeft1 < 0)
        {
            timeLeft1 = spawnTime;
            int rnd = Random.Range(0, crystals.Length);
            GameObject currCrystal;
            currCrystal = crystals[rnd];
            Vector3 spawnAt = new Vector3(transform.position.x + Random.Range(-spawnDist, spawnDist), transform.position.y, transform.position.z + Random.Range(-spawnDist, spawnDist));
            GameObject crystal = Instantiate(currCrystal, spawnAt, Quaternion.Euler(90, 0, 0));
        }
        if (timeLeft2 < 0)
        {
            timeLeft2 = spawnTime;
            int rnd = Random.Range(0, crystals.Length);
            GameObject currCrystal;
            currCrystal = crystals[rnd];
            Vector3 spawnAt = new Vector3(transform.position.x + Random.Range(-spawnDist, spawnDist), transform.position.y, transform.position.z + Random.Range(-spawnDist, spawnDist));
            GameObject crystal = Instantiate(currCrystal, spawnAt, Quaternion.Euler(90, 0, 0));
        }
        if (timeLeft3 < 0)
        {
            timeLeft3 = spawnTime;
            int rnd = Random.Range(0, crystals.Length);
            GameObject currCrystal;
            currCrystal = crystals[rnd];
            Vector3 spawnAt = new Vector3(transform.position.x + Random.Range(-spawnDist, spawnDist), transform.position.y, transform.position.z + Random.Range(-spawnDist, spawnDist));
            GameObject crystal = Instantiate(currCrystal, spawnAt, Quaternion.Euler(90, 0, 0));
        }
        if (timeLeft4 < 0)
        {
            timeLeft4 = spawnTime;
            int rnd = Random.Range(0, crystals.Length);
            GameObject currCrystal;
            currCrystal = crystals[rnd];
            Vector3 spawnAt = new Vector3(transform.position.x + Random.Range(-spawnDist, spawnDist), transform.position.y, transform.position.z + Random.Range(-spawnDist, spawnDist));
            GameObject crystal = Instantiate(currCrystal, spawnAt, Quaternion.Euler(90, 0, 0));
            crystal.transform.SetParent(gameObject.transform);
        }

        /*
        playerI = playerF;
        playerF.x = player.transform.position.x;
        playerF.y = player.transform.position.z;

        v1.x = (playerI.x - gameObject.transform.position.x);
        v1.y = (playerI.y - gameObject.transform.position.z);
        v1 = v1.normalized;
        v2 = (playerF - playerI).normalized;

        Vector2 v = (((v1 * v1Scale) + (v2 * v2Scale)).normalized) * speed;

        rb.velocity = new Vector3(v.x, 0, v.y);
        */

        rb.velocity = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z).normalized * speed;
    }

    public IEnumerator Destroy()
    {
        vfx.playRate = Mathf.Lerp(100, 4000, 5);
        yield return new WaitForSeconds(destroyIn);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public GameObject target;
    public float speed = 20f;
    public float maxSpeed = 100f;

    public float attackTime = 5f;
    public GameObject[] orbs;

    private void Start()
    {
        for(int i = 0; i < orbs.Length;i++)
        {
            orbs[i].SetActive(true);
        }
    }
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
        if(speed < maxSpeed)
        {
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime);
        }

    }

}

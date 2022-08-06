using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Monster moveAttack;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("Tag Issue");
        if (other.CompareTag("Player"))
        {
            Debug.LogError("Works");
            moveAttack.canWork = true;
            FindObjectOfType<AudioManager>().PlaySound("Theme");
        }
    }
}

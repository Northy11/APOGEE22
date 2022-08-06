using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Attached to player, kills him if a monster is touched
public class Monster_Encounter : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        //Check if touched
        if (collider.tag == "Monster")
        {
            Debug.Log("Game Over");
            Destroy(gameObject);
        }
    }
}

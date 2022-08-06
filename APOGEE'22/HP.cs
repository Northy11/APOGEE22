using RengeGames.HealthBars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HP : MonoBehaviour
{
    public UltimateCircularHealthBar healthbar;
    private float max = 0.8f;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        healthbar.SetSegmentCount(5);
        healthbar.SetRemovedSegments(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            healthbar.AddRemovePercent(0.2f);
            Debug.Log("Reduce bar");
            //max = max - 0.1f;
        }

        var wantedPos = Camera.main.WorldToViewportPoint(target.position);
        transform.position = wantedPos;
      
    }
}



using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Player : MonoBehaviour
{
    public NavMeshAgent agent;
    public Camera cam;
    public ThirdPersonCharacter charecter;

    Ray ray;
    RaycastHit hit;

    void Start()
    {
        agent.updateRotation = false;
    }
    void Update()
    {
        //Move to Click
        if (Input.GetMouseButtonDown(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }

        }

        //Velocity Controls
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            charecter.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            charecter.Move(Vector3.zero, false, false);
        }
    }
}

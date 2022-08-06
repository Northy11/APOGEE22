using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    public Animator animator;
    //public SkinnedMeshRenderer mesh;
    //Physics Movement
    [SerializeField] private GameObject followObject;
    [SerializeField] private float followspeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    private Transform followTarget;
    private Rigidbody body;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;
    public float speed = 10f;
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";

    internal void SetGrip(float v)
    {
        gripTarget = v;
        //Debug.Log(gripTarget);

    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
        //Debug.Log("Trigger: " +triggerTarget);
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Physics Movement
        followTarget = followObject.transform;
        body = GetComponent<Rigidbody>();
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.mass = 20f;

        //Teleport hands
        body.position = followTarget.position;
        body.rotation = followTarget.rotation;
        //Animation
        //animator = GetComponent<Animator>();
        //mesh = GetComponentInChildren<SkinnedMeshRenderer>();

    }

    // Update is called once per frame
   private void FixedUpdate()
    {
        PhysicsMove();
      
    }
    private void Update()
    {
        AnimateHand();
    }
    private void PhysicsMove()
    {
        //Position

        var positionWithOffest = followTarget.TransformPoint(positionOffset); 
        var distance = Vector3.Distance(positionWithOffest, transform.position);
        body.velocity = (positionWithOffest - transform.position).normalized * (followspeed * distance);

        //Rotation
        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);

    }

    void AnimateHand()
    {
        /* if(gripCurrent != gripTarget)
         {
             gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
             animator.SetFloat(animatorGripParam, gripCurrent);
         }
         if(triggerCurrent != triggerTarget)
         {
             triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
             animator.SetFloat(animatorTriggerParam, triggerCurrent);
         }
        */
        animator.SetFloat(animatorGripParam, gripTarget);
        animator.SetFloat(animatorTriggerParam, triggerTarget);

    }
   /* public void ToggleVisibility()
    {
        mesh.enabled = !mesh.enabled;
         
    }
   */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Cam : MonoBehaviour
{
    public int FPS = 60;
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveD = Vector3.zero;

    float yR;
    float xR;
    public float sentivity = 2;
    float cxR;
    float cyR;
    float yRV;
    float xRV;
    public float smoother = 0.1f;

    private void Start()
    {
        Application.targetFrameRate = FPS;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if(controller.isGrounded)
        {
            moveD = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveD = transform.TransformDirection(moveD);
            moveD *= speed;
            if(Input.GetButton("Jump"))
            {
                moveD.y = jumpSpeed;
            }
        }
        moveD.y -= gravity * Time.deltaTime;
        controller.Move(moveD * Time.deltaTime);

        yR += Input.GetAxis("Mouse X") * sentivity;
        xR -= Input.GetAxis("Mouse Y") * sentivity;
        xR = Mathf.Clamp(xR, -80, 100);
        cxR = Mathf.SmoothDamp(cxR, xR, ref xRV, smoother);
        cyR = Mathf.SmoothDamp(cyR, yR, ref yRV, smoother);
        transform.rotation = Quaternion.Euler(xR, yR, 0);
    }
}

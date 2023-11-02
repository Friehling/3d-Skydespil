using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;


    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

    float verticalLockRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;

    Rigidbody rb;

    PhotonView PV;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (PV.IsMine)
            return;

        look();
        Move();
        Jump();
        
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);

        
    }
    void look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse x") * mouseSensitivity);

        verticalLockRotation += Input.GetAxisRaw("Mouse y") * mouseSensitivity;
        verticalLockRotation = Mathf.Clamp(verticalLockRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLockRotation;

    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position+transform.TransformDirection(moveAmount)*Time.fixedDeltaTime);
    }



}
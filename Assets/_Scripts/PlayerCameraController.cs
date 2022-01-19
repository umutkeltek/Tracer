using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float lookSmoothing;
    
    private Transform playerTranform;
    private Vector2 smoothedVelocity;
    private Vector2 currentLookingDirection;

    private void Awake()
    {
        playerTranform = transform.root;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        Vector2 cameraRotationInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        cameraRotationInput = Vector2.Scale(cameraRotationInput, new Vector2(lookSensitivity * lookSmoothing,lookSensitivity * lookSmoothing));
        smoothedVelocity = Vector2.Lerp(smoothedVelocity, cameraRotationInput, 1 / lookSmoothing);

        currentLookingDirection += smoothedVelocity;
        
        transform.localRotation = Quaternion.AngleAxis(-currentLookingDirection.y,Vector3.right);
        playerTranform.localRotation = Quaternion.AngleAxis(currentLookingDirection.x, playerTranform.up);
    }
}

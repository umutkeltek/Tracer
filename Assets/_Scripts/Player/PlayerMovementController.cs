 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreatingCharacters.Player

{ 
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController : MonoBehaviour
    {
     [SerializeField] protected float movementSpeed = 5f;
     [SerializeField] protected float jumpForce = 4f;
     [SerializeField] protected float mass = 1f;
     [SerializeField] protected float damping = 5f;
     
     private float jumpHeight = 1.0f;
     private float gravityValue = -9.81f;
     private bool groundedPlayer;
     private Vector3 playerVelocity;
     
     protected CharacterController characterController;
     protected float velocityY;
     protected Vector3 currentImpact;
     
     private readonly float gravity = Physics.gravity.y;

     protected virtual void Awake()
     {
         
         characterController = GetComponent<CharacterController>();
         
     }
 
     protected virtual void Update()
     {
         Move();
         Jump();
     }
 
     protected virtual void Move()
     {  /*groundedPlayer = characterController.isGrounded;
         if (groundedPlayer && playerVelocity.y < 0)
         {
             playerVelocity.y = 0f;
         }

         Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
         move = transform.TransformDirection(move);
         characterController.Move(move * Time.deltaTime * movementSpeed);

         if (move != Vector3.zero)
         {
             gameObject.transform.forward = move;
         }

         // Changes the height position of the player..
         if (Input.GetButtonDown("Jump") && groundedPlayer)
         {
             playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
         }

         if (!groundedPlayer)
         {
             playerVelocity.y += gravityValue * Time.deltaTime;
         }
         characterController.Move(playerVelocity * Time.deltaTime);*/
         
         
         if (characterController.isGrounded && velocityY < 0f)
         {
             velocityY = 0f;
         }
         Vector3 movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
         movementInput = transform.TransformDirection(movementInput);

         velocityY += gravity * Time.deltaTime;
         Vector3 velocity = movementInput * movementSpeed + Vector3.up * velocityY;

         if (currentImpact.magnitude > 0.2f)
         {
             velocity += currentImpact;
         }
         /*if (movementInput != Vector3.zero)
         {
             gameObject.transform.forward = movementInput;
         }*/
         
         /*if (Input.GetKeyDown(KeyCode.Space)&& characterController.isGrounded)
         {
             velocityY += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
             velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
         }*/
            
         /*velocity.y += gravity * Time.deltaTime;
         velocityY += gravity * Time.deltaTime;*/
         characterController.Move(velocity * Time.deltaTime);
         currentImpact = Vector3.Lerp(currentImpact, Vector3.zero, damping * Time.deltaTime);
         //transform.Translate(new Vector3(movementInput.x,0f,movementInput.y));
     }

     public void ResetImpact()
     {
         currentImpact = Vector3.zero;
         velocityY = 0f;
     }

     public void ResetImpactY()
     {
         currentImpact.y = 0f;
         velocityY = 0f;
     }

     protected virtual void Jump()
     {
         if (Input.GetKeyDown(KeyCode.Space))
         {    
             if (characterController.isGrounded)
             {  
                 AddForce(Vector3.up, jumpForce);
             }
         }
     }

     public void AddForce(Vector3 direction, float magnitude)
     {  
         currentImpact += direction.normalized * magnitude /   mass;
         
     }
 }
    
}


using System;
using System.Collections;
using System.Collections.Generic;
using CreatingCharacters.Player;
using UnityEngine;

namespace CreatingCharacters.Abilities
{
    [RequireComponent(typeof(PlayerMovementController))]
    public class TracerBlink : DashAbility

    {
        [SerializeField] private int maxBlinks = 3;
        [SerializeField] private float blinkRechargeTime;
        private int remainingBlinks = 3;
        private float currentBlinkRechargeTime;
        protected override void Update()
        {
            base.Update();

            if (remainingBlinks < maxBlinks)
            {
                currentBlinkRechargeTime += Time.deltaTime;
                if (currentBlinkRechargeTime >= blinkRechargeTime)
                {
                    remainingBlinks++;
                    currentBlinkRechargeTime = 0f;
                }
            }
        }

        public override IEnumerator Cast()
        {  
            StartCoroutine(Dash());
            yield return null;
        }

        protected override IEnumerator Dash()
        {
            if (remainingBlinks <= 0)
            {
                yield break;
            }

            remainingBlinks--;
            Vector3 movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            
            if (movementInput.magnitude == 0f)
            {
                movementInput.z = 1f;
            }
            
            movementInput = transform.TransformDirection(movementInput);
            playerMovementController.AddForce(movementInput, dashForce);
            
            yield return new WaitForSeconds(dashDuration);
            playerMovementController.ResetImpact();
            
            
        }
    }
}
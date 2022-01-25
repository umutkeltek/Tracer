using System;
using System.Collections;
using System.Collections.Generic;
using CreatingCharacters.Player;
using UnityEngine;

namespace CreatingCharacters.Abilities
{   
    [RequireComponent(typeof(PlayerMovementController))]
    public class DashAbility : Ability
    {
        [SerializeField] protected float dashForce;
        [SerializeField] protected float dashDuration;
        protected PlayerMovementController playerMovementController;
        private void Awake()
        {
            playerMovementController = GetComponent<PlayerMovementController>();
        }
        
        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(Cast());
                
            }
        }
        public override IEnumerator Cast()
        {
            StartCoroutine(Dash());
            yield return null;
        }

        protected virtual IEnumerator Dash()
        {   playerMovementController.AddForce(Camera.main.transform.forward, dashForce);
            yield return new WaitForSeconds(dashDuration);
            playerMovementController.ResetImpact();
        }
        
        
    }

 
}



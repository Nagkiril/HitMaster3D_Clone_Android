using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Characters.Components
{
    public class CharacterRagdoll : MonoBehaviour
    {
        [SerializeField] Rigidbody[] ragdollParts;

        private void Awake()
        {
            Deactivate();
        }


        public void Deactivate()
        {
            foreach (var part in ragdollParts)
            {
                part.isKinematic = true;
            }
        }

        public void Activate()
        {
            foreach (var part in ragdollParts)
            {
                part.isKinematic = false;
            }
        }
        
        public void ApplyReaction(Vector3 reactionVector, Transform reactionTarget)
        {
            foreach (var part in ragdollParts)
            {
                if (part.transform == reactionTarget)
                {
                    part.AddForce(reactionVector, ForceMode.VelocityChange);
                }
            }
        }
    }
}
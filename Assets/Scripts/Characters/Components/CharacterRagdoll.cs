using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Characters.Components
{
    public class CharacterRagdoll : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _ragdollParts;

        private void Awake()
        {
            Deactivate();
        }


        public void Deactivate()
        {
            foreach (var part in _ragdollParts)
            {
                part.isKinematic = true;
            }
        }

        public void Activate()
        {
            foreach (var part in _ragdollParts)
            {
                part.isKinematic = false;
            }
        }
        
        public void ApplyReaction(Vector3 reactionVector, Transform reactionTarget)
        {
            foreach (var part in _ragdollParts)
            {
                if (part.transform == reactionTarget)
                {
                    part.AddForce(reactionVector, ForceMode.VelocityChange);
                }
            }
        }
    }
}
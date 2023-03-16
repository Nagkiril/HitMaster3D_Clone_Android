using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Characters.Components
{
    public class CharacterVisuals : MonoBehaviour
    {
        [SerializeField] private Animator _ownAnim;
        [SerializeField] private CharacterRagdoll _ragdoll;


        public void SetMoving(bool isMoving)
        {
            _ownAnim.SetBool("Moving", isMoving);
        }

        public void SetDeath()
        {
            _ownAnim.enabled = false;
            if (_ragdoll != null)
            {
                _ragdoll.Activate();
            }
        }

        public void HitReaction(Vector3 hitVector, Transform hitTarget)
        {
            if (_ragdoll != null)
            {
                _ragdoll.ApplyReaction(hitVector, hitTarget);
            }
        }
    }
}
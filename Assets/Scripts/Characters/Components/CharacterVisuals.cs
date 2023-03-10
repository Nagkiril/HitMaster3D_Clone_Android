using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Characters.Components
{
    public class CharacterVisuals : MonoBehaviour
    {
        [SerializeField] Animator ownAnim;
        [SerializeField] CharacterRagdoll ragdoll;


        public void SetMoving(bool isMoving)
        {
            ownAnim.SetBool("Moving", isMoving);
        }

        public void SetDeath()
        {
            ownAnim.enabled = false;
            if (ragdoll != null)
            {
                ragdoll.Activate();
            }
        }

        public void HitReaction(Vector3 hitVector, Transform hitTarget)
        {
            if (ragdoll != null)
            {
                ragdoll.ApplyReaction(hitVector, hitTarget);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Characters.Components
{
    public class CharacterVisuals : MonoBehaviour
    {
        [SerializeField] Animator ownAnim;


        public void SetMoving(bool isMoving)
        {
            ownAnim.SetBool("Moving", isMoving);
        }
    }
}
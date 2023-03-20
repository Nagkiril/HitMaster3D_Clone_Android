using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Characters.Components
{
    //We're setting us up for inheritance here, instead of using an interface, because there's common implementation of Initialize method
    //We may also want to drag them inside Inspector, which will be problematic with an interface
    public class CharacterCollider : MonoBehaviour
    {
        public Character owner { get; private set; }

        public virtual void Initialize(Character owner)
        {
            this.owner = owner;
        }

        public virtual void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
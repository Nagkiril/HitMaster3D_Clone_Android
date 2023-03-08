using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Characters.Components
{
    public class CharacterInteractor : MonoBehaviour
    {
        public Character Owner { get; private set; }

        public event Action<Character> OnCharacterTouched;

        public void Initialize(Character owner)
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            var otherInteractor = other.GetComponent<CharacterInteractor>();
            if (otherInteractor != null && Owner != otherInteractor.Owner)
            {
                OnCharacterTouched?.Invoke(otherInteractor.Owner);
            }
        }
    }
}
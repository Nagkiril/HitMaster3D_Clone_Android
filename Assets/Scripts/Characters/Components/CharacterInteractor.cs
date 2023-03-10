using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.Level;

namespace TestTask.Characters.Components
{
    public class CharacterInteractor : MonoBehaviour
    {
        public Character Owner { get; private set; }

        public event Action<Character> OnCharacterTouched;
        public event Action<Waypoint> OnWaypointTouched;

        public void Initialize(Character owner)
        {
            Owner = owner;
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherRigidbody = other.attachedRigidbody;
            if (otherRigidbody != null)
            {
                var otherInteractor = otherRigidbody.GetComponent<CharacterInteractor>();
                if (otherInteractor != null && Owner != otherInteractor.Owner)
                {
                    OnCharacterTouched?.Invoke(otherInteractor.Owner);
                }
                var otherWaypoint = otherRigidbody.GetComponent<Waypoint>();
                if (otherWaypoint != null)
                {
                    OnWaypointTouched?.Invoke(otherWaypoint);
                }
            }
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
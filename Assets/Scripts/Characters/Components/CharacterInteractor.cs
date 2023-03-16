using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.Level;

namespace TestTask.Characters.Components
{
    public class CharacterInteractor : MonoBehaviour
    {
        public Character owner { get; private set; }

        public event Action<Character> onCharacterTouched;
        public event Action<Waypoint> onWaypointTouched;

        public void Initialize(Character owner)
        {
            this.owner = owner;
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherRigidbody = other.attachedRigidbody;
            if (otherRigidbody != null)
            {
                var otherInteractor = otherRigidbody.GetComponent<CharacterInteractor>();
                if (otherInteractor != null && owner != otherInteractor.owner)
                {
                    onCharacterTouched?.Invoke(otherInteractor.owner);
                }
                var otherWaypoint = otherRigidbody.GetComponent<Waypoint>();
                if (otherWaypoint != null)
                {
                    onWaypointTouched?.Invoke(otherWaypoint);
                }
            }
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
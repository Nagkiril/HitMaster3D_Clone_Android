using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.Level;

namespace TestTask.Characters.Components
{
    public class CharacterInteractor : CharacterCollider
    {
        public event Action<Character> onCharacterTouched;
        public event Action<Waypoint> onWaypointTouched;

        private void OnTriggerEnter(Collider other)
        {
            var otherRigidbody = other.attachedRigidbody;
            //At the moment there are no Characters that listen to both callbacks, meaning there's a point in separating this class out to avoid excessive GetComponent calls
            //I decided to leave them in, because they allow us to easily scale any Character behaviour, while the price of a couple GetComponents on rare collisions feels negligible
            if (otherRigidbody != null)
            {
                var otherInteractor = otherRigidbody.GetComponent<CharacterInteractor>();
                if (otherInteractor != null && owner != otherInteractor.owner)
                {
                    onCharacterTouched?.Invoke(otherInteractor.owner);
                }
                //We're checking for waypoints this way, because it is possible we may need different reach distances for different actions in the future
                var otherWaypoint = otherRigidbody.GetComponent<Waypoint>();
                if (otherWaypoint != null)
                {
                    onWaypointTouched?.Invoke(otherWaypoint);
                }
            }
        }
    }
}
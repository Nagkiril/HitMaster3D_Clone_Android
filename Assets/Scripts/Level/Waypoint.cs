using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.Characters;

namespace TestTask.Level
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] Transform playerPlacement;

        public event Action OnSegmentReached;



        public Vector3 GetPlayerPlace() => playerPlacement.position;

        public void EnterWaypoint(Character entrant)
        {
            if (entrant is Player)
            {
                OnSegmentReached?.Invoke();
            }
        }
    }
}

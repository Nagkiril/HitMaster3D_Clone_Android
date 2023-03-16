using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.Characters;

namespace TestTask.Level
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private Transform _playerPlacement;

        public event Action onSegmentReached;



        public Vector3 GetPlayerPlace() => _playerPlacement.position;

        public void EnterWaypoint(Character entrant)
        {
            if (entrant is Player)
            {
                onSegmentReached?.Invoke();
            }
        }
    }
}

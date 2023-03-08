using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Level
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] Transform playerPlacement;




        public Vector3 GetPlayerPlace() => playerPlacement.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TestTask.Characters;

namespace TestTask.Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] LevelSegment[] segments;
        static LevelController _instance;
        int _currentSegmentIndex = -1;

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Debug.LogWarning("There has to be only 1 LevelController on the scene! Destroying 2nd instance.");
                Destroy(this);
            }
        }

        public void StartLevel()
        {
            ProgressLevel();
        }

        public void ProgressLevel()
        {
            ChangeToNextSegment();
            ActivateSegment(GetCurrentSegment());
            CheckLevelEnding();
        }
        

        void ChangeToNextSegment()
        {
            _currentSegmentIndex++;
        }

        void ActivateSegment(LevelSegment segment)
        {
            if (segment != null)
            {
                segment.ActivateSegment();
                Player.OrderMovement(segment.GetPlayerPlace());
            }
        }

        void CheckLevelEnding()
        {
            if (GetCurrentSegment() == null)
            {
                SceneManager.LoadScene(0);
            }
        }

        LevelSegment GetCurrentSegment()
        {
            if (_currentSegmentIndex >= 0 && _currentSegmentIndex < segments.Length)
            {
                return segments[_currentSegmentIndex];
            }
            return null;
        }
    }
}
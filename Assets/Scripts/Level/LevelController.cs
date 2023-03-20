using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TestTask.Characters;
using TestTask.UI;

namespace TestTask.Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private LevelSegment[] _segments;
        private static LevelController _instance;
        private int _currentSegmentIndex = -1;


        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                InputManager.onStartInput += OnTappedStart;
                Player.onPlayerDied += OnPlayerDeath;
            }
            else
            {
                Debug.LogWarning("There has to be only 1 LevelController on the scene! Destroying 2nd instance.");
                Destroy(this);
            }
        }

        private void OnDestroy()
        {
            InputManager.onStartInput -= OnTappedStart;
        }

        private void OnTappedStart()
        {
            StartLevel();
        }

        private void OnPlayerDeath()
        {
            SceneManager.LoadScene(0);
        }

        private void OnActiveSegmentCleared()
        {
            ProgressLevel();
        }

        private void StartLevel()
        {
            ProgressLevel();
        }

        private void ProgressLevel()
        {
            ChangeToNextSegment();
            ActivateSegment(GetCurrentSegment());
            CheckLevelEnding();
        }
        

        private void ChangeToNextSegment()
        {
            var currentSegment = GetCurrentSegment();
            if (currentSegment != null)
            {
                currentSegment.onSegmentCleared -= OnActiveSegmentCleared;
            }
            _currentSegmentIndex++;
            currentSegment = GetCurrentSegment();
            if (currentSegment != null)
            {
                currentSegment.onSegmentCleared += OnActiveSegmentCleared;
            }
        }

        private void ActivateSegment(LevelSegment segment)
        {
            if (segment != null)
            {
                segment.ActivateSegment();
                Player.OrderMovement(segment.GetPlayerPlace());
            }
        }

        private void CheckLevelEnding()
        {
            if (GetCurrentSegment() == null)
            {
                SceneManager.LoadScene(0);
            }
        }

        private LevelSegment GetCurrentSegment()
        {
            if (_currentSegmentIndex >= 0 && _currentSegmentIndex < _segments.Length)
            {
                return _segments[_currentSegmentIndex];
            }
            return null;
        }
    }
}
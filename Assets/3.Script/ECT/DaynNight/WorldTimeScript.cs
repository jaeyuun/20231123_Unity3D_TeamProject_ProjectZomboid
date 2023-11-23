using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace WorldTime
{
    public class WorldTimeScript : MonoBehaviour
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;
        [SerializeField] private float _dayLength = 360f; //in second

        public TimeSpan _currentTime;             //using System;
        public float _minuteLength => _dayLength / WorldTimeConstans.MinutesInDay;

        private void Start()
        {
            StartCoroutine(AddMinute());
        }

        private IEnumerator AddMinute()
        {
            _currentTime += TimeSpan.FromMinutes(1);
            WorldTimeChanged?.Invoke(this, _currentTime);
            yield return new WaitForSeconds(_minuteLength);
            StartCoroutine(AddMinute());
        }
    }
}

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace WorldTime
{
    public class WorldTime : MonoBehaviour
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;
        [SerializeField] private float _dayLength; //in second

        private TimeSpan _currentTime;             //using System;
        private float _minuteLength => _dayLength / WorldTimeConstans.MinutesInDay;

        private void Start()
        {
            StartCoroutine(AddMinute());
        }

        private IEnumerator AddMinute()
        {
            _currentTime += TimeSpan.FromMinutes(1);
            WorldTimeChanged?.Invoke(this,_currentTime);
            yield return new WaitForSeconds(_minuteLength);
            StartCoroutine(AddMinute());
        }
    }
}

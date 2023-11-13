using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using System.Linq; 


namespace WorldTime
{
    public class TimeWorldWatcher : MonoBehaviour
    {
        [SerializeField] private WorldTime _worldTime;

        [SerializeField] private List<Schedule> _schedule;

        private void Start()
        {
           _worldTime.WorldTimeChanged += CheckSchedule;
        }

        private void OnDestroy()
        {
            _worldTime.WorldTimeChanged -= CheckSchedule;
        }

        private void CheckSchedule(object sender, TimeSpan newTime)
        {
            var schedule =
                _schedule.FirstOrDefault(sok=> sok.Hour ==
                   sok.Hour == newTime.Hours &&
                   schedule.Minute == newTime.Minute);

            schedule?._action?.Invoke();
        }
        [SerializeField] private class Schedule
        {
            public int Hour;
            public int Minute;
            public UnityEvent _action;
        }
    }
}


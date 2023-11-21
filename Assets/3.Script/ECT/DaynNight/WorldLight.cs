using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Experimental.Rendering.Universal;


namespace WorldTime
{
    public class WorldLight : MonoBehaviour
    {
        private Light _light;

        [SerializeField] private WorldTime _worldTime;
        [SerializeField] private Gradient _gradient;

        private void Awake()
        {
            TryGetComponent(out _worldTime);
            _light = GetComponent<Light>();
            _worldTime.WorldTimeChanged += OnWorldTimeChanged;
        }

        private void OnDestroy()
        {
            _worldTime.WorldTimeChanged -= OnWorldTimeChanged;
        }

        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
            _light.color = _gradient.Evaluate(PercentOfDay(newTime));
        }

        private float PercentOfDay(TimeSpan timeSpan)
        {
            return (float)timeSpan.TotalMinutes % WorldTimeConstans.MinutesInDay / WorldTimeConstans.MinutesInDay;
        }

    }

}

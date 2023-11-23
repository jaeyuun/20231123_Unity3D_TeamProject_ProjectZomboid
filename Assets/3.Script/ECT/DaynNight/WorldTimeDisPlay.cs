using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace WorldTime
{
    [RequireComponent(typeof(TMP_Text))]
    public class WorldTimeDisPlay : MonoBehaviour
    {
        [SerializeField] private WorldTimeScript _worldTime;

        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _worldTime.WorldTimeChanged += OnWolrdTimeChanged;
        }

        private void OnDestroy()
        {
            _worldTime.WorldTimeChanged -= OnWolrdTimeChanged;
        }

        private void OnWolrdTimeChanged(object sender, TimeSpan newTime)
        {
            _text.SetText(newTime.ToString(@"hh\:mm"));
        }    
    }
}



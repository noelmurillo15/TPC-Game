/*
* ComboSystem -
* Created by : Allan N. Murillo
* Last Edited : 5/19/2020
*/

using UnityEngine;

namespace ANM.Scriptables.Player
{
    [CreateAssetMenu(menuName = "Scriptables/Player/Combo System")]
    public class ComboSystem : ScriptableObject
    {
        [SerializeField] private bool _inCombo = false;
        [SerializeField] private float _timer;
        [SerializeField] private float _comboInterval;
        [SerializeField] private float _lastConfirmedHitTime;

        //    TODO : if player is in combo, add a damage multiplier that increases the longer the combo holds

        private void OnEnable()
        {
            _comboInterval = 1f;
            _inCombo = false;
            ResetTimer();
        }

        private void ResetTimer()
        {
            _timer = 0f;
        }

        public void ConfirmedHit()
        {
            //    Initial Combo
            if (!_inCombo)
            {
                _lastConfirmedHitTime = Time.timeSinceLevelLoad;
                _timer = _comboInterval;
                _inCombo = true;
                return;
            }

            _timer -= Time.timeSinceLevelLoad - _lastConfirmedHitTime;
            if (_timer <= 0)
            {
                //    TODO : Combo failed
                _inCombo = false;
                return;
            }

            //    Combo has been chained successfully
            _timer = _comboInterval;
            _lastConfirmedHitTime = Time.timeSinceLevelLoad;
        }

        //    TODO : use this to add a damage multiplier to combo chain hits
        public bool GetIsInCombo()
        {
            return _inCombo;
        }
    }
}

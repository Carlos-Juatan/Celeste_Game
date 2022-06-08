using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GameAssets.Characters.Player.Testing
{
    public class CronometerTime : MonoBehaviour
    {
		[SerializeField] TMP_Text _hightTimeText;
		[SerializeField] TMP_Text _totalTimeText;
		[SerializeField] TMP_Text _targetText;
		[SerializeField] TMP_Text _currentTimeText;

        float _currentTimer = 0f;
        List<float> _hightTimes = new List<float>();
        List<float> _totalTimes = new List<float>();
        List<bool> _targetTimes = new List<bool>();
        bool _StopTimer = true;
        bool _touchInTarget = false;
        bool _SetHigher = true;

        void Start() => _currentTimer = 0f;

        void Update() {
            
            // If press button
            if(Input.GetButtonDown("Jump")){
                _currentTimer = 0f;
                _StopTimer = false;
                _touchInTarget = false;
                _SetHigher = true;
            }

            // Set Current Text
            SetText(_currentTimeText, "Current Time", _currentTimer, null);

            // Set Current Time
            if(!_StopTimer){
                _currentTimer += Time.deltaTime;
            }
        }

        public void SetHightTime(){
            if(_SetHigher){
                _SetHigher = false;
                _hightTimes.Add(_currentTimer);
                SetText(_hightTimeText, "Hight Time", 0f, _hightTimes);
            }
        }

        public void SetTarget(bool hasTouch){
            if(!_touchInTarget){
                _touchInTarget = hasTouch;
                _targetTimes.Add(_touchInTarget);
                SetText(_targetText, "Target", 0f, null, _targetTimes);
            }
        }

        public void StopCounter(){
            if(!_StopTimer){
                if(!_touchInTarget){
                    SetTarget(false);
                }
                _StopTimer = true;
                _totalTimes.Add(_currentTimer);
                SetText(_totalTimeText, "Total Time", 0f, _totalTimes);
            }
        }

        void SetText(TMP_Text text, string msg, float fistTime, List<float> times = null, List<bool> targets = null){
            string setText = msg;

            if(fistTime != 0f){
                setText += "\n";
                setText += fistTime.ToString();
            }

            if(times != null){
                foreach (float time in times){
                    if(time > 0f){
                        setText += "\n";
                        setText += time.ToString();
                    }
                }
            }

            if(targets != null){
                foreach(bool target in targets){
                    setText += "\n";
                    setText += target.ToString();
                }
            }

            text.text = setText;
        }
    }
}
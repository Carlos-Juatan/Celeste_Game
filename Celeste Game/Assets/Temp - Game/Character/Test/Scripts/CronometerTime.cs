using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GameAssets.Characters.Player
{
    public class CronometerTime : MonoBehaviour
    {
		[SerializeField] TMP_Text _lasttimeText;
		[SerializeField] TMP_Text _currenttimeText;

        float _currentTimer = 0f;
        List<float> _allTimes = new List<float>();
        bool _StopTimer = true;
        bool _updateLastTime = true;

        void Start() => _currentTimer = 0f;

        void Update() {
            
            if(Input.GetButtonDown("Jump")){
                _currentTimer = 0f;
                _StopTimer = false;
            }

            SetText(_currenttimeText, "Current Time", _currentTimer, null);

            if(_StopTimer){
                if(_updateLastTime){
                    _allTimes.Add(_currentTimer);
                    SetText(_lasttimeText, "Last Time", 0f, _allTimes);
                    _updateLastTime = false;
                }

            }else{
                _currentTimer += Time.deltaTime;
            }
        }

        public void StopCounter(){
            _StopTimer = true;
            _updateLastTime = true;
        }

        void SetText(TMP_Text text, string msg, float fistTime, List<float> times = null){
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

            text.text = setText;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SystemManager.GameManagement;

namespace GameSystems.SimpleUI.GameplayUI
{
	public class Timer
	{
		float allTime = 0;
		int startTime = 120, sec = 0, min = 0;
		TimerType type;
		TMP_Text timeText;
		
		//delegate void UpdateType();
		//event UpdateType updateType;
		
		public delegate void CountdownStop();
		public static event CountdownStop countdownStop;

#region Contstructor Methods.
		public Timer(TimerType type, TMP_Text timeText, int startTime)
		{
			this.type = type;
			this.timeText = timeText;
			this.startTime = startTime;
			
			switch (type)
			{
				case TimerType.Desable:
					timeText.gameObject.SetActive(false);
					break;
				case TimerType.Count:
					allTime = 0;
					//StaticEventGameManager.gameManagerUpdateGameplay += Count;
					GameManager.instance.GameplayState.updateBase += Count;
					
					break;
				case TimerType.Countdown:
					allTime = this.startTime;
					countdownStop += Stop;
					//StaticEventGameManager.gameManagerUpdateGameplay += Countdown;
					GameManager.instance.GameplayState.updateBase += Countdown;
					
					break;
			}
		}
#endregion

#region External Reaction Methods.
		//public void UpdateTimer()
		//{
		//	updateType?.Invoke();
		//}
#endregion

#region Private Methods.
		void Count()
		{
			allTime += Time.deltaTime;
			Convert();
		}
		
		void Countdown()
		{
			allTime -= Time.deltaTime;
			Convert();
			if(allTime <= 0)
			{
				allTime = 0;
				countdownStop?.Invoke();
			}
		}
		
		void Convert()
		{
			sec = (int) (allTime + 1) % 60;
			min = (int) (allTime + 1) / 60;
			
			// Set screen text
			timeText.text = "Time: " +min.ToString("00") + ":" + sec.ToString("00");
			//timeText.text = string.Format("Time: {00}:{00}", min, sec);
			//timeText.text = "Time: " + min + ":" + sec;
		}
		
		void Stop()
		{
			ResetEvents();
			GameManager.instance.SwitchState(GameManager.instance.LoseState);
		}
		void ResetEvents()
		{
			allTime = startTime;
			countdownStop -= Stop;
			//StaticEventGameManager.gameManagerUpdateGameplay -= Count;
			//StaticEventGameManager.gameManagerUpdateGameplay -= Countdown;
			GameManager.instance.GameplayState.updateBase -= Count;
			GameManager.instance.GameplayState.updateBase -= Countdown;
		}
#endregion
	}

	public enum TimerType
	{
		Desable,
		Count,
		Countdown
	}
}
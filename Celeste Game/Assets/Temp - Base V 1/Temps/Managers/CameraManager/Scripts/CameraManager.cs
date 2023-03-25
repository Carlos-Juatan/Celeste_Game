using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal;

using GameAssets.Characters.Player;

namespace SystemManager.CameraManagement
{
	public class CameraManager : MonoBehaviour
	{
		CinemachineVirtualCamera _cinemachineVirtualCamera;
        PixelPerfectCamera _pixelPerfectCamera;
        PlayerController _playerController;
		LimitBounds _limitBounds;
		LimitBoundsReferece _boundsReferece;
		
		bool hasActivate;
		
		void OnEnable()
		{
			hasActivate = false;
			Check();
		}
		
		void Check()
		{
			if(_cinemachineVirtualCamera == null)
				StartCoroutine("FidingVirtualCamera");
			
			if(_pixelPerfectCamera == null)
				StartCoroutine("FidingPixelPerfect");
			
			if(_playerController == null)
				StartCoroutine("FidingPlayer");
			
			if(_cinemachineVirtualCamera != null & _pixelPerfectCamera != null & _playerController != null)
				if(!hasActivate)
					ApplyModifications();
		}
		
		void ApplyModifications()
		{
			hasActivate = true;
			Vector3 pos = _playerController.transform.position;
			pos.z = -10;
			_cinemachineVirtualCamera.transform.position = pos;
			_cinemachineVirtualCamera.Follow = _playerController.transform;
			_pixelPerfectCamera.enabled = true;
			
			_limitBounds = FindObjectOfType<LimitBounds>();
			_boundsReferece = FindObjectOfType<LimitBoundsReferece>();
			_limitBounds.SetBounds(_boundsReferece.GetBounds());
		}
		
		void OnDestroy()
		{
			if(_cinemachineVirtualCamera != null)
				_cinemachineVirtualCamera.transform.position = new Vector3(0, 0, -10);
			
			if(_pixelPerfectCamera != null)
				_pixelPerfectCamera.enabled = false;
		}
		
		IEnumerator FidingVirtualCamera()
		{
			do{
				_cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
				
				yield return null;
				
			}while(_cinemachineVirtualCamera == null);
				
			Check();
		}
		
		IEnumerator FidingPixelPerfect()
		{
			do{
				if(Camera.main != null)
					_pixelPerfectCamera = Camera.main.GetComponent<PixelPerfectCamera>();
				
				yield return null;
				
			}while(_pixelPerfectCamera == null);
				
			Check();
		}
		
		IEnumerator FidingPlayer()
		{
			do{
				
				_playerController = FindObjectOfType<PlayerController>();
				
				yield return null;
				
			}while(_playerController == null);
				
			Check();
		}
		
	}
}
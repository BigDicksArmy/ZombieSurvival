using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioSource audioSource;
	AudioClip lastAudioClip;
	AudioClip currentAudioClip;
	void Awake()
	{
		
		currentAudioClip = lastAudioClip;
	}
	AudioClip GetSound()
	{
		return EquipmentController.Instance.Current.Firearm.GetComponent<WeaponController>().clip;
	}
	void Update()
	{
		currentAudioClip = GetSound();
		if (lastAudioClip != currentAudioClip)
		{
			audioSource.clip = weaponSound;
			WeaponController.ShotEvent += PlaySound;
		}
	}
	void PlaySound()
	{
		audioSource.Play();
	}
}

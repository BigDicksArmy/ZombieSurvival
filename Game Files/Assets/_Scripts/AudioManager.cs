using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
	public AudioSource audioSource;

	public WeaponController Controller
	{
		get
		{
			return EquipmentController.Instance.Current.Firearm.GetComponent<WeaponController>();
		}
	}
	public void PlaySound(AudioClip clip)
	{
		audioSource.clip = clip;
		audioSource.Play();
	}
	void Update()
	{
		if (Controller != null)
		{
			if (audioSource.clip != Controller.Shooting)
			{
				audioSource.clip = Controller.Shooting;
				WeaponController._Shot = new WeaponAudio(PlaySound);
				WeaponController._Reload = new WeaponAudio(PlaySound);
			}
		}
	}
}

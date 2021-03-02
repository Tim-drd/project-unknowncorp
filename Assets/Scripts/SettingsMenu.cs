using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{

	Resolution[] resolutions;
	public Dropdown resolutionDropdown;

	//permet de récuperer toutes les résolutions possible de l'écran du joueur
	void Start()
	{
		//permet de récuperer toutes les résolutions possible de l'écran du joueur et d'éviter les doublons
		resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
		resolutionDropdown.ClearOptions();


		//ajoute des résolutions à la dropdown
		List<string> options = new List<string>();

		int currentResolutionIndex = 0;

		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = resolutions[i].width + "x" + resolutions[i].height;
			options.Add(option);

			if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
			{
				currentResolutionIndex = i;
			}
		}

		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();
	}

	public AudioMixer audioMixer;

	public void setMusic(float volume)
	{
		audioMixer.SetFloat("music", volume);
	}
	
	public void setSound(float volume)
	{
		audioMixer.SetFloat("sound", volume);
	}

	public void SetFullScreen(bool isFullScreen)
	{
		Screen.fullScreen = isFullScreen;
	}

	// appliquer la nouvelle résolution
	public void SetResolution(int resolutionIndex)
	{
		Screen.SetResolution(Screen.width, Screen.height, true);
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}


}

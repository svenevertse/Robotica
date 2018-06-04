using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

    public AudioMixer masterAudio;
    public AudioMixer musicAudio;

    public Dropdown resDropdown;
    public Dropdown graphDropDown;

    Resolution[] resolutions;

    void Start ()
    {

        GetResolutions();
        GetCurrentGraphics();

    }

	public void VolumeSetting (float volume)
    {

        masterAudio.SetFloat("volume", volume);

    }

    public void GraphicsSettings (int qualityIndex)
    {

        QualitySettings.SetQualityLevel(qualityIndex);

    } 

    public void SetResolution (int resIndex)
    {

        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }

    void GetResolutions ()
    {

        resolutions = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> resOptions = new List<string>();

        int currentResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {

            string option = resolutions[i].width + "*" + resolutions[i].height;
            resOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {

                currentResolution = i;

            }

        }

        resDropdown.AddOptions(resOptions);
        resDropdown.value = currentResolution;
        resDropdown.RefreshShownValue();

    }

    void GetCurrentGraphics ()
    {
       graphDropDown.value =  QualitySettings.GetQualityLevel();

    }
}

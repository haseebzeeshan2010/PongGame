using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SFXChanger : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider volumeSlider;
    [SerializeField] private AudioSource[] audioSources;

    private List<float> originalVolumes = new List<float>();

    private void Start()
    {

        foreach (var source in audioSources)
        {
            originalVolumes.Add(source.volume);
        }

    }
    public void ChangeAudioSourcesVolume()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = originalVolumes[i] * volumeSlider.value;
        }
    }


}

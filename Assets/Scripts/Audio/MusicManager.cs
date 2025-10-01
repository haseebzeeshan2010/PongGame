using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource MainTrack;
    [SerializeField] private AudioSource MellowTrack;


    void Start()
    {

        // Sync playback
        MainTrack.Play();
        MellowTrack.Play();
        GameVolume();
    }

    void Update()
    {
    }

    public void MenuVolume()
    {
        StartCoroutine(LerpVolumes(0f, 0.2f,2f));
    }

    public void GameVolume()
    {
        StartCoroutine(LerpVolumes(0.4f, 0.4f,5f));
    }

    IEnumerator LerpVolumes(float targetVolumeMain, float targetVolumeMellow, float duration)
    {
        float elapsed = 0f;

        // Store starting volumes
        float startVolumeMain = MainTrack.volume;
        float startVolumeMellow = MellowTrack.volume;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            MainTrack.volume = Mathf.Lerp(startVolumeMain, targetVolumeMain, elapsed / duration);
            MellowTrack.volume = Mathf.Lerp(startVolumeMellow, targetVolumeMellow, elapsed / duration);
            yield return null;
        }
        MainTrack.volume = targetVolumeMain;
        MellowTrack.volume = targetVolumeMellow;
    }
}

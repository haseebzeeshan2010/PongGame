using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class MuteToggle : MonoBehaviour
{
    [SerializeField] Sprite mutedSprite;
    [SerializeField] Sprite unmutedSprite;

    [SerializeField] UnityEngine.UI.Image imageComponent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMute()
    {
        AudioListener.pause = !AudioListener.pause;
        if (AudioListener.pause)
        {
            imageComponent.sprite = mutedSprite;
        }
        else
        {
            imageComponent.sprite = unmutedSprite;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private int[] indexArmClips;

    private bool isPlayKick = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }

    public void PlayClip(int index)
    {
        if ((index >= 0) && (index < clips.Length))
        {
            audioSource.clip = clips[index];
            audioSource.Play(); 
        }
    }

    public void PlayKick(int index)
    {
        if (isPlayKick) return;
        if ((index >= 0) && (index < indexArmClips.Length))
        {
            int indexClip = indexArmClips[index];
            if ((indexClip >= 0) && (indexClip < clips.Length))
            {
                audioSource.clip = clips[indexClip];
                audioSource.Play();
                isPlayKick = true;
                Invoke("EndPlayKick", 1.5f);
            }
        }
    }

    private void EndPlayKick()
    {
        isPlayKick = false;
    }
}

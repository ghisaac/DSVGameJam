using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;
    [Header("Custom Clips")]
    public CustomClip TableMelting;
    public CustomClip LavaSFX;
    public CustomClip CupStack;

    private void Awake()
    {
       if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayTableMelting(GameObject sourceOfSound)
    {
        AudioPlayer.Instance.Play3DSound(TableMelting, sourceOfSound);
    }

    public void PlayLavaLoop()
    {
        AudioPlayer.Instance.Play2DSound(LavaSFX);
    }
    public void PlayStackCup()
    {
        AudioPlayer.Instance.Play2DSound(CupStack);
    }

}

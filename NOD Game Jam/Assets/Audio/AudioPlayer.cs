using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance = null;

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

    public void Play2DSound(CustomClip clip)
    {
        PlayClip(clip);
    }

    public void Play3DSound(CustomClip clip, GameObject sourceOfAudio)
    {
        PlayClip(clip, sourceOfAudio);
    }

    public void PlayClip(CustomClip clip, GameObject sourceOfAudio = null)
    {
        GameObject customSoundClip = new GameObject();
        customSoundClip.name = "SFX(" + clip.name + ")";
        customSoundClip.AddComponent<AudioSource>();

        if (sourceOfAudio != null)
        {
            customSoundClip.transform.SetParent(sourceOfAudio.transform);
            customSoundClip.transform.localPosition = new Vector3(0f, 0f, 0f);
            customSoundClip.GetComponent<AudioSource>().spatialBlend = 1;
        }
        else
        {
            customSoundClip.GetComponent<AudioSource>().spatialBlend = 0;
        }
        clip.PlayCustomClip(customSoundClip.GetComponent<AudioSource>());
        if (!clip.Loop)
        {
            Instance.StartCoroutine(WaitAndDestroy(customSoundClip.GetComponent<AudioSource>()));
        }
    }

    private IEnumerator WaitAndDestroy(AudioSource source)
    {
        while (source.isPlaying)
        {
            yield return 0;
        }
        Destroy(source.gameObject);
        yield return 0;
    }
}

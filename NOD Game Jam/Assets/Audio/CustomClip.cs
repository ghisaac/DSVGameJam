using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "CustomClip", menuName = "ScriptableObjects/CustomClip")]
public class CustomClip : ScriptableObject
{
    public AudioClip[] AudioClips;
    [Range(0f, 1f)]
    public float Volume = 1f;
    [Range(0.05f, 2.5f)]
    public float Pitch = 1f;
    public float PitchVariance = 0f;
    public bool Loop = false;
    [Range(0f, 25f)]
    public float MinDistance = 1f;
    [Range(1f, 250f)]
    public float MaxDistance = 10;
    public AudioRolloffMode RolloffMode;
    [Header("If Custom Rolloff mode")]
    public AudioSourceCurveType CurveType;
    public AnimationCurve CustomCurve;

    private AudioSource _source;

    public void PlayCustomClip(AudioSource source)
    {
        source.pitch = Random.Range(Pitch - PitchVariance, Pitch + PitchVariance);
        source.volume = Volume;
        source.loop = Loop;
        source.dopplerLevel = 0;
        source.playOnAwake = false;
        source.clip = AudioClips[Random.Range(0, AudioClips.Length)];
        source.minDistance = MinDistance;
        source.maxDistance= MaxDistance;

        if (RolloffMode == AudioRolloffMode.Custom)
        {
            source.SetCustomCurve(CurveType, CustomCurve);
        }
        _source = source;
        source.Play();
    }
}

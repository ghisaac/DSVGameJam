using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASR_LightBlink : MonoBehaviour
{
    [System.Serializable]
    public struct LightStruct 
    {
        public Light[] Lights;
    }

    public LightStruct[] LightStructs;

    public float DelayBetweenLights = 1f;
    private float _delayTimer;

    private int _activeLightStructInt;

    public float LowerLightTime = 0.3f;


	private void Update()
	{
        _delayTimer += Time.deltaTime;

        if (_delayTimer >= DelayBetweenLights){

            StartCoroutine(LowerIntensity(LightStructs[_activeLightStructInt]));

            if (++_activeLightStructInt == LightStructs.Length){
                _activeLightStructInt = 0;
            }

            _delayTimer = 0f;
        }

	}

    private IEnumerator LowerIntensity(LightStruct activeLights)
    {

        List<float> startValues = new List<float>();

        for (int i = 0; i < activeLights.Lights.Length; i++){
            startValues.Add(activeLights.Lights[i].intensity);
        }

        float progress = 0;
        float timer = 0;

        while (progress <= 1f)
        {
            timer += Time.deltaTime;
            progress = timer / LowerLightTime;

            for (int i = 0; i < activeLights.Lights.Length; i++){
                activeLights.Lights[i].intensity = Mathf.Lerp(startValues[i], 0, progress);
            }

            yield return null;
        }

        progress = 0;
        timer = 0;

        while (progress <= 1f)
        {
            timer += Time.deltaTime;
            progress = timer / LowerLightTime;

            for (int i = 0; i < activeLights.Lights.Length; i++)
            {
                activeLights.Lights[i].intensity = Mathf.Lerp(0, startValues[i], progress);
            }
            yield return null;
        }

    }



}

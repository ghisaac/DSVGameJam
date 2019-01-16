using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIL_RandomLavaBubbles : MonoBehaviour
{
    public GameObject particleEffectList;
    private float _timer = 0;
    private float _randomNumber = 0;
    private int _currentIndex = 0;

    private void Start()
    {
        _randomNumber = Random.Range(0.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _randomNumber)
        {
            _randomNumber = Random.Range(0.0f, 2.0f);
            _timer = 0f;
            if (++_currentIndex >= particleEffectList.transform.childCount)
            {
                _currentIndex = 0;
                foreach(Transform child in particleEffectList.transform)
                {
                    child.gameObject.SetActive(false);
                }
                particleEffectList.transform.GetChild(_currentIndex).gameObject.SetActive(true);
            }
            else
            {
                particleEffectList.transform.GetChild(_currentIndex).gameObject.SetActive(true);
            }
        }
    }
}

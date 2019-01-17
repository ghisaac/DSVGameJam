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

    // går igenom en lista med partikel effekter och aktiverar de. Får det att se ut som att det slumpas fram bubblor etc. i lavan, men är determenistiskt.
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
                SoundManager.Instance.PlayPlayerLand();
            }
            else
            {
                particleEffectList.transform.GetChild(_currentIndex).gameObject.SetActive(true);
            }
        }
    }
}

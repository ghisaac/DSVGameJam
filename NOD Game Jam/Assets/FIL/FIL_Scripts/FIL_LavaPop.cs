using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIL_LavaPop : MonoBehaviour
{

    [SerializeField]
    private float _timeBeforeDestroy = 4f;

    private float _timer = 0f;

    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _timeBeforeDestroy)
        {
            Destroy(gameObject);
        }
    }
}

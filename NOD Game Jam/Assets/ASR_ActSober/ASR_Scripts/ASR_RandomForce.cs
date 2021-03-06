﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASR_RandomForce : MonoBehaviour
{
    //public Rigidbody[] _rigidbodies;
    public ASR_CharacterController[] Characters;
    public float StartForce;
    public float ForceMultiplier;
    private float _force;
    public float PushTime = 2f;
    [Header("Random Force MinMax time")]
    public float MinRandomTime;
    public float MaxRandomTime;

    //private IEnumerator _activeCoroutine;

    void Start()
    {
        _force = StartForce;
        StartCoroutine(RandomForceTimer());
    }

    public void ActivateForceGenerator()
    {
        ResetForce();
        StartCoroutine(RandomForceTimer());
        //_activeCoroutine = RandomForceTimer();
        //StartCoroutine(_activeCoroutine);
    }

    public void AddCharacters(ASR_CharacterController[] characters)
    {
        Characters = characters;
    }

    public void ResetForce()
    {
        _force = StartForce;
    }
    /*
    private void AddRandomForce()
    {
        float randomHorizontal = Random.value > 0.5 ? 1 : -1;

        foreach (Rigidbody rb in _rigidbodies)
        {

            rb.AddForce(rb.transform.right * randomHorizontal * _force, ForceMode.Impulse);
        }
        _force *= ForceMultiplier;
    }
    */

    private IEnumerator RandomForceTimer()
    {
        yield return new WaitForSeconds(Random.Range(MinRandomTime, MaxRandomTime));
        //AddRandomForce();
        yield return AddForceOverTime();
        StartCoroutine(RandomForceTimer());
    }

    private IEnumerator AddForceOverTime(){

        float randomHorizontal = Random.value > 0.5 ? 1 : -1;

        float timer = 0f;

        while (timer < PushTime){

            /*
            foreach (Rigidbody rb in _rigidbodies)
            {
                rb.AddForce(rb.transform.right * randomHorizontal * _force, ForceMode.Acceleration);
            }
            */

            foreach (ASR_CharacterController cc in Characters)
            {
                if (cc != null)
                    cc.AddForce(cc.transform.right * randomHorizontal * _force);
            }

            timer += Time.deltaTime;

            yield return null;

        }

        _force *= ForceMultiplier;

    }

}

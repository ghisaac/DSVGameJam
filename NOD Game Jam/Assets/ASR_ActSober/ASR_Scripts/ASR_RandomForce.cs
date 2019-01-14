﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASR_RandomForce : MonoBehaviour
{
    public Rigidbody[] _rigidbodies;
    public float StartForce;
    public float ForceMultiplier;
    private float _force;
    [Header("Random Force MinMax time")]
    public float MinRandomTime;
    public float MaxRandomTime;
    
    void Start()
    {
        _force = StartForce;
        StartCoroutine(RandomForceTimer());
    }

    void Update()
    {

    }

    private void AddRandomForce()
    {
        float randomHorizontal = Random.value > 0.5 ? 1 : -1;



        foreach (Rigidbody rb in _rigidbodies)
        {

            rb.AddForce(rb.transform.right * randomHorizontal * _force, ForceMode.Impulse);
        }
        _force *= ForceMultiplier;
    }

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

        while (timer < 2){
            
            foreach (Rigidbody rb in _rigidbodies)
            {
                rb.AddForce(rb.transform.right * randomHorizontal * _force, ForceMode.Acceleration);
            }
            Debug.Log("VArv");

            timer += Time.deltaTime;

            yield return null;

        }

        _force *= ForceMultiplier;

    }

}
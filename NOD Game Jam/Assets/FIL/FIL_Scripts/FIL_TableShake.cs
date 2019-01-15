using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FIL
{
    public class FIL_TableShake : MonoBehaviour
    {
        [SerializeField] private float shakeAmount;
        [SerializeField] private float shakeDuration;
        [SerializeField] private GameObject tableMesh;

        private float shakePercentage;
        private float startAmount;
        private float startDuration;
        private float amount;
        private float duration;
        private bool isRunning = false;

        private void Start()
        {
            amount = shakeAmount;
            duration = shakeDuration;
            tableMesh = transform.GetChild(0).gameObject;
        }

        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.Return) && !isRunning)
            {
                ShakeTable();
            }
        }

        public void ShakeTable()
        {
            shakeAmount += amount;
            startAmount = shakeAmount;
            shakeDuration += duration;
            startDuration = shakeDuration;

            if (!isRunning)
                StartCoroutine(Shake());
        }


        private IEnumerator Shake()
        {
            isRunning = true;

            while (shakeDuration > 0.01f)
            {
                Vector3 rotationAmount = UnityEngine.Random.insideUnitSphere * shakeAmount;
                rotationAmount.z = 0;

                shakePercentage = shakeDuration / startDuration;

                shakeAmount = startAmount * shakePercentage;
                shakeDuration = Mathf.Lerp(shakeDuration, 0, Time.deltaTime);

                tableMesh.transform.localRotation = Quaternion.Euler(rotationAmount);
                yield return null;
            }

            tableMesh.transform.localRotation = Quaternion.identity;
            isRunning = false;
        }

    }
}

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
        private float timeInTableZone = 3;

        [SerializeField]
        private float _drownSpeed = 0.005f;

        private Vector3 _startPos;

        private WaitForSeconds _waitForSeconds;

        private bool isShaking = false;

        private void Start()
        {
            amount = shakeAmount;
            duration = shakeDuration;
            tableMesh = transform.GetChild(0).gameObject;
            _startPos = transform.position;
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
            if (!isShaking)
            {
                isShaking = true;
                FIL_GameManager.instance.RemoveTableFromList(gameObject);
                shakeAmount += amount;
                startAmount = shakeAmount;
                shakeDuration += duration;
                startDuration = shakeDuration;

                if (!isRunning)
                    StartCoroutine(Shake());
            }
        }

        public void DrownTable(float wait)
        {
            Debug.Log("DrownTable()");
            _waitForSeconds = new WaitForSeconds(wait);
            StartCoroutine("Drown");
        }

        private IEnumerator Drown()
        {
            ShakeTable();
            yield return _waitForSeconds;
            while (true)
            {
                yield return new WaitForEndOfFrame();
                Vector3 newPos = transform.position - new Vector3(0, _drownSpeed * Time.deltaTime, 0);
                transform.position = newPos;

                if (transform.position.y < -2f)
                {
                    break;
                }
            }
            Instantiate(FIL_GameManager.instance._smokePrefab, _startPos, Quaternion.Euler(-90, 0, 0));
            GameObject bubble = Instantiate(FIL_GameManager.instance._lavaBubblePrefab, _startPos, Quaternion.identity);
            bubble.transform.localScale *= 2;
            yield return new WaitForEndOfFrame();
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

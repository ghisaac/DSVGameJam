using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FIL
{
    public class FIL_CheckLava : MonoBehaviour
    {
        [SerializeField]
        private LayerMask mask;
        private FIL_PlayerController controller;
        [SerializeField]
        private float _delayBeforeDrown = 3f;
        private float _timer = 0f;
        private bool isSinking = false;
        private float timer = 0f;
        private float sinkTime = 2f;

        private void Start()
        {
            controller = GetComponent<FIL_PlayerController>();
        }


        void Update()
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down, 0.7f, mask);
            if (hits.Length == 0)
            {
                _timer = 0f;
            }
            else
            {
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.gameObject.name == "LavaFloor" && !isSinking)
                    {
                        isSinking = true;
                        Debug.Log("hit lava");
                        GameObject bubble = Instantiate(FIL_GameManager.instance._lavaBubblePrefab, transform.position, Quaternion.identity);
                        bubble.transform.localScale *= 2;
                        SoundManager.Instance.PlayFallInLava();
                    }
                    else if (hit.collider.gameObject.layer == 9 && hit.collider.name == "Table")
                    {
                        _timer += Time.deltaTime;
                        if (_timer > _delayBeforeDrown)
                        {
                            _timer = 0f;
                            hit.collider.GetComponent<FIL_TableShake>().DrownTable(2f);
                        }
                    }
                }
            }
            if (isSinking)
                Sink();
        }

        private void Sink()
        {
            timer += Time.deltaTime;
            if(timer > sinkTime)
            {
                timer = 0f;
                isSinking = false;
                controller.LoseLife();
            }

        }
    }
}
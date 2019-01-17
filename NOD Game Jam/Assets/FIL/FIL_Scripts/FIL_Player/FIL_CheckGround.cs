using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FIL
{
    public class FIL_CheckGround : MonoBehaviour
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

        //kollar om spelaren landat i lava eller står på ett bord. Spelar en partikeleffekt och får spelaren att förlora ett liv. 
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
                        InLava();
                    }
                    else if (hit.collider.gameObject.layer == 9 && hit.collider.name == "Table")
                    {
                        OnTable(hit);
                    }
                }
            }
            if (isSinking)
                Sink();
        }

        //spelaren står på ett bord. sänker det bordwet om spelaren står på ett bord för länge.
        private void OnTable(RaycastHit hit)
        {
            _timer += Time.deltaTime;
            if (_timer > _delayBeforeDrown)
            {
                _timer = 0f;
                hit.collider.GetComponent<FIL_TableShake>().DrownTable(2f);
            }
        }

        //spelaren är i lava; spelar ett ljud och påbörjar en timer för att kalla på FIL_playerController.LoseLife().
        private void InLava()
        {
            isSinking = true;
            GameObject bubble = Instantiate(FIL_GameManager.instance._lavaBubblePrefab, transform.position, Quaternion.identity);
            bubble.transform.localScale *= 2;
            SoundManager.Instance.PlayFallInLava();
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
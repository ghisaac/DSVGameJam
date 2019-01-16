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

        private void Start()
        {
            controller = GetComponent<FIL_PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Physics.Raycast(transform.position, Vector3.down, 0.7f, mask))
            {
                controller.LoseLife();
                GameObject bubble = Instantiate(FIL_GameManager.instance._lavaBubblePrefab, transform.position, Quaternion.identity);
                bubble.transform.localScale *= 2;
            }
        }
    }
}
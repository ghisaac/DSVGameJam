using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FIL
{
    public class FIL_CheckLava : MonoBehaviour
    {
        [SerializeField]
        private LayerMask mask;

        // Update is called once per frame
        void Update()
        {
            if (Physics.Raycast(transform.position, Vector3.down, 0.7f, mask))
            {
                FIL_GameManager.instance.PlayerDeath(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}
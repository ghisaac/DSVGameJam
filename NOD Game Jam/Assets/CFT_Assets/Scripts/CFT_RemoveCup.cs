using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFT_RemoveCup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Cup"))
        {
            Destroy(other.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSR_PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<PlayerController>().animator;    
    }

    void Update()
    {
        
    }
}

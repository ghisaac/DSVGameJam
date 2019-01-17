using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class KSR_PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rewired.Player player;
    private Rigidbody rb;

    private float lastMagnitude;

    void Start()
    {
        anim = GetComponent<PlayerController>().animator;
        player = Rewired.ReInput.players.GetPlayer(GetComponent<PlayerController>().myPlayer.RewierdId);
        rb = GetComponent<Rigidbody>();
        lastMagnitude = rb.velocity.magnitude;
    }

    void Update()
    {
        anim.SetFloat("VelocityX", player.GetAxis("Horizontal"));
        anim.SetFloat("VelocityZ", player.GetAxis("Vertical"));

        float currentMagnitude = rb.velocity.magnitude;
        if (lastMagnitude > 6 && currentMagnitude < 4)
            anim.SetTrigger("Crash");
        lastMagnitude = currentMagnitude;
    }
}

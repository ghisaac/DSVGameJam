using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGR_Indicator : MonoBehaviour
{
    public ParticleSystem indicator;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        indicator = Instantiate(indicator, transform);
        indicator.startColor = playerController.myPlayer.PlayerColor;
    }
}

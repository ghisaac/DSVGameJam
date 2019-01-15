﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundFromHere : MonoBehaviour
{

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SoundManager.Instance.PlayTableMelting(gameObject);
        }
    }
}

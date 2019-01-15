using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play2DSoundFromHere : MonoBehaviour
{

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SoundManager.Instance.PlayLavaLoop();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Destroy(GameObject.Find("SFX(LavaLoop)").gameObject);
        }
    }
}

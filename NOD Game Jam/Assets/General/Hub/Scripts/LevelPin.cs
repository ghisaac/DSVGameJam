using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPin : MonoBehaviour
{
    [SerializeField]
    private int pinSceneIndex;

    public int GetSceneIndex()
    {
        return pinSceneIndex;
    }
}

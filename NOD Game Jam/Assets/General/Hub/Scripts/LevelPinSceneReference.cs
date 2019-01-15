using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelPinSceneReference : MonoBehaviour
{
    [SerializeField]
    private int pinSceneIndex;

    public int GetSceneIndex()
    {
        return pinSceneIndex;
    }
}

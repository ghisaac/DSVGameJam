using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelPinSceneReference : MonoBehaviour
{
    [SerializeField]
    private Scene pinScene;
    private int pinSceneIndex;

    private void Start()
    {
        pinSceneIndex = pinScene.buildIndex;
    }

    public int GetSceneIndex()
    {
        return pinSceneIndex;
    }
}

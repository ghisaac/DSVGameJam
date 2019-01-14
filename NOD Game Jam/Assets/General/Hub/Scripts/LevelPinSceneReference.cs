using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelPinSceneReference : MonoBehaviour
{
    public Scene myScene;
    public int mySceneIndex;

    public Scene GetMyScene()
    {
        return SceneManager.GetSceneByBuildIndex(mySceneIndex);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPin : MonoBehaviour
{
    public enum Level { FooBar, Cafeteria, L50, Entrance, GamesLab, L70 }

    [SerializeField]
    private Level myLevel;
    [SerializeField]
    private int levelBuildIndex;

    public int GetSceneIndex() { return levelBuildIndex; }

    public Level GetLevel() { return myLevel; }
}

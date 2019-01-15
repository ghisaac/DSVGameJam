using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPin : MonoBehaviour
{
    public enum Level { FooBar, Cafeteria, L50, Entrance, GamesLab, L70 }

    [SerializeField]
    private Level myLevel;
    [SerializeField]
    private Sprite myMapSprite;
    [SerializeField]
    private int levelBuildIndex;

    public Level GetLevel() { return myLevel; }
    public Sprite GetMapSprite() { return myMapSprite; }
    public int GetSceneIndex() { return levelBuildIndex; }


}

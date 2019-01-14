using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;
    [Header("UI")]
    public CustomClip ButtonHover;
    public CustomClip ButtonPress, GameSelect;
    [Header("Player General")]
    public CustomClip PlayerLand;
    public CustomClip PlayerRun, CollidePlayer, CollideTerrain;
    [Header("Floor is Lava")]
    public CustomClip TableMelting;
    public CustomClip TableShaking, LavaSFX;
    [Header("Kaffekopp Stacking")]
    public CustomClip CupStack;
    public CustomClip CupImpact;
    [Header("Kontorstols-racing")]
    public CustomClip TapFeet;
    public CustomClip SpeedBoost;
    [Header("Hela Havet Sneakar")]
    public CustomClip TeacherIdle;
    public CustomClip TeacherAlerted, StudentAskHelp, StudentDemandHelp, DuckFromSight, SitDown;
    [Header("Act Sober")]
    public CustomClip Burp;
    public CustomClip DrunkIdle, PlayerFall;

    private void Awake()
    {
       if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //UI
    public void PlayButtonHover()
    {
        AudioPlayer.Instance.Play2DSound(ButtonHover);
    }
    public void PlayButtonPress()
    {
        AudioPlayer.Instance.Play2DSound(ButtonPress);
    }
    public void PlayGameSelect()
    {
        AudioPlayer.Instance.Play2DSound(GameSelect);
    }

    //PLAYER GENERAL
    public void PlayPlayerRun(GameObject sourceOfSound)
    {
        AudioPlayer.Instance.Play3DSound(PlayerRun, sourceOfSound);
    }
    public void PlayPlayerLand(GameObject sourceOfSound)
    {
        AudioPlayer.Instance.Play3DSound(PlayerLand, sourceOfSound);
    }
    public void PlayCollidePlayer(GameObject sourceOfSound)
    {
        AudioPlayer.Instance.Play3DSound(CollidePlayer, sourceOfSound);
    }
    public void PlayCollideTerrain(GameObject sourceOfSound)
    {
        AudioPlayer.Instance.Play3DSound(CollideTerrain, sourceOfSound);
    }

    //FLOOR IS LAVA
    public void PlayTableMelting(GameObject sourceOfSound)
    {
        AudioPlayer.Instance.Play3DSound(TableMelting, sourceOfSound);
    }
    public void PlayTableShaking(GameObject sourceOfSound)
    {
        AudioPlayer.Instance.Play3DSound(TableShaking, sourceOfSound);
    }
    public void PlayLavaLoop()
    {
        AudioPlayer.Instance.Play2DSound(LavaSFX);
    }

    //KAFFEKOPP STACKING
    public void PlayCupStack()
    {
        AudioPlayer.Instance.Play2DSound(CupStack);
    }
    public void PlayCupFallen()
    {
        AudioPlayer.Instance.Play2DSound(CupImpact);
    }

    //KONTORSTOLSRACING
    public void PlaySpeedUp(GameObject sourceOfSound)
    {
        AudioPlayer.Instance.Play3DSound(TapFeet, sourceOfSound);
    }
    public void PlayBoost(GameObject sourceOfSound)
    {
        AudioPlayer.Instance.Play3DSound(SpeedBoost, sourceOfSound);
    }

    //HELA HAVET SNEAKAR
    public void PlayTeacherIdle()
    {
        AudioPlayer.Instance.Play2DSound(TeacherIdle);
    }
    public void PlayTeacherAlerted()
    {
        AudioPlayer.Instance.Play2DSound(TeacherAlerted);
    }
    public void PlayStudentAskHelp()
    {
        AudioPlayer.Instance.Play2DSound(StudentAskHelp);
    }
    public void PlayStudentDemandHelp()
    {
        AudioPlayer.Instance.Play2DSound(StudentDemandHelp);
    }
    public void PlayDuckFromSight()
    {
        AudioPlayer.Instance.Play2DSound(DuckFromSight);
    }
    public void PlaySitDown()
    {
        AudioPlayer.Instance.Play2DSound(SitDown);
    }

    //ACT SOBER 
    public void PlayBurp()
    {
        AudioPlayer.Instance.Play2DSound(Burp);
    }
    public void PlayDrunkIdle()
    {
        AudioPlayer.Instance.Play2DSound(DrunkIdle);
    }
    public void PlayPlayerFall()
    {
        AudioPlayer.Instance.Play2DSound(PlayerFall);
    }
}

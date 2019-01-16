using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;
    public CustomClip BackgroundMusic;
    [Header("UI")]
    public CustomClip ButtonHover;
    public CustomClip ButtonPress, GameSelect, ChainSwoop, ScrollSweep;
    [Header("General")]
    public CustomClip PlayerLand;
    public CustomClip PlayerRun, PlayerJump, CollidePlayer, CollideTerrain, RoundStart, GainPoints, TickTock, VictorySound;
    [Header("Floor is Lava")]
    public CustomClip TableMelting;
    public CustomClip TableShaking, LavaSFX, FallInLava;
    [Header("Kaffekopp Stacking")]
    public CustomClip CupStack;
    public CustomClip CupImpact, CoffeeSplash;
    [Header("Kontorstols-racing")]
    public CustomClip TapFeet;
    public CustomClip SpeedBoost, ChairRoll, Countdown;
    [Header("Hela Havet Sneakar")]
    public CustomClip TeacherIdle;
    public CustomClip TeacherAlerted, StudentAskHelp, StudentDemandHelp, DuckFromSight, SitDown, Caught;
    [Header("Act Sober")]
    public CustomClip Burp;
    public CustomClip DrunkIdle, PlayerFall;
    [Header("Geoguessr")]
    public CustomClip FoundSpot;

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

    //BGM
    public void PlayBGM() {
        AudioPlayer.Instance.Play2DSound(BackgroundMusic);
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
    public void PlayChainSwoop() {
        AudioPlayer.Instance.Play2DSound(ChainSwoop);
    }
    public void PlayScrollSweep() {
        AudioPlayer.Instance.Play2DSound(ScrollSweep);
    }

    //GENERAL
    public void PlayPlayerRun(GameObject sourceOfSound)
    {
        AudioPlayer.Instance.Play3DSound(PlayerRun, sourceOfSound);
    }
    public void PlayPlayerLand(GameObject sourceOfSound)
    {
        AudioPlayer.Instance.Play3DSound(PlayerLand, sourceOfSound);
    }
    public void PlayPlayerJump(GameObject sourceOfSound) {
        AudioPlayer.Instance.Play3DSound(PlayerJump, sourceOfSound);
    }
    public void PlayCollidePlayer(GameObject sourceOfSound)
    {
        AudioPlayer.Instance.Play3DSound(CollidePlayer, sourceOfSound);
    }
    public void PlayCollideTerrain(GameObject sourceOfSound)
    {
        AudioPlayer.Instance.Play3DSound(CollideTerrain, sourceOfSound);
    }
    public void PlayRoundStart()
    {
        AudioPlayer.Instance.Play2DSound(CollideTerrain);
    }
    public void PlayTicking()
    {
        AudioPlayer.Instance.Play2DSound(TickTock);
    }
    public void PlayVictorySound()
    {
        AudioPlayer.Instance.Play2DSound(VictorySound);
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
    public void PlayFallInLava(GameObject sourceOfSound)
    {
        AudioPlayer.Instance.Play3DSound(FallInLava, sourceOfSound);
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
    public void PlayCoffeeSplash()
    {
        AudioPlayer.Instance.Play2DSound(CoffeeSplash);
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
    public void PlayChairRolling(GameObject sourceOfSound)
    {
        AudioPlayer.Instance.Play3DSound(ChairRoll, sourceOfSound);
    }
    public void PlayCountdown()
    {
        AudioPlayer.Instance.Play2DSound(Countdown);
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
    public void PlayCaught()
    {
        AudioPlayer.Instance.Play2DSound(Caught);
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

    //GEOGUESSER
}

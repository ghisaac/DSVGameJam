using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScreen : MonoBehaviour
{

    [SerializeField] private Sprite[] icons;

    [SerializeField] private ScoreScreenPlayerScore[] playerScoreObjects;
    public bool testing = false;



    [Header("Animation")]
    [SerializeField] private AnimationCurve dropDownCurv;
    [SerializeField] private AnimationCurve liftDownCurv;


    [SerializeField]
    private Vector2 startPos, targetPos;

    [SerializeField]
    private float animationDuration;

    private float animationTimer;

    [SerializeField]
    private float sceneDuration;

    private float sceneTimer;

    private int animationDirection = 1;

    private bool hasPlayedChainSound = false;

    void Start()
    {
        if(testing)
            Player.SpawnTestPlayers(3);
        ActivateScores();
        SoundManager.Instance.PlayChainSwoop();
    }


    void Update()
    {
        animationTimer += Time.deltaTime * animationDirection;
        sceneTimer += Time.deltaTime;
        animationTimer = Mathf.Clamp(animationTimer, 0, animationDuration);
        float factor = animationTimer / animationDuration;
        DropDownAnimation(factor, animationDirection > 0 ? dropDownCurv : liftDownCurv);
        if (sceneTimer >= (sceneDuration - animationTimer))
        {
            if (!hasPlayedChainSound)
            {
                SoundManager.Instance.PlayChainSwoop();
                hasPlayedChainSound = true;
            }
            animationDirection = -1;

        }

        if (sceneTimer >= sceneDuration)
            SceneManager.LoadScene("HUB");
            
    }

    private void ActivateScores()
    {
        for(int i = 0; i < Player.AllPlayers.Count; i++)
        {
            Player tempPlayer = Player.GetPlayerAtPlacement(i);
            playerScoreObjects[i].SetValues(tempPlayer, icons[tempPlayer.RewierdId]);
        }
        for(int i = Player.AllPlayers.Count; i < playerScoreObjects.Length; i++)
        {
            playerScoreObjects[i].gameObject.SetActive(false);
        }
    }

    private void DropDownAnimation(float factor, AnimationCurve curve)
    {
        Vector2 delta = targetPos - startPos;
        transform.localPosition = startPos + delta * curve.Evaluate(factor);
    }
}

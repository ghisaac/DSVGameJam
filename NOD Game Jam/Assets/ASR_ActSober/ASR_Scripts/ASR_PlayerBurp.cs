using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASR_PlayerBurp : MonoBehaviour
{
    public GameObject BurpParticle;
    public float RandomTimeRange = 5f;
    private float _delayBetweenBurps;
    private float _timer = 0f;
    private ASR_CharacterController[] _activePlayers;
    public float TimeBetweenBurps;
    public bool RandomTimeBetweenBurps;

    private void Start()
    {
        //If bool RandomTimeBetweenBurps is set to true the burps come in an random timespan between 1 and whatever time the designer sets in the inspector
        if (RandomTimeBetweenBurps)
        {
            SetRandomDelayTime();
        }
        else
        {
            //If bool is false the burps come on a fixed timespan set by the designer in the inspector
            _delayBetweenBurps = TimeBetweenBurps;
        }
        _activePlayers = ASR_GameManager.Instance.GetAllCharacters();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        //Plays the burpsound and particle effect when the timer is same or larger than the delay between burps
        if(_timer >= _delayBetweenBurps)
        {
            
            RandomBurps();
            if (RandomTimeBetweenBurps)
            {
                SetRandomDelayTime();
            }
            else
            {
                _delayBetweenBurps = TimeBetweenBurps;
            }
            
            _timer = 0f;
        }


    }
    //Method to set the random delay between burps
    private void SetRandomDelayTime()
    {
        _delayBetweenBurps = Random.Range(1, RandomTimeRange);
    }

    //Method to play the burpsound and burp particle effect. Selects a random active player in the scene to play the burp from
    private void RandomBurps()
    {
        
        
        if(_activePlayers.Length > 0)
        {
            GameObject tempBurp = Instantiate(BurpParticle, _activePlayers[Random.Range(0, _activePlayers.Length)].BurpTransform);
            SoundManager.Instance.PlayBurp();

            //
            StartCoroutine(DestroyBurp(tempBurp));
            
        }
    }
    //Coroutine to destroy the instantiated burp particle effect
    private IEnumerator DestroyBurp(GameObject gO)
    {
        yield return new WaitForSeconds(3f);
        Destroy(gO);
    }

}

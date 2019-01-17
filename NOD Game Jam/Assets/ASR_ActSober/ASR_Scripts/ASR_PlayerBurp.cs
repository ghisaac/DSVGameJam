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
        if (RandomTimeBetweenBurps)
        {
            SetRandomDelayTime();
        }
        else
        {
            _delayBetweenBurps = TimeBetweenBurps;
        }
        _activePlayers = ASR_GameManager.Instance.GetAllCharacters();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

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

    private void SetRandomDelayTime()
    {
        _delayBetweenBurps = Random.Range(1, RandomTimeRange);
    }

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

    private IEnumerator DestroyBurp(GameObject gO)
    {
        yield return new WaitForSeconds(3f);
        Destroy(gO);
    }

}

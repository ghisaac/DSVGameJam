using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASR_RandomForce : MonoBehaviour
{
    
    public ASR_CharacterController[] Characters;
    public float StartForce;
    public float ForceMultiplier;
    private float _force;
    public float PushTime = 2f;
    [Header("Random Force MinMax time")]
    public float MinRandomTime;
    public float MaxRandomTime;

    

    void Start()
    {
        _force = StartForce;
        StartCoroutine(RandomForceTimer());
    }

    //Activates the random force generator
    public void ActivateForceGenerator()
    {
        ResetForce();
        StartCoroutine(RandomForceTimer());
        
    }

    //Method to add the active players in the scene to characterlist
    public void AddCharacters(ASR_CharacterController[] characters)
    {
        Characters = characters;
    }

    //Method to reset the force applied to players to the startingforce
    public void ResetForce()
    {
        _force = StartForce;
    }
    
    //Coroutine to add force to the players randomly over time.
    private IEnumerator RandomForceTimer()
    {
        yield return new WaitForSeconds(Random.Range(MinRandomTime, MaxRandomTime));
        
        yield return AddForceOverTime();
        StartCoroutine(RandomForceTimer());
    }

    //Coroutine to add push force on the players over a set amount of time
    private IEnumerator AddForceOverTime(){

        //Chooses random side from which the push force should be applied
        float randomHorizontal = Random.value > 0.5 ? 1 : -1;

        float timer = 0f;

        //Applies push force while timer is less than the PushTime
        while (timer < PushTime){

            

            foreach (ASR_CharacterController cc in Characters)
            {
                if (cc != null)
                    cc.AddForce(cc.transform.right * randomHorizontal * _force);
            }

            timer += Time.deltaTime;

            yield return null;

        }

        //Increases the amount of force by a factor set in the inspector that will be applied next time AddForceOverTime is called
        _force *= ForceMultiplier;

    }

}

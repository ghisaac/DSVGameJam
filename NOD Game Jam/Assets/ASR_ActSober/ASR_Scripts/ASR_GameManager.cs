using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ASR_GameManager : MonoBehaviour
{

    public static ASR_GameManager Instance;

    public Vector3[] StartPositions;

    public GameObject PlayerPrefab;

    public GameObject InstructionPanel;
    public float TimeForInstructions = 5f;
    public float CountdownTime = 3f;


    private List<ASR_CharacterController> _allCharacters = new List<ASR_CharacterController>();
    private List<ASR_CharacterController> _activeCharacters = new List<ASR_CharacterController>();
    private List<ASR_CharacterController> _characterPlacement = new List<ASR_CharacterController>();


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FindPlayers()
    {

        // Hitta spelare som är med


    }

    private void InitializePlayers()
    {

        foreach (Player player in Player.AllPlayers)
        {
            


        }

        // Instaciera spelarobjekt och koppla deras id till CharacterController

    }

    public void PlayerKnockedOut(ASR_CharacterController character)
    {
        _characterPlacement.Add(character);
        _activeCharacters.Remove(character);

    }

    private void ResetPlayers()
    {



    }

    private void StartGame()
    {
        foreach (ASR_CharacterController cc in _allCharacters){
            cc.ActivatePlayer();
        }
    }

    private IEnumerator ShowInstructions() 
    {
        InstructionPanel.SetActive(true);
        yield return new WaitForSeconds(TimeForInstructions);
        InstructionPanel.SetActive(false);

    }

    private IEnumerator CountdownToStart()
    {
        // Nedräkning eller nått
        yield return new WaitForSeconds(CountdownTime);

        StartGame();
    }


}

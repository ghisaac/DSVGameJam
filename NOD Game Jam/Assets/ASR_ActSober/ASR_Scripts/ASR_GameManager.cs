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

    public int AmountOfRounds = 3;
    private int _roundCounter = 0;


    private List<ASR_CharacterController> _allCharacters = new List<ASR_CharacterController>();
    private List<ASR_CharacterController> _activeCharacters = new List<ASR_CharacterController>();
    private List<ASR_CharacterController> _characterPlacement = new List<ASR_CharacterController>();
    private int[] _placementScores = { 1, 2, 3, 4 };

    private ASR_RandomForce _forceGenerator;


    [Header("DEBUGGING")]
    public bool Debug;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        _forceGenerator = FindObjectOfType<ASR_RandomForce>();
        InitializePlayers();
    }

    private void InitializePlayers()
    {
        if (Debug)
        {
            for (int i = 0; i < 4; i++)
            {
                ASR_CharacterController character = Instantiate(PlayerPrefab, StartPositions[i], Quaternion.identity).GetComponent<ASR_CharacterController>();
                _allCharacters.Add(character);
                character.enabled = false;
            }
        }

        for (int i = 0; i < Player.AllPlayers.Count; i++)
        {
            ASR_CharacterController character = Instantiate(PlayerPrefab, StartPositions[i], Quaternion.identity).GetComponent<ASR_CharacterController>();
            _allCharacters.Add(character);
            character.Player = Player.AllPlayers[i];
            character.enabled = false;
        }

    }

    public void PlayerKnockedOut(ASR_CharacterController character)
    {
        _characterPlacement.Add(character);
        _activeCharacters.Remove(character);

        if (_activeCharacters.Count == 1){
            RoundFinished();
        }
    }

    private void RoundFinished()
    {
        _characterPlacement.Add(_activeCharacters[0]);
        _activeCharacters[0].Deactivate();

        for (int i = 0; i < _characterPlacement.Count; i++)
        {
            _characterPlacement[0].AddScore(_placementScores[0]);
        }

        if (_roundCounter == AmountOfRounds){
            
        } else {
            
        }

    }

    private void CalculateWinner()
    {
        
    }

    private void ResetPlayers()
    {
        foreach (ASR_CharacterController cc in _allCharacters)
        {
            cc.OnRespawn();
        }
    }

    private void ResetLists()
    {
        _characterPlacement.Clear();
        _activeCharacters.Clear();
        _activeCharacters.AddRange(_allCharacters);
    }

    private void StartGame()
    {
        ResetLists();
        _forceGenerator.ResetForce();
        _roundCounter++;

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

    private IEnumerator Countdown()
    {
        // Nedräkning eller nått
        yield return new WaitForSeconds(CountdownTime);

    }

    private IEnumerator RoundFeedback()
    {
        // Uppdatera poäng eller nått
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator ActivateGame()
    {
        yield return ShowInstructions();

        yield return Countdown();

        _forceGenerator.ActivateForceGenerator();
        StartGame();
    }

    private IEnumerator RestartGame()
    {
        yield return RoundFeedback();

        ResetPlayers();

        yield return Countdown();
        StartGame();
    }

}

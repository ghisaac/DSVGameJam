using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ASR_GameManager : MonoBehaviour
{

    public static ASR_GameManager Instance;

    public ASR_UIManager UIManager;

    public Transform[] StartPositionTransforms;

    public GameObject PlayerPrefab;
    public GameObject[] PlayerPrefabs;

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
    public bool SpawnFourPlayers;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        _forceGenerator = FindObjectOfType<ASR_RandomForce>();
        InitializePlayers();
        StartCoroutine(ActivateGame());
    }

    private void InitializePlayers()
    {
        if (SpawnFourPlayers)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject instance = Instantiate(PlayerPrefab, StartPositionTransforms[i].position, Quaternion.identity);
                ASR_CharacterController character = instance.GetComponentInChildren<ASR_CharacterController>();
                _allCharacters.Add(character);
                character.Initialize();
            }
        } 
        else 
        {
            for (int i = 0; i < Player.AllPlayers.Count; i++)
            {
                GameObject instance = Instantiate(PlayerPrefab, StartPositionTransforms[i].position, Quaternion.identity);
                ASR_CharacterController character = instance.GetComponentInChildren<ASR_CharacterController>();
                _allCharacters.Add(character);
                character.Player = Player.AllPlayers[i];
                character.Initialize();
            }
        }

        _forceGenerator.AddCharacters(_allCharacters.ToArray());
        UIManager.FillUpDictionarys(_allCharacters.ToArray());
        StartCoroutine(StartCharacterAnimationsWithDelay());
    }

    public void PlayerKnockedOut(ASR_CharacterController character)
    {
        if (_activeCharacters.Count == 0)
            return;

        _characterPlacement.Add(character);
        _activeCharacters.Remove(character);

        UIManager.SetPlacementGui(character, _activeCharacters.Count + 1);

        if (_activeCharacters.Count == 1){
            RoundFinished();
        }
    }

    private void RoundFinished()
    {
        _characterPlacement.Add(_activeCharacters[0]);
        _activeCharacters[0].Deactivate();
        UIManager.SetPlacementGui(_activeCharacters[0], 1);

        Debug.Log("Round finished");

        for (int i = 0; i < _characterPlacement.Count; i++)
        {
            _characterPlacement[i].AddScore(_placementScores[i]);
        }

        UpdateScoreInUI();

        if (_roundCounter == AmountOfRounds){
            CalculateWinner();
        } else {
            StartCoroutine(RestartGame());
        }

    }

    private void UpdateScoreInUI()
    {
        foreach(ASR_CharacterController cc in _allCharacters)
        {
            UIManager.SetScoreGui(cc, cc.Score);
        }

    }

    private void CalculateWinner()
    {
        Debug.Log("Someone won!");

        _allCharacters.Sort((p2, p1) => p1.Score.CompareTo(p2.Score));

        foreach (ASR_CharacterController cc in _allCharacters){
            Debug.Log("Score = " + cc.Score);
        }

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
        //_roundCounter++;

        Debug.Log("StartRound");

        foreach (ASR_CharacterController cc in _allCharacters)
        {
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

        yield return UIManager.RoundCountdownTimer(++_roundCounter);

        _forceGenerator.ActivateForceGenerator();
        StartGame();
    }

    private IEnumerator RestartGame()
    {
        yield return Countdown();
        UIManager.InactivatePlacementGui();
        Debug.Log("Restart game");
        yield return RoundFeedback();

        ResetPlayers();

        yield return UIManager.RoundCountdownTimer(++_roundCounter);
        StartGame();
    }

    private IEnumerator StartCharacterAnimationsWithDelay()
    {
        float animationSpeedModifier = 0.6f;

        for (int i = 0; i < _allCharacters.Count; i++)
        {
            _allCharacters[i].CharacterAnimator.SetFloat("AnimationSpeed", animationSpeedModifier);

            yield return new WaitForSeconds(0.2f);

            animationSpeedModifier += 0.3f;
        }

    }

}

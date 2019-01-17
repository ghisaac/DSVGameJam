using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HHS_GameManager : MonoBehaviour {

    public static HHS_GameManager instance;

    [Header("Players")]
    [HideInInspector]
    public List<HHS_Player> activePlayers = new List<HHS_Player>();

    public GameObject[] PointsUI;


    public Transform[] startPosition;
    public SpriteRenderer[] goalIndicators;


    [Header("UI")]
    public float RoundTime = 60f;
    private bool roundIsActive = false;
    public TextMeshProUGUI TimeUI;
    public TextMeshProUGUI RoundCountDownTimerUI;

    [Header("Rounds")]
    public int waitForStartTimer = 3;
    public int MiniGameRounds = 3;
    [HideInInspector]
    private float roundTimer;
    private int goalCounter = 0;

    [Header("")]
    public List<GameObject> GoalChairs = new List<GameObject>();
    private List<GameObject> usedGoalChairs = new List<GameObject>();

    [Header("Teacher")]
    public HHS_Teacher Teacher;

    private void Awake() {
        if (instance != null) {
            Destroy(instance);
        }

        instance = this;
       // Player.SpawnTestPlayers(playersInGame);
    }
    private void Start() {
        InitializeGame();
    }

    private void AssignRandomChairs() {
        foreach (HHS_Player player in activePlayers) {
            GameObject goal = GoalChairs[Random.Range(0, GoalChairs.Count)];
            usedGoalChairs.Add(goal);
            GoalChairs.Remove(goal);
            player.SetGoal(goal);
            player.GetComponent<PlayerController>().stateMachine.TransitionToState<HHS_GroundState>();
        }
    }

    public void ResetGoals() {
        GoalChairs.AddRange(usedGoalChairs);
        usedGoalChairs.Clear();
        goalCounter = 0;
    }

    public void ResetRound() {
        ResetGoals();
        ResetPlayers();
        StartCoroutine(WaitForStartRound());
    }

    private void StartRound() {

        roundTimer = RoundTime;
        Timer.StartTimer(roundTimer);
        roundIsActive = true;
        Teacher.StartStudent();
        //Byt state på spelare (lås upp kontroller)
        AssignRandomChairs();

    }

    private void EndRound() {
        //Showpoints? LeaderBoard?
        --MiniGameRounds;
        foreach (HHS_Player player in activePlayers) {
            player.GetComponent<PlayerController>().stateMachine.TransitionToState<HHS_FrozenState>();
        }
        if (MiniGameRounds <= 0) {
            GameOver();
        }
        else {
            ResetRound();
        }
    }

    private void GameOver() {
        //Tilldela betyg
        //Visa Betyg
        //GO NEXT
        SortPlayersByPoints();
    }

    private void SortPlayersByPoints() {

        List<HHS_Player> finalScore = new List<HHS_Player>();
        finalScore.AddRange(activePlayers);

        finalScore.Sort(delegate(HHS_Player a, HHS_Player b) 
        { return (a.Points).CompareTo(b.Points);
        });
        finalScore.Reverse();

        for (int i = 0; i < finalScore.Count; i++)
        {
            Debug.Log(finalScore[i].PlayerID + " , " + finalScore[i].Points);
        }

        AwardPointsForTheGame(finalScore);

    }

    private void AwardPointsForTheGame(List <HHS_Player> sortedList) {

        Player[] players = new Player[activePlayers.Count];

        for (int i = 0; i < activePlayers.Count; i++) {
            players[i] = activePlayers[i].GetComponent<PlayerController>().myPlayer;
        }
        Player.DistributePoints(players);
    }

    private void ResetPlayers() {
        foreach (HHS_Player player in activePlayers) {
            //Lås spelare 
            //Sätta animationer
            player.ResetPosition();
        }
    }

    private void InitializeGame() 
    {
        //Instansiera mängden spelare i Player.AllPlayers.Count
        //Sätt dem på nån position och få dem att komma ihåg sin startposition.
        roundTimer = RoundTime;
       
        // loopa på Player.AllPlayers.Count

        for (int i = 0; i < CreatePlayer.allSpawnedPlayerControllers.Count; i++)
        {
          
            activePlayers.Add(CreatePlayer.allSpawnedPlayerControllers[i].GetComponent<HHS_Player>());
            activePlayers[i].GetComponent<HHS_Player>().PlayerID = i;
            activePlayers[i].GetComponent<HHS_Player>().Startposition = startPosition[i].position;
            activePlayers[i].GetComponent<HHS_Player>().goalindicator = goalIndicators[i];
            activePlayers[i].transform.position = activePlayers[i].Startposition;
            PointsUI[i].gameObject.SetActive(true);
        }
        StartCoroutine(WaitForStartRound());
    }

    private IEnumerator WaitForStartRound() 
    {
        RoundCountDownTimerUI.gameObject.SetActive(true);
        roundTimer = RoundTime;
        for (int i = waitForStartTimer; i > 0; i--) {
            RoundCountDownTimerUI.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        RoundCountDownTimerUI.text = "0";
        StartRound();
        yield return new WaitForSeconds(1f);
        RoundCountDownTimerUI.gameObject.SetActive(false);
    }

    public void PlayerReachedGoal(int playerIndex) {
        activePlayers[playerIndex].Points += (int)(roundTimer * 100);
        goalCounter++;
        if (goalCounter == activePlayers.Count) {
            roundIsActive = false;
            EndRound();
        }
    }
 

    private void Update() {
        if (roundIsActive) {
            roundTimer -= Time.deltaTime;
            if(roundTimer <= 0) {
                roundTimer = 0;
                roundIsActive = false;
                EndRound();
            }
        }


        
        for (int i = 0; i < Player.AllPlayers.Count; i++)
        {
            Player.AllPlayers[i].LocalScore = activePlayers[i].Points;
        }
        TimeUI.text = ((int)(roundTimer * 100)).ToString();
    }
}

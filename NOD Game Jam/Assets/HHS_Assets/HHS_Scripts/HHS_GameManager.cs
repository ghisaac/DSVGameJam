using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HHS_GameManager : MonoBehaviour {

    public static HHS_GameManager instance;

    [Header("Players")]
    public HHS_Player[] players;
    [HideInInspector]
    public List<HHS_Player> activePlayers = new List<HHS_Player>();
    public int playersInGame = 2;

    public TextMeshProUGUI[] PointsUI;



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
        }
    }

    public void ResetGoals() {
        GoalChairs.AddRange(usedGoalChairs);
        usedGoalChairs.Clear();
    }

    public void ResetRound() {
        ResetGoals();
        ResetPlayers();
        AssignRandomChairs();
        StartCoroutine(WaitForStartRound());
    }

    private void StartRound() {
        roundTimer = RoundTime;
        roundIsActive = true;
        StartCoroutine(Teacher.ActiveStudentQuestion());
        //Byt state på spelare (lås upp kontroller)
    }

    private void EndRound() {
        //Showpoints? LeaderBoard?
        --MiniGameRounds;
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
    }

    private void ResetPlayers() {
        foreach (HHS_Player player in players) {
            //Lås spelare 
            //Sätta animationer
            player.ResetPosition();
        }
    }

    private void InitializeGame() 
    {
        //Kolla hur många spelare som är aktiva
        roundTimer = RoundTime;

        for (int i = 0; i < playersInGame; i++) {
            players[i].gameObject.SetActive(true);
            activePlayers.Add(players[i]);
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

    public void PlayerReachedGoal(int player) {
        activePlayers[player - 1].Points += (int)roundTimer * 100;

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

        for(int i = 0; i < activePlayers.Count; i++) {
            PointsUI[i].text = activePlayers[i].Points.ToString();
        }
        TimeUI.text = ((int)(roundTimer * 100)).ToString();
    }
}

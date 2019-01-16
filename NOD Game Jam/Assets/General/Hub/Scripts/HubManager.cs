using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{
    private GameObject currentSelection;
    private Rewired.Player leavingPlayer;
    private int currentPinIndex;
    private bool selectOnCooldown;
    private bool waitingForConfirmation;
    private Image mapImage;

    [SerializeField] private AnimationCurve myCurve;
    [SerializeField] private float highlightAnimationDuration;
    private Vector2 startPos, targetPos;
    private float highlightAnimationTimer;


    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject textHighlighter;
    [SerializeField] private GameObject[] levelPins;
    [SerializeField] private List<string> randomNames; 

    void Start()
    {
        mapImage = map.GetComponent<Image>();
        currentPinIndex = 0;
        currentSelection = levelPins[currentPinIndex];
        selectOnCooldown = false;
    }

    void Update()
    {
        HighlightTimer();

        if (waitingForConfirmation)
        {
            HandleConfirmationInput(leavingPlayer);
            return;
        }

        HandleJoins();

        if (!selectOnCooldown)
        {
            HandleLevelSelection();
        }
    }

    private void HighlightTimer()
    {
        highlightAnimationTimer += Time.deltaTime;
        highlightAnimationTimer = Mathf.Clamp(highlightAnimationTimer, 0, highlightAnimationDuration);
        HighlightAnimation(highlightAnimationTimer / highlightAnimationDuration);

    }

    private void HighlightAnimation(float factor)
    {
        Vector2 delta = targetPos - startPos;
        textHighlighter.transform.position = startPos + (delta * myCurve.Evaluate(factor));
    }

    private void HandleJoins()
    {
        foreach (Rewired.Player p in ReInput.players.AllPlayers)
        {
            if (p.GetButtonDown("Start"))
            {
                if (!Player.CheckIfPlayerExists(p.id))
                {
                    new Player(p.id, AssignName());
                    Debug.Log("Player added: " + p.name);
                }
            }

            else if (p.GetButtonDown("Back"))
            {
                if (Player.CheckIfPlayerExists(p.id))
                {
                    leavingPlayer = p;
                    ToggleConfirmationPanel();
                }
            }
        }
    }

    private void HandleLevelSelection()
    {
        if (ReInput.players.AllPlayers[1].GetAxisRaw("Vertical") < 0)
        {
            currentPinIndex += 1;

            if (currentPinIndex >= levelPins.Length)
            {
                currentPinIndex = 0;
            }

            StartCoroutine(SelectionCooldown());
            DehighlightLevel();
            currentSelection = levelPins[currentPinIndex];
        }
        
        else if (ReInput.players.AllPlayers[1].GetAxisRaw("Vertical") > 0)
        {
            if (currentPinIndex == 0)
            {
                currentPinIndex = levelPins.Length;
            }

            currentPinIndex -= 1;

            StartCoroutine(SelectionCooldown());
            DehighlightLevel();
            currentSelection = levelPins[currentPinIndex];
        }

        if (ReInput.players.AllPlayers[1].GetButtonDown("A"))
        {
            if (Player.AllPlayers.Count < 2) { Debug.Log("Less than two players present"); return; }

            int currentSelectionSceneIndex = currentSelection.GetComponent<LevelPin>().GetSceneIndex();
            SceneManager.LoadScene(currentSelectionSceneIndex);
        }

        HighlightLevel();
    }

    private void HighlightLevel()
    {
        LevelPin currentLevelPin = currentSelection.GetComponent<LevelPin>();
        mapImage.sprite = currentLevelPin.GetMapSprite();
        startPos = textHighlighter.transform.position;
        targetPos = currentLevelPin.currentTextObject.transform.position;
        highlightAnimationTimer = 0;

        currentSelection.GetComponent<Image>().color = new Color(255, 0, 0);
    }

    private void DehighlightLevel()
    {
        currentSelection.GetComponent<Image>().color = new Color(20, 20, 20);
    }

    private string AssignName()
    {
        int randomIndex = Random.Range(0, randomNames.Count);
        string name = randomNames[randomIndex];

        foreach (Player p in Player.AllPlayers)
        {
            if (name == p.Name)
            {
                name = AssignName();
            }
        }

        return name;
    }

    private void ToggleConfirmationPanel()
    {
        confirmationPanel.SetActive(!confirmationPanel.activeSelf);
        waitingForConfirmation = !waitingForConfirmation;
    }

    private void HandleConfirmationInput(Rewired.Player player)
    {
        if (player.GetButtonDown("A"))
        {
            Player.RemovePlayer(player.id);
            Debug.Log("Player removed: " + player.name);
            ToggleConfirmationPanel();
        }

        if (player.GetButtonDown("B"))
        {
            ToggleConfirmationPanel();
        }
    }

    private IEnumerator SelectionCooldown()
    {
        selectOnCooldown = true;
        yield return new WaitForSeconds(0.32f);
        selectOnCooldown = false;
    }
}

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
        startPos = currentSelection.GetComponent<LevelPin>().currentTextObject.transform.position;
        SoundManager.Instance.PlayBGM();
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

        if (!selectOnCooldown && Player.AllPlayers.Count > 0 && Player.AllPlayers[0] != null)
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
                    PlayButtonSound();
                    new Player(p.id, AssignName());
                    Debug.Log("Player added: " + p.name);
                }
            }

            else if (p.GetButtonDown("Back"))
            {
                if (Player.CheckIfPlayerExists(p.id))
                {
                    PlayButtonSound();
                    leavingPlayer = p;
                    ToggleConfirmationPanel();
                }
            }
        }
    }

    private void HandleLevelSelection()
    {
        if (Player.AllPlayers[0].Input.GetAxisRaw("Vertical") < 0)
        {
            currentPinIndex += 1;

            if (currentPinIndex >= levelPins.Length)
            {
                currentPinIndex = 0;
            }

            StartCoroutine(SelectionCooldown());
            currentSelection = levelPins[currentPinIndex];
        }
        
        else if (Player.AllPlayers[0].Input.GetAxisRaw("Vertical") > 0)
        {
            if (currentPinIndex == 0)
            {
                currentPinIndex = levelPins.Length;
            }

            currentPinIndex -= 1;

            StartCoroutine(SelectionCooldown());
            currentSelection = levelPins[currentPinIndex];
        }

        if (Player.AllPlayers[0].Input.GetButtonDown("A"))
        {
            if (Player.AllPlayers.Count < 2) { Debug.Log("Less than two players present"); return; }
            StartCoroutine(StevenIsAight());
            int currentSelectionSceneIndex = currentSelection.GetComponent<LevelPin>().GetSceneIndex();

            ControlsShow.ShowControlsForGameid(currentPinIndex, () => { SceneManager.LoadScene(currentSelectionSceneIndex); });
            gameObject.SetActive(false);
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
    }

    private string AssignName()
    {
        int randomIndex = Random.Range(0, randomNames.Count);
        string name = randomNames[randomIndex];

        foreach (Player p in Player.AllPlayers)
        {
            if (name == p.Name) { name = AssignName(); }
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
            PlayButtonSound();
            Player.RemovePlayer(player.id);
            Debug.Log("Player removed: " + player.name);
            ToggleConfirmationPanel();
        }

        if (player.GetButtonDown("B"))
        {
            PlayButtonSound();
            ToggleConfirmationPanel();
        }
    }

    private void PlayButtonSound()
    {
        SoundManager.Instance.PlayButtonPress();
    }

    private IEnumerator SelectionCooldown()
    {
        selectOnCooldown = true;
        SoundManager.Instance.PlayScrollSweep();
        yield return new WaitForSeconds(0.32f);
        selectOnCooldown = false;
    }

    private IEnumerator StevenIsAight()
    {
        SoundManager.Instance.PlayGameSelect();
        yield return new WaitForSeconds(1.5f);
    }
}

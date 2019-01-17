using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ASR_UIManager : MonoBehaviour
{
    public TextMeshProUGUI RoundNumber, RoundCountdown, Winscreen;
    public TextMeshProUGUI[] PlayerScoreGUIs;
    public TextMeshProUGUI[] PlayerPlacementGuis;
    public int TestWinNumber;

    //Dictionaries to hold the score and placement UI for each active player
    private Dictionary<ASR_CharacterController, TextMeshProUGUI> _scoreGUIDictionary = new Dictionary<ASR_CharacterController, TextMeshProUGUI>();
    private Dictionary<ASR_CharacterController, TextMeshProUGUI> _placementGUIDictionary = new Dictionary<ASR_CharacterController, TextMeshProUGUI>();

    public GameObject[] Portraits;

    public GameObject ConfettiParticles;
    
    //Method to fill up the dictionaries with the active players in the scene, also activates each active players score UI
    public void FillUpDictionarys(ASR_CharacterController[] characterControllerList)
    {
        for(int i = 0; i < characterControllerList.Length; i++)
        {
            _scoreGUIDictionary.Add(characterControllerList[i], PlayerScoreGUIs[characterControllerList[i].Player.RewierdId]);
            _placementGUIDictionary.Add(characterControllerList[i], PlayerPlacementGuis[characterControllerList[i].Player.RewierdId]);

            SetScoreGuiActive(characterControllerList[i]);
        }
    }

    //Method to set players score in the UI
    public void SetScoreGui(ASR_CharacterController characterController, int score)
    {
        _scoreGUIDictionary[characterController].SetText("Score: {0}", score);
    }
    
    //Method to set players placement in the UI, also activates the placement UI
    public void SetPlacementGui(ASR_CharacterController characterController, int placement)
    {
        switch (placement)
        {
            case 1:
                _placementGUIDictionary[characterController].SetText("1st");
                break;
            case 2:
                _placementGUIDictionary[characterController].SetText("2nd");
                break;
            case 3:
                _placementGUIDictionary[characterController].SetText("3rd");
                break;
            default:
                _placementGUIDictionary[characterController].SetText("4th");
                break;
        }
        _placementGUIDictionary[characterController].gameObject.SetActive(true);

        //_placementGUIDictionary[characterController].SetText("Placement: {0}", placement);
        //_placementGUIDictionary[characterController].gameObject.SetActive(true);
    }

    //Method to set a players portrait and score active in the UI
    public void SetScoreGuiActive(ASR_CharacterController characterController)
    {
        Portraits[characterController.Player.RewierdId].SetActive(true);
        _scoreGUIDictionary[characterController].gameObject.SetActive(true);
    }

    //Method to set a players score UI inactive
    public void SetScoreGuiInActive(ASR_CharacterController characterController)
    {
        _scoreGUIDictionary[characterController].gameObject.SetActive(false);
    }

    //Method to set a players placement UI Active
    public void SetPlacementGUIActive(ASR_CharacterController characterController)
    {
        _placementGUIDictionary[characterController].gameObject.SetActive(true);
    }

    //Method to set a players placement UI Inactive
    public void SetPlacementGUIInActive(ASR_CharacterController characterController)
    {
        _placementGUIDictionary[characterController].gameObject.SetActive(false);
    }

    //Method to inactivate all placement UI:s active on screen
    public void InactivatePlacementGui()
    {
        for(int i = 0; i < PlayerPlacementGuis.Length; i++)
        {
            PlayerPlacementGuis[i].gameObject.SetActive(false);
        }
    }

    //Method to get a players score UI
    public TextMeshProUGUI GetScoreGUI(ASR_CharacterController characterController)
    {
        return _scoreGUIDictionary[characterController];
    }

    //Method to get a players placement UI
    public TextMeshProUGUI GetPlacementGUI(ASR_CharacterController characterController)
    {
        return _placementGUIDictionary[characterController];
    }

    //Coroutine to activate the round UI and countdown UI and start the countdown
    public IEnumerator RoundCountdownTimer(int roundNumber)
    {
        RoundNumber.SetText("Round {0}", roundNumber);
        RoundNumber.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        SoundManager.Instance.PlayCountdown();
        RoundCountdown.SetText("3");
        RoundCountdown.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        RoundCountdown.SetText("2");
        yield return new WaitForSeconds(1);
        RoundCountdown.SetText("1");
        yield return new WaitForSeconds(1);
        RoundNumber.gameObject.SetActive(false);
        RoundCountdown.gameObject.SetActive(false);

    }

    //Coroutine to activate the winscreen when all rounds are completed
    public IEnumerator WinScreenCoroutine(string playerName)
    {
        SoundManager.Instance.PlayVictorySound();
        ConfettiParticles.SetActive(true);
        Winscreen.SetText(playerName + " Wins!");
        Winscreen.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(3f);

        Winscreen.gameObject.SetActive(false);
    }
}

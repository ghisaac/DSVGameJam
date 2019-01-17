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

    private Dictionary<ASR_CharacterController, TextMeshProUGUI> _scoreGUIDictionary = new Dictionary<ASR_CharacterController, TextMeshProUGUI>();
    private Dictionary<ASR_CharacterController, TextMeshProUGUI> _placementGUIDictionary = new Dictionary<ASR_CharacterController, TextMeshProUGUI>();

    public GameObject[] Portraits;

    public void FillUpDictionarys(ASR_CharacterController[] characterControllerList)
    {
        for(int i = 0; i < characterControllerList.Length; i++)
        {
            _scoreGUIDictionary.Add(characterControllerList[i], PlayerScoreGUIs[characterControllerList[i].Player.RewierdId]);
            _placementGUIDictionary.Add(characterControllerList[i], PlayerPlacementGuis[characterControllerList[i].Player.RewierdId]);

            SetScoreGuiActive(characterControllerList[i]);
        }
    }

    public void SetScoreGui(ASR_CharacterController characterController, int score)
    {
        _scoreGUIDictionary[characterController].SetText("Score: {0}", score);
    }

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

    public void SetScoreGuiActive(ASR_CharacterController characterController)
    {
        Portraits[characterController.Player.RewierdId].SetActive(true);
        _scoreGUIDictionary[characterController].gameObject.SetActive(true);
    }

    public void SetScoreGuiInActive(ASR_CharacterController characterController)
    {
        _scoreGUIDictionary[characterController].gameObject.SetActive(false);
    }

    public void SetPlacementGUIActive(ASR_CharacterController characterController)
    {
        _placementGUIDictionary[characterController].gameObject.SetActive(true);
    }

    public void SetPlacementGUIInActive(ASR_CharacterController characterController)
    {
        _placementGUIDictionary[characterController].gameObject.SetActive(false);
    }

    public void InactivatePlacementGui()
    {
        for(int i = 0; i < PlayerPlacementGuis.Length; i++)
        {
            PlayerPlacementGuis[i].gameObject.SetActive(false);
        }
    }


    public TextMeshProUGUI GetScoreGUI(ASR_CharacterController characterController)
    {
        return _scoreGUIDictionary[characterController];
    }

    public TextMeshProUGUI GetPlacementGUI(ASR_CharacterController characterController)
    {
        return _placementGUIDictionary[characterController];
    }

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

    public IEnumerator WinScreenCoroutine(int playerWinNumber)
    {
        SoundManager.Instance.PlayVictorySound();
        Winscreen.SetText("Player {0} Wins!", playerWinNumber);
        Winscreen.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(3f);

        Winscreen.gameObject.SetActive(false);
    }
}

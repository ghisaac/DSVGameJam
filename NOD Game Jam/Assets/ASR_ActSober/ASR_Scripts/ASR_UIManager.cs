using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ASR_UIManager : MonoBehaviour
{
    public TextMeshProUGUI RoundNumber, RoundCountdown, Winscreen;
    public int TestWinNumber;

    private Dictionary<ASR_CharacterController, TextMeshProUGUI> _scoreGUIDictionary = new Dictionary<ASR_CharacterController, TextMeshProUGUI>();
    private Dictionary<ASR_CharacterController, TextMeshProUGUI> _placementGUIDictionary = new Dictionary<ASR_CharacterController, TextMeshProUGUI>();

    public IEnumerator RoundCountdownTimer(int roundNumber)
    {
        RoundNumber.SetText("Round {0}", roundNumber);
        RoundNumber.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);

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
        Winscreen.SetText("Player {0} Wins!", playerWinNumber);
        Winscreen.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(3f);

        Winscreen.gameObject.SetActive(false);
    }
}

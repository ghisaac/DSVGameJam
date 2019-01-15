using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ASR_UIManager : MonoBehaviour
{
    public TextMeshProUGUI RoundNumber, RoundCountdown, Winscreen;
    public int TestWinNumber;

    // Start is called before the first frame update
    void Start()
    {
        /*
        RoundNumber.enabled = false;
        RoundCountdown.enabled = false;
        Winscreen.enabled = false;
        */
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ActivateWinScreen(TestWinNumber);
        }
    }

    public void ActivateWinScreen(int playerWinNumber)
    {
        Winscreen.SetText("Player {0} Wins!", playerWinNumber);
        StartCoroutine(WinScreenCoroutine());
    }

    private IEnumerator WinScreenCoroutine()
    {
        Debug.Log("Entered winscreen activate");
        Winscreen.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(3f);

        Winscreen.gameObject.SetActive(false);
    }
}

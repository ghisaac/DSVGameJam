using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using TMPro;
using UnityEngine.UI;

public class ControlsShow : MonoBehaviour
{
    public Sprite[] Controls;
    public TextMeshProUGUI[] AllPlayerNames;
    public GameObject ActualImageGuy;
    public Action callBack;
    public HashSet<int> ReadyPlayers = new HashSet<int>();
    public bool isActive = false;
    private static ControlsShow instance;

    //Instance setting
    public void Awake()
    {
        instance = this;
    }

    //Update stuff
    public void Update()
    {
        if (!isActive) return;
        AddPressingPlayers();
        EveryoneWasPressed();
    }
    private void AddPressingPlayers()
    {
        for (int i = 0; i < Player.AllPlayers.Count; i++)
        {
            int reid = Player.AllPlayers[i].RewierdId;
            if (ReInput.players.GetPlayer(reid).GetButtonDown("A") && !ReadyPlayers.Contains(reid))
            {
                ReadyPlayers.Add(reid);
                AllPlayerNames[reid].text = Player.GetPlayerByRewindID(reid).Name;
                AllPlayerNames[reid].gameObject.SetActive(true);
            }
        }
    }
    private void EveryoneWasPressed()
    {
        if (ReadyPlayers.Count >= Player.AllPlayers.Count)
            callBack?.Invoke();
    }

    //Static publics
    public static void ShowControlsForGameid(int game, Action callbackWhenStop)
    {
        Debug.Log("index: " + game);
        for (int i = 0; i < instance.AllPlayerNames.Length; i++)
            instance.AllPlayerNames[i].gameObject.SetActive(false);
        instance.ReadyPlayers.Clear();
        instance.ActualImageGuy.SetActive(true);
        instance.ActualImageGuy.GetComponent<Image>().sprite = instance.Controls[game];
        instance.callBack = callbackWhenStop;
        instance.isActive = true;
    }
    public static void HideControls()
    {
        for (int i = 0; i < instance.AllPlayerNames.Length; i++)
            instance.AllPlayerNames[i].gameObject.SetActive(false);
        instance.ReadyPlayers.Clear();
        instance.ActualImageGuy.SetActive(false);
        instance.callBack = null;
        instance.isActive = false;

    }
}

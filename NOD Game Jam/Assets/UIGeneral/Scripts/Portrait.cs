using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Portrait : MonoBehaviour
{
    [SerializeField] private Image ImageSprite;
    [SerializeField] private TextMeshProUGUI profileName;
    [SerializeField] private Image profileImage;
    [SerializeField] private TextMeshProUGUI points;
    [SerializeField] private GameObject profile;

    [SerializeField]
    private int ID;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!Player.CheckIfPlayerExists(0))
        {
            profile.SetActive(false);
            return;
        }
        else
        {
            profile.SetActive(true);
        }

        profileName.text = Player.GetPlayerByRewindID(ID).Name;
        points.text = "" + Player.GetPlayerByRewindID(ID).Points;
        ID = Player.GetPlayerByRewindID(ID).RewierdId;
    
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScreenPlayerScore : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI placement, name, score;
    [SerializeField] private Image portrait;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetValues(Player player)
    {
        placement.text = "" + (Player.GetPlayerPlacementByID(player.RewierdId) + 1);
        name.text = player.Name;
        score.text = "" + player.Points;
    }
}

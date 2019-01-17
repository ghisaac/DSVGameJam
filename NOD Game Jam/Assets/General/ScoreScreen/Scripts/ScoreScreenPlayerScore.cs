using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScreenPlayerScore : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI placement, name, score;
    [SerializeField] private Image portrait;


    public void SetValues(Player player, Sprite sprite)
    {
        placement.text = "" + (Player.GetPlayerPlacementByID(player.RewierdId) + 1);
        name.text = player.Name;
        score.text = "" + player.Points;
        portrait.sprite = sprite;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        placement.text = "";
        name.text = "";
        score.text = "";
        portrait.sprite = null;
        gameObject.SetActive(false);
    }
}

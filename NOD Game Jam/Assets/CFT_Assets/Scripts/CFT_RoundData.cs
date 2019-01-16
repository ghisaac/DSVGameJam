using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFT_RoundData
{
    public float range = 0.25f;
    public Dictionary<int, int[]> placements = new Dictionary<int, int[]>();
    int _placement = 0;

    public void SetRoundPlacement(Dictionary<int, float> playerList)
    {
        _placement++;
        List<int> winner = new List<int>();
        float highest = GetHighest(playerList);

        Dictionary<int, float> loosers = new Dictionary<int, float>();

        foreach (KeyValuePair<int, float> _playerAndScore in playerList)
        {
            if (_playerAndScore.Value >= (highest - range))
            {
                winner.Add(_playerAndScore.Key); 
            }
            else
            {
                loosers.Add(_playerAndScore.Key, _playerAndScore.Value);
            }         
        }

        placements.Add(_placement, winner.ToArray());

        if (loosers.Count > 0)
        {
            SetRoundPlacement(loosers);
        }

    }

    public float GetHighest(Dictionary<int, float> playerList)
    {
        float highest = 0;
        foreach (KeyValuePair<int, float> _playerAndScore in playerList)
        {
            if (_playerAndScore.Value >= highest)
            {
                highest = _playerAndScore.Value;
            }

        }
        return highest;
    }

    public int[] GetPlacement(int placement)
    {
        if (placements.ContainsKey(placement))
            return placements[placement];
        else
            return null;
    }
}

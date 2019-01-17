using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFT_PlayerData : IEquatable<CFT_PlayerData>
{
    public int Id { get; set; }
    public int Score { get; set; }
    public int TotalPlacement { get; set; }

    public CFT_PlayerData(int id)
    {
        this.Id = id;
    }

    public bool Equals(CFT_PlayerData other)
    {
        return this.Id == other.Id;
    }
}

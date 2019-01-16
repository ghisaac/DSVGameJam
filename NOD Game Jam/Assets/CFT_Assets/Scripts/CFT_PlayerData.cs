using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFT_PlayerData : IComparable<CFT_PlayerData>
{
    public int Id { get; set; }
    public int Score { get; set; }

    public CFT_PlayerData(int id)
    {
        this.Id = id;
    }

    public int CompareTo(CFT_PlayerData other)
    {
        return this.Id.CompareTo(other.Id);
    }
}

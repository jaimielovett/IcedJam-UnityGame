using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward {

    public string Name { get; set; }
    public int Cost { get; set; }
    public bool IsUnlocked { get; set; }

    public Reward(string name, int cost, bool isUnlocked)
    {
        Name = name;
        Cost = cost;
        IsUnlocked = isUnlocked;
    }
}

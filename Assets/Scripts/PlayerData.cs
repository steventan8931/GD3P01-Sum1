﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public string m_Name = "";
    [Range(0,10)]
    public int m_HP = 0;
    [Range(0, 10)]
    public int m_AttackDamage = 0;
    [Range(0, 10)]
    public int m_MoveSpeed = 0;
    public GameObject m_Weapon;
}

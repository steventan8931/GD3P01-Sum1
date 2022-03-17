using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public GameObject m_ProjectilePrefab;

    public override void Attack(Transform _HandPos, PlayerData _Data)
    {
        GameObject obj = Instantiate(m_ProjectilePrefab, _HandPos);
        obj.GetComponent<WeaponProjectile>().m_Damage = _Data.m_AttackDamage;
        obj.transform.parent = null;
    }
}

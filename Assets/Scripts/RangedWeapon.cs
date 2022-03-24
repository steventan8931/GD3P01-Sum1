using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    //Projectile prefab
    public GameObject m_ProjectilePrefab;

    public override void Attack(Transform _HandPos, PlayerData _Data)
    {
        //Spawn the projectile prefab 
        GameObject obj = Instantiate(m_ProjectilePrefab, _HandPos);
        //Update the projectile's damage with the player data's attack damage
        obj.GetComponent<WeaponProjectile>().m_Damage = _Data.m_AttackDamage;
        obj.transform.parent = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public override void Attack(Transform _HandPos, PlayerData _Data)
    {
        //Create a spherecast to check if the enemy is in range
        Collider[] objects = Physics.OverlapSphere(_HandPos.position, 3.0f);

        foreach (Collider hit in objects)
        {
            //If there enemy is in range then deal damage
            if (hit.GetComponent<TrainingDummy>())
            {
                hit.GetComponent<TrainingDummy>().TakeDamage(_Data.m_AttackDamage);
            }
        }
    }
}

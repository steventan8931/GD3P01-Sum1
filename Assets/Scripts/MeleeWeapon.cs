using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public override void Attack(Transform _HandPos, PlayerData _Data)
    {
        _HandPos.GetChild(0).GetComponent<Animator>().ResetTrigger("Attack");
        _HandPos.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");

        Collider[] objects = Physics.OverlapSphere(_HandPos.position, 3.0f);

        foreach (Collider hit in objects)
        {
            if (hit.GetComponent<TrainingDummy>())
            {
                hit.GetComponent<TrainingDummy>().TakeDamage(_Data.m_AttackDamage);
            }
        }
    }
}

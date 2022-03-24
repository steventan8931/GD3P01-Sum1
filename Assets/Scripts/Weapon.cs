using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Virtual Function to be called and overriden by the different weapons
    public virtual void Attack(Transform _HandPos, PlayerData _Data)
    {
    }
}

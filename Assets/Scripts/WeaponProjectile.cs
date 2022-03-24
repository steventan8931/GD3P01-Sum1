using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    //Damage of the bullet to the enemy
    public int m_Damage = 0;
    //Projectile Speed
    public float m_MoveSpeed = 10f;
    //Timer to increment for destorying the object
    private float m_DecayTimer = 0.0f;

    private void Update()
    {
        //Add to decay timer
        m_DecayTimer += Time.deltaTime;

        //Move forward in facing direction
        transform.position += transform.forward * m_MoveSpeed * Time.deltaTime;

        //Check for sphere overlap
        Collider[] objects = Physics.OverlapSphere(transform.position, 1.0f);

        foreach (Collider hit in objects)
        {
            //If it hits eneny, deal damage and destroy self
            if (hit.GetComponent<TrainingDummy>())
            {
                hit.GetComponent<TrainingDummy>().TakeDamage(m_Damage);
                Destroy(gameObject);
            }
        }

        //If is logner than the decay time destory object
        if (m_DecayTimer > 5.0f)
        {
            Destroy(gameObject);
        }
    }


}

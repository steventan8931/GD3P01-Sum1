using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    //Character Movemenet
    private Vector3 m_Movement = new Vector2(0, 0);
    private Rigidbody m_Rigid;

    public Transform m_Feet;
    public LayerMask m_GroundMask;

    //Player Class
    public PlayerData m_Data;
    public Transform m_HandPosition;
    public bool m_Attacked = false;
    public float m_AttackTimer = 0.0f;

    //Character Stats
    public int m_CurrentHP = 0;
    public int m_MaxHP = 0;

    //UI 
    public RectTransform m_Canvas;
    public Text m_CurrentHPText;
    public Text m_MaxHpText;

    private void Start()
    {
        //Assign variables
        m_Rigid = GetComponent<Rigidbody>();
        m_MaxHP = m_Data.m_HP;
        m_CurrentHP = m_MaxHP;
        m_CurrentHPText.text = m_MaxHP.ToString();
        m_MaxHpText.text = m_MaxHP.ToString();
    }

    private void Update()
    {
        MovePlayer();
        Attack();
    }

    //Controls Character Movement
    private void MovePlayer()
    {
        //Gets the player input for the x axis
        m_Movement.x = Input.GetAxisRaw("Horizontal");
        //Multipley the input with the speed value of the player data
        m_Movement *= m_Data.m_MoveSpeed;

        if (m_Movement.x > 0) //If the player is moving right
        {
            //Flip variables to match the direction
            transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
            m_Canvas.localScale = new Vector3(-1, 1, 1);
        }
        else if (m_Movement.x < 0 ) //If the player is moving left
        {
            //Flip variables to match the direction
            transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
            m_Canvas.localScale = new Vector3(1, 1, 1);
        }

        //Update the players speed
        m_Rigid.velocity = new Vector3(m_Movement.x, m_Rigid.velocity.y, m_Movement.z);

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //If the player's feet transform is touching the ground
            if (Physics.CheckSphere(m_Feet.position, 0.1f,m_GroundMask))
            {
                //Makes the player jump
                m_Rigid.AddForce(Vector3.up * 200);
            }

        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(m_HandPosition.position, 3.0f);
    }

    //Checks for player attack
    private void Attack()
    {
        //If the player clicks LMB and attack is not on cooldown
        if (Input.GetKeyDown(KeyCode.Mouse0) && !m_Attacked)
        {
            //Plays the weapons attack animation
            m_HandPosition.GetChild(0).GetComponent<Animator>().ResetTrigger("Attack");
            m_HandPosition.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");
            //Calls the attack function from the weapon object's weapon script
            m_Data.m_Weapon.GetComponent<Weapon>().Attack(m_HandPosition,m_Data);
            //Set attacked to true
            m_Attacked = true;
        }

        //If the player has attacked
        if (m_Attacked)
        {
            //Increase the timer
            m_AttackTimer += Time.deltaTime;

            //If the attack timer is greater than the cooldown
            if (m_AttackTimer > 0.5f)
            {
                //Reset the attack timer
                m_Attacked = false;
                m_AttackTimer = 0.0f;
            }
        }
    }

    //Function to be called by enemies to deal damage to the player, and destroying the player when hp hits 0
    public void TakeDamage(int _Damage)
    {
        if (m_CurrentHP > 1)
        {
            m_CurrentHP -= _Damage;
        }
        else
        {
            Destroy(gameObject);
            m_CurrentHP = 0;
        }
        m_CurrentHPText.text = m_CurrentHP.ToString();
    }
}

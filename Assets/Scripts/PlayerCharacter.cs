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
    public RectTransform m_Canvas;
    public Text m_CurrentHPText;
    public Text m_MaxHpText;

    private void Start()
    {
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

        if (m_Attacked)
        {
            m_AttackTimer += Time.deltaTime;
            if (m_AttackTimer > (1) * 0.5f)
            {
                m_Attacked = false;
                m_AttackTimer = 0.0f;
            }
        }
    }

    private void MovePlayer()
    {
        m_Movement.x = Input.GetAxisRaw("Horizontal");
        //m_Movement.z = Input.GetAxisRaw("Vertical");

        m_Movement *= m_Data.m_MoveSpeed;

        if (m_Movement.x > 0)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
            m_Canvas.localScale = new Vector3(-1, 1, 1);
        }
        else if (m_Movement.x < 0 )
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
            m_Canvas.localScale = new Vector3(1, 1, 1);
        }

        //if (m_Movement.z > 0)
        //{
        //    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        //}
        //else if (m_Movement.z < 0)
        //{
        //    transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        //}

        m_Rigid.velocity = new Vector3(m_Movement.x, m_Rigid.velocity.y, m_Movement.z);

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.CheckSphere(m_Feet.position, 0.1f,m_GroundMask))
            {
                m_Rigid.AddForce(Vector3.up * 200);
            }

        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(m_HandPosition.position, 3.0f);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !m_Attacked)
        {
            m_HandPosition.GetChild(0).GetComponent<Animator>().ResetTrigger("Attack");
            m_HandPosition.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");
            m_Data.m_Weapon.GetComponent<Weapon>().Attack(m_HandPosition,m_Data);
            m_Attacked = true;
        }
    }

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

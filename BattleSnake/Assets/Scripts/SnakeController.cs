using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeController : Controller {

    Snake m_Snake;
    Scrollbar m_ChargeBar;

    // Use this for initialization
    void Start () {
        m_Snake = GetComponent<Snake>();
        m_ChargeBar = GameObject.Find("ChargeBar").GetComponent<Scrollbar>();
        m_Snake.bulletCount = UIManager.bulletStart;
        m_Snake.lives = UIManager.lifeStart;
    }
	
    // Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            m_Snake.setMoveDirection(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            m_Snake.setMoveDirection(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            m_Snake.setMoveDirection(Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            m_Snake.setMoveDirection(Vector3.down);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_Snake.Charge();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(m_Snake.bulletCount > 0)
            {
                GameObject.Find("BulletUI " + m_Snake.bulletCount.ToString()).GetComponent<Image>().color = Color.white;
                m_Snake.Shoot();
            }
            
        }
        m_ChargeBar.size = m_Snake.chargeCounter / Snake.chargeTime;
    }

    public override void OnCollect(string tag)
    {
        if(tag == "Bullet")
        {
            GameObject.Find("BulletUI " + (m_Snake.bulletCount + 1).ToString()).GetComponent<Image>().color = Color.red;
        }
    }

    public override void OnKill()
    {

    }
}

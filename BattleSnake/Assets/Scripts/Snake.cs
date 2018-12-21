using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    [SerializeField] int m_TeamNumber = 0;
    [SerializeField] Color m_Color = Color.white;

    public const int minX = -25;
    public const int maxX = 25;
    public const int minY = -13;
    public const int maxY = 12;
    public const float chargeTime = 0.5f;
    const float m_MoveSpeed = 0.125f;

    private int m_BulletCount = 0;
    private int m_LifeCount = 1;
    private float m_TimeStep = m_MoveSpeed;
    private float m_ChargeCounter = 0.0f;
    private float m_Timer = 0.0f;
    private float m_RespawnCounter = -1.0f;
    private bool m_IsCharging = false;
    private bool m_IsAlive = true;
    private Vector3 m_MoveDirection = Vector3.right;
    private Vector3 m_CurrentMoveDirection = Vector3.right;
    private SnakePiece m_SnakePiece;
    private Controller m_Controller;
    public Transform m_CollectableParent;

    public float chargeCounter
    {
        get { return m_ChargeCounter; }
    }

    public int bulletCount
    {
        get { return m_BulletCount; }
        set { m_BulletCount = value; }
    }

    public Vector3 currentMoveDirection
    {
        get { return m_CurrentMoveDirection; }
    }

    public int teamNumber
    {
        get { return m_TeamNumber; }
        set { m_TeamNumber = value; }
    }

    public int lives
    {
        get { return m_LifeCount; }
        set { m_LifeCount = value; }
    }

    public SnakePiece CheckForCollision(Vector3 position)
    {
        for(int i = 0; i < transform.parent.childCount; ++i)
        {
            if(Vector2.Distance(transform.parent.GetChild(i).position, position) < 0.1f)
            {
                return transform.parent.GetChild(i).GetComponent<SnakePiece>();
            }
        }
        return null;
    }

    public Color color
    {
        get { return m_Color; }
        set
        {
            m_Color = value;
            for (int i = 0; i < transform.parent.childCount; ++i)
            {
                transform.parent.GetChild(i).GetComponent<SpriteRenderer>().color = value;
            }
        }
    }

    // Use this for initialization
    void Awake()
    {
        m_SnakePiece = GetComponent<SnakePiece>();
        m_Controller = GetComponent<Controller>();
        m_CollectableParent = GameObject.Find("Collectables").transform;
        AddPiece(2);
        SnakeManager.snakeList.Add(this);
        color = m_Color;
    }

    void AddPiece(int count = 1)
    {
        for (int i = 0; i < count; ++i)
        {
            transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<SnakePiece>().AddPiece(m_Color);
        }
    }

    private void OnDestroy()
    {
        SnakeManager.snakeList.Remove(this);
    }

    public void setMoveDirection(Vector3 direction)
    {
        if ((direction == Vector3.right && currentMoveDirection == Vector3.left) ||
            (direction == Vector3.left && currentMoveDirection == Vector3.right) ||
            (direction == Vector3.up && currentMoveDirection == Vector3.down) ||
            (direction == Vector3.down && currentMoveDirection == Vector3.up))
        {
            return;
        }
       m_MoveDirection = direction;
    }

    public void Charge()
    {
        if(chargeCounter > chargeTime && !m_IsCharging)
        {
            m_TimeStep *= m_MoveSpeed;
            m_IsCharging = true;
        }
    }

    public void Shoot()
    {
        GameObject fireball = Instantiate(Resources.Load("Fireball") as GameObject, transform.position + m_CurrentMoveDirection, transform.rotation);
        fireball.GetComponent<FireballScript>().Shoot(m_MoveDirection);
        --m_BulletCount;
    }

    private void UpdateCharge()
    {
        if (m_IsCharging)
        {
            m_ChargeCounter -= Time.deltaTime;
            if (chargeCounter <= 0.0f)
            {
                m_TimeStep = m_MoveSpeed;
                m_IsCharging = false;
            }
        }
        else if (chargeCounter < chargeTime)
        {
            m_ChargeCounter += (Time.deltaTime * 0.2f);
        }
    }

    void CheckForCollectables()
    {
        for (int i = 0; i < m_CollectableParent.childCount; ++i)
        {
            Transform child = m_CollectableParent.GetChild(i);
            if (child.position == transform.position)
            {
                child.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), child.position.z);

                if (child.tag == "Health")
                {
                    transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<SnakePiece>().AddPiece(m_Color);
                    m_Controller.OnCollect(child.tag);
                }
                else if (child.tag == "Bullet" && m_BulletCount < 3)
                {
                    m_Controller.OnCollect(child.tag);
                    ++m_BulletCount;
                }
            }
        }
    }

    void CheckForCollisions()
    {
        for (int i = 0; i < SnakeManager.snakeList.Count; ++i)
        {
            if (SnakeManager.snakeList[i] != this)
            {
                SnakePiece piece = SnakeManager.snakeList[i].CheckForCollision(transform.position);
                if (piece)
                {
                    if (chargeCounter > 0.0f)
                    {
                        piece.DestroyPiece();
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_RespawnCounter > 0.0f)
        {
            UpdateCharge();
            m_Timer += Time.deltaTime;
            if (m_Timer > m_TimeStep)
            {
                m_Controller.OnTick();
                m_SnakePiece.MovePiece(m_MoveDirection);
                m_CurrentMoveDirection = m_MoveDirection;
                m_Timer -= m_TimeStep;
                CheckForCollectables();
                CheckForCollisions();
            }
        }
    }
}

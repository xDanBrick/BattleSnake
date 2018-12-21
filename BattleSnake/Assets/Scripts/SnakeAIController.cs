using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAIController : Controller {

    Snake m_Snake;

    enum AIState { idle, collectHealth, attack };

    AIState m_AiState = AIState.idle;
    float m_StateTimer = 0.0f;
    int m_CollectCounter = 0;
    int m_CurrentCollectableIndex = 0;
    bool m_IdleClockwise = false;
    Transform m_CurrentSnakePieceTransform;
    Snake m_PlayerSnake;
    int m_targetSnakeLength = 10;

    // Use this for initialization
    void Start () {
        m_Snake = GetComponent<Snake>();
        for(int i = 0; i < SnakeManager.snakeList.Count; ++i)
        {
            if(m_Snake.teamNumber != SnakeManager.snakeList[i].teamNumber)
            {
                m_CurrentSnakePieceTransform = SnakeManager.snakeList[i].transform.parent.GetChild(Random.Range(0, SnakeManager.snakeList[i].transform.parent.childCount)).transform;
                m_PlayerSnake = SnakeManager.snakeList[i];
                return;
            }
        }
        m_targetSnakeLength = Random.Range(2, 6);
    }
	
    void Idle()
    {
        if(m_Snake.currentMoveDirection == Vector3.right && transform.position.x == Snake.maxX - 1)
        {
            m_Snake.setMoveDirection(m_IdleClockwise ? Vector3.down : Vector3.up);
        }
        else if (m_Snake.currentMoveDirection == Vector3.down && transform.position.y == Snake.minY + 1)
        {
            m_Snake.setMoveDirection(m_IdleClockwise ? Vector3.left : Vector3.right);
        }
        else if (m_Snake.currentMoveDirection == Vector3.left && transform.position.x == Snake.minX + 1)
        {
            m_Snake.setMoveDirection(m_IdleClockwise ? Vector3.up : Vector3.down);
        }
        else if (m_Snake.currentMoveDirection == Vector3.up && transform.position.y == Snake.maxY - 1)
        {
            m_Snake.setMoveDirection(m_IdleClockwise ? Vector3.right : Vector3.left);
        }
    }

    bool Inline(Snake player)
    {
        //Returns true if x coorinates are alligned and it is going up or down
        for (int snakePiece = 0; snakePiece < player.transform.parent.childCount; snakePiece++)
        {
            Transform child = player.transform.parent.GetChild(snakePiece);
            if (child.position.x == transform.position.x)
            {
                if (m_Snake.currentMoveDirection == Vector3.down || m_Snake.currentMoveDirection == Vector3.up)
                {
                    return true;
                }
            }
            //Returns true if y coorinates are alligned and it is going right or left
            else if (child.position.y == transform.position.y)
            {
                if (m_Snake.currentMoveDirection == Vector3.right || m_Snake.currentMoveDirection == Vector3.left)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void GoTowardsPoint(Vector2 position)
    {
        if (position.x > transform.position.x && m_Snake.currentMoveDirection != Vector3.left)
        {
            m_Snake.setMoveDirection(Vector3.right);
        }
        if (position.x < transform.position.x && m_Snake.currentMoveDirection != Vector3.right)
        {
            m_Snake.setMoveDirection(Vector3.left);
        }
        if (position.y < transform.position.y && m_Snake.currentMoveDirection != Vector3.up)
        {
            m_Snake.setMoveDirection(Vector3.down);
        }
        else if (position.y > transform.position.y && m_Snake.currentMoveDirection != Vector3.down)
        {
            m_Snake.setMoveDirection(Vector3.up);
        }
    }

    void CollectHealth()
    {
        OnCollect("Health");
        GoTowardsPoint(m_Snake.m_CollectableParent.GetChild(m_CurrentCollectableIndex).position);
    }

    public override void OnCollect(string tag) 
    {
        float shortestDifference = 10000.0f;
        for (int i = 0; i < m_Snake.m_CollectableParent.childCount; ++i)
        {
            if(tag == m_Snake.m_CollectableParent.GetChild(i).tag)
            {
                float diff = Vector2.Distance(m_Snake.m_CollectableParent.GetChild(i).position, transform.position);
                if (diff < shortestDifference)
                {
                    shortestDifference = diff;
                    m_CurrentCollectableIndex = i;
                }
            }
        }
    }

    public override void OnTick()
    {
        if (transform.parent.childCount < m_targetSnakeLength)
        {
            CollectHealth();
        }
        else if (m_Snake.bulletCount < 2)
        {
            OnCollect("Bullet");
            GoTowardsPoint(m_Snake.m_CollectableParent.GetChild(m_CurrentCollectableIndex).position);
        }
        else
        {
            GoTowardsPoint(m_CurrentSnakePieceTransform.position);
            //CollectHealth();
            if (Inline(m_PlayerSnake))
            {
                m_Snake.Shoot();
            }
            //if (m_StateTimer > 3.0f)
            //{

            //m_StateTimer = 0.0f;
            //m_AiState = AIState.idle;
            //m_IdleClockwise = Random.Range(0, 2) == 0 ? true : false;
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        


        //m_StateTimer += Time.deltaTime;
        //switch (m_AiState)
        //{
        //    case AIState.idle:
        //    {
        //        Idle();
        //        if (m_StateTimer > 3.0f)
        //        {
        //             m_StateTimer = 0.0f;
        //             m_AiState = AIState.collectHealth;
        //        }
        //        break;
        //    }
        //    case AIState.collectHealth:
        //    {
        //       GoTowardsPoint(m_CurrentSnakePieceTransform.position);
        //       //CollectHealth();

        //        if (m_StateTimer > 3.0f)
        //        {

        //            //m_StateTimer = 0.0f;
        //            //m_AiState = AIState.idle;
        //            //m_IdleClockwise = Random.Range(0, 2) == 0 ? true : false;
        //        }
        //        break;
        //    }
        //}
    }
}

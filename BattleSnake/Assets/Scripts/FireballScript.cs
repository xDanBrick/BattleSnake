using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour {

    public Vector3 m_MoveDirection;

    public void Shoot(Vector3 shootDirection)
    {
        m_MoveDirection = shootDirection;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(m_MoveDirection * 0.5f);
        if (transform.position.y > Snake.maxY || transform.position.y < Snake.minY || transform.position.x > Snake.maxX || transform.position.x < Snake.minX)
        {
            Destroy(gameObject);
            return;
        }
        for (int i = 0; i < SnakeManager.snakeList.Count; ++i)
        {
            SnakePiece piece = SnakeManager.snakeList[i].CheckForCollision(transform.position);
            if(piece)
            {
                piece.DestroyPiece();
                Destroy(gameObject);
            }
        }
    }
}

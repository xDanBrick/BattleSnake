using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePiece : MonoBehaviour {

    Vector3 currentDirection = Vector3.right;
    Vector3 previousDirection = Vector3.right;
    SnakePiece nextPiece = null;

    private void setNextPiece()
    {
        if (transform.parent.childCount > transform.GetSiblingIndex() + 1)
        {
            Transform next = transform.parent.GetChild(transform.GetSiblingIndex() + 1);
            if (next)
            {
                nextPiece = next.GetComponent<SnakePiece>();
            }
        }
        else
        {
            nextPiece = null;
        }
    }

    public void DestroyPiece()
    {
        if (GetComponent<Snake>())
        {
            Destroy(transform.parent.gameObject);
            return;
        }
        if (nextPiece)
        {
            nextPiece.DestroyPiece();
        }
        Destroy(gameObject);
    }

    public void AddPiece(Color color)
    {
        Vector3 newPosition = transform.position;
        if(currentDirection == Vector3.right)
        {
            newPosition.x -= 1.0f;
        }
        if (currentDirection == Vector3.left)
        {
            newPosition.x += 1.0f;
        }
        if (currentDirection == Vector3.up)
        {
            newPosition.y -= 1.0f;
        }
        if (currentDirection == Vector3.down)
        {
            newPosition.y += 1.0f;
        }
        GameObject piece = Instantiate(Resources.Load("Piece") as GameObject, newPosition, transform.rotation, transform.parent);
        piece.GetComponent<SpriteRenderer>().color = color;
        setNextPiece();
    }

    // Use this for initialization
    void Start () {
        setNextPiece();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MovePiece(Vector3 direction)
    {
        previousDirection = currentDirection;
        currentDirection = direction;
        transform.Translate(direction);

        if(transform.position.y > Snake.maxY)
        {
            transform.position = new Vector3(transform.position.x, Snake.minY, transform.position.z);
        }
        else if (transform.position.y < Snake.minY)
        {
            transform.position = new Vector3(transform.position.x, Snake.maxY, transform.position.z);
        }
        if (transform.position.x > Snake.maxX)
        {
            transform.position = new Vector3(Snake.minX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < Snake.minX)
        {
            transform.position = new Vector3(Snake.maxX, transform.position.y, transform.position.z);
        }
        if (nextPiece)
        {
            nextPiece.MovePiece(previousDirection);
        }
    }
}

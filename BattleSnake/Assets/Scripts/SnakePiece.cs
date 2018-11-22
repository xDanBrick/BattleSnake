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
    }

    public void AddPiece()
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
        Instantiate(Resources.Load("Piece") as GameObject, newPosition, transform.rotation, transform.parent);
        setNextPiece();
    }

    // Use this for initialization
    void Start () {
        setNextPiece();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    const int minX = -13;
    const int maxX = 13;
    const int minY = -6;
    const int maxY = 4;

    public void MovePiece(Vector3 direction)
    {
        previousDirection = currentDirection;
        currentDirection = direction;
        transform.Translate(direction);

        if(transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }
        else if (transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < minX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
        if (nextPiece)
        {
            nextPiece.MovePiece(previousDirection);
        }
    }
}

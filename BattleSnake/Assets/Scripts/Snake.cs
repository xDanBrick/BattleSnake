using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    const int minX = -13;
    const int maxX = 13;
    const int minY = -6;
    const int maxY = 4;

    const float timeStep = 0.125f;

    float timer = 0.0f;
    Vector3 moveDirection = Vector3.right;
    Vector3 currentMoveDirection = Vector3.right;
    SnakePiece snakePiece;

    GameObject[] collectables;

    // Use this for initialization
    void Start()
    {
        snakePiece = GetComponent<SnakePiece>();
        collectables = GameObject.FindGameObjectsWithTag("Collectable");
        snakePiece.AddPiece();
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
        moveDirection = direction;
    }

    //4096 / 512
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeStep)
        {
            snakePiece.MovePiece(moveDirection);
            currentMoveDirection = moveDirection;
            timer -= timeStep;
            for (int i = 0; i < collectables.Length; ++i)
            {
                if (collectables[i].transform.position == transform.position)
                {
                    transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<SnakePiece>().AddPiece();
                    collectables[i].transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), collectables[i].transform.position.z);
                }
            }
        }
    }
}

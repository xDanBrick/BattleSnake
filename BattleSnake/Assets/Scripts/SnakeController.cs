using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

    float timer = 0.0f;
    Vector3 moveDirection = Vector3.right;
    Vector3 currentMoveDirection = Vector3.right;
    SnakePiece snakePiece;

    GameObject[] collectables;

    // Use this for initialization
    void Start () {
        snakePiece = GetComponent<SnakePiece>();
        collectables = GameObject.FindGameObjectsWithTag("Collectable");
        snakePiece.AddPiece();
    }
	//4096 / 512
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.RightArrow) && currentMoveDirection != Vector3.left)
        {
            moveDirection = Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentMoveDirection != Vector3.right)
        {
            moveDirection = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentMoveDirection != Vector3.down)
        {
            moveDirection = Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentMoveDirection != Vector3.up)
        {
            moveDirection = Vector3.down;
        }

        timer += Time.deltaTime;
        if(timer > 0.5f)
        {
            snakePiece.MovePiece(moveDirection);
            currentMoveDirection = moveDirection;
            timer -= 1.0f;
            for (int i = 0; i < collectables.Length; ++i)
            {
                if(collectables[i].transform.position == transform.position)
                {
                    Debug.Log("Collected");
                    transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<SnakePiece>().AddPiece();
                }
            }
        }
    }
}

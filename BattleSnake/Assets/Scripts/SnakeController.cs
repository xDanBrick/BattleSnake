using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

    Snake snake;

    // Use this for initialization
    void Start () {
        snake = GetComponent<Snake>();
    }
	
    // Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
           snake.setMoveDirection(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            snake.setMoveDirection(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            snake.setMoveDirection(Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            snake.setMoveDirection(Vector3.down);
        }
    }
}

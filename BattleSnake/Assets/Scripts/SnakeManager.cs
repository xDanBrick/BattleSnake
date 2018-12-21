using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour {

    public static List<Snake> snakeList = new List<Snake>();
    float timer = 0.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 5.0f)
        {
            timer = 0.0f;
            GameObject newSnake = Instantiate(Resources.Load("EnemySnake") as GameObject, new Vector3(Random.Range(Snake.minX, Snake.maxX), Random.Range(Snake.minX, Snake.maxX)), transform.rotation);
            newSnake.transform.GetChild(0).GetComponent<Snake>().color = Color.green;
        }
        
    }
}

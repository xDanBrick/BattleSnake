using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static int bulletCount = 3;
    public static int bulletStart = 2;
    public static int lifeCount = 3;
    public static int lifeStart = 3;

    // Use this for initialization
    void Start () {
        for(int i = 0; i < bulletCount; ++i)
        {
            GameObject piece = Instantiate(Resources.Load("BulletUI") as GameObject, transform);
            piece.name = "BulletUI " + (i + 1).ToString();
            piece.GetComponent<RectTransform>().localPosition = new Vector3(280.0f + ((float)i * 30.0f), 208);
            if(i < bulletStart)
            {
                piece.GetComponent<Image>().color = Color.red;
            }
        }

        for (int i = 0; i < lifeCount; ++i)
        {
            GameObject piece = Instantiate(Resources.Load("Life") as GameObject, transform);
            piece.name = "Life " + (i + 1).ToString();
            piece.GetComponent<RectTransform>().localPosition = new Vector3(-376.0f + ((float)i * 30.0f), 208);
            if (i < lifeStart)
            {
                piece.GetComponent<Image>().color = Color.red;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

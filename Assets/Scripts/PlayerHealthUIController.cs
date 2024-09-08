using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUIController : MonoBehaviour
{
    private GameObject[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        hearts = new GameObject[3];
        hearts[0] = GameObject.Find("OneHeart");
        hearts[1] = GameObject.Find("TwoHeart");
        hearts[2] = GameObject.Find("ThreeHeart");
    }

    // Update is called once per frame
    void Update()
    {
        int health = GameManager.Instance.playerHealth;
        for (int i = 0; i < 3; i++)
		{
			if (i < health)
			{
				hearts[i].SetActive(true);
			}
			else
			{
				hearts[i].SetActive(false);
			}
		}
    }
}

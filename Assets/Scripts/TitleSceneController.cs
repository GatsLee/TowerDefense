using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
		{
			LoadGameScene();
		} 
    }

    public void LoadGameScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("PlayScene");
	}
}

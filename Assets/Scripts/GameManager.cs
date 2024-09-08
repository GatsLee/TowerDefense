using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    // Enemy Prefabs;
    [SerializeField] private GameObject LV1Enemy;
    [SerializeField] private GameObject LV2Enemy;
    [SerializeField] private GameObject LV3Enemy;

    // Lv1: 4 points, Lv2: 8 points, Lv3: 9 points
    public Vector3[] LV1PathArr;
    public Vector3[] LV2PathArr;
    public Vector3[] LV3PathArr;

    // Lv1: { 6, 0, 0 }, Lv2: { 3, 3, 0 }, Lv3: { 2, 2, 2 }
    public int[] LV1EnemyArr;
    public int[] LV2EnemyArr;
    public int[] LV3EnemyArr;

    //GameOver UI manage
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI gameOverText;

    // check if enemy is spawned
    private bool []enemySpawnArr;

    public int currentLevel = 1;
    public int playerHealth = 3;
    public int enemySurvived = 6;
    public int playerMoney = 100;
    public int unitSum = 0;
    public bool isGameOver = false;

    private float spawnInterval = 1.0f;


    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>() as GameManager;

                if (instance == null)
                    Debug.LogError("There needs to be one active GameManager script on a GameObject in your scene.");
            }

            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
		LV1PathArr = new Vector3[]
{
			new Vector3(-3.785f, 3.2f, 0f),
			new Vector3(-3.785f, -0.03f, 0f),
			new Vector3(3.8f, -0.03f, 0f),
			new Vector3(3.8f, -3.4f, 0f)
};

		LV2PathArr = new Vector3[]
		{
			new Vector3(-5.5f, 0.5f, 0),
			new Vector3(-5.5f, 3.5f, 0),
			new Vector3(5.5f, 3.5f, 0),
			new Vector3(5.5f, 0.5f, 0),
			new Vector3(-5.5f, 0.5f, 0),
			new Vector3(-5.5f, -3.5f, 0),
			new Vector3(5.5f, -3.5f, 0),
			new Vector3(5.5f, 0.5f, 0)
		};

		LV3PathArr = new Vector3[]
		{
			new Vector3(-5.5f, 0.5f, 0),
			new Vector3(-5.5f, 3.5f, 0),
			new Vector3(5.5f, 3.5f, 0),
			new Vector3(5.5f, 0.5f, 0),
			new Vector3(-5.5f, 0.5f, 0),
			new Vector3(-5.5f, -3.5f, 0),
			new Vector3(5.5f, -3.5f, 0),
			new Vector3(5.5f, 0.5f, 0),
			new Vector3(-5.5f, 0.5f, 0)
		};

        LV1EnemyArr = new int[] { 1, 1, 1,
                                    1, 1, 1};
        LV2EnemyArr = new int[] { 1, 1, 1,
        						    2, 2, 2};
        LV3EnemyArr = new int[] { 1, 1,
        						    2, 2,
                                        3, 3};
        enemySpawnArr = new bool[] {false, false, false, false, false, false};
		currentLevel = 1;
        gameOverUI.SetActive(false);
	}

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // According to Level, spawn enemies
        if (spawnInterval <= 0)
		{
			GameObject enemy = InstantiateRandomEnemy();
            if (enemy != null) { 
                spawnInterval = 1.0f;
            }
		}
		else
		{
			spawnInterval -= Time.deltaTime;
		}

        // Check if Game is Over
        if (unitSum >= 6 || playerHealth <= 0)
        {
            Debug.Log("Game Over!");
            gameOverUI.SetActive(true);
            if (playerHealth <= 0)
			{
				gameOverText.text = "Game Over!";
			}
			else if (enemySurvived <= 0)
			{
				gameOverText.text = "You Win!";
			}
            isGameOver = true;
        }
        if (isGameOver && Input.GetKeyDown(KeyCode.Escape))
		{
            Application.Quit();
			return;
		}
    }

    GameObject InstantiateRandomEnemy()
    {
        int randEnemy = Random.Range(0, 6);
        if (enemySpawnArr[randEnemy] == true)
		{
			return null;
		}
        GameObject enemy = null;
        switch(currentLevel)
		{
			case 1:
				enemy = Instantiate(LV1Enemy, LV1PathArr[0], Quaternion.identity);
				break;
			case 2:
				enemy = Instantiate(LV2Enemy, LV2PathArr[0], Quaternion.identity);
				break;
			case 3:
				enemy = Instantiate(LV3Enemy, LV3PathArr[0], Quaternion.identity);
				break;
		}
        enemySpawnArr[randEnemy] = true;
        return enemy;
    }

    public void ReplayGame()
    {
        unitSum = 0;
        playerHealth = 3;
        enemySurvived = 6;
        playerMoney = 100;
        currentLevel = 1;
        gameOverUI.SetActive(false);
        SceneManager.LoadScene("GameScene");
        for (int i = 0; i < 6; i++)
        {
            enemySpawnArr[i] = false;
        }
    }

    public void QuitGame()
	{
        Application.Quit();
	}
}

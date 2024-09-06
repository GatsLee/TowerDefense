using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentLevel = 1;
    private float spawnInterval = 1.0f;

    private static GameManager instance;

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

    private bool []enemySpawnArr;

    // Start is called before the first frame update
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
            if (enemy == null) { return; }
			spawnInterval = 1.0f;
		}
		else
		{
			spawnInterval -= Time.deltaTime;
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
        return enemy;
    }
}

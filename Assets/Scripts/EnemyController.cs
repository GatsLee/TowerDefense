using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    private int mapLevel = 1;
    [SerializeField] private int enemyType = 1;
    private int coorIndex;
    private float speed = 0.8f;
    private int health = 0;
    private int moneyEarned = 10;

    private Vector3[] pathIndex;
    private int currentTargetIndex = 0;
    private Vector3 prevPosition;


    void Start()
    {
        switch (mapLevel)
        {
            case 1:
                coorIndex = 4;
                pathIndex = GameManager.Instance.LV1PathArr;
                break;
            case 2:
                coorIndex = 8;
                pathIndex = GameManager.Instance.LV2PathArr;
                break;
            case 3:
                coorIndex = 9;
                pathIndex = GameManager.Instance.LV3PathArr;
                break;
        }
        switch(enemyType)
        {
            case 1:
                health = 2;
                moneyEarned = 20;
                break;
            case 2:
                health = 4;
                moneyEarned = 40;
                break;
            case 3:
                health = 8;
                moneyEarned = 60;
                break;
        }

        transform.position = pathIndex[0];
        prevPosition = transform.position;
    } 

    void Update()
    {
        if (currentTargetIndex < coorIndex)
        {
            Vector3 targetPosition = pathIndex[currentTargetIndex];

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, pathIndex[currentTargetIndex]) < 0.1f)
            {
                currentTargetIndex++;
            }
            
            Vector3 direction = (transform.position - prevPosition).normalized;
            
            ChangeSpriteDirection(direction);

            prevPosition = transform.position;
        }
        if (currentTargetIndex == coorIndex)
		{
            GameManager.Instance.playerHealth -= 1;
            GameManager.Instance.unitSum += 1;
			Destroy(gameObject);
            return;
		}
        if (health <= 0)
		{
			GameManager.Instance.enemySurvived -= 1;
            GameManager.Instance.playerMoney += moneyEarned;
			GameManager.Instance.unitSum += 1;
            Debug.Log(GameManager.Instance.unitSum);
			Destroy(gameObject);
		}
    }

    private void ChangeSpriteDirection(Vector3 direction)
    {
        if (direction.x >= 0.90f)      // Moving right
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (direction.x <= -0.90f) // Moving left
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (direction.y >= 0.90f) // Moving up
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (direction.y <= -0.90f) // Moving down
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Target hit");
        if (collision.gameObject.tag == "TypeOneBullet")
        {
            health -= 1;
            // AudioManager.Instance.Play("ExplosionLV1");
        }
        else if (collision.gameObject.tag == "TypeTwoBullet")
        {
            health -= 1;
            // AudioManager.Instance.Play("ExplosionLV2");
        }
        else if (collision.gameObject.tag == "TypeThreeBullet")
        {
            health -= 3;
            // AudioManager.Instance.Play("ExplosionLV3");
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public Vector3 GetDirection()
	{
		return (transform.position - prevPosition).normalized;
	}
}

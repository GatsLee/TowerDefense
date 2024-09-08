using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TypeOneDefenseUnitController : MonoBehaviour
{
    [SerializeField]
    private GameObject typeOneBullet;

    private CircleCollider2D rangeCollider;

    private float fireInterval = 1.0f;
    private Transform shootPartTransform;

    public GameObject targetLocked;
	private Transform targetTransform;
	private Vector3 targetDirection;
	private float targetSpeed;
	private float bulletSpeed = 3.0f;
	private float correctionValue = -10.0f;

	void Start()
    {
        rangeCollider = GetComponent<CircleCollider2D>();
        shootPartTransform = transform.Find("shootType1");
        targetLocked = null;
    }

    void Update() 
    {
        if (targetLocked == null)
        {
			return;
		}
  //      if (targetLocked.isDead == true)
		//{
  //    		targetLocked = null;
  //          return;
  //      }
        //Debug.Log("Fire Interval: " + fireInterval);
        if (fireInterval <= 0)
		{
			ShootBullet();
            // shoot the bullet
            // change the body with bullet
            // if the target is hit or the bullet is out of range,
            //  reset the fire interval immediately
            // else
			fireInterval = 1.0f;
		}
		else
		{
            RotateShootingPart();
			fireInterval -= Time.deltaTime;
		}
        // if fire interval is done
        // shoot the bullet: instantiate the bullet & set the bullet's target
        // change the body with no bullet for fire interval
		
		// check if the target is destroyed
		if (targetLocked.IsDestroyed())
		{
			targetLocked = null;
		}

		// if game is over, destroy the defense unit
		if (GameManager.Instance.isGameOver)
		{
			Destroy(gameObject);
		}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (targetLocked != null)
		{
			return;
		}
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("Enemy in range");
			targetLocked = collision.gameObject;
            EnemyController enemyController = targetLocked.GetComponent<EnemyController>();
			targetTransform = enemyController.transform;
            targetDirection = enemyController.GetDirection();
            targetSpeed = enemyController.GetSpeed();
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (targetLocked == null)
		{
			// renew targetLocked
			if (collision.gameObject.tag == "Enemy")
			{
				//Debug.Log("Enemy in range");
				targetLocked = collision.gameObject;
				EnemyController enemyController = targetLocked.GetComponent<EnemyController>();
				targetTransform = enemyController.transform;
				targetDirection = enemyController.GetDirection();
				targetSpeed = enemyController.GetSpeed();
			}
			return;
		}

		if (collision.gameObject == targetLocked)
		{
			targetTransform = targetLocked.transform;
			targetDirection = targetTransform.position - transform.position;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject == targetLocked)
		{
            //Debug.Log("Enemy out of range");
			targetLocked = null;
		}
	}

	private void RotateShootingPart()
	{
	    Vector3 direction = targetLocked.transform.position - transform.position;
		float distToEnemy = direction.magnitude;
		float timeToHit = distToEnemy / bulletSpeed;

		Vector3 predictedPosition = targetLocked.transform.position
										+ (targetDirection * targetSpeed * timeToHit);
		Vector3 predictedDirection = predictedPosition - transform.position;

		float angle = Mathf.Atan2(predictedDirection.y, predictedDirection.x) * Mathf.Rad2Deg;
		shootPartTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
	}

	private void ShootBullet()
	{
        GameObject bullet = Instantiate(typeOneBullet, shootPartTransform.position, shootPartTransform.rotation);
        Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidBody.AddForce(new Vector2(shootPartTransform.up.x, shootPartTransform.up.y).normalized * bulletSpeed, ForceMode2D.Impulse);
        //Debug.Log("Bullet Fired");
	}
}

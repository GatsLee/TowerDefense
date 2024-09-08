using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TypeTwoDefenseUnitController : MonoBehaviour
{
	[SerializeField] private GameObject typeTwoBullet;

	private CircleCollider2D rangeCollider;

	private float fireInterval = 1.0f;
	private Transform shootPartTransform;

	public GameObject targetLocked;
	private Transform targetTransform;
	private Vector3 targetDirection;
	private float targetSpeed;
	private float bulletSpeed = 3.0f;

	void Start()
	{
		rangeCollider = GetComponent<CircleCollider2D>();
		shootPartTransform = transform.Find("shootType2");
		targetLocked = null;
	}

	// Update is called once per frame
	void Update()
	{
		if (targetLocked == null)
		{
			return;
		}

		if (fireInterval <= 0)
		{
			ShootBullet();
			fireInterval = 1.0f;
		}
		else
		{
			RotateShootingPart();
			fireInterval -= Time.deltaTime;
		}

		if (targetLocked.IsDestroyed())
		{
			targetLocked = null;
		}

		if (GameManager.Instance.isGameOver)
		{
			Destroy(gameObject);
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

	private void ShootBullet()
	{
		StartCoroutine(ShootMultipleBullets());
	}

	private IEnumerator ShootMultipleBullets()
	{
		Debug.Log("ShootMultipleBullets");	
		// Shoot the first bullet
		ShootSingleBullet();

		// Wait for 0.5 seconds
		yield return new WaitForSeconds(0.5f);

		// Shoot the second bullet
		ShootSingleBullet();
	}

	private void ShootSingleBullet()
	{
		GameObject bullet = Instantiate(typeTwoBullet, shootPartTransform.position, shootPartTransform.rotation);
		Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
		bulletRigidBody.AddForce(new Vector2(shootPartTransform.up.x, shootPartTransform.up.y).normalized * bulletSpeed, ForceMode2D.Impulse);
	}

}

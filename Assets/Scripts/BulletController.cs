using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 startPosition;

    [SerializeField] private float shootingRange = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOutofRange())
		{
			Destroy(gameObject);
		}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
		{
			//collision.gameObject.GetComponent<EnemyController>().TakeDamage(1);
			Destroy(gameObject);
		}
    }

    private bool isOutofRange()
    {
        if (Vector3.Distance(startPosition, transform.position) > shootingRange)
        {
            return true;
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DefenseUnitSpawner : MonoBehaviour, IPointerEnterHandler, 
									IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	public GameObject typeOneSprite;
	public GameObject typeTwoSprite;
	public GameObject typeThreeSprite;

	public GameObject typeOnePrefab;
	public GameObject typeTwoPrefab;
	public GameObject typeThreePrefab;

	private bool isDragging = false;
	private GameObject currentDefense;
	private GameObject defenseSprite;
	private GameObject defensePrefab;
	private Vector3 originalPosition;

	void Start()
    {
        Button defenseUnitButton = GetComponent<Button>();
		if (transform.tag == "TypeOne")
		{
			defenseSprite = typeOneSprite;
			defensePrefab = typeOnePrefab;
		}
		else if (transform.tag == "TypeTwo")
		{ 
			defenseSprite = typeTwoSprite;
			defensePrefab = typeTwoPrefab;
		}
		else if (transform.tag == "TypeThree")
		{ 
			defenseSprite = typeThreeSprite;
			defensePrefab = typeThreePrefab;
		}
		originalPosition = defenseSprite.transform.position;
	}

	void Update()
	{
		if (isDragging && currentDefense != null)
		{
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			currentDefense.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
		}
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		// Put Description of the defense unit here
		//Debug.Log("Pointer Enter");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		// Remove Description of the defense unit here
		//Debug.Log("Pointer Exit");
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		//Debug.Log("Pointer Down");
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		currentDefense = defenseSprite;
		isDragging = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		//Debug.Log("Pointer Up");
		if (MapManager.Instance.activeTilePosition != Vector3.zero)
		{
			//Debug.Log("Placing Defense Unit");
			GameObject test = Instantiate(defensePrefab, MapManager.Instance.activeTilePosition, Quaternion.identity);
			//Debug.Log("Placed Position: " + MapManager.Instance.activeTilePosition);
			//Debug.Log("Defense Unit Position: " + test.transform.position);
			//Debug.Log(test);
		}
		isDragging = false;
		currentDefense.transform.position = originalPosition;
	}

	public bool IsPointerOverUI()
	{
		return EventSystem.current.IsPointerOverGameObject();
	}
}

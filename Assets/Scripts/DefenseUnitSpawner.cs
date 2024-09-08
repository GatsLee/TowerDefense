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

	private int defenseCost;

	void Start()
    {
        Button defenseUnitButton = GetComponent<Button>();
		if (transform.tag == "TypeOne")
		{
			defenseSprite = typeOneSprite;
			defensePrefab = typeOnePrefab;
			defenseCost = 50;
		}
		else if (transform.tag == "TypeTwo")
		{ 
			defenseSprite = typeTwoSprite;
			defensePrefab = typeTwoPrefab;
			defenseCost = 70;
		}
		else if (transform.tag == "TypeThree")
		{ 
			defenseSprite = typeThreeSprite;
			defensePrefab = typeThreePrefab;
			defenseCost = 500;
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
		//check if player has enough money
		if (GameManager.Instance.playerMoney < defenseCost)
		{
			Debug.Log("Not enough money");
			return;
		}
		//Debug.Log("Pointer Down");
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		currentDefense = defenseSprite;
		isDragging = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		//Debug.Log("Pointer Up");
		if (GameManager.Instance.playerMoney < defenseCost)
		{
			Debug.Log("Not enough money");
			return;
		}
		if (MapManager.Instance.activeTilePosition != Vector3.zero)
		{
			//Debug.Log("Placing Defense Unit");
			GameObject test = Instantiate(defensePrefab, MapManager.Instance.activeTilePosition, Quaternion.identity);
			GameManager.Instance.playerMoney -= defenseCost;
		}
		isDragging = false;
		currentDefense.transform.position = originalPosition;
	}

	public bool IsPointerOverUI()
	{
		return EventSystem.current.IsPointerOverGameObject();
	}
}

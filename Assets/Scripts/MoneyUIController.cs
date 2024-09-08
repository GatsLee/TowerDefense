using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tmpObject = transform.GetChild(0).gameObject;
        moneyText = tmpObject.GetComponent<TextMeshProUGUI>();
        moneyText.text = GameManager.Instance.playerMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = GameManager.Instance.playerMoney.ToString();
    }
}

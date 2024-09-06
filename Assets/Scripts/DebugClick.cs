using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button testButton = GameObject.Find("Button").GetComponent<Button>();
        testButton.onClick.AddListener(() => Debug.Log("Button Clicked!"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

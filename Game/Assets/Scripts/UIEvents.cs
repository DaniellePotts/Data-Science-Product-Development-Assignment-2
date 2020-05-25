using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEvents : MonoBehaviour
{
    // Start is called before the first frame update

    public Button DashboardTrigger;

    void Start()
    {
        DashboardTrigger = GetComponent<Button>();

        DashboardTrigger.onClick.AddListener(Test);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test()
    {
        Debug.Log("Test");
    }
}

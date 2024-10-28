using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
     [Header("Paineis")]
     public GameObject HelpStatus;
     
    // Start is called before the first frame update
    void Start()
    {
        HelpStatus.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void StatusInfo(){
        HelpStatus.SetActive(!HelpStatus.activeSelf);
    }
}

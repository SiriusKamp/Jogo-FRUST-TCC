using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryGM : MonoBehaviour
{
    public GameObject diaryPannel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            diaryPannel.SetActive(true);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pause : MonoBehaviour
{
    
    private bool isPaused=false;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Gun gun;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                pausePanel.SetActive(false);
                isPaused = false;
                Time.timeScale = 1.0f;
                gun.enabled = true;
            }
            else
            {
                pausePanel.SetActive(true);
                isPaused = true;
                Time.timeScale = 0;
                gun.enabled = false;
            }
        }
    }
}

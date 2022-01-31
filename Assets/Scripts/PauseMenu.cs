using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    Canvas canvas;
    public static bool isPaused = false;
    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        canvas.enabled = true;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Pause()
    {
        canvas.enabled = false;
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void PauseTime()
    {
        Time.timeScale = 0f;
    }
    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }
}

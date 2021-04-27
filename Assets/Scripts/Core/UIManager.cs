﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text timeText;
    [SerializeField] Text statusText;
    [SerializeField] GameObject menu;
    public Text counter;
    [SerializeField] GameObject touchControl;
    public GameObject moveJoystick;
    public GameObject attackJoystick;

    Vector2 touchInput;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<UIManager>();
            return _instance;
        }
    }

    private void Start()
    {
#if UNITY_ANDROID
        touchControl.SetActive(true);
#else
        touchControl.SetActive(false);
#endif
    }

    public void ShowMenu(string statusText, string timeText)
    {
        this.statusText.text = statusText;
        this.timeText.text = timeText;
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void OnRestart()
    {
        GameManager.Instance.OnRestart();
    }

    public void OnExit()
    {
        GameManager.Instance.OnExit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour, IGameManager
{
    public readonly Dictionary<int, int> FontSizeByNumberOfDecimals = new Dictionary<int, int>
    {
        {0, 30},
        {1, 26},
        {2, 23},
        {3, 20},
        {4, 16}
	};

    public int numberOfDecimals;
	public eManagerStatus Status { get; private set; }

    [SerializeField] private GameObject _screenOverlay;

    [Header("UI panels")]
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _controlsPanel;

	public void Startup()
	{
        Status = eManagerStatus.Initializing;
        Status = eManagerStatus.Started;
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeOptionsPanelState();
        }
    }

    public void ChangeNumberOfDecimals(int newNumberOfDecimals)
	{
        numberOfDecimals = newNumberOfDecimals;
        StringExtensions.UpdateNumberOfDecimals();
	}

    public void ChangeOptionsPanelState()
	{
        _screenOverlay.SetActive(!_screenOverlay.activeSelf);
        _optionsPanel.SetActive(!_optionsPanel.activeSelf);
        _mainPanel.SetActive(!_mainPanel.activeSelf);
    }

    public void ChangeControlsPanelState()
    {
        _screenOverlay.SetActive(!_screenOverlay.activeSelf);
        _controlsPanel.SetActive(!_controlsPanel.activeSelf);
        _mainPanel.SetActive(!_mainPanel.activeSelf);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour, IGameManager
{
    public Action NumberOfDecimalsChanged;
    public readonly Dictionary<int, int> FontSizeByNumberOfDecimals = new Dictionary<int, int>
    {
        {0, 30},
        {1, 26},
        {2, 23},
        {3, 20},
        {4, 16}
	};
	public eManagerStatus status { get; private set; }

    [HideInInspector] public int numberOfDecimals;

    [SerializeField] private TMPro.TMP_Dropdown _numberOfDecimalsDropdown;
    [SerializeField] private GameObject _screenOverlay;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _controlsPanel;

	public void Startup()
	{
        status = eManagerStatus.Initializing;
        numberOfDecimals = _numberOfDecimalsDropdown.value;
        status = eManagerStatus.Started;
	}

    public void ChangeNumberOfDecimals(int newNumberOfDecimals)
	{
        numberOfDecimals = newNumberOfDecimals;
        StringExtensions.UpdateNumberOfDecimals();
        //NumberOfDecimalsChanged?.Invoke();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
            ChangeOptionsPanelState();
		}
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

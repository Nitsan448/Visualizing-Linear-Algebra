using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [Header("UI panels")]
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _controlsPanel;

    private Fader _fader;

	public void Startup()
	{
        Status = eManagerStatus.Initializing;
        _fader = GetComponent<Fader>();
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
        ChangePanelState(_optionsPanel);
    }

    public void ChangeControlsPanelState()
    {
        ChangePanelState(_controlsPanel);
    }

    private void ChangePanelState(GameObject panel)
	{
        _fader.StopAllCoroutines();
		if (!panel.activeSelf)
		{
            _fader.FadeInCanvasGroup(panel.GetComponent<CanvasGroup>());
            _fader.FadeOutCanvasGroup(_mainPanel.GetComponent<CanvasGroup>());
		}
		else
		{
            _fader.FadeOutCanvasGroup(panel.GetComponent<CanvasGroup>());
            _fader.FadeInCanvasGroup(_mainPanel.GetComponent<CanvasGroup>());
        }
    }
}

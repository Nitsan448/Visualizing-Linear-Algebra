using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour, IGameManager
{
    public static Action NumberOfDecimalsChanged;
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
        NumberOfDecimalsChanged?.Invoke();
	}
}

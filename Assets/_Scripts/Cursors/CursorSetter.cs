using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class CursorSetter: MonoBehaviour
{
    [SerializeField] private bool _addSetCursorOnHoverToActiveChildren;

    private static Texture2D _standardCursor;
    private static Texture2D _inputFieldCursor;
    private static Texture2D _buttonCursor;

    private Button[] _buttons;
    private TMP_InputField[] _tmpInputFields;
    private InputField[] _inputFields;
    private TMP_Dropdown[] _tmpDropdowns;
    private Toggle[] _toggles;

    private void Start()
	{
        AddCursorOnHoverScriptToChildren();

        _standardCursor = Resources.Load<Texture2D>("Cursors/StandardCursor");
        _inputFieldCursor = Resources.Load<Texture2D>("Cursors/InputFieldCursor");
        _buttonCursor = Resources.Load<Texture2D>("Cursors/ButtonCursor");
        SetCursorToStandard();
    }

    private void AddCursorOnHoverScriptToChildren()
	{
        _buttons = GetComponentsInChildren<Button>(true);
        _tmpInputFields = GetComponentsInChildren<TMP_InputField>(true);
        _inputFields = GetComponentsInChildren<InputField>(true);
        _tmpDropdowns = GetComponentsInChildren<TMP_Dropdown>(true);
        _toggles = GetComponentsInChildren<Toggle>(true);

        foreach (Button button in _buttons)
		{
            if(button.gameObject.GetComponent<SetCursorOnHover>() == null)
			{
                button.gameObject.AddComponent<SetCursorOnHover>();
			}
        }
        foreach (TMP_InputField tmpInputField in _tmpInputFields)
        {
            if (tmpInputField.gameObject.GetComponent<SetCursorOnHover>() == null)
			{
                tmpInputField.gameObject.AddComponent<SetCursorOnHover>();
			}
        }
        foreach (InputField inputField in _inputFields)
        {
            if (inputField.gameObject.GetComponent<SetCursorOnHover>() == null)
			{
                inputField.gameObject.AddComponent<SetCursorOnHover>();
			}
        }
        foreach (Toggle toggle in _toggles)
        {
            if (toggle.gameObject.GetComponent<SetCursorOnHover>() == null)
            {
                toggle.gameObject.AddComponent<SetCursorOnHover>();
            }
        }
        foreach (TMP_Dropdown dropDown in _tmpDropdowns)
        {
            if (dropDown.gameObject.GetComponent<SetCursorOnHover>() == null)
            {
                dropDown.gameObject.AddComponent<SetCursorOnHover>();
            }
        }
    }

    public static void SetCursorToStandard()
    {
        Cursor.SetCursor(_standardCursor, new Vector2(6, 6), cursorMode: CursorMode.Auto);
    }

    public static void SetCursorToInputField()
	{
        Cursor.SetCursor(_inputFieldCursor, new Vector2(_inputFieldCursor.width / 2, _inputFieldCursor.height / 2), cursorMode: CursorMode.Auto);
    }
    public static void SetCursorToButton()
    {
        Cursor.SetCursor(_buttonCursor, new Vector2(6, 0), cursorMode: CursorMode.Auto);
    }
}

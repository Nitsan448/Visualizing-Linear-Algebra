using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class CursorSetter: MonoBehaviour
{
    [SerializeField] private bool _addCursorScriptToPrefabs;

    private static Texture2D _standardCursor;
    private static Texture2D _inputFieldCursor;
    private static Texture2D _buttonCursor;

    private Button[] _buttonsInScene;
    private TMP_InputField[] _tmpInputFieldsInScene;
    private InputField[] _inputFieldsInScene;

    private void Start()
	{
		if (_addCursorScriptToPrefabs)
		{
            AddCursorOnHoverScriptToPrefabs();
		}

        _standardCursor = Resources.Load<Texture2D>("Cursors/StandardCursor");
        _inputFieldCursor = Resources.Load<Texture2D>("Cursors/InputFieldCursor");
        _buttonCursor = Resources.Load<Texture2D>("Cursors/ButtonCursor");
        SetCursorToStandard();
    }

    private void AddCursorOnHoverScriptToPrefabs()
	{
        _buttonsInScene = Resources.FindObjectsOfTypeAll<Button>();
        _tmpInputFieldsInScene = Resources.FindObjectsOfTypeAll<TMP_InputField>();
        _inputFieldsInScene = Resources.FindObjectsOfTypeAll<InputField>();

        foreach(Button button in _buttonsInScene)
		{
            if(button.GetComponent<SetCursorOnHover>() == null)
			{
                button.gameObject.AddComponent<SetCursorOnHover>();
			}
		}
        foreach (TMP_InputField tmpInputField in _tmpInputFieldsInScene)
        {
            if(tmpInputField.GetComponent<SetCursorOnHover>() == null)
			{
                tmpInputField.gameObject.AddComponent<SetCursorOnHover>();
			}
        }
        foreach (InputField inputField in _inputFieldsInScene)
        {
			if (inputField.GetComponent<SetCursorOnHover>())
			{
                inputField.gameObject.AddComponent<SetCursorOnHover>();
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

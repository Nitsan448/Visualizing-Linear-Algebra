using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class CursorSetter: MonoBehaviour
{
    private static Texture2D _standardCursor;
    private static Texture2D _inputFieldCursor;
    private static Texture2D _buttonCursor;

    private Button[] _buttonsInChildren;
    private TMP_InputField[] _tmpInputFieldsInChildren;
    private InputField[] _inputFieldsInSceneChildren;

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
        _buttonsInChildren = GetComponentsInChildren<Button>();
        _tmpInputFieldsInChildren = GetComponentsInChildren<TMP_InputField>();
        _inputFieldsInSceneChildren = GetComponentsInChildren<InputField>();

        foreach(Button button in _buttonsInChildren)
		{
            if(button.GetComponent<SetCursorOnHover>() == null)
			{
                button.gameObject.AddComponent<SetCursorOnHover>();
			}
		}
        foreach (TMP_InputField tmpInputField in _tmpInputFieldsInChildren)
        {
            if(tmpInputField.GetComponent<SetCursorOnHover>() == null)
			{
                tmpInputField.gameObject.AddComponent<SetCursorOnHover>();
			}
        }
        foreach (InputField inputField in _inputFieldsInSceneChildren)
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

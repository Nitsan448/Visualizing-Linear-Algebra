using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SetCursorOnHover : MonoBehaviour
{
	private EventTrigger _eventTrigger;
	private delegate void SetCursor();
	private SetCursor _setCursorDelegate;
	private void Start()
	{
		SetCursorDelegateValue();
		_eventTrigger = gameObject.AddComponent<EventTrigger>();
		AddOnPointerEnterListener();
		AddOnPointerExitListener();
	}

	private void SetCursorDelegateValue()
	{
		Button button = GetComponent<Button>();
		TMP_InputField tmpInputField = GetComponent<TMP_InputField>();
		InputField inputField = GetComponent<InputField>();
		if (button != null)
		{
			_setCursorDelegate = CursorSetter.SetCursorToButton;
		}
		else if (inputField != null || tmpInputField != null)
		{
			_setCursorDelegate = CursorSetter.SetCursorToInputField;
		}
		else
		{
			Destroy(this);
		}
	}

	private void AddOnPointerEnterListener()
	{
		EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
		pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
		pointerEnterEntry.callback.AddListener(delegate { _setCursorDelegate(); });
		_eventTrigger.triggers.Add(pointerEnterEntry);
	}

	private void AddOnPointerExitListener()
	{
		EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
		pointerEnterEntry.eventID = EventTriggerType.PointerExit;
		pointerEnterEntry.callback.AddListener(delegate { CursorSetter.SetCursorToStandard(); });
		_eventTrigger.triggers.Add(pointerEnterEntry);
	}
}

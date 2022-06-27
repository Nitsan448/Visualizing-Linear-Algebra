using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class SetCursorOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private delegate void SetCursorDelegate();
	private SetCursorDelegate _setCursorMethod;
	private Selectable _selectable;

	private List<Type> _buttonCursorTypes = new List<Type>
	{
		typeof(Toggle),
		typeof(TMP_Dropdown),
		typeof(Dropdown),
		typeof(Button),
		typeof(Slider),
	};

	private List<Type> _inputCursorTypes = new List<Type>
	{
		typeof(TMP_InputField),
		typeof(InputField),
	};

	private void Awake()
	{
		_selectable = GetComponent<Selectable>();
		SetCursorDelegateValue();
	}

	private void OnDisable()
	{
		CursorSetter.SetCursorToStandard();
	}

	private void SetCursorDelegateValue()
	{
		if (_buttonCursorTypes.Contains(_selectable.GetType()))
		{
			_setCursorMethod = CursorSetter.SetCursorToButton;
		}
		else if (_inputCursorTypes.Contains(_selectable.GetType()))
		{
			_setCursorMethod = CursorSetter.SetCursorToInputField;
		}
		else
		{
			Destroy(this);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (_selectable.interactable)
		{
			_setCursorMethod();
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		CursorSetter.SetCursorToStandard();
	}
}

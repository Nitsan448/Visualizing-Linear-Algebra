using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectTransformInput : MonoBehaviour
{
    [SerializeField] private Transform _objectTransform;

    private TMP_InputField _objectPositionInput;
    // Start is called before the first frame update
    void Start()
    {
        _objectPositionInput = GetComponent<TMP_InputField>();
        _objectPositionInput.onValueChanged.AddListener(delegate { ChangeObjectValue(); });
        UpdateInputFieldText();
    }

	private void OnEnable()
	{
        TransformationsManager.TransformationApplied += UpdateInputFieldText;
	}

	private void OnDisable()
	{
        TransformationsManager.TransformationApplied -= UpdateInputFieldText;
    }

	private void ChangeObjectValue()
	{
        Vector3 newValue = StringExtensions.StringToVector3(_objectPositionInput.text);
        switch (Managers.Transformations.transformValueToManipulate)
        {
            case eTransformValue.Position:
                _objectTransform.position = newValue;
                break;

            case eTransformValue.Rotation:
                _objectTransform.eulerAngles = newValue;
                break;

            case eTransformValue.Scale:
                _objectTransform.localScale = newValue;
                break;
        }
    }

    public void UpdateInputFieldText()
	{
        string newText = string.Empty;
        switch (Managers.Transformations.transformValueToManipulate)
        {
            case eTransformValue.Position:
                newText = StringExtensions.Vector3ToString(_objectTransform.position);
                break;

            case eTransformValue.Rotation:
                newText = StringExtensions.Vector3ToString(_objectTransform.eulerAngles);
                break;

            case eTransformValue.Scale:
                newText = StringExtensions.Vector3ToString(_objectTransform.localScale);
                break;
        }
        _objectPositionInput.text = newText;
	}
}

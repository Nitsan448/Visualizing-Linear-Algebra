using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectTransformInput : MonoBehaviour
{
    private TMP_InputField _objectTransformInput;
    // Start is called before the first frame update
    void Start()
    {
        _objectTransformInput = GetComponent<TMP_InputField>();
        _objectTransformInput.onValueChanged.AddListener(delegate { ChangeObjectValue(); });
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
        Vector3 newValue = StringExtensions.StringToVector3(_objectTransformInput.text);
        switch (Managers.Transformations.transformValueToManipulate)
        {
            case eTransformValue.Position:
                Managers.Transformations.ObjectToTransform.position = newValue;
                break;

            case eTransformValue.Rotation:
                Managers.Transformations.ObjectToTransform.eulerAngles = newValue;
                break;

            case eTransformValue.Scale:
                Managers.Transformations.ObjectToTransform.localScale = newValue;
                break;
        }
    }

    public void UpdateInputFieldText()
	{
        string newText = string.Empty;
        switch (Managers.Transformations.transformValueToManipulate)
        {
            case eTransformValue.Position:
                newText = StringExtensions.Vector3ToString(Managers.Transformations.ObjectToTransform.position);
                break;

            case eTransformValue.Rotation:
                newText = StringExtensions.Vector3ToString(Managers.Transformations.ObjectToTransform.eulerAngles);
                break;

            case eTransformValue.Scale:
                newText = StringExtensions.Vector3ToString(Managers.Transformations.ObjectToTransform.localScale);
                break;
        }
        _objectTransformInput.text = newText;
	}
}

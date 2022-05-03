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
                Vector4 position = TransformExtensions.ConvertToVector4(Managers.Transformations.ObjectToTransform.position, Managers.Transformations.positionVectorWValue);
                newText = StringExtensions.Vector4ToString(position);
                break;

            case eTransformValue.Rotation:
                Vector4 rotationEuler = TransformExtensions.ConvertToVector4(Managers.Transformations.ObjectToTransform.eulerAngles, Managers.Transformations.rotationVectorWValue);
                newText = StringExtensions.Vector4ToString(rotationEuler);
                break;

            case eTransformValue.Scale:
                Vector4 scale = TransformExtensions.ConvertToVector4(Managers.Transformations.ObjectToTransform.localScale, Managers.Transformations.scaleVectorWValue);
                newText = StringExtensions.Vector4ToString(scale);
                break;
        }
        _objectTransformInput.text = newText;
	}
}

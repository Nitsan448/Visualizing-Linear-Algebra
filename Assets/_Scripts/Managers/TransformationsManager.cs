using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;

public class TransformationsManager : MonoBehaviour, IGameManager
{
    public static Action TransformationApplied;
    public eManagerStatus status { get; private set; }
    public Transform ObjectToTransform;
    public eTransformValue transformValueToManipulate = eTransformValue.Position;
    public float positionVectorWValue { get; private set; } = 1;
    public float rotationVectorWValue { get; private set; } = 1;
    public float scaleVectorWValue { get; private set; } = 1;

    [SerializeField] private TMP_InputField _matrixInput;

    [SerializeField] private TMP_Dropdown valueToChangeDropdown;

    private Matrix4x4 _matrix;

    public void Startup()
    {
        status = eManagerStatus.Initializing;
        status = eManagerStatus.Started;
    }

    public void ApplyTransformation()
	{
        UpdateMatrixFromUI();

        TransformObject();

        TransformationApplied?.Invoke();
    }

    private void UpdateMatrixFromUI()
    {
        _matrix = StringExtensions.stringToMatrix(_matrixInput.text);
    }

    private void TransformObject()
	{
        switch (transformValueToManipulate)
        {
            case eTransformValue.Position:
                ApplyTransformationOnPosition();
                break;

            case eTransformValue.Rotation:
                ApplyTransformationOnRotation();
                break;

            case eTransformValue.Scale:
                ApplyTransformationOnScale();
                break;
        }
    }

    private void ApplyTransformationOnPosition()
	{
        Vector4 currentPosition = TransformExtensions.ConvertToVector4(ObjectToTransform.position, positionVectorWValue);
        Vector4 newPosition = _matrix * currentPosition;
        positionVectorWValue = newPosition.w;
        ObjectToTransform.position = newPosition;
    }

    private void ApplyTransformationOnRotation()
    {
        Vector4 currentRotation = TransformExtensions.ConvertToVector4(ObjectToTransform.eulerAngles, rotationVectorWValue);
        Vector4 newRotation = _matrix * currentRotation;
        rotationVectorWValue = newRotation.w;
        ObjectToTransform.eulerAngles = newRotation;
    }

    private void ApplyTransformationOnScale()
    {
        Vector4 currentScale = TransformExtensions.ConvertToVector4(ObjectToTransform.localScale, scaleVectorWValue);
        Vector4 newScale = _matrix * currentScale;
        scaleVectorWValue = newScale.w;
        ObjectToTransform.localScale = newScale;
    }

    //TODO: add apply transformation on vertices option

    public void InvertMatrix()
	{
        UpdateMatrixFromUI();
        _matrix = _matrix.inverse;
        UpdateMatrixUI();
	}

    public void TransposeMatrix()
    {
        UpdateMatrixFromUI();
        _matrix = _matrix.transpose;
        UpdateMatrixUI();
    }

    private void UpdateMatrixUI()
	{
        _matrixInput.text = StringExtensions.UpdateNumberOfDecimalsShownMatrix(_matrixInput.text);
        _matrixInput.pointSize = Managers.UI.FontSizeByNumberOfDecimals[Managers.UI.numberOfDecimals];
    }

    public void ChangeToCommonMatrix(int index)
	{
        MatrixTransformation matrixTrasnformation = CommonMatrixTransfomations.matrixByIndex[index];
        _matrix = matrixTrasnformation.Matrix;
        valueToChangeDropdown.value = (int)matrixTrasnformation.TransformValue;
        UpdateMatrixUI();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TransformationsManager : MonoBehaviour, IGameManager
{
    public static Action TransformationApplied;
    public eManagerStatus status { get; private set; }
    public Transform ObjectToTransform;
    public eTransformValue transformValueToManipulate = eTransformValue.Position;

    [SerializeField] private TMP_InputField _firstRow;
    [SerializeField] private TMP_InputField _secondRow;
    [SerializeField] private TMP_InputField _thirdRow;
    [SerializeField] private TMP_InputField _fourthRow;

    public float positionVectorWValue { get; private set; } = 1;
    public float rotationVectorWValue { get; private set; } = 1;
    public float scaleVectorWValue { get; private set; } = 1;

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
        SetMatrixRow(0, _firstRow.text);
        SetMatrixRow(1, _secondRow.text);
        SetMatrixRow(2, _thirdRow.text);
        SetMatrixRow(3, _fourthRow.text);
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

    private void SetMatrixRow(int row, string vector)
	{
        Vector4 result = StringExtensions.StringToVector4(vector);
        _matrix.SetRow(row, result);
    }

    public void InvertMatrix()
	{
        UpdateMatrixFromUI();
        _matrix = _matrix.inverse;
        UpdateMatrixUI();
	}

    private void UpdateMatrixUI()
	{
        _firstRow.text = StringExtensions.Vector4ToString(_matrix.GetRow(0));
        _secondRow.text = StringExtensions.Vector4ToString(_matrix.GetRow(1));
        _thirdRow.text = StringExtensions.Vector4ToString(_matrix.GetRow(2));
        _fourthRow.text = StringExtensions.Vector4ToString(_matrix.GetRow(3));
    }

    public void ChangeToCommonMatrix(int index)
	{
        _matrix = CommonMatrixTransfomations.matrixByIndex[index];
        UpdateMatrixUI();
	}
}

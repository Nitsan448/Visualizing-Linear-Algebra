using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;

public class TransformationsManager : MonoBehaviour, IGameManager
{
    public event Action TransformationApplied;
    public event Action MatrixUpdated;
    public eManagerStatus Status { get; private set; }
    public Transform ObjectToTransform;
    public eTransformValue transformValueToManipulate;
    public float positionVectorWValue { get; private set; } = 1;
    public float rotationVectorWValue { get; private set; } = 1;
    public float scaleVectorWValue { get; private set; } = 1;

    public Matrix4x4 Matrix { get; set; } = CommonMatrixTransfomations.Identity;

    public void Startup()
    {
        Status = eManagerStatus.Initializing;
        Status = eManagerStatus.Started;
    }

	public void ApplyTransformation()
	{
        TransformObject();

        TransformationApplied?.Invoke();
    }

    private void TransformObject()
	{
        switch (transformValueToManipulate)
        {
            case eTransformValue.Vertices:
                ApplyTransformationOnVertices();
                break;

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

    private void ApplyTransformationOnVertices()
    {
        Mesh mesh = ObjectToTransform.gameObject.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = new Vector3[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Vector4 vertexPosition = ObjectToTransform.TransformPoint(mesh.vertices[i]);
            vertexPosition = TransformExtensions.ConvertToVector4(vertexPosition, 1);
            vertices[i] = Matrix * vertexPosition;
            vertices[i] = ObjectToTransform.InverseTransformPoint(vertices[i]);
        }
        mesh.vertices = vertices;
    }

    private void ApplyTransformationOnPosition()
	{
        Vector4 currentPosition = TransformExtensions.ConvertToVector4(ObjectToTransform.position, positionVectorWValue);
        Vector4 newPosition = Matrix * currentPosition;
        positionVectorWValue = newPosition.w;
        ObjectToTransform.position = newPosition;
    }

    private void ApplyTransformationOnRotation()
    {
        Vector4 currentRotation = TransformExtensions.ConvertToVector4(ObjectToTransform.eulerAngles, rotationVectorWValue);
        Vector4 newRotation = Matrix * currentRotation;
        rotationVectorWValue = newRotation.w;
        ObjectToTransform.eulerAngles = newRotation;
    }

    private void ApplyTransformationOnScale()
    {
        Vector4 currentScale = TransformExtensions.ConvertToVector4(ObjectToTransform.localScale, scaleVectorWValue);
        Vector4 newScale = Matrix * currentScale;
        scaleVectorWValue = newScale.w;
        ObjectToTransform.localScale = newScale;
    }

    public void InvertMatrix()
	{
        Matrix = Matrix.inverse;
        MatrixUpdated?.Invoke();
	}

    public void TransposeMatrix()
    {
        Matrix = Matrix.transpose;
        MatrixUpdated?.Invoke();
    }

    public void ChangeToCommonMatrix(int index)
	{
        Matrix = CommonMatrixTransfomations.matrixByIndex[index];
        MatrixUpdated?.Invoke();
	}

    public void UpdateMatrixFromString(string newMatrix)
	{
		if (StringExtensions.IsMatrixStringFormatValid(newMatrix))
		{
            Managers.Transformations.Matrix = StringExtensions.StringToMatrix(newMatrix);
		}
        MatrixUpdated?.Invoke();
    }
}

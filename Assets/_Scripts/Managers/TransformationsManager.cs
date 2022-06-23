using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;

public class TransformationsManager : MonoBehaviour, IGameManager
{
    public Action TransformationApplied;
    public Action MatrixUpdated;
    public eManagerStatus status { get; private set; }
    public Transform ObjectToTransform;
    public eTransformValue transformValueToManipulate = eTransformValue.Position;
    public float positionVectorWValue { get; private set; } = 1;
    public float rotationVectorWValue { get; private set; } = 1;
    public float scaleVectorWValue { get; private set; } = 1;

    [SerializeField] private TMP_Dropdown valueToChangeDropdown;

    public Matrix4x4 Matrix { get; set; } = CommonMatrixTransfomations.Identity.Matrix;

    public void Startup()
    {
        status = eManagerStatus.Initializing;
        status = eManagerStatus.Started;
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
            case eTransformValue.Position:
                ApplyTransformationOnPosition();
                break;

            case eTransformValue.Rotation:
                ApplyTransformationOnRotation();
                break;

            case eTransformValue.Scale:
                ApplyTransformationOnScale();
                break;

            case eTransformValue.Vertices:
                ApplyTransformationOnVertices();
                break;
        }
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

    private void ApplyTransformationOnVertices()
    {
        Mesh mesh = ObjectToTransform.gameObject.GetComponent<MeshFilter>().mesh;
        Debug.Log(mesh.vertices);
        Vector3[] vertices = new Vector3[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Vector4 vertex = TransformExtensions.ConvertToVector4(mesh.vertices[i], 1);
            vertices[i] = Matrix * vertex;
        }
        mesh.vertices = vertices;
        Debug.Log(mesh.vertices);
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
        MatrixTransformation matrixTrasnformation = CommonMatrixTransfomations.matrixByIndex[index];
        Matrix = matrixTrasnformation.Matrix;
        valueToChangeDropdown.value = (int)matrixTrasnformation.TransformValue;
        MatrixUpdated?.Invoke();
	}
}

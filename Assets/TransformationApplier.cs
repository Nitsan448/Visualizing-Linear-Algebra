using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationApplier : MonoBehaviour
{
    public float PositionVectorWValue { get; set; } = 1;
    public float RotationVectorWValue { get; set; } = 1;
    public float ScaleVectorWValue { get; set; } = 1;
    public bool CoroutineActive { get; private set; }
    public float LerpDuration { get; set; }

    public float StartingLerpDuration = 0.5f;
    private TransformationsManager _transformationsManager;


    private void Awake()
	{
        _transformationsManager = Managers.Transformations;
	}

    public void TransformObject()
    {
        switch (_transformationsManager.transformValueToManipulate)
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
        Mesh mesh = _transformationsManager.ObjectToTransform.gameObject.GetComponent<MeshFilter>().mesh;
        Vector3[] targetVertices = new Vector3[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Vector4 vertexPosition = _transformationsManager.ObjectToTransform.TransformPoint(mesh.vertices[i]);
            vertexPosition = TransformExtensions.ConvertToVector4(vertexPosition, 1);
            targetVertices[i] = _transformationsManager.Matrix * vertexPosition;
            targetVertices[i] = _transformationsManager.ObjectToTransform.InverseTransformPoint(targetVertices[i]);
        }
        StartCoroutine(LerpVertices(targetVertices));
    }

    private IEnumerator LerpVertices(Vector3[] targetVertices)
    {
        CoroutineActive = true;
        Mesh mesh = _transformationsManager.ObjectToTransform.gameObject.GetComponent<MeshFilter>().mesh;
        float currentTime = 0;
        Vector3[] startingVertices = mesh.vertices;
        Vector3[] newVertices = new Vector3[mesh.vertices.Length];
        while (currentTime < LerpDuration)
        {
            for (int i = 0; i < mesh.vertices.Length; i++)
			{
                newVertices[i] = Vector3.Lerp(startingVertices[i], targetVertices[i], currentTime / LerpDuration);
            }
            mesh.vertices = newVertices;
            currentTime += Time.deltaTime;
            yield return null;
        }
        mesh.vertices = targetVertices;
        CoroutineActive = false;
    }

    private void ApplyTransformationOnPosition()
    {
        Vector4 currentPosition = TransformExtensions.ConvertToVector4
            (_transformationsManager.ObjectToTransform.position, PositionVectorWValue);
        Vector4 newPosition = _transformationsManager.Matrix * currentPosition;
        PositionVectorWValue = newPosition.w;
        StartCoroutine(LerpPosition(newPosition));
    }

    private IEnumerator LerpPosition(Vector3 targetPosition)
    {
        CoroutineActive = true;
        float currentTime = 0;
        Vector3 startingPosition = _transformationsManager.ObjectToTransform.position;
        while (currentTime < LerpDuration)
        {
            _transformationsManager.ObjectToTransform.position = Vector3.Lerp
                (startingPosition, targetPosition, currentTime / LerpDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        _transformationsManager.ObjectToTransform.position = targetPosition;
        CoroutineActive = false;
    }

    private void ApplyTransformationOnRotation()
    {
        Vector4 currentRotation = TransformExtensions.ConvertToVector4
            (_transformationsManager.ObjectToTransform.eulerAngles, RotationVectorWValue);
        Vector4 newRotation = _transformationsManager.Matrix * currentRotation;
        RotationVectorWValue = newRotation.w;
        StartCoroutine(LerpRotation(newRotation));
    }

    private IEnumerator LerpRotation(Vector3 targetRotation)
    {
        CoroutineActive = true;
        float currentTime = 0;
        Vector3 startingRotation = _transformationsManager.ObjectToTransform.eulerAngles;
        while (currentTime < LerpDuration)
        {
            _transformationsManager.ObjectToTransform.eulerAngles = Vector3.Lerp
                (startingRotation, targetRotation, currentTime / LerpDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        _transformationsManager.ObjectToTransform.eulerAngles = targetRotation;
        CoroutineActive = false;
    }

    private void ApplyTransformationOnScale()
    {
        Vector4 currentScale = TransformExtensions.ConvertToVector4
            (_transformationsManager.ObjectToTransform.localScale, ScaleVectorWValue);
        Vector4 newScale = _transformationsManager.Matrix * currentScale;
        ScaleVectorWValue = newScale.w;
        StartCoroutine(LerpScale(newScale));
    }

    private IEnumerator LerpScale(Vector3 targetScale)
    {
        CoroutineActive = true;
        float currentTime = 0;
        Vector3 startingScale = _transformationsManager.ObjectToTransform.localScale;
        while (currentTime < LerpDuration)
        {
            _transformationsManager.ObjectToTransform.localScale = Vector3.Lerp
                (startingScale, targetScale, currentTime / LerpDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        _transformationsManager.ObjectToTransform.localScale = targetScale;
        CoroutineActive = false;
    }
}

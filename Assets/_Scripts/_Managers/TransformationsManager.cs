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
    public Transform ObjectToTransform;
    public eTransformValue transformValueToManipulate;

    public Matrix4x4 Matrix { get; set; } = CommonMatrixTransfomations.Identity;
    public eManagerStatus Status { get; private set; }
    public TransformationApplier TransformationApplier { get; private set; }
    public bool ApplyContinuously { get; set; } = false;
    public Vector3[] MeshStartingVertices { get; set; }

    [SerializeField] private GhostObjects _ghostObjects;

    public void Startup()
    {
        Status = eManagerStatus.Initializing;
        TransformationApplier = GetComponent<TransformationApplier>();
        MeshStartingVertices = ObjectToTransform.GetComponent<MeshFilter>().mesh.vertices;
        Status = eManagerStatus.Started;
    }

	public void FixedUpdate()
	{
		if(ApplyContinuously)
		{
            ApplyTransformation();
		}
	}

	public void ApplyTransformation()
	{
		if (!TransformationApplier.CoroutineActive)
		{
            TransformationApplier.TransformObject();

            StartCoroutine(doWhenObjectTransformed());
        }
    }

    private IEnumerator doWhenObjectTransformed()
	{
        while (TransformationApplier.CoroutineActive)
		{
            yield return null;
		}

        TransformationApplied?.Invoke();
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
            UpdateMatrix(StringExtensions.StringToMatrix(newMatrix));
		}
    }

    public void UpdateApplyContinouslyValue(bool applyContinously)
    {
        ApplyContinuously = applyContinously;
		if (applyContinously)
		{
            TransformationApplier.LerpDuration = 0;
            _ghostObjects.gameObject.SetActive(false);
        }
		else
		{
            TransformationApplier.LerpDuration = TransformationApplier.StartingLerpDuration;
            _ghostObjects.gameObject.SetActive(true);
            _ghostObjects.DeleteAllGhosts();
        }
    }

    public void ResetObjectToOrigin()
	{
        ObjectToTransform.position = Vector3.zero;
        ObjectToTransform.eulerAngles = Vector3.zero;
        ObjectToTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        ObjectToTransform.GetComponent<MeshFilter>().mesh.vertices = MeshStartingVertices;
        _ghostObjects.DeleteAllGhosts();
    }

    public void UpdateMatrix(Matrix4x4 newMatrix)
	{
        Matrix = newMatrix;
        MatrixUpdated?.Invoke();
	}
}

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

    [SerializeField] private GameObject _ghostPrefab;
    [SerializeField] private int _maxGhosts;
    [SerializeField] private int _ghostsStartingAlpha;

    private float _alphaChangeBetweenGhosts;

    private Matrix4x4 _matrix;
    private List<GameObject> _ghosts = new List<GameObject>();

    public void Startup()
    {
        status = eManagerStatus.Initializing;
        _alphaChangeBetweenGhosts = _ghostsStartingAlpha / _maxGhosts;
        status = eManagerStatus.Started;
    }

    public void ApplyTransformation()
	{
        UpdateMatrix();

        UpdateGhosts();

        TransformObject();

        TransformationApplied?.Invoke();
    }

    private void UpdateGhosts()
	{
        if (_ghosts.Count < _maxGhosts)
        {
            CreateGhost();
        }
		else
		{
            UpdateGhostsTransform();
		}

        SetGhostsTransperancy();
    }

    private void CreateGhost()
	{
        GameObject newGhost = Instantiate(_ghostPrefab);
        TransformExtensions.SetObjectTransform(newGhost.transform, ObjectToTransform);
        _ghosts.Add(newGhost);
    }

    private void UpdateGhostsTransform()
	{
        for (int i = 0; i < _ghosts.Count; i++)
        {
            GameObject ghost = _ghosts[i];
            if (i != _ghosts.Count - 1)
            {
                TransformExtensions.SetObjectTransform(ghost.transform, _ghosts[i+1].transform);
            }
            else
            {
                TransformExtensions.SetObjectTransform(ghost.transform, ObjectToTransform);
            }
        }
    }

    private void SetGhostsTransperancy()
    {
        for (int i = 0; i < _ghosts.Count; i++)
        {
            int numberOfAlphaChanges = _ghosts.Count - (i + 1);
            SetGhostTransperancy(_ghosts[i], numberOfAlphaChanges);
        }
    }

    private void SetGhostTransperancy(GameObject ghost, int numberOfAlphaChanges)
	{
        Material material = ghost.GetComponent<MeshRenderer>().material;
        float newAlphaValue = _ghostsStartingAlpha - _alphaChangeBetweenGhosts * numberOfAlphaChanges;
        material.color = new Color(material.color.r, material.color.g, material.color.b, newAlphaValue / 255);
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
        Vector4 currentPosition = TransformExtensions.ConvertToVector4(ObjectToTransform.position);
        ObjectToTransform.position = _matrix * currentPosition;
    }

    private void ApplyTransformationOnRotation()
    {
        Vector4 currentRotation = TransformExtensions.ConvertToVector4(ObjectToTransform.eulerAngles);
        ObjectToTransform.eulerAngles = _matrix * currentRotation;
    }

    private void ApplyTransformationOnScale()
    {
        Vector4 currentScale = TransformExtensions.ConvertToVector4(ObjectToTransform.localScale);
        ObjectToTransform.localScale = _matrix * currentScale;
    }

    private void UpdateMatrix()
	{
        SetMatrixRow(0, _firstRow.text);
        SetMatrixRow(1, _secondRow.text);
        SetMatrixRow(2, _thirdRow.text);
        SetMatrixRow(3, _fourthRow.text);
    }

    private void SetMatrixRow(int row, string vector)
	{
        Vector4 result = StringExtensions.StringToVector4(vector);
        _matrix.SetRow(row, result);
    }
}

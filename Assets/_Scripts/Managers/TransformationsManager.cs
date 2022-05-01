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

        CreateGhostObject();

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

        TransformationApplied?.Invoke();
    }

    private void CreateGhostObject()
	{
        if (_ghosts.Count < _maxGhosts)
        {
            //Add material change
            GameObject newGhost = Instantiate(_ghostPrefab);
            newGhost.transform.position = ObjectToTransform.position;
            newGhost.transform.rotation = ObjectToTransform.rotation;
            newGhost.transform.localScale = ObjectToTransform.localScale;
            _ghosts.Add(newGhost);
        }
		else
		{
            for(int i = 0; i < _ghosts.Count; i++)
		    {

                GameObject ghost = _ghosts[i];
                if (i != _ghosts.Count - 1)
			    {
                    if(_ghosts.Count >= _maxGhosts)
				    {
                        ghost.transform.position = _ghosts[i + 1].transform.position;
                        ghost.transform.rotation = _ghosts[i + 1].transform.rotation;
                        ghost.transform.localScale = _ghosts[i + 1].transform.localScale;
                    }
                }
			    else
			    {
                    ghost.transform.position = ObjectToTransform.position;
                    ghost.transform.rotation = ObjectToTransform.rotation;
                    ghost.transform.localScale = ObjectToTransform.localScale;
                }
            }
		}
        SetGhostsTransperancy();
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

    private void ApplyTransformationOnPosition()
	{
        Vector4 currentPosition = new Vector4(ObjectToTransform.position.x, ObjectToTransform.position.y, ObjectToTransform.position.z, 1);
        ObjectToTransform.position = _matrix * currentPosition;
    }

    private void ApplyTransformationOnRotation()
    {
        Vector3 newRotation = _matrix * ObjectToTransform.eulerAngles;
        ObjectToTransform.rotation = Quaternion.Euler(newRotation);
    }

    private void ApplyTransformationOnScale()
    {
        Vector4 currentScale = new Vector4(ObjectToTransform.localScale.x, ObjectToTransform.localScale.y, ObjectToTransform.localScale.z, 1);
        ObjectToTransform.localScale = _matrix * currentScale;
    }

    private void UpdateMatrix()
	{
        FloatListToMatrixRow(0, StringExtensions.VectorStringToFloatList(_firstRow.text));
        FloatListToMatrixRow(1, StringExtensions.VectorStringToFloatList(_secondRow.text));
        FloatListToMatrixRow(2, StringExtensions.VectorStringToFloatList(_thirdRow.text));
        FloatListToMatrixRow(3, StringExtensions.VectorStringToFloatList(_fourthRow.text));
    }

    private void FloatListToMatrixRow(int row, List<float> floatList)
    {
        Vector4 result = new Vector4(floatList[0], floatList[1], floatList[2], floatList[3]);
        _matrix.SetRow(row, result);
    }
}

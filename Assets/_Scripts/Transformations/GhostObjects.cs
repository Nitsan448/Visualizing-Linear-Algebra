using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObjects : MonoBehaviour
{
    [SerializeField] private GameObject _ghostPrefab;
    [SerializeField] private int _maxGhosts;
    [SerializeField] private int _ghostsStartingAlpha;

    private float _alphaChangeBetweenGhosts;
    private List<GameObject> _ghosts = new List<GameObject>();
    private Transform _previousObjectTransform;
    private Mesh _previousObjectMesh;


    private void Start()
	{
        GameObject transformHolder = new GameObject();
        _previousObjectTransform = transformHolder.transform;
        TransformExtensions.CopyTransform(_previousObjectTransform, Managers.Transformations.ObjectToTransform);
        _previousObjectMesh = Managers.Transformations.ObjectToTransform.GetComponent<MeshFilter>().mesh;
        _alphaChangeBetweenGhosts = (_ghostsStartingAlpha / _maxGhosts);
    }

	private void OnEnable()
	{
        Managers.Transformations.TransformationApplied += UpdateGhosts;
	}

	private void OnDisable()
	{
        Managers.Transformations.TransformationApplied -= UpdateGhosts;
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

        _previousObjectMesh = Managers.Transformations.ObjectToTransform.GetComponent<MeshFilter>().mesh;
        TransformExtensions.CopyTransform(_previousObjectTransform, Managers.Transformations.ObjectToTransform);
    }

    private void CreateGhost()
    {
        GameObject newGhost = Instantiate(_ghostPrefab, transform);
        MeshExtensions.CopyVertices(newGhost.GetComponent<MeshFilter>().mesh, _previousObjectMesh);
        TransformExtensions.CopyTransform(newGhost.transform, _previousObjectTransform);
        _ghosts.Add(newGhost);
    }

    private void UpdateGhostsTransform()
    {
        for (int i = 0; i < _ghosts.Count; i++)
        {
            if (i != _ghosts.Count - 1)
            {
                MeshExtensions.CopyVertices(_ghosts[i].GetComponent<MeshFilter>().mesh, _ghosts[i + 1].GetComponent<MeshFilter>().mesh);
                TransformExtensions.CopyTransform(_ghosts[i].transform, _ghosts[i + 1].transform);
            }
            else
            {
                MeshExtensions.CopyVertices(_ghosts[i].GetComponent<MeshFilter>().mesh, _previousObjectMesh);
                TransformExtensions.CopyTransform(_ghosts[i].transform, _previousObjectTransform);
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
}

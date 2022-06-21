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


    private void Start()
	{
        GameObject transformHolder = new GameObject();
        _previousObjectTransform = transformHolder.transform;
        TransformExtensions.CopyTransform(_previousObjectTransform, Managers.Transformations.ObjectToTransform);
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

        TransformExtensions.CopyTransform(_previousObjectTransform, Managers.Transformations.ObjectToTransform);
    }

    private void CreateGhost()
    {
        GameObject newGhost = Instantiate(_ghostPrefab, transform);
        TransformExtensions.CopyTransform(newGhost.transform, _previousObjectTransform);
        _ghosts.Add(newGhost);
    }

    private void UpdateGhostsTransform()
    {
        for (int i = 0; i < _ghosts.Count; i++)
        {
            GameObject ghost = _ghosts[i];
            if (i != _ghosts.Count - 1)
            {
                TransformExtensions.CopyTransform(ghost.transform, _ghosts[i + 1].transform);
            }
            else
            {
                TransformExtensions.CopyTransform(ghost.transform, _previousObjectTransform);
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

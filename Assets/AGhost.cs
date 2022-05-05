using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AGhost : MonoBehaviour
{
    [SerializeField] private GameObject _ghostPrefab;
    [SerializeField] private int _maxGhosts;
    [SerializeField] private int _ghostsStartingAlpha;

    private float _alphaChangeBetweenGhosts;
    private List<GameObject> _ghosts = new List<GameObject>();
    private Transform _previousObjectTransform;


    public abstract void Start();

    private void OnEnable()
    {
        TransformationsManager.TransformationApplied += UpdateGhosts;
    }

    private void OnDisable()
    {
        TransformationsManager.TransformationApplied -= UpdateGhosts;
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

        CopyTransform(_previousObjectTransform, Managers.Transformations.ObjectToTransform);
    }

    protected abstract void CopyTransform(Transform objectToCopyTo, Transform objectToCopyFrom);

    private void CreateGhost()
    {
        GameObject newGhost = Instantiate(_ghostPrefab, transform);
        CopyTransform(newGhost.transform, _previousObjectTransform);
        _ghosts.Add(newGhost);
    }

    private void UpdateGhostsTransform()
    {
        for (int i = 0; i < _ghosts.Count; i++)
        {
            GameObject ghost = _ghosts[i];
            if (i != _ghosts.Count - 1)
            {
                CopyTransform(ghost.transform, _ghosts[i + 1].transform);
            }
            else
            {
                CopyTransform(ghost.transform, _previousObjectTransform);
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

    protected abstract void SetGhostTransperancy(GameObject ghost, int numberOfAlphaChanges);
}

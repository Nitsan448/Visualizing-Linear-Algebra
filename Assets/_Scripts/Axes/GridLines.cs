using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLines : MonoBehaviour
{
    [SerializeField] private GameObject _gridLinePrefab;
    [SerializeField] private int gridSize;
    // Start is called before the first frame update
    void Start()
    {
        CreateGridOnXAxis();
        CreateGridOnZAxis();
    }

    private void CreateGridOnXAxis()
	{
        for (int i = -gridSize; i <= gridSize; i++)
        {
            GameObject gridLine = Instantiate(_gridLinePrefab, gameObject.transform);
            LineRenderer line = gridLine.GetComponent<LineRenderer>();
            line.SetPosition(0, new Vector3(-gridSize, 0, i));
            line.SetPosition(1, new Vector3(gridSize, 0, i));
        }
    }

    private void CreateGridOnZAxis()
    {
        for (int i = -gridSize; i <= gridSize; i++)
        {
            GameObject gridLine = Instantiate(_gridLinePrefab, gameObject.transform);
            LineRenderer line = gridLine.GetComponent<LineRenderer>();
            line.SetPosition(0, new Vector3(i, 0, -gridSize));
            line.SetPosition(1, new Vector3(i, 0, gridSize));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VectorsManager : MonoBehaviour, IGameManager
{
    public static Action VectorsUpdated;

    public eManagerStatus status { get; private set; }
    public object result { get; private set; }
    public VectorsOperator vectorOperation { get; private set; }
    public Dictionary<int, Vector3> vectorByIndex{ get; private set; }

    [SerializeField] private Vector3[] startingVectors;

	public void Startup()
    {
        status = eManagerStatus.Initializing;

        vectorOperation = new VectorsOperator(eVectorOperations.DotProduct);
        InitializeVectorByIndexDictionary();
        UpdateResult();

        status = eManagerStatus.Started;
    }

    private void InitializeVectorByIndexDictionary()
	{
        vectorByIndex = new Dictionary<int, Vector3>();
        vectorByIndex[1] = startingVectors[0];
        vectorByIndex[2] = startingVectors[1];
    }

    public void SetVectorByString(int vector, string newVector)
    {
        vectorByIndex[vector] = StringExtensions.StringToVector3(newVector);
        UpdateResult();
    }

    public void UpdateResult()
    {
        result = vectorOperation.DoOperation(vectorByIndex[1], vectorByIndex[2]);
        VectorsUpdated?.Invoke();
    }

    public void NormalizeVector(int vectorIndex)
	{
        vectorByIndex[vectorIndex] = vectorByIndex[vectorIndex].normalized;
        UpdateResult();
    }
}

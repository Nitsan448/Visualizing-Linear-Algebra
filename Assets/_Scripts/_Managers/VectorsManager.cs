using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VectorsManager : MonoBehaviour, IGameManager
{
    public Action VectorsUpdated;

    public List<Vector3> Vectors = new List<Vector3>();
    public eManagerStatus status { get; private set; }
    public object result { get; private set; }
    public VectorsOperator vectorOperation { get; private set; }


    public void Startup()
    {
        status = eManagerStatus.Initializing;

        vectorOperation = new VectorsOperator(eVectorOperations.DotProduct);
        UpdateResult();

        status = eManagerStatus.Started;
    }

    public void SetVectorByString(int vector, string newVector)
    {
		if (StringExtensions.IsVectorStringFormatValid(newVector))
		{
            Vectors[vector] = StringExtensions.StringToVector3(newVector);
        }
        UpdateResult();
    }

    public void UpdateResult()
    {
        result = vectorOperation.DoOperation(Vectors[0], Vectors[1]);
        VectorsUpdated?.Invoke();
    }

    public void NormalizeVector(int vectorIndex)
	{
        Vectors[vectorIndex] = Vectors[vectorIndex].normalized;
        UpdateResult();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VectorsManager : MonoBehaviour, IGameManager
{
    public event Action OperationChanged;
    public event Action VectorsUpdated;

    public List<Vector3> Vectors = new List<Vector3>();
    public eManagerStatus Status { get; private set; }
    public object Result { get; private set; }
    public VectorsOperator VectorOperation { get; private set; }

    public eVectorOperations StartingOperation;

    public void Startup()
    {
        Status = eManagerStatus.Initializing;

        VectorOperation = new VectorsOperator(StartingOperation);
        UpdateResult();

        Status = eManagerStatus.Started;
    }

    public void SetVectorByString(int vectorIndex, string newVector)
    {
		if (StringExtensions.IsVectorStringFormatValid(newVector))
		{
            Vectors[vectorIndex] = StringExtensions.StringToVector3(newVector);
        }
        UpdateResult();
    }

    public void UpdateResult()
    {
        Result = VectorOperation.DoOperation(Vectors[0], Vectors[1]);
        VectorsUpdated?.Invoke();
    }

    public void NormalizeVector(int vectorIndex)
	{
        Vectors[vectorIndex] = Vectors[vectorIndex].normalized;
        UpdateResult();
    }

    public void UpdateOperation(eVectorOperations operation)
	{
        VectorOperation.Operation = operation;
        UpdateResult();
        OperationChanged?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorsOperator
{
    public eVectorOperations operation;

    public VectorsOperator(eVectorOperations vectorOperation)
    {
        operation = vectorOperation;
    }

    public object DoOperation(Vector3 firstVector, Vector3 secondVector)
    {
        switch (operation)
        {
            case eVectorOperations.Addition:
                return firstVector + secondVector;

            case eVectorOperations.DotProduct:
                return Vector3.Dot(firstVector, secondVector);

            case eVectorOperations.CrossProduct:
                return Vector3.Cross(firstVector, secondVector);

            case eVectorOperations.Reflection:
                return Vector3.Reflect(firstVector, secondVector);

            case eVectorOperations.Projection:
                return Vector3.Project(firstVector, secondVector);
        }
        return Vector3.zero;
    }
}

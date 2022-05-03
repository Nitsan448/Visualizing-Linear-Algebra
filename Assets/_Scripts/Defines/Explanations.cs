using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Explanations
{
    public const string DotProductExplanation = "Dot product explanation";
    public const string CrossProductExplanation = "Cross product explanation";
    public const string ReflectionExplanation = "Reflection explanation";
    public const string ProjectionExplanation = "Projection explanation";

    public const string MatrixTransformationExplanation = "Matrix transformation explanation";

    public static readonly Dictionary<eVectorOperations, string> ExplanationByVectorOperation = new Dictionary<eVectorOperations, string>()
    {
        { eVectorOperations.DotProduct, DotProductExplanation },
        { eVectorOperations.CrossProduct, CrossProductExplanation },
        { eVectorOperations.Reflection, ReflectionExplanation },
        { eVectorOperations.Projection, ProjectionExplanation }
    };
}
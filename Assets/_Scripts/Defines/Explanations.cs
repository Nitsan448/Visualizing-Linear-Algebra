using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Explanations
{
    public const string DotProductExplanation = "Dot product explanation";
    public const string CrossProductExplanation = "The Cross product of two vectors is a vector that is perpendicular to both of them.\n" +
		"And thus a normal to the plane containing them.";
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
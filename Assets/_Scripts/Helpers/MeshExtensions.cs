using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshExtensions
{
    public static void CopyVertices(Mesh meshToCopyTo, Mesh meshToCopyFrom)
	{
        meshToCopyTo.vertices = meshToCopyFrom.vertices;
	}
}

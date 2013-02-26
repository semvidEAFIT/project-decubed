using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Vector3EqualityComparer : IEqualityComparer<Vector3Int>{

    public bool Equals(Vector3Int v1, Vector3Int v2)
    {
        return v1.Equals(v2);
    }

    public int GetHashCode(Vector3Int obj)
    {
		int x = obj.x;
		int y = obj.y;
		int z = obj.z;
		
        string xcomp = (x >= 0) ? "0" + x : "1" + Mathf.Abs(x); //0 Positivo o 0, 1 Negativo
        string ycomp = (y >= 0) ? "0" + y : "1" + Mathf.Abs(y); //0 Positivo o 0, 1 Negativo
        string zcomp = (z >= 0) ? "0" + z : "1" + Mathf.Abs(z); //0 Positivo o 0, 1 Negativo
       
		string hashCode = xcomp + ycomp + zcomp;

        return (int.Parse(hashCode));
    }
}

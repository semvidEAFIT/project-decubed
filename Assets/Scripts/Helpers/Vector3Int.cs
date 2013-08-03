using UnityEngine;
using System.Collections;

public class Vector3Int {

	private int xInt,yInt,zInt;
	
	#region Constructors
	public Vector3Int(int x, int y, int z){
		this.xInt = x;
		this.yInt = y;
		this.zInt = z;
	}
	
	public Vector3Int(Vector3 position) : this(Mathf.RoundToInt(position.x),Mathf.RoundToInt(position.y),Mathf.RoundToInt(position.z)){
		
	}
	#endregion
		
	#region Gets and Sets
	public int x {
		get {
			return this.xInt;
		}
		set {
			xInt = value;
		}
	}

	public int y {
		get {
			return this.yInt;
		}
		set {
			yInt = value;
		}
	}

	public int z {
		get {
			return this.zInt;
		}
		set {
			zInt = value;
		}
	}
	
	public Vector3 ToVector3{
		get {
			return new Vector3(xInt,yInt,zInt);
		}
	}

	#endregion
	
	public override bool Equals (object v)
	{
		if (v != null && v is Vector3Int) {
			Vector3Int v2 = (Vector3Int)v;
			return this.x == v2.x && this.y == v2.y && this.z == v2.z;
		}
		return false;
	}
	
	public Vector3Int Add (Vector3 v3)
	{
		return new Vector3Int (v3 + this.ToVector3);
	}
	
	/// <summary>
	/// Serves as a hash function for a <see cref="Vector3Int"/> object.
	/// </summary>
	/// <returns>
	/// A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.
	/// </returns>
	public override int GetHashCode ()
	{
		int x = this.xInt;
		int y = this.yInt;
		int z = this.zInt;
		
        string xcomp = (x >= 0) ? "0" + x : "1" + Mathf.Abs(x); //0 Positivo o 0, 1 Negativo
        string ycomp = (y >= 0) ? "0" + y : "1" + Mathf.Abs(y); //0 Positivo o 0, 1 Negativo
        string zcomp = (z >= 0) ? "0" + z : "1" + Mathf.Abs(z); //0 Positivo o 0, 1 Negativo
       
		string hashCode = xcomp + ycomp + zcomp;

        return (int.Parse(hashCode));
	}
	
	public override string ToString ()
	{
		return string.Format ("[Vector3Int: x={0}, y={1}, z={2}]", x, y, z);
	}
}

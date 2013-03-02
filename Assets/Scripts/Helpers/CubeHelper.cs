using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Cube helper.
/// 
/// TODO: Quitar esta clase y mover metodos a Level, por que Level es el qeu tiene esta responsabilidad
/// </summary>
public class CubeHelper
{
	/// <summary>
	/// Returns the next position available based on the position given. It looks vertically
	/// </summary>
	/// <returns>
	/// The top position.
	/// </returns>
	/// <param name='position'>
	/// Position.
	/// </param>
	public static Vector3Int GetTopPosition (Vector3 position)
	{
		Vector3Int finalPosition = new Vector3Int(position);
		bool occupied = Level.Singleton.ContainsElement(finalPosition);
		
		if (occupied){
			while (occupied) {
				finalPosition.y++;
				Debug.Log("Entro 1 " + position);
				occupied = Level.Singleton.ContainsElement (finalPosition);
				Debug.Log("Entro 2" + occupied);
			}
		}else{
			
			while (!occupied) {
				finalPosition.y--;
				occupied = Level.Singleton.ContainsElement (finalPosition);
				
				if (finalPosition.y < 0.5)//TODO:Change floor check
                {
                    break;
                }
			}
			finalPosition.y++;
		}
		
		return finalPosition;
	}
	
	/// <summary>
	/// Checks if the position given is available based on the jump power, Returns null if its not available
	/// </summary>
	public static bool CheckAvailablePosition(Vector3 position, out Vector3Int finalPosition, int height){
		finalPosition = GetTopPosition(position);
		return (finalPosition.y - Mathf.RoundToInt(position.y) <= height);
	}
	
	//TODO Cambiar a PositionIsAvailable
	public static bool IsFree (Vector3 position)
	{
		return !Level.Singleton.ContainsElement (position);
	}
//
//	public static Vector3 GetLastPositionInDirection (Vector3 position, Vector3 direction)
//	{
//		int diff = GetDifferenceInDirection (position, direction.normalized);
//		if (diff >= 0) {
//			return position + (direction * (diff - 1));
//		} else {
//			throw new UnityException ("No encontro cubo en esa direccion: " + direction);
//		}
//	}
//	
//	public static Entity GetEntityInPosition (Vector3 position)
//	{
//		if (Level.Singleton.ContainsElement (position)) {
//			return Level.Singleton.getEntity (position);
//		} else {
//			return null;
//		}
//	}
//
//	public static Vector3[] GetPositionsAround (Vector3 position)
//	{
//		List<Vector3> positionsAround = new List<Vector3> ();
//		positionsAround.Add (position + Vector3.up);
//		positionsAround.Add (position + Vector3.down);
//		positionsAround.Add (position + Vector3.right);
//		positionsAround.Add (position + Vector3.left);
//		positionsAround.Add (position + Vector3.forward);
//		positionsAround.Add (position + Vector3.back);
//		return positionsAround.ToArray ();
//	}
//
//	public static Entity[] GetEntitiesAround (Vector3 position)
//	{
//		Vector3[] positionsAround = GetPositionsAround (position);
//		List<Entity> entitiesAround = new List<Entity> ();
//		foreach (Vector3 v in positionsAround) {
//			if (!IsFree (v)) {
//				entitiesAround.Add (GetEntityInPosition (v));
//			}
//		}
//		return entitiesAround.ToArray ();
//	}
//	
//	public static int GetDifferenceInDirection (Vector3 position, Vector3 direction)
//	{
//		direction = direction.normalized;
//		List<Vector3> inColumn = new List<Vector3> ();
//		Vector3 valueFinder = VectorHelper.BinaryInverse (direction);
//		valueFinder = VectorHelper.Abs (valueFinder);
//		Vector3 columnPosition = VectorHelper.Multiply (position, valueFinder);
//		Vector3 valueGetter = VectorHelper.Abs (direction);
//		float min = -1;
//		foreach (Vector3Int key in Level.Singleton.Entities.Keys) {
//			Vector3 keyV = key.PositionFloats;
//			if (!(position == keyV)
//				&& VectorHelper.Eq (VectorHelper.Multiply (keyV, valueFinder), columnPosition)) {
//				Vector3 directionTemp = keyV - position;
//				float diff = Vector3.Dot (direction, directionTemp);
//				if (diff > 0) {
//					if (min == -1) {
//						min = diff;
//					} else {
//						min = Mathf.Min (min, diff);
//					}
//				}
//			}
//		}
//		return Mathf.RoundToInt (min);
//	}
}

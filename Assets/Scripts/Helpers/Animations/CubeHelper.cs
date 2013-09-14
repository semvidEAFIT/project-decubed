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
		Vector3Int finalPosition = new Vector3Int (position);
		Level level = Level.Singleton;
		bool occupied = Level.Singleton.ContainsElement (finalPosition);
		
		if (occupied) {
			while (occupied) {
				finalPosition.y++;
				occupied = level.ContainsElement (finalPosition);
			}
		} else {
			
			while (!occupied) {
				finalPosition.y--;
				occupied = level.ContainsElement (finalPosition);
				
				if (finalPosition.y < 0.5) {//TODO:Change floor check
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
	public static bool CheckAvailablePosition (Vector3 position, out Vector3Int finalPosition, int height)
	{
		if (Level.Singleton.IsInDimension (position)) {
			
			finalPosition = GetTopPosition (position);
			return (finalPosition.y - Mathf.RoundToInt (position.y) <= height);
			
		} else {
			finalPosition = null;
			return false;
		}
	}

	public static bool IsFree (Vector3Int position)
	{
		return !Level.Singleton.ContainsElement(position);
	}
	
	public static List<Command> GetListOptions(Command [] commands){
		List<Command> options = new List<Command>();
		foreach (Command c in commands){
			options.Add(c);
		}
		return options;
	}
	/// <summary>
	/// Checks the last position in the specify direction.
	/// </summary>
	/// <returns>
	/// True if there is a last position and it isnt the.
	/// </returns>
	/// <param name='direction'>
	/// If set to <c>true</c> direction.
	/// </param>
	/// <param name='finalPosition'>
	/// If set to <c>true</c> final position.
	/// </param>
	/// 
	///
	/// <summary>
	/// Checks the last position in the specify direction.
	/// </summary>
	/// <returns>
	///  True if there is a last position and it isnt next to psition.
	/// </returns>
	/// <param name='position'>
	/// position to serch from
	/// </param>
	/// <param name='direction'>
	/// directin to serch
	/// </param>
	/// <param name='finalPosition'>
	/// out the las position. Vector3Int.zero return == false
	/// </param>
	/// 
	public static bool CheckLastPosition(Vector3Int position ,Vector3Int direction, out Vector3Int finalPosition){
		Vector3Int aux = new Vector3Int(position.ToVector3);
		aux = aux.Add(direction.ToVector3);
		aux = aux.Add(direction.ToVector3);
		Vector3Int c;
		for(int i = 0; i < 11; i++){
			if(Level.Singleton.ContainsElement(aux)||aux.y==0){
				if(i>0 || aux.y==0){
					finalPosition = aux.Add(new Vector3(direction.x*-1,direction.y*-1,direction.z*-1));
					return true;
				}else{
					finalPosition = new Vector3Int(Vector3.zero);
					return false;
				}
			}
			aux = aux.Add(direction.ToVector3);
		}
		finalPosition = new Vector3Int(Vector3.zero);
		return false;
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

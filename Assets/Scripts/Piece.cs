using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PieceType
{
	none = -1,
	ramp = 0,
	jump = 1,
	slide = 2,
}
public class Piece : MonoBehaviour
{
	public PieceType type;
	public int visualIndex;
}

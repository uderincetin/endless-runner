using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { set; get; }

	private const float DISTANCE_BEFORE_SEGMENTS = 100.0f;
	private const int MAX_SEGMENTS_ON_SCREEN = 10;
	private Transform cameraContainer;
	private int amountOfActiveSegments;
	private int continousSegments;
	private int currentSpawnZ;
	private int currentLevel;
	private int y1, y2, y3;
	
	
	// List of Pieces
	public List<Piece> ramp = new List<Piece>();
	public List<Piece> jump = new List<Piece>();
	public List<Piece> slide = new List<Piece>();
	public List<Piece> pieces = new List<Piece>();

	public Piece GetPiece(PieceType pt, int visualIndex)
	{
		Piece p = pieces.Find(x => x.type == pt && x.visualIndex == visualIndex && !x.gameObject.activeSelf);
		
		if (p == null)
		{
			GameObject go = null;
			if (pt == PieceType.ramp)
				go = ramp[visualIndex].gameObject;
			else if (pt == PieceType.jump)
				go = jump[visualIndex].gameObject;
			else if (pt == PieceType.slide)
				go = slide[visualIndex].gameObject;

			go = Instantiate(go);
			p = go.GetComponent<Piece>();
			pieces.Add(p);
		}
		return p;
	}

}

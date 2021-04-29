using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private const int COIN_VALUE = 5;

	public static GameManager Instance { set; get; }

	private bool isGameStarted = false;
	private PlayerManager playerManager;

	//UI deðerleri
	public Text scoreText, diamondText, modifierText;
	private float score, diamond, modifier;
	private int lastScore;

	private void Awake()
	{
		Instance = this;
		modifier = 1;
		modifierText.text = "x" + modifier.ToString("0.0");
		scoreText.text = score.ToString("0");
		diamondText.text = diamond.ToString("0");
		playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
	}

	private void Update()
	{
		if(MobileInput.Instance.Tap && !isGameStarted)
		{
			isGameStarted = true;
			playerManager.StartGame();
		}

		if (isGameStarted)
		{
			score += (Time.deltaTime * modifier);
			if(lastScore != (int) score)
			{
				lastScore = (int)score;
				scoreText.text = score.ToString("0");
			}
		}

	}
	public void GetCoin()
	{
		diamond++;
		score += COIN_VALUE;
		diamondText.text = diamond.ToString("0");
		scoreText.text = score.ToString("0");
	}

	public void UpdateModifier(float modifierAmount)
	{
		modifier = 1.0f + modifierAmount;
		modifierText.text = "x" + modifier.ToString("0.0");
	}

}

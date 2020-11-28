using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameUi))]
public class GameController : MonoBehaviour {

	public static GameController Instance { get; private set; }

	[SerializeField] private int knifeCount;

	[Header("Knife Spawning")] 
	[SerializeField]
	private Vector2 knifeSpawnPosition;

	[SerializeField] private GameObject knifeObject;
	
	public GameUi GameUi { get; private set; }

	private void Awake()
	{
		Instance = this;
		GameUi = GetComponent<GameUi>();
	}

	private void Start()
	{
		GameUi.SetInitialDisplayedKnifeCount(knifeCount);
		SpawnKnife();
	}

	public void OnSuccessfulKnifeHit()
	{
		if (knifeCount > 0)
		{
			SpawnKnife();
		}
		else
		{
			StartGameOverSequence(true);
		}
	} 
	
	public void SpawnKnife()
	{
		knifeCount--;
		Instantiate(knifeObject, knifeSpawnPosition, Quaternion.identity);

	}

	public void StartGameOverSequence(bool win)
	{
		StartCoroutine("GameOverSequenceCoroutine", win);
	}

	private IEnumerator GameOverSequenceCoroutine(bool win)
	{
		if (win)
		{
			yield return new WaitForSecondsRealtime(0.3f);
			GameUi.ShowRestartButton();
		}
		else
		{
			GameUi.ShowRestartButton();
		}
	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WaitForNextFrame = UnityEngine.WaitForEndOfFrame;

public class UIManager : MonoBehaviour
{
    private float timeElapsed;
	[SerializeField] private float uiStaySeconds;
	[SerializeField] private GameObject player;
    [SerializeField] private GameObject timerText;
    [SerializeField] private GameObject fadeImageUI;
    [SerializeField] private GameObject levelStartText;
    [SerializeField] private GameObject levelFailedText;
    [SerializeField] private GameObject levelCompleteText;
	[SerializeField] private GameObject gameEndText;
	[SerializeField] private KeyboardMovement canMoveReference;

	void Start ()
	{
        TurnOnLevelStartUI();
	}

    private void Update()
	{
        GetAndSetTime();
		if (player.transform.localScale.x > 2.251f)
        {
            StartCoroutine(LevelFailedUI());
        }
	}	

    private void GetAndSetTime()
    {
        if (!timerText.activeSelf)
            return;
        timeElapsed = Time.timeSinceLevelLoad-uiStaySeconds;
        timerText.GetComponent<Text>().text = "Time Elapsed: " + timeElapsed.ToString("#0.0");
    }

	public void TurnOnLevelStartUI()
	{
		StartCoroutine(LevelStartUI());
	}

	public void TurnOnLevelCompleteUI()
	{
		StartCoroutine(LevelCompleteUI());
	}

	public void TurnOnLevelFailedUI()
	{
		StartCoroutine(LevelFailedUI());
	}

	public void TurnOnGameEndUI()
	{
		StartCoroutine(GameEndUI());
	}

    private IEnumerator LevelStartUI()
    {
        Debug.Log("Level Start");
        fadeImageUI.SetActive(true);
        levelStartText.SetActive(true);
        yield return new WaitForSeconds(uiStaySeconds);
		fadeImageUI.SetActive(false);
        levelStartText.SetActive(false);
		canMoveReference.SetCanMove(true);
        timerText.SetActive(true);
    }

	private IEnumerator LevelCompleteUI()
    {
        Debug.Log("Level Complete");
        fadeImageUI.SetActive(true);
        levelCompleteText.SetActive(true);
		canMoveReference.SetCanMove(false);
        yield return new WaitForSeconds(uiStaySeconds);
        SceneManager.LoadScene("MazeLevel_2");
    }
	
	private IEnumerator LevelFailedUI()
    {
        Debug.Log("Level Failed");
        fadeImageUI.SetActive(true);
        levelFailedText.SetActive(true);
		canMoveReference.SetCanMove(false);
		Destroy(player.GetComponent<CircleCollider2D>());
        yield return new WaitForSeconds(uiStaySeconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	private IEnumerator GameEndUI()
	{
		Debug.Log("Game End");
        fadeImageUI.SetActive(true);
        gameEndText.SetActive(true);
		canMoveReference.SetCanMove(false);
		Destroy(player.GetComponent<CircleCollider2D>());
        yield return new WaitForSeconds(uiStaySeconds*1.5f);
        SceneManager.LoadScene("MazeLevel_1");
	}
}

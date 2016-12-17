using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public CharacterController character;
	public string gameScene;
	public Animation introAnimation;

	void Start ()
	{
		SetState(GameState.start);
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void RestartGame ()
	{
		SceneManager.LoadScene(gameScene);
	}

	void EnableCharacter (bool enable)
	{
		character.enabled = enable;
		character.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = enable; 
		character.GetComponentInChildren<LookAtInteraction>().enabled = enable;
		if(enable)
			Cursor.lockState = CursorLockMode.Locked;
		else
			Cursor.lockState = CursorLockMode.None;
	}

	#region State Machine

	public enum GameState { undefined, start, mainGameLoop, gameOver, gameWon, restart };

	GameState gameState = GameState.undefined; 

	void SetState (GameState theState)
	{
		switch(theState)
		{
			case GameState.start:
				EnableCharacter(false);
				introAnimation.Play();
				StartCoroutine(SetStateDelayed(introAnimation.clip.length, GameState.mainGameLoop));
				break;
			case GameState.mainGameLoop:
				EnableCharacter(true);
				break;
			case GameState.gameOver:
				break;
			case GameState.gameWon:
				EnableCharacter(false);
				GetComponent<AudioSource>().Play();
				StartCoroutine(SetStateDelayed(GetComponent<AudioSource>().clip.length, GameState.restart));
				break;
			case GameState.restart:
				RestartGame();
				break;
		}

		gameState = theState;
	}

	// This is a "Coroutine". You can recognize it by the IEnumerator return value.
	// It is used to execute code with a timing.
	// In order to work, it has to be called with the command StartCoroutine(CoroutineName());
	// Make yourself familiar with Coroutines - you'll need them!
	// https://unity3d.com/de/learn/tutorials/topics/scripting/coroutines
	IEnumerator SetStateDelayed (float delayTime, GameState state)
	{
		// the yield statement pauses the execution for a certain time. 
		yield return new WaitForSeconds(delayTime);

		SetState(state);
	}

	#endregion

	#region Main Gameloop

	int collectedObjects = 0;

	public void CollectObject (int objectID)
	{
		collectedObjects++;

		EnableCharacter(false);
		CloseUpObjectPreview.instance.ShowPreview(objectID);
	}

	public void OnClosePreview ()
	{
		EnableCharacter(true);

		if(collectedObjects == 3)
		{
			StartCoroutine(SetStateDelayed(1f, GameState.gameWon));
		}
	}

	#endregion

	#region Accessing the object

	// Kind of like a Singleton. Easy way to access MonoBehaviors that only exist once in a game. 
	public static GameManager instance;

	// Awake() is like Start() but will be called earlier. See https://docs.unity3d.com/Manual/ExecutionOrder.html
	void Awake ()
	{
		GameManager.instance = this;
	}

	#endregion
}

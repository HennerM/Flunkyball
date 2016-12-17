using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameStarter : MonoBehaviour {

	// in order to load a scene via the scene manager, it has to be added to tbe build manager
	// https://docs.unity3d.com/Manual/BuildSettings.html
	public string gameScene;

	bool gameSceneIsLoaded = false;

	void Start ()
	{
		Restart();
	}

	public void Restart ()
	{
		if(gameSceneIsLoaded)
		{
			SceneManager.UnloadScene(gameScene);
		}

		SceneManager.LoadScene(gameScene, LoadSceneMode.Additive);
		gameSceneIsLoaded = true;
	}

	#region Accessing the object

	public static GameStarter instance;

	void Awake () 
	{
		GameStarter.instance = this;
	}

	#endregion
}

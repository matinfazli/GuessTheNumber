using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

	public Button EasyButton;
	public Button NormalButton;
	public Button HardButton;
	public Button InsaneButton;

	void Start () 
	{
		AttachListeners();	
	}

	private void AttachListeners()
	{
		EasyButton.GetComponent<Button>().onClick.AddListener(delegate { StartGame(1, 10, 4); });
		NormalButton.GetComponent<Button>().onClick.AddListener(delegate { StartGame(1, 100, 7); });
		HardButton.GetComponent<Button>().onClick.AddListener(delegate { StartGame(-1000, 1000, 10); });
		InsaneButton.GetComponent<Button>().onClick.AddListener(delegate { StartGame(Random.Range(-10000, 0), Random.Range(1, 10000), 15); });
	}

	private void StartGame(int minNumber, int maxNumber, int maxTryCount)
	{
		CrossSceneService.MinNumber = minNumber;
		CrossSceneService.MaxNumber = maxNumber;
		CrossSceneService.MaxGuessCount = maxTryCount;
		
		SceneManager.LoadScene("Game");
	}

}

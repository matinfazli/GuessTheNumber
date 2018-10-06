using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Text LevelRangeText;
    public Text GuessCountText;
    public Text HintText;
    public Text GameEndingText;
    public InputField GuessInputField;
    public Button SubmitButton;

    private int _targetNumber;
    private int _guessCount;

    public void Start()
    {
        _guessCount = 0;
        _targetNumber = Random.Range(CrossSceneService.MinNumber, CrossSceneService.MaxNumber);
        
        LevelRangeText.GetComponent<Text>().text = CrossSceneService.MinNumber + " to " + CrossSceneService.MaxNumber;
        GuessCountText.GetComponent<Text>().text = _guessCount + "/" + CrossSceneService.MaxGuessCount + " attempts";
        
        SubmitButton.GetComponent<Button>().onClick.AddListener(OnGuessSubmit);
    }

    private void OnGuessSubmit()
    {
        UpdateTryCount();
        
        int guessNumber;
        int.TryParse(GuessInputField.GetComponent<InputField>().text, out guessNumber);

        if (guessNumber == _targetNumber)
        {
            StartCoroutine(GameWon());
        }
        else if (_guessCount == CrossSceneService.MaxGuessCount)
        {
            StartCoroutine(GameLost());
        }
        else if (guessNumber > _targetNumber)
        {
            ShowTooHighHint();
        }
        else if (guessNumber < _targetNumber)
        {
            ShowTooLowHint();
        }
    }

    private void UpdateTryCount()
    {
        _guessCount++;
        GuessCountText.GetComponent<Text>().text = _guessCount + "/" + CrossSceneService.MaxGuessCount + " attempts";
    }

    private void ShowTooHighHint()
    {
        HintText.GetComponent<Text>().text = "Nah, try a lower number";
    }
    
    private void ShowTooLowHint()
    {
        HintText.GetComponent<Text>().text = "Nah, try a higher number";
    }

    private void DisableSubmit()
    {
        SubmitButton.GetComponent<Button>().enabled = false;
    }

    private IEnumerator GameWon()
    {
        DisableSubmit();
        GameEndingText.GetComponent<Text>().text = "You Won!";
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }
    
    private IEnumerator GameLost()
    {
        DisableSubmit();
        GameEndingText.GetComponent<Text>().text = "You Lost!";
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }
}
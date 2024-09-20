using UnityEngine;
using UnityEngine.UI;
using TMPro;  

public class SettingsController : MonoBehaviour
{
    public TMP_InputField ballSpeedInputField;  
    public TMP_InputField maxScoreInputField;   
    public TMP_Text errorMessageText;           
    public Button applySettingsButton;          

    private float ballSpeed = 10f;  
    private int maxScore = 5;       

    
    void Start()
    {
        errorMessageText.text = ""; 

        applySettingsButton.onClick.AddListener(OnApplySettings);
    }

    public void OnApplySettings()
    {
        bool isValid = true; 

        if (float.TryParse(ballSpeedInputField.text, out float newBallSpeed))
        {
            if (newBallSpeed >= 5 && newBallSpeed <= 15)
            {
                ballSpeed = newBallSpeed;
            }
            else
            {
                ShowErrorMessage("Ball speed must be between 5 and 15.");
                isValid = false;
            }
        }
        else
        {
            ShowErrorMessage("Invalid ball speed input.");
            isValid = false;
        }

        if (int.TryParse(maxScoreInputField.text, out int newMaxScore))
        {
            if (newMaxScore > 0)
            {
                maxScore = newMaxScore;
            }
            else
            {
                ShowErrorMessage("Max score must be a positive number.");
                isValid = false;
            }
        }
        else
        {
            ShowErrorMessage("Invalid max score input.");
            isValid = false;
        }

        if (isValid)
        {
            PlayerPrefs.SetFloat("BallSpeed", ballSpeed);  
            PlayerPrefs.SetInt("MaxScore", maxScore);      
            errorMessageText.text = "Settings applied successfully!";
        }
    }

    private void ShowErrorMessage(string message)
    {
        errorMessageText.text = message;
        CancelInvoke();  
        Invoke("ClearErrorMessage", 3f);  
    }

    
    private void ClearErrorMessage()
    {
        errorMessageText.text = "";
    }
}
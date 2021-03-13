using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordCheck : MonoBehaviour
{
    [SerializeField] private string password;
    public InputField passwordField;
    [HideInInspector] public string userInput;

    public Text degubText;

    public void CheckPassword(GameObject target)
    {
        userInput = passwordField.text;

        if (userInput != null)
        {
            if (userInput == password)
            {
                target.SetActive(false);
            }
            else
            {
                degubText.color = new Color(255, 0, 0);
                degubText.text = "Senha inválida";
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassaFase : MonoBehaviour
{
    [SerializeField] private GameObject modelButtonHalfLine;
    [SerializeField] private GameObject modelButtonOneLine;
    [SerializeField] private GameObject modelButtonTwoLines;
    [SerializeField] private GameObject modelButtonThreeLines;

    public void RestartFase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        PoolManager.Instance.ResetPool(modelButtonHalfLine);
        PoolManager.Instance.ResetPool(modelButtonOneLine);
        PoolManager.Instance.ResetPool(modelButtonTwoLines);
        PoolManager.Instance.ResetPool(modelButtonThreeLines);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + -1);
    }
}

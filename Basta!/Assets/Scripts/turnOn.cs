using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;

    public void LigarButton ()
    {
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        button3.gameObject.SetActive(true);
        button4.gameObject.SetActive(true);

    }

    public void LigarSoUm ()
    {
        button4.gameObject.SetActive(true);
    }
        
}

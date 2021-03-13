using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    private float randomNumber;
    private Animator animator;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        animator = GetComponent<Animator>();

        while (true)
        {
            randomNumber = Random.Range(0f, 11f);

            if (randomNumber >= 7f)
            {
                animator.Play("Flip");
            }

            yield return new WaitForSeconds(randomNumber / 2);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCredits : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator.Play("Credits");
        StartCoroutine(WaitForAnimation());
    }

    // Update is called once per frame
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        GameController.Instance.GameScene = Assets.Scripts.GameScene.MainMenu;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameController.Instance.GameScene = Assets.Scripts.GameScene.MainMenu;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleAnimation : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] Image image;
    [SerializeField] Sprite defaultAnim;
    [SerializeField] Sprite[] animationList;
    int animIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        defaultAnim = animationList[0];
    }

    // Update is called once per frame
    IEnumerator AnimationInProgress()
    {
        yield return new WaitForSeconds(delay);
        animIndex++;
        if (animIndex >= animationList.Length) {
            animIndex = 0;
            StopCoroutine(AnimationInProgress());
        }
        else {
            StartCoroutine(AnimationInProgress());
        }
        image.sprite = animationList[animIndex];
    }

    public void RunAnimation() {
        StartCoroutine(AnimationInProgress());
    }

    public void StopAnimation() {
        StopCoroutine(AnimationInProgress());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Vector2 dest;
    [SerializeField] float speedX = 0.400f;
    [SerializeField] float speedY = 0.208f;
    [SerializeField] GameObject parallaxPrefab;
    RectTransform parallaxTransform;
    int counter = 1;
    bool stopParallax = false;

    private void Start() {
        parallaxTransform = GetComponent<RectTransform>();
        if (parallaxPrefab != null) {
            Instantiate(parallaxPrefab, transform);
        }
    }

    IEnumerator ParallaxProgress() {
        yield return new WaitForEndOfFrame();
        parallaxTransform.localPosition = new Vector2(parallaxTransform.localPosition.x - speedX, parallaxTransform.localPosition.y - speedY);
        if (parallaxTransform.localPosition.x < dest.x && parallaxTransform.localPosition.x < dest.y) {
            if (parallaxPrefab != null) {
                parallaxTransform.localPosition = new Vector2();
            }
            if (!stopParallax) {
                StartCoroutine(ParallaxProgress());
            }
        }
    }

    public void RunAnimation() {
        stopParallax = false;
        StartCoroutine(ParallaxProgress());
    }

    public void StopAnimation() {
        stopParallax = true;
        StopCoroutine(ParallaxProgress());
    }
}

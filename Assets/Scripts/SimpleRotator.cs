using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    [SerializeField] int frame;
    [SerializeField] float delay;
    RectTransform rectTransform;
    int rotationIndex;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    IEnumerator RotationInProgress() {
        yield return new WaitForSeconds(delay);
        rotationIndex++;
        if (rotationIndex >= frame) {
            rotationIndex = 0;
            StopCoroutine(RotationInProgress());
        }
        else {
            StartCoroutine(RotationInProgress());
        }
        rectTransform.Rotate(0, 0, -360 / frame);
    }

    public void RunRotator() {
        StartCoroutine(RotationInProgress());
    }

    public void StopRotator() {
        StopCoroutine(RotationInProgress());
    }
}

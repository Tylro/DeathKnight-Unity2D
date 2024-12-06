using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartShards : MonoBehaviour
{
    public Image fill;

    public float targetFillAmount;
    public float lerpDuration = 1.5f;
    public float initialFillAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator LerpFill()
    {
        float elapsedTime = 0f;

        while(elapsedTime < lerpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / lerpDuration);

            float lerpedFillAmount = Mathf.Lerp(initialFillAmount, targetFillAmount, t);
            fill.fillAmount = lerpedFillAmount;

            yield return null;
        }

        fill.fillAmount = targetFillAmount;

        if(fill.fillAmount == 1)
        {
            PlayerController.Instance.maxHealth++;
            PlayerController.Instance.onHealthChangedCallback();
            PlayerController.Instance.heartShards = 0;
        }
    }
}

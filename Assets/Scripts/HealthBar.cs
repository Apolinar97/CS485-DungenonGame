using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthBar : MonoBehaviour
{
    //source https://www.youtube.com/watch?v=CA2snUe7ARM

    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private float updateSpeedSeconds = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
       GetComponentInParent<CombatantScript>().OnHealthChanged += HandleHealthChanged;    
    }
     
    private void HandleHealthChanged(float health)
    {
       StartCoroutine(ChangeHealthBar(health));
    }

    private IEnumerator ChangeHealthBar(float health)
    {
        float preChanged = foregroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChanged, health, elapsed / updateSpeedSeconds);
            yield return null;
        }

        foregroundImage.fillAmount = health;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.current.transform);
        transform.Rotate(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

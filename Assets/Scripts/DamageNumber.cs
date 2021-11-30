using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamageNumber : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Color textColor;

    private float disappearTimer = 1f;

    static float DISAPPEAR_SPEED = 3f;

    static public int damage = 2;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetComponent<TextMeshProUGUI>();
        textColor = text.color;
        text.SetText(damage.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 20f) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            textColor.a -= DISAPPEAR_SPEED * Time.deltaTime;
            text.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

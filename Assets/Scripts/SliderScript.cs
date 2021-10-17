using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener((v) => {
            Debug.Log("speed: " + v);
            _sliderText.text = v.ToString("0");
	   PlayerPrefs.SetInt(Constants.SPEED, (int) v);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toogleInput(bool value) {
        Debug.Log("toggle: " + value);
        PlayerPrefs.SetInt(Constants.PARTICLES, value ? 1 : 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.PickerWheelUI;
using UnityEngine.UI;
using TMPro;

public class SpinReward : MonoBehaviour
{
    [SerializeField] private Button spinButton;
    [SerializeField] private TextMeshProUGUI SpinButtonText;

    [SerializeField] private PickerWheel pickerWheel;

    // Start is called before the first frame update
    void Start()
    {
        if (spinButton)
        {
            spinButton.onClick.AddListener(() =>
            {
                SpinButtonText.text = "Spinning";
                pickerWheel.Spin();
            });

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

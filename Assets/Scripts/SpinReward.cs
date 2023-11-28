using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.PickerWheelUI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SpinReward : MonoBehaviour
{
    [SerializeField] private Button spinButton;
    [SerializeField] private TextMeshProUGUI SpinButtonText;

    [SerializeField] private PickerWheel pickerWheel;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {   

        if (spinButton)
        {
            spinButton.interactable = true;
            SpinButtonText.text = "Spin";

            spinButton.onClick.AddListener(() =>
            {
                spinButton.interactable = false;
                SpinButtonText.text = "Spinning";

                pickerWheel.OnSpinStart(() =>
                {
                    Debug.Log("Spin Started..");
                });

                pickerWheel.OnSpinEnd(WheelPiece =>
                {
                    if (WheelPiece.Label == "Coins")
                    {
                        GameManager.instance.totalGameCoins += WheelPiece.Amount;
                        uiManager.GameQuit();
                    }
                    else if (WheelPiece.Label == "Extra Life")
                    {
                        playerController.RestartFromDeadPosition();

                    }
                    
                });


                pickerWheel.Spin();
            });

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

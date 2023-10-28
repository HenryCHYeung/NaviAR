using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Android;
using Gyroscope= UnityEngine.InputSystem.Gyroscope;
using UnityEngine.InputSystem;

public class AndriodSensor : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI gyroText;
    public TextMeshProUGUI gyroSuppText;
    private void Start()
    { 
        InputSystem.EnableDevice(PressureSensor.current);
        if(SystemInfo.supportsGyroscope){
             if (!Permission.HasUserAuthorizedPermission("android.permission.SENSORS.GPS"))
            {
                Permission.RequestUserPermission("android.permission.SENSORS.GPS");
                InputSystem.EnableDevice(Gyroscope.current);
                gyroSuppText.text="Gyroscope is running";
            }
        }else{
            // Handle the case where the gyroscope is not supported.
            gyroSuppText.text = "Gyroscope not supported on this device.";
        }
    }

    // Update is called once per frame
    private void Update()
    {
        gyroText.text="Gyro rotation rate " + Input.gyro.rotationRate +"\nGyro attitude" + Input.gyro.attitude +"\n Gyroscope Enabled:" + Input.gyro.enabled;
        transform.rotation=Input.gyro.attitude; 
    }

}


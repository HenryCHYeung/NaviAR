using UnityEngine;
using System.Runtime.InteropServices;

public class NativeAPI : MonoBehaviour
{
#if UNITY_IOS && !UNITY_EDITOR
            [DllImport("__Internal")]
            public static extern void sendMessageToMobileApp(string message);
#endif
}


public class messageSent : MonoBehaviour
{

    public static messageSent instance;

    public void ButtonPressed()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.azesmwayreactnativeunity.ReactNativeUnityViewManager"))
            {
                jc.CallStatic("sendMessageToMobileApp" ,  "Here is the Message");
            }
    }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
        #if UNITY_IOS && !UNITY_EDITOR
                        NativeAPI.sendMessageToMobileApp("Here is the Message");
        #endif
        }
    }
}
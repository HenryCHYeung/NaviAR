using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timeline : MonoBehaviour
{
    private Slider timelapse;
    public float maxTime;
    public float minTime;
    public GameObject asset;
    Animator anima;
    // Start is called before the first frame update
    void Start()
    {
        timelapse= GameObject.Find("Slider").GetComponent<Slider>();
        timelapse.minValue=minTime;
        timelapse.maxValue=maxTime;
        anima= asset.GetComponent<Animator>();
    }

    // Update is called once per frame
    void animationSUpdate()
    {
        anima.SetFloat("ExplodeSkullValue",timelapse.value);
    }
}

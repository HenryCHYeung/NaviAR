using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageScript : MonoBehaviour
{
    private ARTrackedImageManager imageManager;//find image tacker managerw
    public GameObject[] ArPrefabs;//imports image library
    private readonly Dictionary<string,GameObject> instantizedPrefabs =new Dictionary<string, GameObject>();//keep dictionary array of created prefabs

    void Awake()
    {
        imageManager= GetComponent<ARTrackedImageManager>(); //find image tracker manager in game
    }
    void OnEnable(){
        imageManager.trackedImagesChanged+= OnTrackedImagesChanged;//Attach event handler when tracked images change
    }
    void OnDisable(){
        imageManager.trackedImagesChanged-= OnTrackedImagesChanged; //Remove event handler
    }
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs){//Event handler when object is tracked, recallibrated, or untracted
        
        foreach(var trackedImage in eventArgs.added){//loop through all new tracked images that have been detected
            var imageName=trackedImage.referenceImage.name;// gets name of the image refrence
            foreach(var curPrefab in ArPrefabs){// loop over the array of prefabs
                if((string.Compare(curPrefab.name,imageName,StringComparison.OrdinalIgnoreCase)==0)&& !instantizedPrefabs.ContainsKey(imageName)){//check if prefab matches the tracked image name, and that the prefab hasn't been created
                    var newPrefab=Instantiate(curPrefab,trackedImage.transform);//Instantiate the prefab, parenting it tothe ARTracked image
                    instantizedPrefabs[imageName]=newPrefab;//add the created prefab to the array
                }
            }

        }

        foreach(var trackedImage in eventArgs.updated){// for all prefabs created so far, set them active or not depending if the current image is being tracked
            instantizedPrefabs[trackedImage.referenceImage.name].SetActive(trackedImage.trackingState==TrackingState.Tracking);
        }

        foreach(var trackedImage in eventArgs.removed){//if the AR subsystem gave up on looking for tracked image
            Destroy(instantizedPrefabs[trackedImage.referenceImage.name]);// destroy its prefab
            instantizedPrefabs.Remove(trackedImage.referenceImage.name);//remove the instance of the array
            //or set prefab instance to inactive
            //instantizedPrefabs[trackedImage.referenceImage.name].SetActive(false);
        }
    }   
}

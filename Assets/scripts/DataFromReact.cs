using UnityEngine;

public class DataFromReact : MonoBehaviour
{
    public string names;
    public string age;
    DataFromReact instance;

private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

public  class JsonObject
    {
        public string name;
        public string age;
    }
// As you can see here is the name of the function that we get the data.
// it should have the same name in RN function postMessage.
    public void GetDatas(string json)
    {
        JsonObject obj = JsonUtility.FromJson<JsonObject>(json);
        name = obj.name;
        age = obj.age;
  }
}
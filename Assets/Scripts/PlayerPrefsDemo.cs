using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefKeys
{
    public const string DemoInteger         = "Demo.Integer";
    public const string DemoFloat           = "Demo.Float";
    public const string DemoString          = "Demo.String";
    public const string DemoBool            = "Demo.Bool";

    public const string DemoList_Size       = "Demo.List.Size";
    public const string DemoList_Element    = "Demo.List.";

    public const string DemoLocation        = "Demo.Location";
    // public const string DemoLocation_X      = "Demo.Location.X";
    // public const string DemoLocation_Y      = "Demo.Location.Y";
    // public const string DemoLocation_Z      = "Demo.Location.Z";

    public const string DemoRotation_W      = "Demo.Rotation.W";
    public const string DemoRotation_X      = "Demo.Rotation.X";
    public const string DemoRotation_Y      = "Demo.Rotation.Y";
    public const string DemoRotation_Z      = "Demo.Rotation.Z";
}

public class PlayerPrefsDemo : MonoBehaviour
{
    [SerializeField] int DemoInteger;
    [SerializeField] float DemoFloat;
    [SerializeField] string DemoString;
    [SerializeField] bool DemoBool;
    [SerializeField] List<int> DemoList;
    [SerializeField] Vector3 DemoLocation;
    [SerializeField] Quaternion DemoRotation;

    // Start is called before the first frame update
    void Start()
    {
        // allocate and fill the list
        int numValues = Random.Range(4, 8);
        DemoList = new List<int>(numValues);
        while (DemoList.Count < numValues)
            DemoList.Add(Random.Range(0, 101));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SaveData();
        if (Input.GetKeyDown(KeyCode.L))
            LoadData();
        if (Input.GetKeyDown(KeyCode.Delete))
            PlayerPrefs.DeleteAll();
    }

    void LoadData()
    {
        DemoInteger = PlayerPrefs.GetInt(PlayerPrefKeys.DemoInteger, 0);
        DemoFloat = PlayerPrefs.GetFloat(PlayerPrefKeys.DemoFloat, 0);
        DemoString = PlayerPrefs.GetString(PlayerPrefKeys.DemoString, string.Empty);

        DemoBool = PlayerPrefs.GetInt(PlayerPrefKeys.DemoBool, 0) == 1;

        // read in the size of the list and allocate it
        int numElements = PlayerPrefs.GetInt(PlayerPrefKeys.DemoList_Size, 0);
        DemoList = new List<int>(numElements);

        // read in every element
        for (int index = 0; index < numElements; ++index)
        {
            var elementKey = PlayerPrefKeys.DemoList_Element + index.ToString();
            DemoList.Add(PlayerPrefs.GetInt(elementKey, 0));
        }

        // load the location        
        DemoLocation = PlayerPrefsExtensions.GetVector3(PlayerPrefKeys.DemoLocation, Vector3.zero);

        // load the rotation
        DemoRotation.w = PlayerPrefs.GetFloat(PlayerPrefKeys.DemoRotation_W, 0f);
        DemoRotation.x = PlayerPrefs.GetFloat(PlayerPrefKeys.DemoRotation_X, 0f);
        DemoRotation.y = PlayerPrefs.GetFloat(PlayerPrefKeys.DemoRotation_Y, 0f);
        DemoRotation.z = PlayerPrefs.GetFloat(PlayerPrefKeys.DemoRotation_Z, 0f);
    }

    void SaveData()
    {
        // store the settings
        PlayerPrefs.SetInt(PlayerPrefKeys.DemoInteger, DemoInteger);
        PlayerPrefs.SetFloat(PlayerPrefKeys.DemoFloat, DemoFloat);
        PlayerPrefs.SetString(PlayerPrefKeys.DemoString, DemoString);

        PlayerPrefs.SetInt(PlayerPrefKeys.DemoBool, DemoBool ? 1 : 0);

        // write out the list
        PlayerPrefs.SetInt(PlayerPrefKeys.DemoList_Size, DemoList.Count);
        for(int index = 0; index < DemoList.Count; ++index)
        {
            var elementKey = PlayerPrefKeys.DemoList_Element + index.ToString();
            PlayerPrefs.SetInt(elementKey, DemoList[index]);
        }

        // save the location
        PlayerPrefsExtensions.SetVector3(PlayerPrefKeys.DemoLocation, DemoLocation);

        // save the rotation
        PlayerPrefs.SetFloat(PlayerPrefKeys.DemoRotation_W, DemoRotation.w);
        PlayerPrefs.SetFloat(PlayerPrefKeys.DemoRotation_X, DemoRotation.x);
        PlayerPrefs.SetFloat(PlayerPrefKeys.DemoRotation_Y, DemoRotation.y);
        PlayerPrefs.SetFloat(PlayerPrefKeys.DemoRotation_Z, DemoRotation.z);

        PlayerPrefs.Save();
    }
}

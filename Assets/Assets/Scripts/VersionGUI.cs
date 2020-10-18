using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class VersionGUI : MonoBehaviour
{
    void OnValidate()
    {
        GetComponent<TextMeshProUGUI>().text = PlayerSettings.bundleVersion;
    }
}

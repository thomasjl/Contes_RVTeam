using System;
using UnityEditor;
using UnityEngine;

public class AutoMaterial_Suffixes : ScriptableObject {
    /// <summary>
    /// Where the suffixes file is by default.
    /// </summary>
    public static string defaultFolderPath = "Assets/Editor/AutoMaterial";

    /// <summary>
    /// Texture suffixes and their corresponding material field.
    /// </summary>
    public AutoMSuffix[] suffixes = new AutoMSuffix[]
    {
        new AutoMSuffix("_MainTex", "_AlbedoTransparency"),
        new AutoMSuffix("_MetallicGlossMap", "_MetallicRoughness"),
        new AutoMSuffix("_OcclusionMap", "_AO"),
        new AutoMSuffix("_DetailNormalMap", "_Normal"),
        new AutoMSuffix("_EmissionMap", "_Emission"),
    };

    /// <summary>
    /// Create a new suffixes file with default suffixes.
    /// </summary>
    /// <returns>The new created suffixes file</returns>
    public static AutoMaterial_Suffixes Create()
    {
        AutoMaterial_Suffixes newSuffixes = (AutoMaterial_Suffixes)ScriptableObject.CreateInstance(typeof(AutoMaterial_Suffixes).Name);
        AssetDatabase.CreateAsset(newSuffixes, AssetDatabase.GenerateUniqueAssetPath(defaultFolderPath + "/Texture suffixes.asset"));
        return newSuffixes;
    }
}

[Serializable]
public class AutoMSuffix {
    public string materialKeyword = "";
    public string[] textureSuffixes;
    public AutoMSuffix(string matKey, string textSufx)
    {
        materialKeyword = matKey;
        textureSuffixes = new string[] { textSufx };
    }
}

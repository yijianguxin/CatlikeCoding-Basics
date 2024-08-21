using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using TMPro;

public class EditorTool
{
    [MenuItem("学习工具/切换渲染管线")]
    public static void SwitchRenderPipeline()
    {
        var defaultPipeline = GraphicsSettings.defaultRenderPipeline;
        if (defaultPipeline == null)
        {
            var pipeline = AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>("Assets/URP/URP.asset");
            if (pipeline == null)
            {
                EditorUtility.DisplayDialog("提示", "找不到自定义渲染管线", "确定");
                return;
            }
            GraphicsSettings.defaultRenderPipeline = pipeline;

            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Point/Point.prefab").GetComponent<MeshRenderer>().material
                = AssetDatabase.LoadAssetAtPath<Material>("Assets/Point/Point URP.mat");
            
            GameObject.Find("Canvas/Text (TMP)").GetComponent<TextMeshProUGUI>().text = "URP";

            GameObject.Find("GPUGraph").GetComponent<GPUGraph>().material = 
                AssetDatabase.LoadAssetAtPath<Material>("Assets/Point/Point URP GPU.mat");

            EditorUtility.DisplayDialog("提示", "切换到自定义渲染管线", "确定");
        }
        else
        {
            GraphicsSettings.defaultRenderPipeline = null;

            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Point/Point.prefab").GetComponent<MeshRenderer>().material
                = AssetDatabase.LoadAssetAtPath<Material>("Assets/Point/Point Surface.mat");

            GameObject.Find("Canvas/Text (TMP)").GetComponent<TextMeshProUGUI>().text = "built-in RP";

            GameObject.Find("GPUGraph").GetComponent<GPUGraph>().material = 
                AssetDatabase.LoadAssetAtPath<Material>("Assets/Point/Point Surface GPU.mat");

            EditorUtility.DisplayDialog("提示", "切换到默认渲染管线", "确定");
        }
    }
}

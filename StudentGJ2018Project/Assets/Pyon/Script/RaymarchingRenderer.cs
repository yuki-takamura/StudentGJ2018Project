using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class RaymarchingRenderer : MonoBehaviour
{
    //全てのカメラに対してCommandBufferを適用するために辞書型とする
    Dictionary<Camera, CommandBuffer> cameras = new Dictionary<Camera, CommandBuffer>();
    Mesh quad;

    [SerializeField]
    Material material = null;

    [SerializeField]
    CameraEvent pass = CameraEvent.BeforeGBuffer;

    Mesh GenerateQuad()
    {
        var mesh = new Mesh();
        mesh.vertices = new Vector3[4]
        {
            new Vector3(1.0f, 1.0f, 0.0f),
            new Vector3(-1.0f, 1.0f, 0.0f),
            new Vector3(-1.0f, -1.0f, 0.0f),
            new Vector3(1.0f, -1.0f, 0.0f)
        };
        mesh.triangles = new int[6] { 0, 1, 2, 2, 3, 0 };
        return mesh;
    }

    //すべてのCommandBufferをクリアする
    void CleanUp()
    {
        foreach (var pair in cameras)
        {
            var cam = pair.Key;
            var buffer = pair.Value;
            if (cam)
            {
                cam.RemoveCommandBuffer(pass, buffer);
            }
        }

        cameras.Clear();
    }

    private void OnEnable()
    {
        CleanUp();
    }

    private void OnDisable()
    {
        CleanUp();
    }

    //カメラごとに1度ずつ呼ばれる
    private void OnWillRenderObject()
    {
        UpdateCommandBuffer();
    }

    void UpdateCommandBuffer()
    {
        var act = gameObject.activeInHierarchy && enabled;
        if(!act)
        {
            OnDisable();
            return;
        }

        var cam = Camera.current;
        if (!cam)
            return;

        //既にCommandBufferを適用済みなら何もしない
        if (cameras.ContainsKey(cam))
            return;

        if (!quad)
            quad = GenerateQuad();

        //CommandBufferの生成と登録
        var buffer = new CommandBuffer();
        buffer.name = "Raymarching";
        buffer.DrawMesh(quad, Matrix4x4.identity, material, 0, 0);
        cam.AddCommandBuffer(pass, buffer);
        cameras.Add(cam, buffer);
    }
}
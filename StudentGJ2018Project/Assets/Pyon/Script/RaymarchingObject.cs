using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RaymarchingObject : MonoBehaviour
{

    [SerializeField]
    string shaderName = "Raymarching/Object";

    private Material material;
    private int scaleId;

    private void Awake()
    {
        material = new Material(Shader.Find(shaderName));
        GetComponent<Renderer>().material = material;
        scaleId = Shader.PropertyToID("_Scale");
    }

    void Update ()
    {
        material.SetVector(scaleId, transform.localScale);
	}
}

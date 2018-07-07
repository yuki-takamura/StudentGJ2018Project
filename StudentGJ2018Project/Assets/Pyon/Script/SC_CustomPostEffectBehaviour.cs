using UnityEngine;

[ExecuteInEditMode]
public class SC_CustomPostEffectBehaviour : MonoBehaviour
{
    [SerializeField]
    public Material postEffectMaterial = null;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, postEffectMaterial);
    }
}

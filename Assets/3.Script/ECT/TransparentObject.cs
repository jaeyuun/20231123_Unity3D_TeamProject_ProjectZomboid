using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float fadeAmount;
    MeshRenderer[] renderers;

    private float originalOpacity = 1f;
    public bool isDoFade = false;

    

    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        /*if (material != null)
        {
            Color matecolor = material.color;
            matecolor.a = newAlpha;
            material.color = matecolor;
        }
        else
        {
            Debug.LogError("타겟 머터리얼이 할당되지 않았습니다.");
        }*/
    }

    private void Update()
    {
        if (isDoFade) SetObject();

        else SetFade();
    }

    private void SetObject()
    {
        for(int i=0; i< renderers.Length; i++)
        {
            foreach (Material material in renderers[i].materials)
            {
                FadeNow(renderers[i]);
            }

            ResetFade(renderers[i]);
        }
    }

    private void SetFade()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            ResetFade(renderers[i]);
        }
    }

    private void FadeNow(MeshRenderer renderer)
    {
        for (int i = 0; i < renderer.materials.Length; i++)
        {
            Color currentColor = renderer.material.color;
            Color SmoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
                Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));
            renderer.material.color = SmoothColor;
        }
    }
    private void ResetFade(MeshRenderer renderer)
    {
        for (int i = 0; i < renderer.materials.Length; i++)
        {
            Color currentColor = renderer.material.color;
            Color SmoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
                Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed * Time.deltaTime));
            renderer.material.color = SmoothColor;
        }
    }

}

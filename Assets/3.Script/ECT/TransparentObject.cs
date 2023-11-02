using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    public bool isTransparent { get; private set; } = false;

    private MeshRenderer[] renderers;
    private WaitForSeconds delay = new WaitForSeconds(0.001f);
    private WaitForSeconds resetDelay = new WaitForSeconds(0.005f);
    private const float Threshold_Alpha = 0.25f;
    private const float Threshold_Max_Timer = 0.5f;

    private bool isReseting = false;
    private float timer = 0f;

    private Coroutine timeCheckCorutine;
    private Coroutine resetCorutine;
    private Coroutine becomeTransParentCorutine;


    private void Awake()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void becomeTransParent()
    {
        if (isTransparent)
        {
            timer = 0f;
            return;
        }
        if (resetCorutine != null && isReseting)
        {
            isReseting = false;
            isTransparent = false;
            StopCoroutine(resetCorutine);
        }

        isTransparent = true;
        becomeTransParentCorutine = StartCoroutine(BecomeTransparentCorutine());
    }

    private void SetMaterialRenderingMode(Material material, float mode, int renderQueue)
    {
        material.SetFloat("Mode", mode);
        material.SetInt("_SrcBland", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBland", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("ZWrite", 0);
        material.DisableKeyword("_Alphaest_On");
        material.EnableKeyword("_AlphaBland_On");
        material.DisableKeyword("_AlphaPremultiply_On");
        material.renderQueue = renderQueue;
    }

    private void SetMaterialTransParent()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            foreach (Material material in renderers[i].materials)
            {
                SetMaterialRenderingMode(material, 3f, 3000);
            }

        }

    }

    private void SetMaterialOpaque()     //붙투명해야하는 오브젝트
    {
        for (int i=0; i < renderers.Length; i++)
        {
            foreach (Material material in renderers[i].materials)
            {
                SetMaterialRenderingMode(material, 0f, -1);
            }
        }
    }

    public void ResetOriginalTransparent()
    {
        SetMaterialOpaque();
        resetCorutine = StartCoroutine(ResetOriginalTransparentCorutine());
    }

    private IEnumerator BecomeTransparentCorutine()
    {
        while(true)
        {
            bool isComplete = true;

            for(int i=0; i< renderers.Length; i++)
            {
                if(renderers[i].material.color.a > Threshold_Alpha) isComplete = false;

                Color color = renderers[i].material.color;
            }
            if(isComplete)
            {
                CheckTimer();
                break;
            }
            yield return delay;
        }
    }

    private IEnumerator ResetOriginalTransparentCorutine()
    {
        bool isComplete = false;

        while(true)
        {
            for(int i = 0; i<renderers.Length; i++)
            {
                if (renderers[i].material.color.a < 1f) isComplete = false;

            }
            if(isComplete)
            {
                isReseting = false;
                break;
            }
        }

        yield return resetDelay;
    }

    public void CheckTimer()
    {
        if (timeCheckCorutine != null) StopCoroutine(timeCheckCorutine);
        timeCheckCorutine = StartCoroutine(CheckTimerCorutine());

    }

    private IEnumerator CheckTimerCorutine()
    {
        timer = 0f;
        while(true)
        {
            timer += Time.deltaTime;

            if(timer > Threshold_Max_Timer)
            {
                isReseting = true;
                ResetOriginalTransparent();
                break;
            }
            yield return null;
        }

    }

        

    



}

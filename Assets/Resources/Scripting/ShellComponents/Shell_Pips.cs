using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Shell_Pips : MonoBehaviour
{
    public PipArrangementType arrangementType = PipArrangementType.Single;
    public Vector3 offset = Vector3.zero;
    [ConditionalField("arrangementType", true, PipArrangementType.Single)] [MinValue(1)] public int pipCount = 1;
    [ConditionalField("arrangementType", false, PipArrangementType.Ring)] public float ringRadius = 1f;
    [ConditionalField("arrangementType", false, PipArrangementType.Line)] public float lineSpacing = 0.2f;
    [ConditionalField("arrangementType", false, PipArrangementType.Cross)] public float crossSize = 1f;
    private void OnDrawGizmos()
    {
       
        Vector3 corePosition = transform.position + transform.rotation * offset;

        Gizmos.color = new Color(1f, 0f, 1f, 1f);

        Gizmos.DrawLine(transform.position, corePosition);
        Gizmos.DrawLine(corePosition - transform.rotation * Vector3.forward * 0.2f, corePosition + transform.rotation * Vector3.forward * 0.2f);
        Gizmos.DrawLine(corePosition - transform.rotation * Vector3.right * 0.2f, corePosition + transform.rotation * Vector3.right * 0.2f);

        Gizmos.color = new Color(1f, 0f, 1f, 0.6f);

        switch (arrangementType)
        {
            case PipArrangementType.Single:
                DrawPip(corePosition);
                break;

            case PipArrangementType.Ring:
                for(int i = 0; i < pipCount; i++)
                {
                    float t = (i*1f / pipCount) * Mathf.PI * 2f;
                    DrawPip(corePosition + transform.rotation * new Vector3(Mathf.Cos(t) * ringRadius, 0f, Mathf.Sin(t) * ringRadius));
                }
                break;

            case PipArrangementType.Line:
                for (int i = 0; i < pipCount; i++)
                {
                    DrawPip(corePosition + transform.rotation * Vector3.forward * lineSpacing * i);
                }
                break;

            case PipArrangementType.Cross:
                int count = pipCount * 2;

                for (int i = 0; i <= count; i++)
                {
                    float f = ((i * 1f / count) - 0.5f) * 2f;
                    DrawPip(corePosition + transform.rotation * (Vector3.forward * crossSize * f));
                }

                for (int i = 0; i <= count; i++)
                {
                    float f = ((i * 1f / count) - 0.5f) * 2f;
                    DrawPip(corePosition + transform.rotation * (Vector3.right * crossSize * f));
                }
                break;

        }
    }
    void DrawPip(Vector3 position)
    {
        Gizmos.DrawSphere(position,0.2f);
    }
}

public enum PipArrangementType
{ 
    Single = 0,
    Ring = 1,
    Line = 2,
    Cross = 3,
}

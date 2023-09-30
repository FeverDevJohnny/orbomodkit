using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JTools.Audio
{
    public class BGMVolume : MonoBehaviour
    {
        public bool isGlobal = false;
        public int priority = 0;
        public AudioClip targetTrack;
        BoxCollider m_box;
        SphereCollider m_sphere;
        MeshCollider m_mesh;
        private void OnDrawGizmosSelected()
        {
            if (!isGlobal)
            {
                if (m_box == null)
                    m_box = GetComponent<BoxCollider>();
                if (m_sphere == null)
                    m_sphere = GetComponent<SphereCollider>();
                if (m_mesh == null)
                    m_mesh = GetComponent<MeshCollider>();


                Gizmos.color = new Color(0f, 1f, 1f, 0.2f);
                Matrix4x4 m_stored = Gizmos.matrix;
                Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);

                if (m_box != null)
                    Gizmos.DrawCube(m_box.center, m_box.size);

                if (m_sphere != null)
                    Gizmos.DrawSphere(m_sphere.center, m_sphere.radius);

                if (m_mesh != null)
                    Gizmos.DrawMesh(m_mesh.sharedMesh);

                Gizmos.matrix = m_stored;
            }
        }
    }
}
using UnityEngine;
using System.Collections;

public class Vec1 : MonoBehaviour
{
    int work = 16;
    float particleSize = 3f;
    float lineWidth = 0.5f;

    LineRenderer line;
    ParticleSystem pe;
    ParticleSystem.Particle[] point;

    void Start()
    {
        pe = gameObject.AddComponent<ParticleSystem>();
        pe.startSpeed = 0;
        pe.startLifetime = float.MaxValue;

        line = gameObject.AddComponent<LineRenderer>();
        line.SetWidth(lineWidth, lineWidth);
        line.SetVertexCount(work+1);

        CreateLineAndPoint();
    }

    void CreateLineAndPoint()
    {
        pe.Emit(work);
        point = new ParticleSystem.Particle[work+1];
        pe.GetParticles(point);

        for (int n =0; n <= work; n++)
        {
            Vector3 pos = new Vector3(Xn(n), Yn(n), 0);
            point[n].position = pos;
            point[n].color = Color.white;
            point[n].size = particleSize;
            line.SetPosition(n, pos);
        }
        pe.SetParticles(point, work);
    }

    float Xn(int n)
    {
        return 6 - (3 * n);
    }

    float Yn(int n)
    {
        return (7 * n) - 12;
    }

}

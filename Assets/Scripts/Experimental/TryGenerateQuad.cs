using System.Collections.Generic;
using UnityEngine;

public class TryGenerateQuad : MonoBehaviour
{
    [SerializeField]
    private MeshFilter LeftScreenMeshFilter;
    [SerializeField]
    private MeshFilter RightScreenMeshFilter;
    [SerializeField]
    private MeshFilter TestScreenMeshFilter;
    //[SerializeField]
    private Mesh LeftScreenMesh;
    private Mesh RightScreenMesh;
    private Mesh TestScreenMesh;

    private List<Vector3> vertexList = new List<Vector3>();
    private List<Vector2> uvList = new List<Vector2>();
    private List<int> indexList = new List<int>();
    private Dictionary<float, List<float>> vertexPairs = new Dictionary<float, List<float>>(); //yz‚ÌƒyƒA‚ðŠi”[‚·‚ékey‚ªy, value‚ªz

    //private int 

    private void Start()
    {   
        LeftScreenMesh = LeftScreenMeshFilter.mesh;
        RightScreenMesh = RightScreenMeshFilter.mesh;
        TestScreenMesh = TestScreenMeshFilter.mesh;

        //DeformMesh(LeftScreenMesh);
        //DeformMesh(RightScreenMesh);
        //DeformUV(LeftScreenMesh);
        //DeformUV(RightScreenMesh);
        //TestDeformMesh(TestScreenMesh);
    }

    private float QuadraticFunction(float param, float x)
    {
        return param * x * x; 
    }

    private void DeformLeftUV(Mesh ScreenMesh)
    {
        ScreenMesh.GetUVs(0, uvList);
        Debug.Log(uvList.Count);

        int ct = 0;

        for (int i = 0; i < Mathf.Sqrt(uvList.Count); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(uvList.Count); j++)
            {
                Debug.Log(uvList[ct]);
                var uv = uvList[ct];
                //uv.y = 0;
                if (uv.y < 0.5)
                {
                    uv.y = 0;
                }
                uvList[ct] = uv;
                ct++;
            }
        }

        ScreenMesh.SetUVs(0, uvList);
        uvList.Clear();
    }
    /*
    private void DeformRightUV(Mesh ScreenMesh)
    {
        ScreenMesh.GetUVs(0, uvList);
        Debug.Log(uvList.Count);

        int ct = 0;

        for (int i = 0; i < Mathf.Sqrt(uvList.Count); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(uvList.Count); j++)
            {
                Debug.Log(uvList[ct]);
                var uv = uvList[ct];
                float param = Mathf.Cos(Mathf.PI * (i / 10f));
                if (param != 0)
                {
                    uv.y = QuadraticFunction(0.5f + 0.25f * Mathf.Cos(Mathf.PI * (i / 10f)), uv.x);
                }
                Debug.Log("UV_X:" + uv.x + ", UV_Y:" + uv.y);
                //uv.y = 0;
                uvList[ct] = uv;
                ct++;
            }
        }

        ScreenMesh.SetUVs(0, uvList);
        uvList.Clear();
    }
    */
    private void DeformUV(Mesh ScreenMesh)
    {
        ScreenMesh.GetUVs(0, uvList);
        Debug.Log(uvList.Count);

        int ct = 0;

        for (int i = 0; i < Mathf.Sqrt(uvList.Count); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(uvList.Count); j++)
            {
                Debug.Log(uvList[ct]);
                var uv = uvList[ct];
                float param = Mathf.Cos(Mathf.PI * (i / 10f));
                if (param != 0 && uv.y < 0.2)
                {
                    if (param > 0)
                    {
                        uv.y = QuadraticFunction(0.5f * param, uv.x);
                    }
                    else
                    {
                        uv.y = QuadraticFunction(Mathf.Abs(0.1f * param), uv.x);
                    }
                }
                Debug.Log("UV_X:" + uv.x + ", UV_Y:" + uv.y);
                //uv.y = 0;
                uvList[ct] = uv;
                ct++;
            }
        }

        ScreenMesh.SetUVs(0, uvList);
        uvList.Clear();
    }

    private void DeformMesh(Mesh ScreenMesh)
    {
        for (int i = 0; i < ScreenMesh.vertices.Length; i++)
        {
            vertexList.Add(ScreenMesh.vertices[i]);
            Debug.Log(ScreenMesh.vertices[i]);
        }

        int rowCnt = 0;
        int columnCnt = 0;

        for (int i = 0; i < Mathf.Sqrt(vertexList.Count); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(vertexList.Count); j++)
            {
                var v = vertexList[rowCnt];
                v.x = 5.0f * Mathf.Cos(Mathf.PI * (j / 10f));
                v.y = -3.0f * Mathf.Sin(Mathf.PI * (j / 10f));
                //v.z = 5.0f * Mathf.Cos(Mathf.PI * (j / 10f));
                vertexList[rowCnt] = v;
                rowCnt++;
            }
            /*
            for (var j = 0; j < 11; j++) //Mathf.Sqrt(vertexList.Count)
            {
                var v = vertexList[columnCnt];
                //v.z = 5.0f * Mathf.Sin(Mathf.PI * (j / 10f));
                //v.z = j;
                vertexList[columnCnt] = v;
                columnCnt+=11;
            }

            columnCnt = i+1;
            */
        }

        ScreenMesh.SetVertices(vertexList);

        vertexList.Clear();
    }

    private void CountMeshVertices(Mesh ScreenMesh)
    {
        Debug.Log(ScreenMesh.vertices.Length);
    }

    private void TestDeformMesh(Mesh ScreenMesh)
    {
        CountMeshVertices(ScreenMesh);

        for (int i = 0; i < ScreenMesh.vertices.Length; i++)
        {
            vertexList.Add(ScreenMesh.vertices[i]);
            Debug.Log(ScreenMesh.vertices[i]);
        }

        int rowCnt = 0;
        int columnCnt = 0;

        for (int i = 0; i < Mathf.Sqrt(vertexList.Count); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(vertexList.Count); j++)
            {
                var v = vertexList[rowCnt];
                v.z = 5.0f * Mathf.Cos(Mathf.PI * (j / 10f));
                v.x = -3.0f * Mathf.Sin(Mathf.PI * (j / 10f));
                vertexList[rowCnt] = v;
                rowCnt++;
            }
        }
        /*
        //Dictionary‚Éy‚É‘Î‰ž‚·‚ézÀ•W‚Ì’l‚ðƒŠƒXƒg‚ÉŠi”[‚·‚é
        for (int i = 0; i < ScreenMesh.vertices.Length; i++)
        {
            vertexList.Add(ScreenMesh.vertices[i]);
            //Debug.Log(ScreenMesh.vertices[i]);
            var v = ScreenMesh.vertices[i];

            if (!(vertexPairs.ContainsKey(v.y)))
            {
                vertexPairs.Add(v.y, new List<float>());
            }

            vertexPairs[v.y].Add(v.z);
        }

        Debug.Log(vertexList.Count + ", " + Mathf.Sqrt(vertexList.Count));

        int cnt = 0;
        for (int i = 0; i < 53; i++)
        {
            for (int j = 0; j < 53; j++)
            {
                var v = vertexList[cnt];
                //v.y = 1.0f * Mathf.Cos(Mathf.PI * (j / 53f));
                v.z = -1.0f * Mathf.Sin(Mathf.PI * (j / 53f));
                vertexList[cnt] = v;
                cnt++;
            }
        }
        */
        /*
        //Ši”[‚µ‚½yÀ•W‚É‘Î‰ž‚·‚ézÀ•W‚ð¸‡‚Éƒ\[ƒg‚·‚é
        foreach (float yValue in vertexPairs.Keys) 
        {
            vertexPairs[yValue].Sort();
        }

        Debug.Log(vertexList.Count + ", " + Mathf.Sqrt(vertexList.Count));

        int rowCnt = 0;
        int columnCnt = 0;
        int cnt = 0;

        foreach (float yValue in vertexPairs.Keys)
        {
            for (int i = 0; i < vertexPairs[yValue].Count; i++)
            {
                var v = new Vector3();
                float zValue = vertexPairs[yValue][i];
                
                v.x = 0;
                //v.y = 1.0f * Mathf.Cos(Mathf.PI * (yValue / 53f));
                v.y = yValue;
                v.z = zValue;
                //v.z = -1.0f * Mathf.Sin(Mathf.PI * (zValue / 53f));
                vertexList[cnt] = v;
                cnt++;
            }
        }
        */

        ScreenMesh.SetVertices(vertexList);

        vertexList.Clear();
    }
}

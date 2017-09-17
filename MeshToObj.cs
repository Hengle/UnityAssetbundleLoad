//*************************************************************************
//	创建日期:	2017/7/11 星期二 10:03:40
//	文件名称:	MeshToObj
//  创建作者:	PGW 	
//	版权所有:	
//	相关说明:	将Unity中的Mesh保存为本地OBJ
//*************************************************************************

//-------------------------------------------------------------------------
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//-------------------------------------------------------------------------



    //-------------------------------------------------------------------------
    public static class MeshToObj
    {
    #region Member variables 
    //-------------------------------------------------------------------------


    //-------------------------------------------------------------------------
    #endregion

    #region Public Method
    //-------------------------------------------------------------------------
    /// <summary>
    /// 公共函数，可被外界调用，使用大写驼峰命名法
    /// </summary>


    public static bool SaveToFile(this Mesh _Mesh,string FilePath)
    {
        if (File.Exists(FilePath))
        {
            Debug.LogWarning("MeshToObj.SaveToFile:File is exists.");
        }
        
        FileStream fs = new FileStream(FilePath,FileMode.OpenOrCreate);
        byte[] Vertices = _Mesh.vertices.VerticesToObjFormat();
        byte[] Normal = _Mesh.normals.NormalToObjFormat();
        byte[] Triangle = _Mesh.triangles.TrianglesToObjFormat();
        byte[] UV = _Mesh.uv.UVToObjFormat();
        fs.Write(Vertices,0,Vertices.Length);
        fs.Write(Normal, 0, Normal.Length);
        fs.Write(Triangle, 0, Triangle.Length);
        fs.Write(UV, 0, UV.Length);

        //判断是否有第二层UV
        if (_Mesh.uv2.Length > 0)
        {
            byte[] UV_2 = _Mesh.uv.UVToObjFormat();
            fs.Write(UV_2, 0, UV_2.Length);
        }

        fs.Dispose();
        fs.Close();
        return true;
    }

    /// <summary>
    /// 处理点数据
    /// </summary>
    /// <param name="Vertices"></param>
    /// <returns></returns>
    public static byte[] VerticesToObjFormat(this Vector3[] Vertices)
    {
        string strTemp = string.Empty;
        foreach (Vector3 Vertice in Vertices)
        {
            strTemp += string.Format("v {0} {1} {2} \r\n", Vertice.x, Vertice.y, Vertice.z);
        }
        byte[] data = Encoding.Default.GetBytes(strTemp);

        return data;
    }
    
    /// <summary>
    /// 处理法线
    /// </summary>
    /// <param name="Normal"></param>
    /// <returns></returns>
    public static byte[] NormalToObjFormat(this Vector3[] Normal)
    {
        string strTemp = string.Empty;
        foreach (Vector3 v3 in Normal)
        {
            strTemp += string.Format("vn {0} {1} {2} \r\n", v3.x, v3.y, v3.z);
        }
        byte[] data = Encoding.Default.GetBytes(strTemp);

        return data;
    }


    /// <summary>
    /// 处理UV
    /// </summary>
    /// <param name="uv"></param>
    /// <returns></returns>
    public static byte[] UVToObjFormat(this Vector2[] uv)
    {
        string strTemp = string.Empty;
        foreach (Vector2 v2 in uv)
        {
            strTemp += string.Format("vt {0} {1} \r\n", v2.x, v2.y);
        }
        byte[] data = Encoding.Default.GetBytes(strTemp);

        return data;
    }

    /// <summary>
    /// 处理三角面数据
    /// </summary>
    /// <param name="Triangles"></param>
    /// <returns></returns>
    public static byte[] TrianglesToObjFormat(this int[] Triangles)
    {
        string strTemp = string.Empty;
        for (int i = 0; i < Triangles.Length; i+=3)
        {
            strTemp += string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2} \r\n",Triangles[i]+1,Triangles[i+1]+1,Triangles[i+2]+1);
        }
        byte[] data = Encoding.Default.GetBytes(strTemp);

        return data;
    }

    #endregion

    #region private Method
    //-------------------------------------------------------------------------
    /// <summary>
    /// 私有函数，不可被外界调用，使用大写驼峰命名法，建议带 双下划线 - ‘__’
    /// </summary>

    //-------------------------------------------------------------------------
    #endregion
}

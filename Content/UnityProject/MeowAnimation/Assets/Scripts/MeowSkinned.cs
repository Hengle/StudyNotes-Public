﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public unsafe class MeowSkinned : MonoBehaviour
{
    /// <summary>
    /// Mesh网格信息
    /// </summary>
    private Mesh mesh;
    /// <summary>
    /// mesh转换骨骼空间坐标系 
    /// (mesh->bone)
    /// </summary>
    private Matrix4x4[] bindPoses;
    /// <summary>
    /// 骨骼信息
    /// bones.localToWorldMatrix (bone->world)
    /// </summary>
    private Transform[] bones;
    /// <summary>
    /// mesh->world
    /// </summary>
    private Matrix4x4[] meshToWorld;
    /// <summary>
    /// 每个顶点的骨骼权重信息
    /// </summary>
    private BoneWeight[] boneWeights;

    /// <summary>
    /// 解决改一个mat其它全改的问题
    /// </summary>
    private MaterialPropertyBlock rendererBlock;
    
    /// <summary>
    /// 临时借用的skinnedMeshRenderer数据
    /// </summary>
    private SkinnedMeshRenderer meshRenderer;

    //传入Shader的Buffer
    private ComputeBuffer bonePositionBuffer;
    private ComputeBuffer boneWeightBuffer;
    private ComputeBuffer vertexBuffer;
    private ComputeBuffer vertexReadBuffer;

    /// <summary>
    /// 最终要画上去的meshRenderer
    /// </summary>
    private MeshRenderer targetRenderer;
    private MeshFilter filter;

    private ComputeShader compute;

    private void Awake()
    {
        compute = Resources.Load<ComputeShader>("CShader");
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        rendererBlock = new MaterialPropertyBlock();
        mesh = meshRenderer.sharedMesh;
        bindPoses = mesh.bindposes;
        boneWeights = mesh.boneWeights;
        
        bones = new Transform[meshRenderer.bones.Length];
        for (uint i = 0; i < bones.Length; ++i)
        {
            bones[i] = meshRenderer.bones[i];
        }

        bonePositionBuffer = new ComputeBuffer(
            bones.Length,
            sizeof(Matrix4x4));
        boneWeightBuffer = new ComputeBuffer(
            boneWeights.Length,
            sizeof(BoneWeight));
        Vector3[] vertices = mesh.vertices;
        vertexBuffer = new ComputeBuffer(
            vertices.Length, sizeof(Vector3));
        vertexReadBuffer = new ComputeBuffer(
             vertices.Length, sizeof(Vector3));
        vertexReadBuffer.SetData(vertices);
        boneWeightBuffer.SetData(boneWeights);

        meshToWorld = new Matrix4x4[bones.Length];
        filter = gameObject.AddComponent<MeshFilter>();
        filter.sharedMesh = mesh;
        targetRenderer = gameObject.AddComponent<MeshRenderer>();
        targetRenderer.sharedMaterial = meshRenderer.sharedMaterial;
        Destroy(meshRenderer);
    }

    private void Update()
    {
        for (int i = 0; i < meshToWorld.Length; ++i)
        {
            // mesh坐标系转世界坐标系
            meshToWorld[i] = bones[i].localToWorldMatrix/* 骨骼坐标系转世界坐标系 */ * bindPoses[i]/* mesh坐标系转骨骼坐标系 */;
        }
        bonePositionBuffer.SetData(meshToWorld);
        // 向GPU传值
        rendererBlock.Clear();

        //shader->BindRootSignature
        compute.SetBuffer(0, "_BoneWeightsBuffer", boneWeightBuffer);
        compute.SetBuffer(0, "_BonePositionBuffer", bonePositionBuffer);
        compute.SetBuffer(0, "_VertexBuffer", vertexBuffer);
        compute.SetBuffer(0, "_VertexReadBuffer", vertexReadBuffer);
        int threadCount = boneWeightBuffer.count;
        compute.SetInt("_Count", threadCount);
        compute.Dispatch(0, (threadCount + 63) / 64, 1, 1);

        rendererBlock.SetBuffer("_BoneWeightsBuffer", boneWeightBuffer);
        rendererBlock.SetBuffer("_BonePositionBuffer", bonePositionBuffer);
        rendererBlock.SetBuffer("_VertexBuffer", vertexBuffer);
        targetRenderer.SetPropertyBlock(rendererBlock);
 
    }

    private void OnDestroy()
    {
        bonePositionBuffer.Dispose();
        boneWeightBuffer.Dispose();
    }
}

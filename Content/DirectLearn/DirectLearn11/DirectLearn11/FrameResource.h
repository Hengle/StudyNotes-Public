#pragma once
#include "../../Common/d3dx12.h"
#include "../../Common/d3dUtil.h"
#include "../../common//UploadBuffer.h"
using namespace DirectX;
using namespace DirectX::PackedVector;

using namespace Microsoft::WRL;

// ���嶥��ṹ��
struct Vertex
{
	XMFLOAT3 Pos;
	XMFLOAT4 Color;
};

// ������������峣�����ݣ�����ģ�
struct ObjectConstants
{
	DirectX::XMFLOAT4X4 world = MathHelper::Identity4x4();
};

// ��������Ĺ��̳������ݣ�ÿ֡�仯��
struct PassConstants
{
	XMFLOAT4X4 viewProj = MathHelper::Identity4x4();
};

struct FrameResource
{
	//ÿ֡��Դ����Ҫ���������������
	ComPtr<ID3D12CommandAllocator> cmdAllocator;
	//ÿ֡����Ҫ��������Դ���������˰�����Ϊ2��������������
	std::unique_ptr<UploadBuffer<ObjectConstants>> objCB = nullptr;
	std::unique_ptr<UploadBuffer<PassConstants>> passCB = nullptr;
	//CPU�˵�Χ��ֵ
	UINT64 fenceCPU = 0;

	FrameResource(const FrameResource& rhs) = delete;
	FrameResource& operator = (const FrameResource& rhs) = delete;
	FrameResource(ID3D12Device* device, UINT passCount, UINT objCount);
	~FrameResource();

};
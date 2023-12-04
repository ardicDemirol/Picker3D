using System;

[Serializable]
public struct PlayerData
{
    public PlayerMovementData MovementData;
    public PlayerMeshData MeshData;
    public PlayerForceData ForceData;
}

[Serializable] 
public struct PlayerMovementData
{
    public float ForwardSpeed;
    public float SideWaySpeed;
}


[Serializable]
public struct PlayerMeshData
{
    public float ScaleCounter;
}

[Serializable]
public struct PlayerForceData
{
    public Unity.Mathematics.float3 ForceParameters;
}


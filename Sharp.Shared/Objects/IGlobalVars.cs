// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global

using Sharp.Shared.CStrike;

namespace Sharp.Shared.Objects;

public interface IGlobalVars : INativeObject
{
    /// <summary>
    ///     真实时间
    /// </summary>
    float RealTime { get; }

    /// <summary>
    ///     帧计数
    /// </summary>
    int FrameCount { get; }

    /// <summary>
    ///     基于EngineTime的帧时间
    /// </summary>
    float AbsoluteFrameTime { get; }

    /// <summary>
    ///     基于EngineTime的帧生成时间
    /// </summary>
    float AbsoluteFrameStartTimeStdDev { get; }

    /// <summary>
    ///     最大允许的玩家数
    /// </summary>
    int MaxClients { get; }

    /// <summary>
    ///     帧时间
    /// </summary>
    float FrameTime { get; }

    /// <summary>
    ///     GameTime
    /// </summary>
    float CurTime { get; }

    /// <summary>
    ///     渲染时间
    /// </summary>
    float RenderTime { get; }

    /// <summary>
    ///     运行状态
    /// </summary>
    bool InSimulation { get; }

    /// <summary>
    ///     Tick计数
    /// </summary>
    int TickCount { get; }

    /// <summary>
    ///     SubTickFraction
    /// </summary>
    float SubTickFraction { get; }

    /// <summary>
    ///     地图名
    /// </summary>
    string MapName { get; }

    /// <summary>
    ///     最大实体数量 <br />
    ///     <remarks>这里指的是最大的Edict数量</remarks>
    /// </summary>
    int MaxEntities { get; }
}

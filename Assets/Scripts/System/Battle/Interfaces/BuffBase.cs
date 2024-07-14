using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffBase
{
    public BuffData Data;
    public bool IsActive { get; protected set; }
    public RoleBase source;

    public virtual void Apply(RoleBase target, RoleBase sourceRole)
    {
        IsActive = true;
        OnApply(target);
    }

    protected virtual void OnApply(RoleBase target)
    {
        // 添加Buff时的效果
    }

    public virtual void Remove(RoleBase target, RoleBase sourceRole)
    {
        IsActive = false;
        OnRemove(target);
    }

    protected virtual void OnRemove(RoleBase target)
    {
        // Buff被移除时的效果
    }

    /// <summary>
    /// 回合前触发效果
    /// </summary>
    /// <param name="target"></param>
    public virtual void BeforeAct(RoleBase target) { }

    /// <summary>
    /// 回合中的效果
    /// </summary>
    /// <param name="target"></param>
    public virtual void OnAct(RoleBase target) { }

    /// <summary>
    /// 回合后的效果
    /// </summary>
    /// <param name="target"></param>
    public virtual void AfterAct(RoleBase target) { }

    /// <summary>
    /// 使用技能时触发
    /// </summary>
    /// <param name="target"></param>
    public virtual void OnCast(RoleBase target) { }

    /// <summary>
    /// 消耗资源时触发
    /// </summary>
    /// <param name="target"></param>
    public virtual void OnCost(RoleBase target) { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基础技能的实现, 所有技能都要从这里继承
/// </summary>
public abstract class SkillBase
{
    public SkillData skillData { get; protected set; }

    public void Initialize(SkillData inputData)
    {
        skillData = inputData;
    }

    // 突然发现无参的情况在有参中定义了也可以,不引用就好了
    /*public abstract void Use();*/
    public abstract void Use(RoleBase caster, RoleBase target);
}

#region 主动技能接口

/// <summary>
/// 平A接口
/// </summary>
public interface IAttackSkill
{
    void Attack(RoleBase caster, RoleBase target);
}

/// <summary>
/// 单体攻击接口
/// </summary>
public interface ISingleAtkSkill
{
    void SingleAtk(RoleBase caster, RoleBase target);
}

/// <summary>
/// 群体攻击接口
/// </summary>
public interface IMultiAtkSkill
{
    void MultiAtk(RoleBase caster, RoleBase target);
}

/// <summary>
/// 回血接口
/// </summary>
public interface IHealSkill
{
    void Heal(RoleBase caster, RoleBase target);
}

/// <summary>
/// 必中添加Buff
/// </summary>
public interface IAddBuffSkill
{
    public void AddBuff();
}

/// <summary>
/// 必中移除Buff
/// </summary>
public interface IRemoveBuffSkill
{
    public void RemoveBuff();
}

/// <summary>
/// 基础概率添加Buff
/// </summary>
public interface IAddBuffExSkill
{
    public void AddBuffEx();
}

/// <summary>
/// 基础概率移除Buff
/// </summary>
public interface IRemoveBuffExSkill
{
    public void RemoveBuffEx();
}

#endregion

#region 被动技能接口

#endregion
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class RoleBase
{
    // 引用的RoleData
    public RoleData Data;

    #region 属性的封装
    // 最大生命值
    private float _maxhealth;
    public float MaxHealth
    {
        get => _maxhealth;
        set => _maxhealth = value;
    }
    private float _health;
    public float Health
    {
        get => _health;
        set => _health = Mathf.Clamp(value, 0, MaxHealth);
    }
    private float _attack;
    public float Attack
    {
        get => _attack;
        // 攻击力不能是负数
        set => _attack = (value >= 0f) ? value : 0f;
    }
    private float _defense;
    public float Defense
    {
        get => _defense;
        // 防御力也不能是负数
        set => _defense = (value >= 0f) ? value : 0f;
    }
    private float _speed;
    public float Speed
    {
        get => _speed;
        // 速度不能小于特定值,不然就等于永远不到它
        set => _speed = (value > 0f) ? value : 10f;
    }
    private float _critchance;
    public float CritChance
    {
        get => _critchance;
        // 暴击率没法超出
        set => _critchance = Mathf.Clamp(value, 0f, 1f);
    }
    private float _critdamage;
    public float CritDamage
    {
        get => _critdamage;
        // 暴击不可能比正常伤害还低
        set => _critdamage = (value > 1f) ? value : 1f;
    }
    private float _hitchance;
    public float HitChance
    {
        get => _hitchance;
        set => _hitchance = (value > 0f) ? value : 0f;
    }
    private float _resistance;
    public float Resistance
    {
        get => _resistance;
        set => _resistance = (value > 0f) ? value : 0f;
    }
    // 存放Buff的列表
    public List<BuffBase> Buffs { get; protected set; } = new List<BuffBase>();
    // 存放技能的列表
    public List<SkillBase> Skills { get; protected set; } = new List<SkillBase>();
    #endregion

    #region 方法的封装

    /// <summary>
    /// 对该角色施加伤害
    /// </summary>
    /// <param name="damage">伤害数值</param>
    public void ApplyDamage(float damage)
    {

    }

    /// <summary>
    /// 退场触发
    /// </summary>
    protected void OnDeath()
    {

    }

    /// <summary>
    /// 添加Buff(必中)
    /// </summary>
    /// <param name="buff">Buff实例</param>
    public void AddBuff(BuffBase buff, RoleBase sourceRole)
    {
        Buffs.Add(buff);
        buff.Apply(this, sourceRole);
    }

    /// <summary>
    /// 移除Buff(必中)
    /// </summary>
    /// <param name="buff">Buff实例</param>
    public void RemoveBuff(BuffBase buff, RoleBase sourceRole)
    {
        Buffs.Remove(buff);
        buff.Remove(this, sourceRole);
    }

    /// <summary>
    /// 根据效果抵抗添加已命中的Buff
    /// </summary>
    /// <param name="buff">Buff实例</param>
    /// <param name="sourceRole">Buff添加来源</param>
    /// <param name="hitValue">命中时的概率</param>
    /// <returns></returns>
    public bool AddBuffEx(BuffBase buff, RoleBase sourceRole, float hitValue)
    {
        float resiValue = 1f - hitValue / (1 + Resistance);
        float rand = UnityEngine.Random.value;
        if(rand >= resiValue)
        {
            AddBuff(buff, sourceRole);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// [弃用]根据自身命中驱散Buff
    /// </summary>
    /// <param name="buff">Buff实例</param>
    /// <param name="sourceRole">来源</param>
    /// <returns></returns>
    public void RemoveBuffEx(BuffBase buff, RoleBase sourceRole)
    {
        
    }

    /// <summary>
    /// 根据技能位置释放技能
    /// </summary>
    /// <param name="skillIndex">技能的序号</param>
    public void CastSkillOfIndex(int skillIndex, RoleBase target)
    {
        Skills[skillIndex].Use(target, this);
    }

    /// <summary>
    /// 根据RoleData初始化角色属性
    /// </summary>
    public void InitializeRole()
    {
        // 附加基础属性
        MaxHealth = Data.Hp;
        Health = MaxHealth;
        Attack = Data.Atk;
        Defense = Data.Def;
        Speed = Data.Spd;
        CritChance = Data.Crit;
        CritDamage = Data.CritDmg;
        HitChance = Data.Hit;
        Resistance = Data.Res;

        // 附加额外属性

        // 添加Buff

        // 添加技能
        if(Data == null)
        {
            Debug.LogError("RoleData未设置");
            return;
        }

        foreach(SkillData skillData in Data.InitialSkills)
        {
            SkillBase skillInstance = CreateSkillInstance(skillData);
            skillInstance.Initialize(skillData);
            // 添加到列表中
            Skills.Add(skillInstance);
        }
    }


    private SkillBase CreateSkillInstance(SkillData skillData)
    {
        Type type = Type.GetType(skillData.ScriptClassName);
        if(type != null && type.IsSubclassOf(typeof(SkillBase)))
        {
            return (SkillBase)Activator.CreateInstance(type);
        }
        else
        {
            Debug.LogError($"Invalid skill script class name:{skillData.ScriptClassName}");
            return null;
        }
    }

    #endregion
}

public abstract class Role
{

}
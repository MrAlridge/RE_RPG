using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有与战斗有关的常量都在这定义
/// </summary>

public class BattleDef
{
    

}

/// <summary>
/// 战斗的状态标志
/// </summary>
public enum BattleState : byte
{
    Enter = 0,      // 进入战斗
    BeforeAct = 1,  // 行动前
    SelectAct = 2,  // 选择行动中
    Acting = 3,     // 行动表演中
    AfterAct = 4,   // 行动后
    ResultWin = 5,  // 战斗胜利
    ResultLose = 6, // 战斗失败
}

/// <summary>
/// Buff触发的时机
/// </summary>
public enum BuffTime : byte
{
    None = 0,
    Enter = 1,          // 自身入场时
    Exit = 2,           // 自身离场时
    BeforeAct = 3,      // 自身行动前
    AfterAct = 4,       // 自身行动后
    AddBuff = 5,        // 添加其他增益时
    RemoveBuff = 6,     // 移除其他增益时
    AddDebuff = 7,      // 添加其他减益时
    RemoveDebuff = 8,   // 移除其他减益时
    DeadSelf = 9,       // 自身死亡时
    ReviveSelf = 10,    // 自身复活时
    DeadAlly = 11,      // 友方死亡时
    ReviveAlly = 12,    // 友方复活时
    Dead = 13,          // 其他单位死亡时
    Revive = 14,        // 其他单位复活时
    OnCrit = 15,        // 暴击时
    OnCrited = 16,      // 受到暴击时
    OnKill = 17,        // 造成击杀时
    OnAttack = 18,      // 使用普攻时
    BeforeAllyCast = 19,// 友方使用技能前
    AfterAllyCast = 20, // 友方使用技能后
    BeforeEnemyCast = 21,// 敌方使用技能前
    AfterEnemyCast = 22,// 敌方使用技能后
    BeforeCast = 23,    // 自身使用技能前
    AfterCast = 24,     // 自身使用技能后
    GetCost = 25,       // 获得资源时
    ConsumeCost = 26,   // 消耗资源时
    OnAllyDamaged = 27, // 友方受伤时
    OnAllyHeal = 28,    // 友方承受治疗时
    OnEnemyDamaged = 29,// 敌方受伤时
    OnEnemyHeal = 30,   // 敌方承受治疗时
}

public enum BuffType : byte
{
    Buff,   // 正常Buff，可被驱散
    Mark,   // 印记，不可被驱散
}

public enum DamageType : byte
{
    Direct,     // 直接伤害
    InDirect,   // 间接伤害
    Convert,    // 传导伤害
}

public enum SkillType : byte
{
    Active,
    Passive,
    Attack,
}

public enum SkillTarget : byte
{
    Anyone,         // 在场活着任意一人
    EnemyAliveOne,  // 敌方活着一人
    AllyAliveOne,   // 我方活着一人
    EnemyAliveAll,  // 敌方活着所有人
    AllyAliveAll,   // 我方活着所有人
    EnemyDeadOne,   // 敌方阵亡一人
    AllyDeadOne,    // 我方阵亡一人
    EnemyNotSummon, // 敌方非召唤物一人
    AllyNotSummon,  // 我方非召唤物一人
    MainCharacter,  // 我方主要角色
}

public enum SkillCastTarget : byte
{
    Target,             // 只有目标哦
    TargetTeam,         // 目标阵营全员
    TargetTeamRandom,   // 目标阵营随机一人
    AllyTeam,           // 友方全体
    AllyHpLow,          // 友方血量最低单位
    EnemyTeam,          // 敌方全体
    EnemyTeamRandom,    // 敌方随机单体
    Summon,             // 召唤物占位
}

public enum SkillCastType : byte
{
    DealDamage,         // 造成伤害
    Heal,               // 治疗
    Recovery,           // 恢复
    AddBuff,            // 添加Buff(必中)
    RemoveBuff,         // 移除Buff(必中)
    AddBuffWithChance,  // 添加Buff(走命中)
    AddSummon,          // 添加召唤物
    RemoveSummon,       // 移除召唤物
}
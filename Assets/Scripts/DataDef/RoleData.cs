using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleData : ScriptableObject
{
    // 基础属性
    public string RoleName;
    public float Hp;
    public float Atk;
    public float Def;
    public float Spd;
    public float Crit;
    public float CritDmg;
    public float Hit;
    public float Res;

    // 初始Buff
    public List<BuffData> InitialBuffs = new List<BuffData>();

    // 初始技能
    public List<SkillData> InitialSkills = new List<SkillData>();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleSkillsManager
{
    private List<SkillBase> skills = new List<SkillBase>();
    private Dictionary<SkillBase, int> skillCDs = new Dictionary<SkillBase, int>();

    public void AddSkill(SkillBase skill)
    {
        skills.Add(skill);
        // 技能的初始CD按照
        skillCDs.Add(skill, skill.skillData.InitialCooldown);
    }

    public void RemoveSkill(SkillBase skill)
    {
        skills.Remove(skill);
        skillCDs.Remove(skill);
    }

    public void UseSkill(SkillBase skill, RoleBase caster, RoleBase target)
    {
        // 目前能使用就代表是可以释放的
        skillCDs[skill] = skill.skillData.CoolDown;
        skill.Use(caster, target);
    }

    public bool CanUseSkill(SkillBase skill)
    {
        // 目前只检查冷却
        if (skillCDs[skill] == 0) { return true; } else { return false; }
    }
}

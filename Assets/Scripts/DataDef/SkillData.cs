using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "数据文件/技能数据")]
public class SkillData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private SkillType _type;
    [SerializeField] private int _cost;
    [SerializeField] private int _cooldown;
    [SerializeField] private int _initialcooldown;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _scriptclassname;

    public string Name => _name;
    public SkillType Type => _type;
    public int Cost => _cost;
    public int CoolDown => _cooldown;
    public int InitialCooldown => _initialcooldown;
    public Sprite Icon => _icon;
    public string ScriptClassName => _scriptclassname;
}

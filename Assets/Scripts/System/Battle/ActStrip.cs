using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 将行动条单独封装成一个类,好操作
/// </summary>
public class ActStrip
{
    // 在场的所有单位
    private List<ActUnit> allUnits;
    // 行动条上的所有单位
    private List<ActUnit> units;
    // 行动条的长度,由一速决定
    private float actLength;

    public ActStrip()
    {
        this.allUnits = new List<ActUnit>();
        this.units = new List<ActUnit>();
        this.actLength = 0f;
    }

    /// <summary>
    /// 根据给定角色生成行动队列
    /// </summary>
    /// <param name="roles">参与战斗的角色</param>
    /// <returns></returns>
    public void GenerateActStrip(List<Role> roles)
    {
        float tempSpeed = 0f;   // 最快一速
        foreach (Role role in roles)
        {
            if(role.speed > tempSpeed)
                tempSpeed = role.speed;
            this.allUnits.Add(new ActUnit(role));
        }
        this.actLength = tempSpeed;
        // 根据curProgress排序
        this.allUnits.Sort((ActUnit x, ActUnit y) =>
        {
            Role roleX = BattleManager.GetRoleById(x.roleId);
            Role roleY = BattleManager.GetRoleById(y.roleId);
            return roleX.speed.CompareTo(roleY.speed);
        });
        this.units = allUnits;
    }

    /// <summary>
    /// 行动至下一个角色并返回它
    /// </summary>
    /// <returns>行动条推进后进行行动的角色</returns>
    public Role ActProgress()
    {
        // 只用看行动条的第一个就知道整体的情况了
        ActUnit unit = this.units[0];
        if (unit.curProgress >= actLength)
        {
            // 复制一份到队尾,删除unit并返回这个角色
            unit.Duplicate(unit.curProgress - actLength);
            Role ret = BattleManager.GetRoleById(unit.roleId);
            units.Remove(unit);
            return ret;
        }
        else
        {
            // 所有人都按照自己的速度行动
            foreach(ActUnit u in units)
            {
                Role role = BattleManager.GetRoleById(u.roleId);
                if(role != null)
                {
                    u.curProgress += role.GetSpeed();
                }
            }
            // 行动完递归调用一次
            return ActProgress();
        }
    }
}

public class ActUnit
{
    // 在本场战斗中的唯一Id
    public int roleId = 0;
    // 当前位于行动条的哪个位置
    public float curProgress;

    public ActUnit()
    {
        this.roleId = 0;
        this.curProgress = 0f;
    }

    // 初始生成角色
    public ActUnit(Role role)
    {
        // 读取角色的Id
        this.roleId = role.Id;
        this.curProgress = role.speed;
    }

    // 生成同一个单位的不同速度副本
    public ActUnit Duplicate(float extraProgress)
    {
        if(this.roleId == 0)
        {
            Debug.LogError("复制了一个空目标");
            return null;
        }
        this.curProgress = extraProgress;
        return this;
    }
}

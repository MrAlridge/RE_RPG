using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ж���������װ��һ����,�ò���
/// </summary>
public class ActStrip
{
    // �ڳ������е�λ
    private List<ActUnit> allUnits;
    // �ж����ϵ����е�λ
    private List<ActUnit> units;
    // �ж����ĳ���,��һ�پ���
    private float actLength;

    public ActStrip()
    {
        this.allUnits = new List<ActUnit>();
        this.units = new List<ActUnit>();
        this.actLength = 0f;
    }

    /// <summary>
    /// ���ݸ�����ɫ�����ж�����
    /// </summary>
    /// <param name="roles">����ս���Ľ�ɫ</param>
    /// <returns></returns>
    public void GenerateActStrip(List<Role> roles)
    {
        float tempSpeed = 0f;   // ���һ��
        foreach (Role role in roles)
        {
            if(role.speed > tempSpeed)
                tempSpeed = role.speed;
            this.allUnits.Add(new ActUnit(role));
        }
        this.actLength = tempSpeed;
        // ����curProgress����
        this.allUnits.Sort((ActUnit x, ActUnit y) =>
        {
            Role roleX = BattleManager.GetRoleById(x.roleId);
            Role roleY = BattleManager.GetRoleById(y.roleId);
            return roleX.speed.CompareTo(roleY.speed);
        });
        this.units = allUnits;
    }

    /// <summary>
    /// �ж�����һ����ɫ��������
    /// </summary>
    /// <returns>�ж����ƽ�������ж��Ľ�ɫ</returns>
    public Role ActProgress()
    {
        // ֻ�ÿ��ж����ĵ�һ����֪������������
        ActUnit unit = this.units[0];
        if (unit.curProgress >= actLength)
        {
            // ����һ�ݵ���β,ɾ��unit�����������ɫ
            unit.Duplicate(unit.curProgress - actLength);
            Role ret = BattleManager.GetRoleById(unit.roleId);
            units.Remove(unit);
            return ret;
        }
        else
        {
            // �����˶������Լ����ٶ��ж�
            foreach(ActUnit u in units)
            {
                Role role = BattleManager.GetRoleById(u.roleId);
                if(role != null)
                {
                    u.curProgress += role.GetSpeed();
                }
            }
            // �ж���ݹ����һ��
            return ActProgress();
        }
    }
}

public class ActUnit
{
    // �ڱ���ս���е�ΨһId
    public int roleId = 0;
    // ��ǰλ���ж������ĸ�λ��
    public float curProgress;

    public ActUnit()
    {
        this.roleId = 0;
        this.curProgress = 0f;
    }

    // ��ʼ���ɽ�ɫ
    public ActUnit(Role role)
    {
        // ��ȡ��ɫ��Id
        this.roleId = role.Id;
        this.curProgress = role.speed;
    }

    // ����ͬһ����λ�Ĳ�ͬ�ٶȸ���
    public ActUnit Duplicate(float extraProgress)
    {
        if(this.roleId == 0)
        {
            Debug.LogError("������һ����Ŀ��");
            return null;
        }
        this.curProgress = extraProgress;
        return this;
    }
}

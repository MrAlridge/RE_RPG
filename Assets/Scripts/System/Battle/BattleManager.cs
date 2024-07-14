using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BattleManager : MonoBehaviour
{
    // 所有的等待都用这个来实现
    private readonly WaitForSeconds _delaySeconds = new WaitForSeconds(0.3f);

    #region 战斗相关数据
    // 存放用来深拷贝的双方数据
    /// <summary>
    /// 玩家队伍的角色数据
    /// </summary>
    List<RoleData> playerTeam;
    /// <summary>
    /// 敌方队伍的角色数据
    /// </summary>
    List<RoleData> enemyTeam;

    /// <summary>
    /// 玩家队伍的角色
    /// </summary>
    public static List<Role> playerRoles;
    /// <summary>
    /// 敌方队伍的角色
    /// </summary>
    public static List<Role> enemyRoles;
    /// <summary>
    /// 当前回合正在行动的角色
    /// </summary>
    Role curActRole;
    /// <summary>
    /// 当前的行动队列和之后的行动队列
    /// </summary>
    ActStrip actStrip;
    /// <summary>
    /// 当前战斗的状态
    /// </summary>
    private BattleState _battleState;

    #endregion

    void Start()
    {
        StartCoroutine(Fight(() => { Debug.Log("战斗胜利"); }, () => { Debug.Log("战斗失败"); }));
    }

    void Update()
    {
        
    }

    IEnumerator Fight(Action winCallback, Action loseCallback)
    {
        // 从这里开始就是进入战斗逻辑的初始化部分
        foreach(RoleData playerData in playerTeam)
        {
            // 初始化玩家阵营的人物
            playerRoles.Add(playerData.ToRole());
        }
        foreach (RoleData enemyData in enemyTeam)
        {
            enemyRoles.Add(enemyData.ToRole());
        }
        // 读取所有单位的速度,初始化行动条
        actStrip.GenerateActStrip(playerRoles.Concat(enemyRoles).ToList());

        // 初始化战斗结果
        int result = 0;

        // 战斗主循环
        while ((int)_battleState < 5 )
        {
            // 处理先机事件,只有刚进入战斗会触发
            if (_battleState == BattleState.Enter)
            {
                // TODO:遍历单位的先机相关属性
                Debug.Log("触发了所有人的先机效果");
                while(_battleState == BattleState.Acting) { yield return _delaySeconds; }
            }

            result = CheckBattleResult();
            if(result == 1) {yield return winCallback; }
            else if(result == 2) { yield return loseCallback; }

            curActRole = actStrip.ActProgress();
            _battleState = BattleState.BeforeAct;
            // TODO: 回合前BUFF检测

            if(playerRoles.Contains(curActRole) && _battleState == BattleState.BeforeAct)
            {
                _battleState = BattleState.SelectAct;
                // 等待玩家操作
                while(_battleState == BattleState.SelectAct) { yield return _delaySeconds; }
            }else if(enemyRoles.Contains(curActRole) && _battleState == BattleState.BeforeAct)
            {
                // TODO: 调用敌人的AI选择技能
            }

            // 技能演出
            if(curActRole.curSkill != null)
            {
                curActRole.CastCurSkill();
                while(_battleState == BattleState.Acting) { yield return _delaySeconds; }
            }

            _battleState = BattleState.AfterAct;
            // TODO: 行动后的逻辑

            // 判断是否满足结束条件
            result = CheckBattleResult();
            if (result == 1) { yield return winCallback; }
            else if (result == 2) { yield return loseCallback; }
        }
    }

    /// <summary>
    /// 检查当前是否满足结束条件
    /// </summary>
    /// <returns>0 - 还没结束，1 - 胜利，2 - 失败</returns>
    public int CheckBattleResult()
    {
        // 玩家阵营全部阵亡 -- 失败
        if(playerRoles.Count == 0) { return 2; }
        // 敌方阵营全部阵亡 -- 胜利
        if(enemyRoles.Count == 0) { return 1; }
        // 否则就是还没结束
        return 0;
    }

    #region 外界获取数据的接口

    /// <summary>
    /// 根据角色的Id获取角色对象
    /// </summary>
    /// <param name="Id">角色的Id</param>
    /// <returns>成功返回角色对象,失败返回null</returns>
    public static Role GetRoleById(int Id)
    {
        foreach (Role role in playerRoles)
        {
            if (role.Id == Id){ return role; }
        }
        foreach(Role role in enemyRoles)
        {
            if (role.Id == Id) { return role; }
        }
        return null;
    }

    #endregion
}

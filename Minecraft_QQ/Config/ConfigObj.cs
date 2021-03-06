﻿using Color_yr.Minecraft_QQ.Utils;
using System.Collections.Generic;
using System.IO;

namespace Color_yr.Minecraft_QQ.Config
{
    class ConfigFile
    {
        /// <summary>
        /// 主要配置文件
        /// </summary>
        public static FileInfo 主要配置文件 { get; set; }
        /// <summary>
        /// 玩家管理配置文件
        /// </summary>
        public static FileInfo 玩家储存 { get; set; }
        /// <summary>
        /// 自动应答配置文件
        /// </summary>
        public static FileInfo 自动应答 { get; set; }
        /// <summary>
        /// 服务器命令配置文件
        /// </summary>
        public static FileInfo 自定义指令 { get; set; }
        /// <summary>
        /// 群配置文件
        /// </summary>
        public static FileInfo 群设置 { get; set; }
    }
    public class PlayerConfig
    {
        /// <summary>
        /// 禁止绑定数据储存
        /// </summary>
        public List<string> 禁止绑定列表 { get; set; } = new List<string> { "Color_yr" };
        /// <summary>
        /// 禁言数据存储
        /// </summary>
        public List<string> 禁言列表 { get; set; } = new List<string> { };
        /// <summary>
        /// 玩家数据储存
        /// </summary>
        public Dictionary<long, PlayerObj> 玩家列表 { get; set; } = new Dictionary<long, PlayerObj> { };
    }
    public class CommandConfig
    {
        /// <summary>
        /// 服务器指令数据储存
        /// </summary>
        public Dictionary<string, CommandObj> 命令列表 { get; set; } = new Dictionary<string, CommandObj> { };
    }
    public class GroupConfig
    {
        /// <summary>
        /// 设置的群数据储存
        /// </summary>
        public Dictionary<long, GroupObj> 群列表 { get; set; } = new Dictionary<long, GroupObj> { };
    }
    public class AskConfig
    {
        /// <summary>
        /// 自动应答存储
        /// </summary>
        public Dictionary<string, string> 自动应答列表 { get; set; } = new Dictionary<string, string> { };
    }
    public class MainConfig
    {
        /// <summary>
        /// 设置
        /// </summary>
        public SettingConfig 设置 { get; set; } = new SettingConfig();
        /// <summary>
        /// 消息
        /// </summary>
        public MessageConfig 消息 { get; set; } = new MessageConfig();
        /// <summary>
        /// 检测消息
        /// </summary>
        public CheckConfig 检测 { get; set; } = new CheckConfig();
        /// <summary>
        /// 管理员指令
        /// </summary>
        public AdminConfig 管理员 { get; set; } = new AdminConfig();
        /// <summary>
        /// Socket配置
        /// </summary>
        public SocketConfig 链接 { get; set; } = new SocketConfig();
        /// <summary>
        /// Mysql配置文件
        /// </summary>
        public MysqlConfig 数据库 { get; set; } = new MysqlConfig();
    }
    public class SettingConfig
    {
        /// <summary>
        /// 自动应答-开关
        /// </summary>
        public bool 自动应答开关 { get; set; } = false;
        /// <summary>
        /// 彩色代码-开关
        /// </summary>
        public bool 颜色代码开关 { get; set; } = false;
        /// <summary>
        /// 维护模式-开关
        /// </summary>
        public bool 维护模式 { get; set; } = false;
        /// <summary>
        /// 同步对话-开关
        /// </summary>
        public bool 始终发送消息 { get; set; } = false;
        /// <summary>
        /// 服务器昵称-开关
        /// </summary>
        public bool 使用昵称发送至服务器 { get; set; } = true;
        /// <summary>
        /// 群昵称-开关
        /// </summary>
        public bool 使用昵称发送至群 { get; set; } = true;
        /// <summary>
        /// 允许玩家绑定ID
        /// </summary>
        public bool 可以绑定名字 { get; set; } = true;
        /// <summary>
        /// 发送日志消息到群
        /// </summary>
        public bool 发送日志到群 { get; set; } = true;
        /// <summary>
        /// 发送群消息间隔
        /// </summary>
        public int 发送群消息间隔 { get; set; } = 100;
    }
    public class MessageConfig
    {
        /// <summary>
        /// 发送至服务器的文本
        /// </summary>
        public string 发送至服务器文本 { get; set; } = "%player%:%message%";
        /// <summary>
        /// 维护时发送的文本
        /// </summary>
        public string 维护提示文本 { get; set; } = "服务器正在维护，请等待维护结束！";
        /// <summary>
        /// 未知的指令
        /// </summary>
        public string 位置指令文本 { get; set; } = "未知指令";
        /// <summary>
        /// 禁止绑定ID
        /// </summary>
        public string 不能绑定文本 { get; set; } = "绑定ID已关闭";
    }
    public class CheckConfig
    {
        /// <summary>
        /// 检测头
        /// </summary>
        public string 检测头 { get; set; } = "#";
        /// <summary>
        /// 在线人数
        /// </summary>
        public string 在线玩家获取 { get; set; } = "在线人数";
        /// <summary>
        /// 状态检测
        /// </summary>
        public string 服务器在线检测 { get; set; } = "服务器状态";
        /// <summary>
        /// 玩家绑定ID
        /// </summary>
        public string 玩家设置名字 { get; set; } = "绑定：";
        /// <summary>
        /// 玩家发送消息
        /// </summary>
        public string 发送消息至服务器 { get; set; } = "服务器：";
    }
    public class AdminConfig
    {
        /// <summary>
        /// 禁言玩家
        /// </summary>
        public string 禁言 { get; set; } = "禁言：";
        /// <summary>
        /// 取消禁言
        /// </summary>
        public string 取消禁言 { get; set; } = "解禁：";
        /// <summary>
        /// 查询ID
        /// </summary>
        public string 查询绑定名字 { get; set; } = "查询：";
        /// <summary>
        /// 重命名玩家
        /// </summary>
        public string 重命名 { get; set; } = "修改：";
        /// <summary>
        /// 切换服务器维护模式
        /// </summary>
        public string 维护模式切换 { get; set; } = "服务器维护";
        /// <summary>
        /// 配置文件重读
        /// </summary>
        public string 重读配置 { get; set; } = "重读文件";
        /// <summary>
        /// 打开菜单
        /// </summary>
        public string 打开菜单 { get; set; } = "打开菜单";
        /// <summary>
        /// 昵称
        /// </summary>
        public string 设置昵称 { get; set; } = "昵称：";
        /// <summary>
        /// 禁止绑定列表
        /// </summary>
        public string 获取禁止绑定列表 { get; set; } = "禁止绑定列表";
        /// <summary>
        /// 禁言列表
        /// </summary>
        public string 获取禁言列表 { get; set; } = "禁言列表";
        /// <summary>
        /// 发送绑定信息QQ号
        /// </summary>
        public long 发送绑定信息QQ号 { get; set; } = 0;
    }
    public class SocketConfig
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string 地址 { get; set; } = "127.0.0.1";
        /// <summary>
        /// 端口
        /// </summary>
        public int 端口 { get; set; } = 25555;
        /// <summary>
        /// 编码类型
        /// </summary>
        public string 编码 { get; set; } = "ANSI";
        /// <summary>
        /// 数据包头
        /// </summary>
        public string 数据头 { get; set; } = "[Head]";
        /// <summary>
        /// 数据包尾
        /// </summary>
        public string 数据尾 { get; set; } = "[End]";
    }

    public class MysqlConfig
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string 地址 { get; set; } = "127.0.0.1";
        /// <summary>
        /// 端口
        /// </summary>
        public int 端口 { get; set; } = 3306;
        /// <summary>
        /// 账户
        /// </summary>
        public string 用户名 { get; set; } = "root";
        /// <summary>
        /// 密码
        /// </summary>
        public string 密码 { get; set; } = "root";
        /// <summary>
        /// Mysql启用
        /// </summary>
        public bool 是否启用 { get; set; } = false;
        /// <summary>
        /// 数据库名
        /// </summary>
        public string 数据库 { get; set; } = "minecraft_qq";
    }
}

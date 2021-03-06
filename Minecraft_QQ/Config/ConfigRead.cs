﻿using Color_yr.Minecraft_QQ.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Color_yr.Minecraft_QQ.Config
{
    class ConfigRead
    {
        /// <summary>
        /// 读取主要配置文件
        /// </summary>
        public MainConfig ReadConfig()
        {
            logs.LogWrite("[INFO][Config]读取主配置");
            try
            {
                var config = JsonConvert.DeserializeObject<MainConfig>
                    (File.ReadAllText(ConfigFile.主要配置文件.FullName));
                bool save = false;
                if (config.数据库 == null)
                {
                    config.数据库 = new MysqlConfig();
                    save = true;
                }
                if (config.检测 == null)
                {
                    config.检测 = new CheckConfig();
                    save = true;
                }
                if (config.消息 == null)
                {
                    config.消息 = new MessageConfig();
                    save = true;
                }
                if (config.管理员 == null)
                {
                    config.管理员 = new AdminConfig();
                    save = true;
                }
                if (config.设置 == null)
                {
                    config.设置 = new SettingConfig();
                    save = true;
                }
                if (config.链接 == null)
                {
                    config.链接 = new SocketConfig();
                    save = true;
                }
                if (save)
                {
                    MessageBox.Show("Mainconfig.json配置文件读取发送错误，已经重写");
                    new ConfigWrite().Config();
                }
                return config;
            }
            catch (Exception e)
            {
                logs.LogWrite("[ERROR][Config]配置文件读取错误：" + e.Message);
                return new MainConfig();
            }
        }
        public PlayerConfig ReadPlayer()
        {
            logs.LogWrite("[INFO][Config]读取玩家配置");
            try
            {
                var config = JsonConvert.DeserializeObject<PlayerConfig>
                    (File.ReadAllText(ConfigFile.玩家储存.FullName));
                bool save = false;
                if (config.玩家列表 == null)
                {
                    config.玩家列表 = new Dictionary<long, PlayerObj>();
                    save = true;
                }
                if (config.禁止绑定列表 == null)
                {
                    config.禁止绑定列表 = new List<string>() { "Color_yr" };
                    save = true;
                }
                if (config.禁言列表 == null)
                {
                    config.禁言列表 = new List<string>();
                    save = true;
                }
                if (save)
                {
                    MessageBox.Show("Player.json配置文件读取发送错误，已经重写");
                    new ConfigWrite().Player();
                }
                return config;
            }
            catch (Exception e)
            {
                logs.LogWrite("[ERROR][Config]配置文件读取错误：" + e.Message);
                return new PlayerConfig();
            }
        }
        public GroupConfig ReadGroup()
        {
            logs.LogWrite("[INFO][Config]读取群设置");
            try
            {
                var config = JsonConvert.DeserializeObject<GroupConfig>
                    (File.ReadAllText(ConfigFile.群设置.FullName));
                if (config.群列表 == null)
                {
                    MessageBox.Show("Group.json配置文件读取发送错误，已经重写");
                    config.群列表 = new Dictionary<long, GroupObj>();
                    new ConfigWrite().Group();
                }
                foreach (var item in config.群列表)
                {
                    if (item.Value.主群 == true)
                    {
                        Minecraft_QQ.GroupSetMain = item.Key;
                        break;
                    }
                }
                return config;
            }
            catch (Exception e)
            {
                logs.LogWrite("[ERROR][Config]配置文件读取错误：" + e.Message);
                return new GroupConfig();
            }
        }
        public AskConfig ReadAsk()
        {
            logs.LogWrite("[INFO][Config]读取自定义应答");
            try
            {
                var config = JsonConvert.DeserializeObject<AskConfig>
                    (File.ReadAllText(ConfigFile.自动应答.FullName));

                if (config.自动应答列表 == null)
                {
                    config.自动应答列表 = new Dictionary<string, string>()
                    {
                        { "服务器菜单", "服务器查询菜单：\r\n【" + Minecraft_QQ.MainConfig.检测.检测头 + Minecraft_QQ.MainConfig.检测.玩家设置名字
                    + "】可以绑定你的游戏ID。\r\n【" + Minecraft_QQ.MainConfig.检测.检测头 + Minecraft_QQ.MainConfig.检测.在线玩家获取
                    + "】可以查询服务器在线人数。\r\n【" + Minecraft_QQ.MainConfig.检测.检测头 + Minecraft_QQ.MainConfig.检测.服务器在线检测
                    + "】可以查询服务器是否在运行。\r\n【" + Minecraft_QQ.MainConfig.检测.检测头 + Minecraft_QQ.MainConfig.检测.发送消息至服务器
                    + "内容】可以向服务器里发送消息。（使用前请确保已经绑定了ID，）"}
                    };
                    MessageBox.Show("Ask.json配置文件读取发送错误，已经重写");
                    File.WriteAllText(ConfigFile.自动应答.FullName, JsonConvert.SerializeObject(config, Formatting.Indented));
                }
                return config;
            }
            catch (Exception e)
            {
                logs.LogWrite("[ERROR][Config]配置文件读取错误：" + e.Message);
                return new AskConfig();
            }
        }
        public CommandConfig ReadCommand()
        {
            logs.LogWrite("[INFO][Config]读取自定义指令");
            try
            {
                var config = JsonConvert.DeserializeObject<CommandConfig>
                    (File.ReadAllText(ConfigFile.自定义指令.FullName));
                if (config.命令列表 == null)
                {
                    config.命令列表 = new Dictionary<string, CommandObj>()
                    {
                        { "插件帮助", new CommandObj
                            {
                                命令 = "qq help",
                                玩家使用 = false,
                                玩家发送 = false,
                                附带参数 = true
                            }
                        },
                        { "查钱", new CommandObj
                            {
                                命令 = "money %player_name%",
                                玩家使用 = true,
                                玩家发送 = false,
                                附带参数 = false
                            }
                        },
                        { "禁言", new CommandObj
                            {
                                命令 = "mute ",
                                玩家使用 = false,
                                玩家发送 = false,
                                附带参数 = true
                            }
                        },
                        { "传送", new CommandObj
                            {
                                命令 = "tpa %player_at%",
                                玩家使用 = true,
                                玩家发送 = false,
                                附带参数 = false
                            }
                        }
                    };
                    MessageBox.Show("Command.json配置文件读取发送错误，已经重写");
                    File.WriteAllText(ConfigFile.自定义指令.FullName, JsonConvert.SerializeObject(config, Formatting.Indented));
                }
                return config;
            }
            catch (Exception e)
            {
                logs.LogWrite("[ERROR][Config]配置文件读取错误：" + e.Message);
                return new CommandConfig();
            }
        }
    }
}

﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Color_yr.Minecraft_QQ
{
    class message
    {
        public static message_send Message_re(string read)
        {
            message_send messagelist = new message_send();
            try
            {
                JObject jsonData = JObject.Parse(read);
                if (jsonData["data"].ToString() == "data")
                {
                    messagelist.group = jsonData["group"].ToString();
                    messagelist.message = jsonData["message"].ToString();
                    messagelist.player = jsonData["player"].ToString();
                    messagelist.is_commder = false;
                }
            }
            catch
            {
                messagelist.player = "没有玩家";
            }
            return messagelist;
        }

        public static void Message(string read)
        {
            int a;
            while (read.IndexOf(socket_config.data_Head) == 0 && read.IndexOf(socket_config.data_End) != -1)
            {
                string buff = use.get_string(read, socket_config.data_Head, socket_config.data_End);
                buff = use.RemoveColorCodes(buff);
                message_send message = Message_re(buff);
                if (string.IsNullOrWhiteSpace(message.message) == true || 
                    string.IsNullOrWhiteSpace(message.player) == true)
                    return;
                if (config_file.mute_list.Contains(message.player.ToLower()) == true)
                    return;
                if (message.is_commder == false && message.group == "group")
                {
                    if (main_config.nick_group == true)
                    {
                        player_save player = use.check_player_form_id(message.player, true);
                        if (player != null && string.IsNullOrWhiteSpace(player.nick) == false)
                            message.message = message.message.Replace(message.player, player.nick);
                    }
                    foreach (group_save value in config_file.group_list.Values)
                    {
                        if (value.say == true)
                            Send.Send_List.Add(value.group_l, message.message);
                    }
                }
                else
                {
                    long.TryParse(message.group, out long group);
                    if (config_file.group_list.ContainsKey(group) == true)
                    {
                        group_save list = config_file.group_list[group];
                        Send.Send_List.Add(list.group_l, message.message);
                    }
                }
                a = read.IndexOf(socket_config.data_End);
                read = read.Substring(a + socket_config.data_End.Length);
            }
        }
    }
}

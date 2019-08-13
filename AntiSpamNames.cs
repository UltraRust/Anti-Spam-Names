using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Oxide.Core.Libraries.Covalence; 

namespace Oxide.Plugins
{
    [Info("Anti Spam Names", "Ultra", "1.1.2")]
    [Description("Replaces domain extensions or specific spam words in player's name. Also renames fake admins.")]

    class AntiSpamNames : CovalencePlugin
    {
        #region Hooks

        void OnUserConnected(IPlayer player)
        {
            string newName = player.Name;

            if (configData.CheckSpamNames)
            {
                foreach (string spamKeyword in configData.SpamKeywordBlacklist)
                {
                    if (player.Name.ToLower().Contains(spamKeyword.ToLower()))
                    {
                        Regex regex = new Regex(spamKeyword, RegexOptions.IgnoreCase);
                        newName = regex.Replace(newName, configData.ReplaceForSpam);
                    }
                }
            }

            if (configData.CheckAdminNames && !player.IsAdmin)
            {
                foreach (string adminName in configData.AdminNameBlacklist)
                {
                    if (player.Name.ToLower().Contains(adminName.ToLower()))
                    {
                        Regex regex = new Regex(adminName, RegexOptions.IgnoreCase);
                        newName = regex.Replace(newName, configData.ReplaceForAdmin);
                    }
                }
            }

            if (player.Name.ToLower() != newName.ToLower())
            {
                player.Rename(newName);
            }
        }

        #endregion

        #region Config

        private ConfigData configData;

        private class ConfigData
        {
            [JsonProperty(PropertyName = "Check admin names")]
            public bool CheckAdminNames;

            [JsonProperty(PropertyName = "Admin name blacklist")]
            public List<string> AdminNameBlacklist;

            [JsonProperty(PropertyName = "Replace for admin")]
            public string ReplaceForAdmin;

            [JsonProperty(PropertyName = "Check spam names")]
            public bool CheckSpamNames;

            [JsonProperty(PropertyName = "Replace for spam")]
            public string ReplaceForSpam;

            [JsonProperty(PropertyName = "Spam keyword blacklist")]
            public List<string> SpamKeywordBlacklist;
        }

        protected override void LoadConfig()
        {
            try
            {
                base.LoadConfig();
                configData = Config.ReadObject<ConfigData>();
                if (configData == null)
                {
                    LoadDefaultConfig();
                }
            }
            catch
            {
                LoadDefaultConfig();
            }
            SaveConfig();

        } 

        protected override void LoadDefaultConfig()
        {
            configData = new ConfigData()
            {
                CheckAdminNames = true,
                CheckSpamNames = true,
                AdminNameBlacklist = new List<string>() { "Administrator", "Admin" },
                ReplaceForAdmin = "Player",
                ReplaceForSpam = "Spam",
                SpamKeywordBlacklist = new List<string>() { ".money", ".ru", ".com", ".pl", ".gg", ".de", ".net", "www.", ".org", ".info", ".cz", ".sk", ".uk", ".cn", ".nl", ".store", ".shop" }

            };
        }

        protected override void SaveConfig()
        {
            Config.WriteObject(configData, true);
            base.SaveConfig();
        }

        #endregion
    }
}

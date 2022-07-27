using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Reponse_Q_E_TpaSystem.Commands
{
    public class GTPA : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "gruptpa";

        public string Help => "gruptpa";

        public string Syntax => "gruptpa";

        public List<string> Aliases => new List<string> { "reponsetpa" };

        public List<string> Permissions => new List<string> { "reponsetpa" };
        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer pl = (UnturnedPlayer)caller;
            var c = Class1.Instance.Configuration.Instance;
            var değer = c.Kayıt.FirstOrDefault(e => e.KullanıcıID == pl.CSteamID);
            string logo = Class1.Instance.Configuration.Instance.logo;
            var g = değer.TpaGroup;
            if (g == true)
            {
                ChatManager.serverSendMessage($"<size=20><color=green>TPA |</color></size> <color=orange>Grup Tpa</color> Başarılı Bir Şekilde Kapatıldı!", Color.white, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);
                değer.TpaGroup = false;
                Class1.Instance.Configuration.Save();

            }
            else if (g == false)
            {
                ChatManager.serverSendMessage($"<size=20><color=green>TPA |</color></size> <color=orange>Grup Tpa</color> Başarılı Bir Şekilde Açıldı!", Color.white, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);

                değer.TpaGroup = true;
                Class1.Instance.Configuration.Save();

            }

        }
    }
}

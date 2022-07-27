using Rocket.API;
using Rocket.Core.Commands;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Reponse_Q_E_TpaSystem
{
    public class Class1 : RocketPlugin<Config>
    {
        public static Class1 Instance;
        public class TpaPlayer
        {
            public UnturnedPlayer fromUplayer;
            public UnturnedPlayer toUplayer;
        }

        public List<TpaPlayer> PlayersTpaList = new List<TpaPlayer>();
        protected override void Load()
        {
            base.Load();
            PlayersTpaList.Clear();
            Instance = this;
            PlayerInput.onPluginKeyTick += test;
            U.Events.OnPlayerConnected += jOİN;
        }

        private void jOİN(UnturnedPlayer player)
        {
            var değer = Configuration.Instance.Kayıt.FirstOrDefault(e => e.KullanıcıID == player.CSteamID);

            if (değer == null)
            {
                Configuration.Instance.Kayıt.Add(new KullanıcıKayıt { KullanıcıID = player.CSteamID, TpaGroup = false });
                var değers = Configuration.Instance.Kayıt.FirstOrDefault(e => e.KullanıcıID == player.CSteamID);
                Configuration.Save();
            }
            else
            {
                return;
            }
        }

        private void test(Player player, uint simulation, byte key, bool state)
        {
            string logo = Class1.Instance.Configuration.Instance.logo;
            UnturnedPlayer uplayer = UnturnedPlayer.FromPlayer(player);
            var Players = PlayersTpaList.Find(p => p.toUplayer.CharacterName == uplayer.CharacterName);
            if (player.input.keys[6])
            {
                ChatManager.serverSendMessage($"<size=20><color=green>TPA |</color></size> <color=orange>{Players.toUplayer.CharacterName}</color> Adlı Kullanıcı İsteğini <color=green>Kabul</color> Etti!", Color.white, null, Players.fromUplayer.SteamPlayer(), EChatMode.SAY, logo, true);

                Players.fromUplayer.Teleport(Players.toUplayer.Position, 0);
                PlayersTpaList.Remove(Players);
            }
            else if (player.input.keys[7])
            {
                ChatManager.serverSendMessage($"<size=20><color=green>TPA |</color></size> <color=orange>{Players.toUplayer.CharacterName}</color> Adlı Kullanıcı İsteğini <color=red>Reddetti</color> Etti!", Color.white, null, Players.fromUplayer.SteamPlayer(), EChatMode.SAY, logo, true);
                PlayersTpaList.Remove(Players);
            }
        }

        public void StartC(UnturnedPlayer upla)
        {
            StartCoroutine(DestroyUI(upla));
        }
        public IEnumerator DestroyUI(UnturnedPlayer player)
        {
            string logo = Class1.Instance.Configuration.Instance.logo;


            yield return new WaitForSeconds(Class1.Instance.Configuration.Instance.KabulEtmeZaman);
            var Players = PlayersTpaList.Find(p => p.toUplayer.CharacterName == player.CharacterName);
            if (Players != null)
            {
                PlayersTpaList.Remove(Players);
                ChatManager.serverSendMessage($"<size=20><color=green>TPA |</color></size> <color=orange>{Players.toUplayer.CharacterName}</color> Adlı Kullanıcı <color=red>Talebe</color> Cevap Vermedi!", Color.white, null, Players.fromUplayer.SteamPlayer(), EChatMode.SAY, logo, true);
            }
        }
   
        protected override void Unload()
        {
            base.Unload();
            Instance = null;
        }
    }
}
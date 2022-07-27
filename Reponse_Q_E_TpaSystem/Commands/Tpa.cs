using Rocket.API;
using Rocket.Unturned.Chat;
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
    public class TPA : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "tpa";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string> { "reponsetpa" };

        public List<string> Permissions => new List<string> { "reponsetpa" };
        public UnturnedPlayer uplayer;
        public void Execute(IRocketPlayer caller, string[] command)
        {
            uplayer = (UnturnedPlayer)caller;
            string logo = Class1.Instance.Configuration.Instance.logo;

            if (command.Length >= 1)
            {
                UnturnedPlayer uplayer2 = UnturnedPlayer.FromName(command[0]);
                var c = Class1.Instance.Configuration.Instance;
                var değer = c.Kayıt.FirstOrDefault(e => e.KullanıcıID == uplayer2.CSteamID);
                if (uplayer2 != null)
                {
                    if (uplayer.Stance == EPlayerStance.DRIVING || uplayer.Stance == EPlayerStance.SITTING)
                    {
                        ChatManager.serverSendMessage($"<size=20><color=green>TPA |</color></size> Arabadayken Tpa Atamazsın!", Color.white, null, uplayer.SteamPlayer(), EChatMode.SAY, logo, true);

                        return;
                    }
                    if (uplayer.CSteamID == uplayer2.CSteamID)
                    {
                        ChatManager.serverSendMessage($"<size=20><color=green>TPA |</color></size> Kendine Tpa Atamazsın!", Color.white, null, uplayer.SteamPlayer(), EChatMode.SAY, logo, true);

                        return;
                    }
                    if (değer.TpaGroup == true)
                    {
                        Console.WriteLine($"{uplayer2.SteamGroupID} + {uplayer.SteamGroupID}");
                        if (uplayer.Player.quests.groupID == uplayer2.Player.quests.groupID)
                        {
                            Class1.Instance.PlayersTpaList.Add(new Class1.TpaPlayer { fromUplayer = uplayer, toUplayer = uplayer2 });

                            ChatManager.serverSendMessage($"<size=20><color=green>TPA GROUP |</color></size> <color=orange>{uplayer2.CharacterName}</color> Adlı Kullanıcıya Tpa İsteği Yolladın!", Color.white, null, uplayer.SteamPlayer(), EChatMode.SAY, logo, true);
                            ChatManager.serverSendMessage($"<size=20><color=green>TPA GROUP |</color></size> <color=orange>{uplayer.CharacterName}</color> Adlı Kullanıcı Sana Tpa İsteği Yolladı Kabul Etmek İçin <color=orange>[</color><color=green>Q</color><color=orange>]</color> Reddetmek İçin <color=orange>[</color><color=red>E</color><color=orange>]</color> Tuşlarını Basınız.", Color.white, null, uplayer2.SteamPlayer(), EChatMode.SAY, logo, true);

                            Class1.Instance.StartC(uplayer2);
                            return;
                        }
                        else
                        {
                            ChatManager.serverSendMessage($"<size=20><color=green>TPA |</color></size> <color=orange>{uplayer2.CharacterName}</color> Adlı Kullanıcı Sadece <color=red>Grup Üyelerinden</color> Tpa İsteği Alıyor.!", Color.white, null, uplayer.SteamPlayer(), EChatMode.SAY, logo, true);
                        }
                    }
                    else
                    {
                        Class1.Instance.PlayersTpaList.Add(new Class1.TpaPlayer { fromUplayer = uplayer, toUplayer = uplayer2 });

                        ChatManager.serverSendMessage($"<size=20><color=green>TPA |</color></size> <color=orange>{uplayer2.CharacterName}</color> Adlı Kullanıcıya Tpa İsteği Yolladın!", Color.white, null, uplayer.SteamPlayer(), EChatMode.SAY, logo, true);
                        ChatManager.serverSendMessage($"<size=20><color=green>TPA |</color></size> <color=orange>{uplayer.CharacterName}</color> Adlı Kullanıcı Sana Tpa İsteği Yolladı Kabul Etmek İçin <color=orange>[</color><color=green>Q</color><color=orange>]</color> Reddetmek İçin <color=orange>[</color><color=red>E</color><color=orange>]</color> Tuşlarını Basınız.", Color.white, null, uplayer2.SteamPlayer(), EChatMode.SAY, logo, true);

                        Class1.Instance.StartC(uplayer2);
                    }

                }
                else
                {
                    ChatManager.serverSendMessage($"<size=20><color=green>TPA |</color></size> <color=orange>{uplayer2.CharacterName}</color> Adlı Kullanıcı Bulunamadı!", Color.white, null, uplayer.SteamPlayer(), EChatMode.SAY, logo, true);
                }
            }
            else
            {
                ChatManager.serverSendMessage($"<size=20><color=green>TPA |</color></size> Yanlış Kullanım <color=orange>Doğru Kullanım</color> /tpa <kullanıcı>!", Color.white, null, uplayer.SteamPlayer(), EChatMode.SAY, logo, true);

            }
        }
    }
}

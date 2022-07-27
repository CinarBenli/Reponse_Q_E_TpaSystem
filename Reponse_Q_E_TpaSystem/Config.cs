using Rocket.API;
using System.Collections.Generic;

namespace Reponse_Q_E_TpaSystem
{
    public class Config : IRocketPluginConfiguration
    {
        public float KabulEtmeZaman;
        public string logo;
        public List<KullanıcıKayıt> Kayıt = new List<KullanıcıKayıt>();
        public void LoadDefaults()
        {
            KabulEtmeZaman = 5;
            Kayıt = new List<KullanıcıKayıt>();
        }
    }
}
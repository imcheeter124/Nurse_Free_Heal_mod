using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Nurse_Free_Heal_mod
{
    public class NursefreehealGlobalNPC : GlobalNPC
    {
        private static int _quoteCount = -1;
        private static int QuoteCount
        {
            get
            {
                if (_quoteCount < 0)
                {
                    _quoteCount = 0;
                    while (Language.Exists($"Mods.Nurse_Free_Heal_mod.NurseQuotes.Quote{_quoteCount}"))
                        _quoteCount++;
                }
                return _quoteCount;
            }
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            if (npc.type != NPCID.Nurse)
            {
                base.GetChat(npc, ref chat);
                return;
            }

            Player player = Main.LocalPlayer;
            var modPlayer = player.GetModPlayer<NurseHealPlayer>();

            if (modPlayer.healCooldown > 0)
            {
                int secsLeft = (modPlayer.healCooldown + 59) / 60;
                chat = Language.GetTextValue("Mods.Nurse_Free_Heal_mod.Chat.CooldownMessage", secsLeft);
                return;
            }

            var config = ModContent.GetInstance<NurseHealConfig>();

            int missing = player.statLifeMax2 - player.statLife;
            int healAmount = (int)(missing * config.HealPercent / 100f);
            if (healAmount > 0)
            {
                player.Heal(healAmount);
                SoundEngine.PlaySound(SoundID.Heal, player.Center);
            }

            for (int i = 0; i < player.buffType.Length; i++)
            {
                int buff = player.buffType[i];
                if (buff != 0
                    && Main.debuff[buff]
                    && !BuffID.Sets.NurseCannotRemoveDebuff[buff])
                {
                    player.DelBuff(i);
                    i--;
                }
            }

            modPlayer.healCooldown = config.CooldownSeconds * 60;
            chat = Language.GetTextValue($"Mods.Nurse_Free_Heal_mod.NurseQuotes.Quote{Main.rand.Next(QuoteCount)}");
        }

        public override void OnKill(NPC npc)
        {
            if (!npc.boss) return;
            var config = ModContent.GetInstance<NurseHealConfig>();
            if (!config.ResetCooldownOnBossKill) return;
            Main.LocalPlayer.GetModPlayer<NurseHealPlayer>().healCooldown = 0;
        }
    }
}

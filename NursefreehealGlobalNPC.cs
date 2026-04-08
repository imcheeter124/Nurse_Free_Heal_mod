using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Nurse_Free_Heal_mod
{
    public class NursefreehealGlobalNPC : GlobalNPC
    {
        private const int QuoteCount = 29;

        public override void GetChat(NPC npc, ref string chat)
        {
            if (npc.type == NPCID.Nurse)
            {
                Player player = Main.LocalPlayer;
                var modPlayer = player.GetModPlayer<NurseHealPlayer>();

                if (modPlayer.healCooldown > 0)
                {
                    base.GetChat(npc, ref chat);
                    return;
                }

                int missing = player.statLifeMax2 - player.statLife;
                if (missing > 0)
                {
                    player.Heal(missing);
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

                var config = ModContent.GetInstance<NurseHealConfig>();
                modPlayer.healCooldown = config.CooldownSeconds * 60;
                chat = Language.GetTextValue($"Mods.Nurse_Free_Heal_mod.NurseQuotes.Quote{Main.rand.Next(QuoteCount)}");
                return;
            }
            base.GetChat(npc, ref chat);
        }
    }
}
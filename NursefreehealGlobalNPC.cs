using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Nursefreeheal
{
    public class NursefreehealGlobalNPC : GlobalNPC
    {
        public override void GetChat(NPC npc, ref string chat)
        {
            if (npc.type == NPCID.Nurse)
            {
                Player player = Main.LocalPlayer;
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
                chat = "[i:58]";
                return;
            }
            base.GetChat(npc, ref chat);
        }
    }
}
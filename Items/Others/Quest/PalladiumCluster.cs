using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AerovelenceMod.Items.Others.Quest
{
    public class PalladiumCluster : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palladium Cluster");
        }
        public override void SetDefaults()
        {
            item.value = Item.sellPrice(0, 0, 2, 35);
            item.maxStack = 1;
            item.width = 30;
            item.height = 28;
            item.rare = ItemRarityID.LightRed;
        }
    }
}
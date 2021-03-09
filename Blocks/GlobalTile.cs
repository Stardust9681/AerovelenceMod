using AerovelenceMod.Blocks.Ores;
using AerovelenceMod.Items.Others.Quest;
using AerovelenceMod.Items.Placeable.Ores;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AerovelenceMod
{
    public class AeroGlobalTile : GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (type == TileID.Copper && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<CopperCluster>());
                }
            }
            if (type == TileID.Tin && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<TinCluster>());
                }
            }
            if (type == TileID.Iron && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<IronCluster>());
                }
            }
            if (type == TileID.Lead && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<LeadCluster>());
                }
            }
            if (type == TileID.Silver && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<SilverCluster>());
                }
            }
            if (type == TileID.Tungsten && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<TungstenCluster>());
                }
            }
            if (type == TileID.Gold && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<GoldCluster>());
                }
            }
            if (type == TileID.Platinum && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<PlatinumCluster>());
                }
            }
            if (type == ModContent.TileType<SlateOreBlock>() && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<SlateCluster>());
                }
            }
            if (type == ModContent.TileType<PhanticOreBlock>() && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<PhanticCluster>());
                }
            }
            if (type == TileID.Cobalt && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<CobaltCluster>());
                }
            }
            if (type == TileID.Palladium && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<PalladiumCluster>());
                }
            }
            if (type == TileID.Mythril && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<MythrilCluster>());
                }
            }
            if (type == TileID.Orichalcum && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<OrichalcumCluster>());
                }
            }
            if (type == TileID.Adamantite && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<AdamantiteCluster>());
                }
            }
            if (type == TileID.Titanium && fail == false)
            {
                if (Main.rand.NextBool(151))
                {
                    Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<TitaniumCluster>());
                }
            }
        }
    }
}
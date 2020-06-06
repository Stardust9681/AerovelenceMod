using Terraria;
using Terraria.ModLoader;
using AerovelenceMod.Items.Placeble.CrystalCaverns;
using System;
using Terraria.ID;

namespace AerovelenceMod.Blocks.CrystalCaverns.Tiles
{
    public class CavernCrystal : ModTile
    {
        public override void SetDefaults()
        {
			mineResist = 2.5f;
			minPick = 59;
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<CavernStone>()] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
			dustType = 59;
			soundType = 21;
			drop = ModContent.ItemType<CavernCrystalItem>();
		}
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = 0.0f;
            g = 0.5f;
            b = 0.9f;
        }
    }
}
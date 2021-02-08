using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AerovelenceMod.Blocks.CrystalCaverns.Tiles.Furniture;

namespace AerovelenceMod.Items.Placeable.CrystalCaverns
{
    public class LargeCrystalItem3 : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("LargeCrystalItem3");
		}
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.sellPrice(0, 0, 0, 0);
            item.consumable = true;
            item.createTile = mod.TileType("LargeCrystal3");
        }
        public override void AddRecipes()
        {
            ModRecipe modRecipe = new ModRecipe(mod);
            modRecipe.AddIngredient(ModContent.ItemType<Glimmerwood>(), 3);
            modRecipe.AddIngredient(ModContent.ItemType<CavernCrystalItem>(), 1);
            modRecipe.AddTile(ModContent.TileType<CrystallineFabricator>());
            modRecipe.SetResult(this, 1);
            modRecipe.AddRecipe();
        }
    }
}

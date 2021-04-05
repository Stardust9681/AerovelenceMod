using AerovelenceMod.Content.Items.Weapons.Ranged;
using System.Collections.Generic;
using AerovelenceMod.Common.Globals.Worlds.WorldGeneration;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;

namespace AerovelenceMod
{
    public class AeroWorld : ModWorld
	{
		public static Dictionary<Vector2, Vector2> ETPLinks = new Dictionary<Vector2, Vector2>();
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int idx = tasks.FindIndex(t => t.Name == "Underworld"); //Terrain
			if (idx == -1)
			{
				idx = 1;
			}

			var pass = new WormGenPass();

			tasks.Insert(idx, pass);    //+1

			totalWeight += pass.Weight;
		}

        public override void PostWorldGen()
        {
			int[] itemsToPlaceInMarbleChests = { ModContent.ItemType<MarbleMusket>(), ItemID.SilverBullet};
			int itemsToPlaceInMarbleChestsChoice = 0;

			int[] itemsToPlaceInGraniteChests = { ModContent.ItemType<GraniteCannon>(), ItemID.SilverBullet };
			int itemsToPlaceInGraniteChestsChoice = 0;

			for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
			{
				Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 51 * 36)
				{
					for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
					{
						if (chest.item[inventoryIndex].type == ItemID.None)
						{
							chest.item[inventoryIndex].SetDefaults(itemsToPlaceInMarbleChests[itemsToPlaceInMarbleChestsChoice]);
							itemsToPlaceInMarbleChestsChoice = (itemsToPlaceInMarbleChestsChoice + 1) % itemsToPlaceInMarbleChests.Length;
							break;
						}
					}
				}
			}
			for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
			{
				Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 50 * 36)
				{
					for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
					{
						if (chest.item[inventoryIndex].type == ItemID.None)
						{
							chest.item[inventoryIndex].SetDefaults(itemsToPlaceInGraniteChests[itemsToPlaceInGraniteChestsChoice]);
							itemsToPlaceInGraniteChestsChoice = (itemsToPlaceInGraniteChestsChoice + 1) % itemsToPlaceInGraniteChests.Length;
							break;
						}
					}
				}
			}
		}
		public override TagCompound Save()
		{
			return new TagCompound {
		{"ETPLinksKeys", new List<Vector2>(ETPLinks.Keys)},
		{"ETPLinksValues", new List<Vector2>(ETPLinks.Values)}
			};
		}
		public override void Load(TagCompound tag)
		{
			if (tag.ContainsKey("ETPLinksKeys"))
			{
				if (tag.ContainsKey("ETPLinksValues"))
				{
					for (int i = 0; i < tag.GetList<Vector2>("ETPLinksKeys").Count; i++)
					{
						ETPLinks.Add(tag.GetList<Vector2>("ETPLinksKeys")[i], tag.GetList<Vector2>("ETPLinksValues")[i]);
					}
				}	
			}
		}
	}
}
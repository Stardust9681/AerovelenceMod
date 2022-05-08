using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AerovelenceMod.Content.Items.Armor.Seashine
{
    [AutoloadEquip(EquipType.Head)]
    public class SeashineHelmet : ModItem
    {

        bool canSpawnCrab = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seashine Helmet");
            Tooltip.SetDefault("6% increased summoning damage\n8% less mana cost\n+1 summon slot");
        }
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<SeashineBodyArmor>() && legs.type == ModContent.ItemType<SeashineLeggings>() && head.type == ModContent.ItemType<SeashineHelmet>();
		}
		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = "Summons a flying crab to protect you\nMovement speed in water heavily increased\nIMPORTANT: Unfortunately, this set does not summon a crab. We are working on implementing it!\nIn the meantime, this set will grant a very large increase to movement speed while\nin water by 20% more than what it was.";
            if (player.wet)
            {
                player.moveSpeed += 0.22f;
            }
            if (Main.myPlayer == player.whoAmI && player.FindBuffIndex(Mod.Find<ModBuff>("SeaCrab").Type) == -1)
            {
                player.AddBuff(Mod.Find<ModBuff>("CrabBuff").Type, 100, false);
                if(player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Weapons.Summoning.SeaCrab>()] <= 0)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("SeaCrab").Type, (int)(25f * player.GetDamage(DamageClass.Summon).Multiplicative), player.GetKnockback(DamageClass.Summon).Base, player.whoAmI);
                }
            }
        } 	

        
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.value = 10;
            Item.rare = ItemRarityID.Green;
            Item.defense = 1;
        }
		public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.06f;
            player.manaCost -= 0.08f;
            player.maxMinions += 1;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ItemID.SandBlock, 20)
                .AddIngredient(ItemID.Seashell, 5)
                .AddIngredient(ItemID.Starfish, 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
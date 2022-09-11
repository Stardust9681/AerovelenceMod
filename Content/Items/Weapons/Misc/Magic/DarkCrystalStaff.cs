using Terraria.ID;
using Terraria;
using Terraria.GameInput;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using AerovelenceMod.Common.Systems;
using System;

namespace AerovelenceMod.Content.Items.Weapons.Misc.Magic
{
    public class DarkCrystalStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
            Tooltip.SetDefault("Use to charge up, and release to discharge a devastating bolt of lightning");
        }
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.mana = 12;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 10;
            Item.useAnimation = 20;
            Item.UseSound = SoundID.Item21;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = 11500 * 5;
            Item.rare = 3;
            //Item.autoReuse = true;
            Item.DamageType = DamageClass.Magic;
            Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.Magic.Skylight_Proj>();
            //Item.shoot = ProjectileID.ShadowBeamFriendly;
            Item.shootSpeed = 12f;
        }
        public override void HoldItem(Player player)
        {
            if(player.itemAnimation == 0)
            {
                player.GetModPlayer<AeroPlayer>().useStyleData = null;
            }
        }
        public override bool? UseItem(Player player)
        {
            AeroPlayer modPlayer = player.GetModPlayer<AeroPlayer>();
            if (modPlayer.useStyleData == null)
            {
                modPlayer.useStyleInt++;
            }
            return true;
        }
        public override bool CanShoot(Player player)
        {
            return base.CanShoot(player) && player.GetModPlayer<AeroPlayer>().useStyleInt != 0 && !player.controlUseItem && player.itemAnimation < player.itemAnimationMax && player.GetModPlayer<AeroPlayer>().useStyleData.Equals(true);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            AeroPlayer modPlayer = player.GetModPlayer<AeroPlayer>();
            float modifier = MathHelper.Clamp(modPlayer.useStyleInt * .01f, 0f, 1f);
            modPlayer.ScreenShakePower = modifier * 48f;
            Vector2 endPos = Vector2.UnitY.RotatedBy(player.compositeFrontArm.rotation);
            float length = (Item.width + Item.height) * player.GetAdjustedItemScale(Item);
            endPos *= length;
            endPos += player.MountedCenter;
            if (player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectileDirect(source, endPos, velocity.RotatedBy(MathHelper.PiOver4), type, (int)(damage * ((modifier * 1.15f) + 1f)), knockback, player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y).netUpdate = true;
            }
            modPlayer.useStyleInt = 0;
            modPlayer.useStyleData = false;
            return false;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            AeroPlayer modPlayer = player.GetModPlayer<AeroPlayer>();

            if(modPlayer.useStyleInt > 0)
            {
                if(player.controlUseItem && (modPlayer.useStyleData == null || (bool)modPlayer.useStyleData == true))
                {
                    modPlayer.useStyleInt++;
                    if (player.itemAnimation < 2)
                    {
                        if(player.CheckMana(Item, pay: true))
                        {
                            player.itemAnimation = player.itemAnimationMax;
                            //Not waiting to fire..
                            modPlayer.useStyleData = false;
                        }
                        else
                        {
                            player.itemAnimation = player.itemAnimationMax;
                            //Waiting to fire..
                            modPlayer.useStyleData = true;
                        }
                    }
                }
                else
                {
                    //Waiting to fire..
                    modPlayer.useStyleData = true;
                    if(player.itemAnimation < (player.itemAnimationMax/2)-1)
                    {
                        player.itemAnimation = player.itemAnimationMax;
                    }
                }
            }

            float itemAngle = (Main.MouseWorld - player.Center).ToRotation();
            player.direction = Main.MouseWorld.X > player.Center.X ? 1 : -1;
            if (player.direction > 0)
                player.itemRotation = itemAngle;
            else
                player.itemRotation = itemAngle + MathHelper.Pi;
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, itemAngle - MathHelper.PiOver2);

            if (player.controlUseItem)
            {
                Vector2 endPos = Vector2.UnitY.RotatedBy(player.compositeFrontArm.rotation);
                float length = (Item.width + Item.height) * player.GetAdjustedItemScale(Item);
                endPos *= length;
                endPos += player.MountedCenter;
                if (Main.rand.NextFloat() < modPlayer.useStyleInt * .01f && modPlayer.useStyleInt < 101)
                {
                    Dust d = Dust.NewDustDirect(endPos + Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 16f, 1, 1, ModContent.DustType<Dusts.NormalDusts.Crystal>());
                    d.noGravity = true;
                    d.velocity = (endPos - d.position).SafeNormalize(Vector2.Zero);
                    d.scale = Main.rand.NextFloat(1.1f, 1.3f);
                }
                Lighting.AddLight(endPos, Color.Cyan.ToVector3());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.Audio;
using ReLogic.Content;
using Terraria.DataStructures;
using AerovelenceMod;
using Mono.Cecil;
using Steamworks;

namespace AerovelenceMod.Content.Items.Weapons.Misc.Magic
{
    public class CthulhusWrath : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
            DisplayName.SetDefault("Cthulhu's Wrath");
            Tooltip.SetDefault("Its angered essence is trapped.\nCharges up during use, increasing firepower significantly.");
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.crit = 7;
            Item.damage = 14;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.mana = 4;
            Item.UseSound = SoundID.Item20;
            Item.useStyle = 5;
            Item.knockBack = 6;
            Item.value = 53000 * 5;
            Item.rare = 11;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.ImpFireball;
            Item.shootSpeed = 18f;
        }
        public override bool? UseItem(Player player)
        {
            float itemAngle = (Main.MouseWorld - player.Center).ToRotation();
            if (player.direction > 0)
                player.itemRotation = itemAngle;
            else
                player.itemRotation = itemAngle + MathHelper.Pi;
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, itemAngle - MathHelper.PiOver2);
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            AeroPlayer modPlayer = player.GetModPlayer<AeroPlayer>();
            if (modPlayer.useStyleData == null)
                return false; //Make sure we're not using null values
            //We use Main.MouseWorld here, make sure the shooting player is this client
            if (player.whoAmI == Main.myPlayer && player.controlUseItem)
            {
                //Using this item guarantees useStyleData type to be Vector2[]
                //Called orbs because that's what they'll look like when drawn. :)
                Vector2[] orbs = (Vector2[])modPlayer.useStyleData;
                if (orbs.Length == 0)
                    return false;

                //Cache Main.mouseworld.
                Vector2 mouseWorld = Main.MouseWorld;

                //Pick the index of orb to use based on the player's itemanimation
                int index = (int)(((float)(player.itemAnimation-1) / (float)player.itemAnimationMax) * (orbs.Length));
                Vector2 pos = player.MountedCenter + (orbs[index]*4);
                Vector2 vel = (mouseWorld - pos).SafeNormalize(Vector2.Zero) * Item.shootSpeed;
                Projectile p = Projectile.NewProjectileDirect(source, pos-vel, vel, type, damage, knockback/orbs.Length, player.whoAmI);
                p.penetrate = 1;
                p.timeLeft = 60;
                return false;
            }
            //Vector2[] orbs2 = (Vector2[])modPlayer.useStyleData;
            //Projectile.NewProjectile(source, position, velocity, ProjectileID.DD2FlameBurstTowerT3Shot, damage * orbs2.Length, knockback * orbs2.Length, player.whoAmI);
            return false;
        }
        public override float UseTimeMultiplier(Player player)
        {
            AeroPlayer modPlayer = player.GetModPlayer<AeroPlayer>();
            //Since this works as a generic update hook when being held...
            //Use this to catch cases where these values aren't their default
            if(!player.controlUseItem && modPlayer.useStyleInt != 0)
            {
                modPlayer.useStyleInt = 0;
                modPlayer.useStyleData = null;
            }
            if (modPlayer.useStyleData == null)
                return base.UseTimeMultiplier(player); //Make sure we're not using null values
            //Using this item guarantees useStyleData type to be Vector2[]
            //Called orbs because that's what they'll look like when drawn. :)
            Vector2[] orbs = (Vector2[])modPlayer.useStyleData;
            if (orbs.Length == 0)
                return base.UseTimeMultiplier(player);
            //Fires more frequently with more orbs
            return base.UseTimeMultiplier(player) / orbs.Length;
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            AeroPlayer modPlayer = player.GetModPlayer<AeroPlayer>();
            //Here, useStyleInt is the duration of time the player has channelled this weapon for
            if (player.controlUseItem)
            {
                modPlayer.useStyleInt++;

                //No null values please
                if (modPlayer.useStyleData == null)
                    modPlayer.useStyleData = new Vector2[0];
                //Get the offset position(s)
                Vector2[] orbs = ((Vector2[])(modPlayer.useStyleData));

                //Just doing this because it's used a ton
                int lng = orbs.Length;
                //useStyleInt being compared to a gradually-longer duration, to increase orb count
                //And lng < X is maximum number of points this can have
                if (modPlayer.useStyleInt > (lng + 1) * 45 + (lng * 90) && lng < 5)
                {
                    //New Vector2[] to reassign
                    Vector2[] newOrbs = new Vector2[lng + 1];
                    for (int i = 0; i < newOrbs.Length; i++)
                    {
                        //Assert new values and rotations, AND add another value
                        float newRotation = modPlayer.useStyleInt * 0.0349f + ((MathHelper.TwoPi / newOrbs.Length) * i);
                        newOrbs[i] = Vector2.UnitY.RotatedBy(newRotation)*16f;
                    }
                    //Push new array to modplayer
                    modPlayer.useStyleData = newOrbs;
                }
                else if(lng > 0)
                {
                    //New Vector2[] to reassign
                    Vector2[] newOrbs = orbs;
                    for (int i = 0; i < lng; i++)
                    {
                        //Assert new values and rotations
                        float newRotation = modPlayer.useStyleInt * 0.0349f + ((MathHelper.TwoPi/newOrbs.Length) * i);
                        newOrbs[i] = Vector2.UnitY.RotatedBy(newRotation)*16f;
                    }
                    //Push new array to modplayer
                    modPlayer.useStyleData = newOrbs;
                }
            }
            else
            {
                //Reset these when the player stops channeling, so they don't persist
                modPlayer.useStyleInt = 0;
                modPlayer.useStyleData = null;
            }

            float itemAngle = (Main.MouseWorld - player.Center).ToRotation();
            if (player.direction > 0)
                player.itemRotation = itemAngle;
            else
                player.itemRotation = itemAngle + MathHelper.Pi;
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, itemAngle - MathHelper.PiOver2);
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            noHitbox = true;
            hitbox = new Rectangle(0, 0, 0, 0);
        }
        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
            AeroPlayer modPlayer = player.GetModPlayer<AeroPlayer>();
            //No null values please
            if (modPlayer.useStyleData == null)
                modPlayer.useStyleData = new Vector2[0];
            //Get the offset position(s)
            Vector2[] orbs = ((Vector2[])(modPlayer.useStyleData));

            //Just doing this because it's used a ton
            int lng = orbs.Length;

            mult = lng*.5f;
        }
    }
}

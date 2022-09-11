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
using System.Runtime.InteropServices;
using AerovelenceMod.Common.Utilities;
using Terraria.Graphics.Shaders;

namespace AerovelenceMod.Content.Projectiles.Weapons.Magic
{
    public class CarbonCadence_Proj : ModProjectile
    {
        //0 = when spawned
        //1 = floating
        //? = EXPLODE IMMEDIATELY
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Diamond Crystal Shard");
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.alpha = 100;
            Projectile.timeLeft = 600;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.localNPCHitCooldown = 30;
            Projectile.usesLocalNPCImmunity = true;
        }
        public override void AI()
        {
            ShiftAIState(); //Change ai[1] to gradually change ai state using this method
            Color lightColour = Color.Lerp(Color.LightCyan, Color.Cyan * .8f, Projectile.ai[0] * .5f);
            Lighting.AddLight(Projectile.Center, lightColour.ToVector3()); //Gradually get brighter
            Projectile.velocity *= .98f;
            float nearestState = NearestValue(Projectile.ai[0], 0f, 1f, 2f);
            if(nearestState == 0)
            {
                Projectile.rotation = Projectile.velocity.X * .18f;
                if(Projectile.velocity.LengthSquared() <= 2f)
                {
                    Projectile.ai[1] = 1f;
                    Projectile.netUpdate = true;
                }
            }
            else if(nearestState == 1)
            {
                Projectile.velocity.Y = (float)Math.Cos(Main.time * .0333f) * .133f;
                if (Main.rand.NextFloat() < .101f*(350f/(Projectile.timeLeft + 1)))
                {
                    Vector2 spawnPos = Projectile.Center + (Vector2.One.RotatedByRandom(MathHelper.Pi) * Main.rand.NextFloat(9f, 18f));
                    SpawnDust(spawnPos, ModContent.DustType<Dusts.NormalDusts.Crystal>(), (Projectile.Center - spawnPos).SafeNormalize(Vector2.Zero), scale: Main.rand.NextFloat(.85f, 1.1f));
                }
            }
            else //If it's anything else, explode this immediately
            {
                if(Projectile.timeLeft > 3)
                    Projectile.timeLeft = 3;
                Projectile.alpha = 255;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.ai[0] = 2;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
                Projectile.velocity.X = -oldVelocity.X;
            if (Projectile.velocity.Y != oldVelocity.Y)
                Projectile.velocity.Y = -oldVelocity.Y;
            return false;
        }
        public override void Kill(int timeLeft)
        {
            ArmorShaderData dustShader = new ArmorShaderData(new Ref<Effect>(Mod.Assets.Request<Effect>("Effects/GlowDustShader", AssetRequestMode.ImmediateLoad).Value), "ArmorBasic");
            for (float i = 0; i < 10; i++)
            {
                float rand = Main.rand.NextFloat(1.1f, 1.4f);
                if(Main.rand.NextBool(9))
                    GlowDustHelper.DrawGlowDustPerfect(Projectile.Center+new Vector2(-1f, 0), ModContent.DustType<Dusts.GlowDusts.GlowCircleQuadStar>(), Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 4f, Color.LightCyan, Main.rand.NextFloat(.95f, 1.05f), dustShader);
                else
                {
                    Dust d = Dust.NewDustDirect(Projectile.Center + new Vector2(-1f, 0), 1, 1, ModContent.DustType<Dusts.NormalDusts.Crystal>());
                    d.noGravity = true;
                    d.scale = rand;
                    d.velocity = Vector2.One.RotatedByRandom(MathHelper.TwoPi) * d.scale;
                    if (d.scale < 1.13f)
                        i-=.5f;
                }
            }
        }
        private void ShiftAIState()
        {
            if (Projectile.ai[0] == Projectile.ai[1]) return;
            Projectile.ai[0] = MathHelper.Lerp(Projectile.ai[0], Projectile.ai[1], .05f);
            if (Math.Abs(Projectile.ai[0] - Projectile.ai[1]) < .1f)
                Projectile.ai[0] = Projectile.ai[1];
        }
        private float NearestValue(float comparison, params float[] values)
        {
            float diff = float.MaxValue;
            float newVal = comparison;
            for(int i = 0; i < values.Length; i++)
            {
                float compDiff = Math.Abs(comparison - values[i]);
                if (compDiff < diff)
                {
                    diff = compDiff;
                    newVal = values[i];
                }
            }
            return newVal;
        }
        private void SpawnDust(Vector2 position, int type, Vector2 velocity, Color colour = default, float scale = 1f)
        {
            if(Main.rand.NextBool(9))
            {
                ArmorShaderData dustShader = new ArmorShaderData(new Ref<Effect>(Mod.Assets.Request<Effect>("Effects/GlowDustShader", AssetRequestMode.ImmediateLoad).Value), "ArmorBasic");
                GlowDustHelper.DrawGlowDustPerfect(Projectile.Center, ModContent.DustType<Dusts.GlowDusts.GlowCircleQuadStar>(), velocity, Color.Cyan, Main.rand.NextFloat(.95f, 1.05f), dustShader);
            }
            else
            {
                Dust d = Dust.NewDustDirect(position, 1, 1, type, Scale: scale*1.2f);
                d.noGravity = true;
                d.velocity = velocity;
            }
        }
    }
}

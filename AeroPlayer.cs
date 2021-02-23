using AerovelenceMod.Dusts;
using AerovelenceMod.Items.Weapons.Melee;
using AerovelenceMod.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;

namespace AerovelenceMod
{
	public class AeroPlayer : ModPlayer
	{
		public bool SoulFig;
		public bool KnowledgeFruit;
		public bool DevilsBounty;


		public bool ZoneCrystalCaverns;
		public bool ZoneCrystalCitadel;

		public bool SoulFire;
		public bool Electrified;
		public bool badHeal;

		public bool QueensStinger;
		public bool EmeraldEmpoweredGem;
		public bool MidasCrown;

		public bool AdobeHelmet;
		public bool PhanticBonus;
		public bool FrostMelee;
		public bool FrostProjectile;
		public bool FrostMinion;
		public bool BurnshockArmorBonus;
		public bool SpiritCultistBonus = false;

		public bool ShiverMinion;
		public bool NeutronMinion = false;
		public bool StarDrone;
		public bool Minicry = false;


		public override void Initialize()
		{

			

			AdobeHelmet = false;
			PhanticBonus = false;
			SpiritCultistBonus = false;
			FrostMelee = false;
			FrostProjectile = false;
			FrostMinion = false;
			BurnshockArmorBonus = false;

			MidasCrown = false;
			EmeraldEmpoweredGem = false;
			QueensStinger = false;
		}



        public override void UpdateDead()
		{
			SoulFire = false;
			badHeal = false;
			Electrified = false;
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			if (FrostMelee)
			{
				if (Main.rand.NextBool(2)) //  50% chance
					target.AddBuff(BuffID.Frostburn, 120, false);
			}
		}

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
			if (PhanticBonus)
            {
				if (damage > 10)
                {
					Vector2 offset = new Vector2(0, -100);
					Projectile.NewProjectile(player.Center + offset, new Vector2(0 + ((float)Main.rand.Next(20) / 10) - 1, -3 + ((float)Main.rand.Next(20) / 10) - 1), ProjectileType<PhanticSoul>(), 6, 1f, Main.myPlayer);
				}
            }
			if (BurnshockArmorBonus)
			{
				if (damage > 25)
				{
					Vector2 offset = new Vector2(0, -100);
					Projectile.NewProjectile(player.Center + offset, new Vector2(0 + ((float)Main.rand.Next(20) / 10) - 1, -3 + ((float)Main.rand.Next(20) / 10) - 1), ProjectileType<BurnshockCrystal>(), 40, 1f, Main.myPlayer);
					Projectile.NewProjectile(player.Center + offset, new Vector2(0 + ((float)Main.rand.Next(20) / 10) - 1, -3 + ((float)Main.rand.Next(20) / 10) - 1), ProjectileType<BurnshockCrystal>(), 40, 1f, Main.myPlayer);
					Projectile.NewProjectile(player.Center + offset, new Vector2(0 + ((float)Main.rand.Next(20) / 10) - 1, -3 + ((float)Main.rand.Next(20) / 10) - 1), ProjectileType<BurnshockCrystal>(), 40, 1f, Main.myPlayer);
				}
			}
		}

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
		{
			if (PhanticBonus)
			{
				if (damage > 10)
				{
					Vector2 offset = new Vector2(0, -100);
					Projectile.NewProjectile(player.Center + offset, new Vector2(0 + ((float)Main.rand.Next(20) / 10) - 1, -3 + ((float)Main.rand.Next(20) / 10) - 1), ProjectileType<PhanticSoul>(), 6, 1f, Main.myPlayer);
				}
			}
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			if (QueensStinger)
			{
				if (proj.type != 181)
					if (Main.rand.NextBool(10)) //  1 in 10 chance
						Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, ProjectileID.Bee, 3, 2, player.whoAmI);
			}
			if (EmeraldEmpoweredGem)
			{
				target.AddBuff(39, 40, false);
			}
			if (MidasCrown)
			{
				target.AddBuff(BuffID.Midas, 900, false);
			}
			if (FrostProjectile)
			{
				if (Main.rand.NextBool(2)) //  50% chance
				{
					target.AddBuff(BuffID.Frostburn, 120, false);
				}
			}
		
			if (SpiritCultistBonus && proj.magic && !target.boss)
			{
				if (target.FindBuffIndex(mod.BuffType("LiftedSpiritsDebuff")) < 1)
				{
					target.velocity.Y -= 20;
				}
				target.AddBuff(mod.BuffType("LiftedSpiritsDebuff"), 210, false);
			}
		}

		public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			if (SoulFire)
			{
				if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustType<WispDust>(), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default, 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.playerDrawDust.Add(dust);
				}
				r *= 0.1f;
				g *= 0.2f;
				b *= 0.7f;
				fullBright = true;
			}
			if (Electrified)
			{
				if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
				{
					int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.AncientLight, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default, 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					Main.playerDrawDust.Add(dust);
				}
				r *= 0.0f;
				g *= 0.2f;
				b *= 0.7f;
				fullBright = true;
			}
		}


		public override void UpdateBiomes()
		{
			ZoneCrystalCaverns = AeroWorld.cavernTiles > 50;
			ZoneCrystalCitadel = AeroWorld.citadelTiles > 50;
		}

        public override void ResetEffects()
		{
			AdobeHelmet = false;
			ShiverMinion = false;
			FrostProjectile = false;
			FrostMelee = false;
			FrostMinion = false;
			PhanticBonus = false;
			BurnshockArmorBonus = false;
			SpiritCultistBonus = false;
			badHeal = false;
			QueensStinger = false;
			NeutronMinion = false;
			StarDrone = false;
			Minicry = false;
		}

		public static readonly PlayerLayer MiscEffects = new PlayerLayer("AerovelenceMod", "MiscEffects", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawInfo drawInfo)
		{
			if (drawInfo.shadow != 0f)
			{
				return;
			}
			Player drawPlayer = drawInfo.drawPlayer;
			Mod mod = ModLoader.GetMod("AerovelenceMod");
			AeroPlayer modPlayer = drawPlayer.GetModPlayer<AeroPlayer>();
			if (modPlayer.badHeal)
			{
				Texture2D texture = mod.GetTexture("Buffs/SoulFire");
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y - 4f - Main.screenPosition.Y);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y - 4f - texture.Height / 2f) / 16f)), 0f, new Vector2(texture.Width / 2f, texture.Height), 1f, SpriteEffects.None, 0);
				Main.playerDrawData.Add(data);
				for (int k = 0; k < 2; k++)
				{
					int dust = Dust.NewDust(new Vector2(drawInfo.position.X + drawPlayer.width / 2f - texture.Width / 2f, drawInfo.position.Y - 4f - texture.Height), texture.Width, texture.Height, DustType<Smoke>(), 0f, 0f, 0, Color.Black);
					Main.dust[dust].velocity += drawPlayer.velocity * 0.25f;
					Main.playerDrawDust.Add(dust);
				}
			}
			if (modPlayer.Electrified)
			{
				Texture2D texture = mod.GetTexture("Buffs/Electrified");
				int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
				int drawY = (int)(drawInfo.position.Y - 4f - Main.screenPosition.Y);
				DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y - 4f - texture.Height / 2f) / 16f)), 0f, new Vector2(texture.Width / 2f, texture.Height), 1f, SpriteEffects.None, 0);
				Main.playerDrawData.Add(data);
				for (int k = 0; k < 2; k++)
				{
					int dust = Dust.NewDust(new Vector2(drawInfo.position.X + drawPlayer.width / 2f - texture.Width / 2f, drawInfo.position.Y - 4f - texture.Height), texture.Width, texture.Height, DustType<Smoke>(), 0f, 0f, 0, Color.Black);
					Main.dust[dust].velocity += drawPlayer.velocity * 0.25f;
					Main.playerDrawDust.Add(dust);
				}
			}
		});

		public override void CopyCustomBiomesTo(Player other)
		{
			AeroPlayer modOther = other.GetModPlayer<AeroPlayer>();
			modOther.ZoneCrystalCaverns = ZoneCrystalCaverns;
			modOther.ZoneCrystalCitadel = ZoneCrystalCitadel;
		}

		public override void SendCustomBiomes(BinaryWriter writer)
		{
			BitsByte flags = new BitsByte();
			flags[0] = ZoneCrystalCaverns;
			flags[1] = ZoneCrystalCitadel;
			writer.Write(flags);
		}



		public override void ReceiveCustomBiomes(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			ZoneCrystalCaverns = flags[0];
			ZoneCrystalCitadel = flags[1];
		}



		public override bool CustomBiomesMatch(Player other)
		{
			AeroPlayer modOther = other.GetModPlayer<AeroPlayer>();
			return ZoneCrystalCaverns == modOther.ZoneCrystalCaverns && ZoneCrystalCitadel == modOther.ZoneCrystalCitadel;
		}

		public override Texture2D GetMapBackgroundImage()
		{
			if (ZoneCrystalCaverns)
			{
				return mod.GetTexture("CrystalCavernsMapBackground");
			}
			return null;
		}





		public static bool BossPresent => Main.npc.ToList().Any(npc => npc.boss && npc.active);

		public NPC GetFarthestBoss
		{
			get
			{
				//Make sure that when checking for bosses you also make sure they're active
				List<NPC> npcList = Main.npc.ToList();
				List<NPC> bosses = npcList.Where(npc => npc.boss && npc.active).Where(me => Vector2.Distance(me.Center, player.Center) == npcList.Where(npc => npc.boss && npc.active).Max(boss => Vector2.Distance(boss.Center, player.Center)) && me.active).ToList();
				return bosses[0];
			}
		}
	}
}

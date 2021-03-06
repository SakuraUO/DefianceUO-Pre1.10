using System;
using Server;
using Server.Guilds;

namespace Server.Items
{
	public class HeroShield : BaseShield
	{
		public override int BasePhysicalResistance{ get{ return 1; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 0; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 125; } }

		public override int AosStrReq{ get{ return 95; } }

		public override int ArmorBase{ get{ return 45; } }
                public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public HeroShield() : base( 0x1BC4 )
		{

			if ( !Core.AOS )
				LootType = LootType.Blessed;
			Hue = 1154;
			Weight = 5.0;
			Name = "a hero shield";


               }

		public HeroShield( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}


		}

}
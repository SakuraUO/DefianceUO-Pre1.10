using System;
using Server;
using Server.FSPvpPointSystem;

namespace Server.Items
{
	public class BattleHelm : BaseArmor
	{
		public override int OldStrBonus{ get{ return 0; } }
		public override int OldDexBonus{ get{ return 0; } }
		//public override int OldIntBonus{ get{ return 2; } }
		public override int ArmorBase{ get{ return 15; } }
                public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }


		[Constructable]
		public BattleHelm() : base( 0x1718 )
		{
                        Name = "BattleMage Crown [Grand Captain]";
			Weight = 5.0;
                        Hue = 1266;
		}

		public BattleHelm( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

				public override bool OnEquip( Mobile from )
		{
			FSPvpSystem.PvpStats ps = FSPvpSystem.GetPvpStats( from );
			if ( PvpRankInfo.GetInfo( ps.RankType ).Rank > 5 ) // Change Min Rank To Use Here!!!
				return base.OnEquip( from );
			else
			{
				from.SendMessage( "You lack the rank to use this item." );
				from.SendMessage( "The item has been removed." );
				this.Delete();
				return false;
			}
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
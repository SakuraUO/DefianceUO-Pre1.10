/////////////////////////////////////////////////
//                                             //
// Automatically generated by the              //
// AddonGenerator script by Arya               //
//                                             //
/////////////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DwarvenForgeSouth2Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new DwarvenForgeSouth2AddonDeed();
			}
		}

		[ Constructable ]
		public DwarvenForgeSouth2Addon()
		{
			AddonComponent ac;
			ac = new AddonComponent( 6558 );
			ac.Name = "Dwarven Forge";
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 6566 );
			ac.Name = "Dwarven Forge";
			AddComponent( ac, 0, 0, 0 );
		}

		public DwarvenForgeSouth2Addon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class DwarvenForgeSouth2AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new DwarvenForgeSouth2Addon();
			}
		}

		[Constructable]
		public DwarvenForgeSouth2AddonDeed()
		{
			Name = "Dwarven Forge South 2";
		}

		public DwarvenForgeSouth2AddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
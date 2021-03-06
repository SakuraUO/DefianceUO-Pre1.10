/**
*	Ladder system by Morten Legarth (c)taunted.dk ( legarth@taunted.dk )
*	Version: v0.10 -  26-02-2005
*
*	This system has been written for use at the Blitzkrieg frees-shard
*	http://blitzkrieg.dorijan.de . Unauthorized reproduction or redistribution
*	is prohibited.
*
*							ChallengeGump.cs
*						-------------------------
*
*	File Description:	This Gump shows details about a
*						possible challenge. The challenger
*						can choose to confirm the challenge.
*/

using System;
using System.Collections;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;
using Server.Items;

namespace Server.Ladder
{
	public class ChallengeGump : Gump
	{
		private DuelObject duel;
		public ChallengeGump(DuelObject duel) : base(50,50)
		{
			this.duel = duel;
			this.Closable = true;
			this.Disposable = true;
			this.Dragable = true;
			this.Resizable = false;
			this.AddPage(0);
			this.AddBackground(63, 103, 350, 475, 9270);
			this.AddLabel(209, 112, 1259, @"Challenge");
			this.AddLabel(101, 141, 1259, @"You are about to challenge " + duel.Player2.Name + " to a duel.");
			this.AddLabel(104, 170, 1259, @"Opponent:");
			this.AddLabel(244, 170, 1259, @"You:");
			this.AddLabel(104, 195, 1259, @"Honor:");
			this.AddLabel(104, 214, 1259, @"Rank:");
			this.AddLabel(244, 195, 1259, @"Honor:");
			this.AddLabel(244, 215, 1259, @"Rank:");
			this.AddLabel(164, 195, 1149, @"" + ((PlayerMobile)duel.Player2).Honor);
			this.AddLabel(164, 215, 1149, @"" + (Ladder.Players.IndexOf(duel.Player2) + 1));
			this.AddLabel(304, 195, 1149, @"" + ((PlayerMobile)duel.Player1).Honor);
			this.AddLabel(304, 215, 1149, @"" + (Ladder.Players.IndexOf(duel.Player1) + 1));

			this.AddLabel(104, 245, 1259, @"Rules:");
			this.AddLabel(104, 270, 1259, @"Duel Type:");
			this.AddLabel(104, 295, 1259, @"Potions allowed:");
			this.AddLabel(104, 320, 1259, @"Summoning allowed:");
			this.AddLabel(104, 345, 1259, @"Looting allowed:");
			this.AddLabel(104, 370, 1259, @"Poisoned Weapons allowed:");
			this.AddLabel(104, 395, 1259, @"Magic Weapons allowed:");
			this.AddLabel(104, 420, 1259, @"Magery allowed:");
			this.AddLabel(104, 445, 1259, @"Maximum Dexterity:");
			this.AddLabel(104, 470, 1259, @"Wager gold:");

			this.AddLabel(304, 270, 1149, @"" + (duel.DuelType == 1 ? "5x Mage" : duel.DuelType == 2 ? "7x Mage" : "Dex Monkey"));
			this.AddLabel(304, 295, 1149, @"" + (duel.Potions ? "Yes" : "No"));
			this.AddLabel(304, 320, 1149, @"" + (duel.Summoning ? "Yes" : "No"));
			this.AddLabel(304, 345, 1149, @"" + (duel.Looting ? "Yes" : "No"));
			this.AddLabel(304, 370, 1149, @"" + (duel.PoisonedWeapons ? "Yes" : "No"));
			this.AddLabel(304, 395, 1149, @"" + (duel.MagicWeapons ? "Yes" : "No"));
			this.AddLabel(304, 420, 1149, @"" + (duel.Magery ? "Yes" : "No"));
			this.AddLabel(304, 445, 1149, @"" + duel.MaxDex);
			this.AddLabel(304, 470, 1149, @"" + duel.Wager);

			this.AddLabel(123, 500, 1259, @"Are you sure you want to challenge?");
			this.AddButton(151, 525, 247, 248, (int)Buttons.Button1, GumpButtonType.Reply, 0);
			this.AddButton(246, 525, 241, 242, (int)Buttons.Button2, GumpButtonType.Reply, 0);

		}
		public enum Buttons
		{
			Button1 = 1,
			Button2 = 0,
		}

		public override void OnResponse(NetState state, RelayInfo info)
		{
			if (duel == null)
				return;

			if (info.ButtonID == (int)Buttons.Button1)
			{

				if (duel.Player1.BankBox == null || duel.Player1.BankBox.GetAmount(typeof(Gold), true) < duel.Wager)
				{
					duel.Player1.SendMessage("Challenge canceled, you don't have enough gold in your bank box");
				}
				else if (duel.MaxDex < duel.Player1.Dex)
				{
					duel.Player1.SendMessage("Challenge canceled, you have too high dex for this challenge.");
				}
				else
				{

					duel.Player1.SendGump(new WaitGump(duel));
					duel.Player2.SendGump(new ChallengedGump(duel));
				}


			}
			else if (info.ButtonID == (int)Buttons.Button2)
			{
				duel.Player1.SendMessage("The challenge has been canceled.");

			}

		}
	}
}
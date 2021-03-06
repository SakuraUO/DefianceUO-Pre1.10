using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.First
{
	public class FeeblemindSpell : AbilitySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Feeblemind", "Rel Wis",
				SpellCircle.First,
				212,
				9031,
				Reagent.Ginseng,
				Reagent.Nightshade
			);

		public FeeblemindSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m );

				OnHit( Caster, m );

				/*SpellHelper.AddStatCurse( Caster, m, StatType.Int );

				if ( m.Spell != null )
					m.Spell.OnCasterHurt();

				m.Paralyzed = false;

				m.FixedParticles( 0x3779, 10, 15, 5004, EffectLayer.Head );
				m.PlaySound( 0x1E4 );*/
			}

			FinishSequence();
		}

		public override void OnHit( Mobile caster, Mobile target )
		{
			if ( SpellHelper.AddStatCurse( Caster, target, StatType.Int ) )
			{
				target.FixedParticles( 0x3779, 10, 15, 5004, EffectLayer.Head );
				target.PlaySound( 0x1E6 );
			}
			else
			{
				//Mobile temp = Caster; // to make magic weapons fizzle their master
				//Caster = caster;
				//DoHurtFizzle();
				//Caster = temp;
				target.FixedParticles( 0x3779, 10, 15, 5004, EffectLayer.Head );
				target.PlaySound( 0x1E6 );
			}

			if ( target.Spell != null )
				target.Spell.OnCasterHurt();

			target.Paralyzed = false;
		}


		private class InternalTarget : Target
		{
			private FeeblemindSpell m_Owner;

			public InternalTarget( FeeblemindSpell owner ) : base( 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
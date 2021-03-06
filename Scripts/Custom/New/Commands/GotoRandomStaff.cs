/*
 * Copyright (c) 2005, Kai Sassmannshausen <kai@sassie.org>
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 * - Redistributions of source code must retain the above copyright
 * notice, this list of conditions and the following disclaimer.
 *
 * - Redistributions in binary form must reproduce the above copyright
 * notice, this list of conditions and the following disclaimer in the
 * documentation and/or other materials provided with the
 * distribution.
 *
 * - Neither the name of Kai Sassmannshausen nor the names of its contributors
 * may be used to endorse or promote products derived from this software without
 * specific prior written permission.
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
 * CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING,
 * BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
 * FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
 * COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
 * OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 *
 *  Version 1.0
 */

using System;
using System.Collections;
using Server.Network;


namespace Server.Scripts.Commands {

    public class grsCommand {
        private NetState targetInstance = null;
        private ArrayList m_Mobiles;

        public static void Initialize() {
            Server.Commands.Register( "grs", AccessLevel.GameMaster, new CommandEventHandler( grs_OnCommand ) );
	}


        [Usage( "grs" )]
	[Description( "Goto a Random Staffmember (below your accesslevel)" )]
	private static void grs_OnCommand( CommandEventArgs e ) {
	    ArrayList states = NetState.Instances;
	    ArrayList m_Mobiles = new ArrayList();

	    if ( states.Count > 0 ) {
		for ( int i = 0; i < states.Count; ++i ) {
	            Mobile m = ((NetState)states[i]).Mobile;
		    if ( (m != null) && (m.AccessLevel != AccessLevel.Player) && (e.Mobile.AccessLevel > m.AccessLevel))
	                m_Mobiles.Add( m );
		}

		if ( m_Mobiles.Count < 1 )
		{
			e.Mobile.SendMessage("No staffmember online that has a lower accesslevel than you.");
			return;
		}

		int targetID = new Random().Next(0, m_Mobiles.Count);

		if ( (Mobile)m_Mobiles[targetID] != null ) {
	            Mobile target = (Mobile)m_Mobiles[targetID];
		    Point3D location = target.Location;
		    Map map = target.Map;
		    if ( e.Mobile != null && !e.Mobile.Deleted )
		        e.Mobile.MoveToWorld( location, map );
		}
	    }
	}
    }
}
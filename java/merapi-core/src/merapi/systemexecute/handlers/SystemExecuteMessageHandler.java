////////////////////////////////////////////////////////////////////////////////
//
//  This program is free software; you can redistribute it and/or modify 
//  it under the terms of the GNU Lesser General Public License as published 
//  by the Free Software Foundation; either version 3 of the License, or (at 
//  your option) any later version.
//
//  This program is distributed in the hope that it will be useful, but 
//  WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
//  or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public 
//  License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License 
//  along with this program; if not, see <http://www.gnu.org/copyleft/lesser.html>.
//
////////////////////////////////////////////////////////////////////////////////

package merapi.systemexecute.handlers;

import merapi.handlers.MessageHandler;
import merapi.messages.IMessage;
import merapi.systemexecute.messages.SystemExecuteMessage;


public class SystemExecuteMessageHandler extends MessageHandler 
{
    //--------------------------------------------------------------------------
    //
    //  Constructors
    //
    //--------------------------------------------------------------------------
	
	/**
	 *  The default constructor
	 */
	public SystemExecuteMessageHandler()
	{
		super( SystemExecuteMessage.SYSTEM_EXECUTE );
	}
	
	//--------------------------------------------------------------------------
	//
	//  Methods
	//
	//--------------------------------------------------------------------------
	
	/**
	 *  Handles an <code>IMessage</code> dispatched from the Bridge.
	 */
	public void handleMessage( IMessage message ) 
	{
		if ( message instanceof SystemExecuteMessage )
		{
			SystemExecuteMessage sem = (SystemExecuteMessage)message;

			//  Use the args passed in the message to do a shell exec
			try 
			{
				String[] args = sem.getArgs();
				Runtime.getRuntime().exec( args );
			}
			catch ( Exception e )
			{
				System.out.println( SystemExecuteMessageHandler.class );
				e.printStackTrace();
			}
		}		
	}
	
}
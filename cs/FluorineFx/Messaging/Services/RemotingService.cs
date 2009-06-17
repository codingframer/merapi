/*
	FluorineFx open source library 
	Copyright (C) 2007 Zoltan Csibi, zoltan@TheSilentGroup.com, FluorineFx.com 
	
	This library is free software; you can redistribute it and/or
	modify it under the terms of the GNU Lesser General Public
	License as published by the Free Software Foundation; either
	version 2.1 of the License, or (at your option) any later version.
	
	This library is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
	Lesser General Public License for more details.
	
	You should have received a copy of the GNU Lesser General Public
	License along with this library; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
*/

using System;

using FluorineFx.Messaging.Config;
using FluorineFx.Messaging;
using FluorineFx.Messaging.Messages;
using FluorineFx.Messaging.Services.Remoting;

namespace FluorineFx.Messaging.Services
{
	/// <summary>
	/// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
	/// </summary>
	class RemotingService : ServiceBase
	{
		public const string RemotingServiceId = "remoting-service";

		public RemotingService(MessageBroker broker, ServiceSettings settings) : base(broker, settings)
		{
		}

		protected override Destination NewDestination(DestinationSettings destinationSettings)
		{
			RemotingDestination remotingDestination = new RemotingDestination(this, destinationSettings);
			return remotingDestination;
		}

		public override object ServiceMessage(IMessage message)
		{
			RemotingMessage remotingMessage = message as RemotingMessage;
			RemotingDestination destination = GetDestination(message) as RemotingDestination;
			ServiceAdapter adapter = destination.ServiceAdapter;
			object result = adapter.Invoke(message);
			return result;
		}

	}
}

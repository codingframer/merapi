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
using System.Collections;
using FluorineFx.Messaging.Messages;
using FluorineFx.Context;

namespace FluorineFx.Messaging.Api
{
	/// <summary>
    /// The client object represents a single client. One client may have multiple connections to different scopes on the same host.
	/// </summary>
	public interface IClient : IAttributeStore
	{
        /// <summary>
        /// Gets the client identity.
        /// </summary>
        /// <remarks>
        /// This will be generated by the server if not passed upon connection from client-side Flex/Flash app.
        /// </remarks>
		string Id{ get; }
        /// <summary>
        /// Get a set of scopes the client is connected to.
        /// </summary>
		ICollection Scopes{ get; }
        /// <summary>
        /// Get a set of connections of a given scope.
        /// </summary>
		ICollection Connections{ get; }
        /// <summary>
        /// Closes all the connections.
        /// </summary>
		void Disconnect();
        /// <summary>
        /// This method supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        void Timeout();
        /// <summary>
        /// Gets an object that can be used to synchronize access. 
        /// </summary>
        object SyncRoot { get; }
        /// <summary>
        /// Gets pending messages.
        /// </summary>
        /// <param name="waitIntervalMillis"></param>
        /// <returns></returns>
        IMessage[] GetPendingMessages(int waitIntervalMillis);
        /// <summary>
        /// This method supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="messageClient"></param>
        void RegisterMessageClient(IMessageClient messageClient);
        /// <summary>
        /// This method supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="messageClient"></param>
        void UnregisterMessageClient(IMessageClient messageClient);
        /// <summary>
        /// Adds a session destroy listener that will be notified when the session is destroyed.
        /// </summary>
        /// <param name="listener">The listener to add.</param>
        void AddSessionDestroyedListener(ISessionListener listener);
        /// <summary>
        /// Removes a session destroy listener.
        /// </summary>
        /// <param name="listener">The listener to remove.</param>
        void RemoveSessionDestroyedListener(ISessionListener listener);
        /// <summary>
        /// Renews a lease.
        /// </summary>
        void Renew();
        /// <summary>
        /// Renews a lease.
        /// </summary>
        /// <param name="clientLeaseTime">The amount of time in minutes before client times out.</param>
        void Renew(int clientLeaseTime);
        /// <summary>
        /// This method supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        int ClientLeaseTime { get; }
        /// <summary>
        /// This method supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="connection"></param>
        void Register(IConnection connection);
        /// <summary>
        /// This method supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="connection"></param>
        void Unregister(IConnection connection);
	}
}

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
using System.Reflection;

using FluorineFx.Context;

namespace FluorineFx.Messaging
{
	/// <summary>
	/// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
	/// </summary>
	public class DotNetFactoryInstance : FactoryInstance
	{
		Type _cachedType;
		object _applicationInstance;

        /// <summary>
        /// Initializes a new instance of the DotNetFactoryInstance class.
        /// </summary>
        /// <param name="flexFactory">The IFlexFactory this FactoryInstance is created from.</param>
        /// <param name="id">The Destination identity.</param>
        /// <param name="properties">The configuration properties for this destination.</param>
        public DotNetFactoryInstance(IFlexFactory flexFactory, string id, Hashtable properties)
            : base(flexFactory, id, properties)
		{
		}
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns>Instance of the given factory.</returns>
		public object CreateInstance()
		{
			Type type = GetInstanceClass();
			object instance = null;
            if (type == null)
            {
                string msg = __Res.GetString(__Res.Type_InitError, this.Source);
                throw new MessageException(msg, new TypeLoadException(msg));
            }

			if (type.IsAbstract && type.IsSealed)
			{
				instance = type;
			}
			else
			{
				instance = Activator.CreateInstance(type, BindingFlags.CreateInstance|BindingFlags.Public|BindingFlags.Instance|BindingFlags.Static, null, new object[]{}, null);
			}
			return instance;
		}
        /// <summary>
        /// Gets or sets the FactoryInstance source.
        /// </summary>
        public override string Source
		{
			get
			{
				return base.Source;
			}
			set
			{
				if(base.Source != value)
				{
					base.Source = value;
					_cachedType = null;
				}
			}
		}
        /// <summary>
        /// Returns the class for the underlying configuration (used when the lookup method is called). 
        /// </summary>
        /// <returns>A Type instance.</returns>
        public override Type GetInstanceClass()
		{
			if( _cachedType == null )
				_cachedType = ObjectFactory.LocateInLac(this.Source);
			return _cachedType;
		}
        /// <summary>
        /// Gets the application-scoped instance.
        /// </summary>
		public object ApplicationInstance
		{
			get
			{
				if( _applicationInstance == null )
				{
					lock(typeof(DotNetFactoryInstance))
					{
						if( _applicationInstance == null )
							_applicationInstance = CreateInstance();
					}
				}
				return _applicationInstance;
			}
		}
	}
}

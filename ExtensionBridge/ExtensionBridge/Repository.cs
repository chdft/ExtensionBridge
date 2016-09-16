﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	public class Repository
	{
		public Repository() { }

		/// <summary>
		/// collection of sources used for getting the extension implementations
		/// </summary>
		/// <remarks>
		/// This reference is guaranteed to not change during the lifetime of this object.
		/// </remarks>
		public IList<ISource> Sources
		{
			get { return _Sources; }
		}
		private IList<ISource> _Sources = new List<ISource>();

		/// <summary>
		/// extensions for the contract <typeparamref name="TContract"/>
		/// </summary>
		/// <typeparam name="TContract">contract the extension must implement</typeparam>
		/// <returns>collection of matching extension</returns>
		public IEnumerable<Extension<TContract>> GetExtensions<TContract>() where TContract : class
		{
			foreach (var source in Sources)
			{
				foreach (var assembly in source.GetAssemblies())
				{
					foreach (Type type in assembly.GetTypes())
					{
						//check first, if the contract is implemented at all; otherwise the search for an extension-attribute isn't nessecary
						//check ContainsGenericParameters, because generic types with unspecified type parameters cannot be instatiated...
						if (type.ImplementsContract<TContract>() && type.DeclaresContract<TContract>() && !type.ContainsGenericParameters)
						{
							yield return new Extension<TContract>(type, source);
							break; //return each type only once
						}
					}
				}
			}
		}
	}
}

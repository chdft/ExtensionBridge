using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionBridge
{
	internal static class TypeHelpers
	{
		/// <summary>
		/// checks if a type implements a contract
		/// </summary>
		/// <typeparam name="TContract">contract interface for which to check</typeparam>
		/// <param name="potentialExtensionType">type which will be checked</param>
		/// <returns>true, if the type implements the contract; false otherwise</returns>
		public static bool ImplementsContract<TContract>(this Type potentialExtensionType) where TContract : class
		{
			return typeof(TContract).IsAssignableFrom(potentialExtensionType);
		}

		/// <summary>
		/// checks if a type declares a contract
		/// </summary>
		/// <typeparam name="TContract">contract interface for which to check</typeparam>
		/// <param name="potentialExtensionType">type which will be checked</param>
		/// <returns>true, if the type declares the contract; false otherwise</returns>
		public static bool DeclaresContract<TContract>(this Type potentialExtensionType) where TContract : class
		{
			foreach (var extensionAttribute in potentialExtensionType.GetCustomAttributes(typeof(ExtensionAttribute)).Cast<ExtensionAttribute>())
			{
				if (extensionAttribute.Contract == typeof(TContract))
				{
					//matching contract
					return true;
				}
			}
			return false;
		}

		public static bool IsContract(this Type potentialContractType)
		{
			return potentialContractType.GetCustomAttributes<ExtensionContractAttribute>().Any();
		}
	}
}

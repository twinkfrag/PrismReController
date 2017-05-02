using System.Collections.Generic;

namespace PrismReController.Shared.Utils
{
	public static class EnumerableEx
	{
		/// <summary>
		/// syntax sugar to <see cref="string.Join"/> with separator ",".
		/// </summary>
		public static string JoinToString<T>(this IEnumerable<T> e)
			=> string.Join(",", e);

		/// <summary>
		/// syntax sugar to <see cref="string.Join"/>
		/// </summary>
		public static string JoinToString<T>(this IEnumerable<T> e, string separator)
			=> string.Join(separator, e);

		/// <summary>
		/// syntax sugar to <see cref="string.Join"/> with separator ",".
		/// </summary>
		public static string JoinToString(this IEnumerable<string> e)
			=> string.Join(",", e);

		/// <summary>
		/// syntax sugar to <see cref="string.Join"/>
		/// </summary>
		public static string JoinToString(this IEnumerable<string> e, string separator)
			=> string.Join(separator, e);

	}
}

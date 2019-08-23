using System;
using System.Collections.Generic;
using System.Text;

namespace NESTExportaDB
{
	public class Coluna
	{
		#region Constructor

		/// <summary>
		/// Metadata for a column in a database table.
		/// </summary>
		public Coluna()
		{
		}

		#endregion

		#region Public-Members

		/// <summary>
		/// The name of the column.
		/// </summary>
		public string Name;

		/// <summary>
		/// Whether or not the column is the table's primary key.
		/// </summary>
		public bool IsPrimaryKey;

		/// <summary>
		/// The data type of the column.
		/// </summary>
		public string DataType;

		/// <summary>
		/// The maximum character length of the data contained within the column.
		/// </summary>
		public int? MaxLength;

		/// <summary>
		/// Whether or not the column can contain NULL.
		/// </summary>
		public bool Nullable;

		/// <summary>
		/// Whether or not the column can contain a Default Value.
		/// </summary>
		public string Default;

		#endregion

		#region Private-Members

		#endregion

		#region Public-Methods

		#endregion

		#region Private-Methods

		#endregion
	}
}


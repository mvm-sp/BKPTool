using System;
using System.Collections.Generic;
using System.Text;

namespace NESTExportaDB
{
	class Procedure
	{
		#region public_members
		public string Name { get; set; }
		public string Content { get; set; }
		#endregion
		#region Constructors
		public Procedure(string name, string content)
		{
			Name = name;
			Content = content;
		}
		#endregion
	}
}

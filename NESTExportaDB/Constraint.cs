using System;
using System.Collections.Generic;
using System.Text;

namespace NESTExportaDB
{
	public class Constraint
	{
		#region public_members
		public string Name { get; set; }
		public string ForeignTable { get; set; }
		public string ForeignColumns { get; set; }
		public string LocalColumns { get; set; }
		public string Type { get; set; }


		#endregion
	}
}

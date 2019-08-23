using System;
using System.Collections.Generic;
using System.Text;

namespace NESTExportaDB
{
	public class Tabela
	{

		#region private_members
		private List<Coluna> columns;
		private List<Constraint> constraints;
		#endregion
		#region public_members
		public string Name { get; set; }

		public List<Coluna> Colunas
		{
			get { return columns; }
			set { columns = value; }
		}

		public List<Constraint> Referencias
		{
			get { return constraints; }
			set { constraints = value; }
		}
		#endregion

		#region Constructors
		public Tabela(string name)
		{
			Name = name;
			columns = new List<Coluna>();
			constraints = new List<Constraint>();
		}
		public Tabela(string name, List<Coluna> colunas)
		{
			Name = name;
			columns = colunas;
			constraints = new List<Constraint>();
		}
		public Tabela(string name, List<Coluna> colunas, List<Constraint> referencias)
		{
			Name = name;
			columns = colunas;
			constraints = referencias;
		}
		#endregion
		#region private methods
		private string NomeChavePrimaria()
		{
			string sRet = "";
			foreach (Constraint mCons in constraints)
			{
				if (mCons.Type == "PRIMARY KEY")
				{
					sRet = mCons.Name;
					break;
				}

			}
			return sRet;
		}
		#endregion
		#region public methods
		public string DefinicaoTabela(bool pIncluiDrop)
		{
			StringBuilder mBuilder = new StringBuilder();
			string mKey = "";
			string mDefault = "";
			if (pIncluiDrop)
			{
				mBuilder.AppendLine("DROP TABLE " + Name + ";\n");
			}
			else
			{
				mBuilder.AppendLine("--DROP TABLE " + Name + ";\n");
			}

			mBuilder.AppendLine("CREATE TABLE " + Name + "(");
			foreach (Coluna mColuna in columns)
			{
				mDefault = "";
				if (mColuna.IsPrimaryKey)
				{
					mKey = mKey + (string.IsNullOrEmpty(mKey) ? "" : ", ") + mColuna.Name;
				}
				if (!string.IsNullOrEmpty(mColuna.Default))
				{
					mDefault = " DEFAULT " + mColuna.Default;
				}
				mBuilder.AppendLine("\t\t" + mColuna.Name + "\t\t\t" + mColuna.DataType + (mColuna.Nullable ? "\t" : "\tNOT ") + "NULL" + mDefault + ",");
			}

			if (!string.IsNullOrEmpty(mKey))
			{
				mBuilder.AppendLine("CONSTRAINT " + NomeChavePrimaria() + " PRIMARY KEY(" + mKey + ")");
			}
			else
			{
				mBuilder.Remove(mBuilder.Length - 3, 1);
			}
			mBuilder.AppendLine(");");
			return mBuilder.ToString();

			/*
			
			*/
		}

		public string ChavesTabela(bool pIncluiPK)
		{
			StringBuilder mBuilder = new StringBuilder();
			string mNome = constraints.Count > 0 ? constraints[0].Name : "";
			string mCamposExt = "";
			string mCamposInt = "";
			string mType = "";
			string mTabela = "";
			foreach (Constraint mCons in constraints)
			{

				/*
				*/
				if (mNome != mCons.Name)
				{
					mBuilder.AppendLine("\n--ADICIONA CONSTRAINTS DA TABELA " + Name + (mType == "PRIMARY KEY" ? "(" + mType + ")" : "  RELACIONADO A TABELA " + mTabela + "(" + mCamposExt + ")"));
					if (mType != "PRIMARY KEY")
					{
						mBuilder.AppendLine("ALTER TABLE " + Name + "\n ADD CONSTRAINT " + mNome + " " + mType + "(" + mCamposInt + ")" + "  REFERENCES " + mTabela + "(" + mCamposExt + ");");
					}
					else if (pIncluiPK)
					{
						mBuilder.AppendLine("ALTER TABLE " + Name + "\n ADD CONSTRAINT " + mNome + " PRIMARY KEY(" + mCamposInt + ");");

					}
					mNome = mCons.Name;
					mCamposExt = "";
					mCamposInt = "";
				}
				mCamposExt = mCamposExt + (string.IsNullOrEmpty(mCamposExt) ? "" : ", ") + mCons.ForeignColumns;
				mCamposInt = mCamposInt + (string.IsNullOrEmpty(mCamposInt) ? "" : ", ") + mCons.LocalColumns;
				mType = mCons.Type;
				mTabela = mCons.ForeignTable;

			}
			// ADD CONSTRAINT  ()  REFERENCES ();

			mBuilder.AppendLine("\n--ADICIONA CONSTRAINTS DA TABELA " + Name + (mType == "PRIMARY KEY" ? "(" + mType + ")" : "  RELACIONADO A TABELA " + mTabela + "(" + mCamposExt + ")"));
			if (mType != "PRIMARY KEY" && !string.IsNullOrEmpty(mType))
			{
				mBuilder.AppendLine("ALTER TABLE " + Name + "\n ADD CONSTRAINT " + mNome + " " + mType + "(" + mCamposInt + ")" + "  REFERENCES " + mTabela + "(" + mCamposExt + ");");
			}
			else if (pIncluiPK && !string.IsNullOrEmpty(mCamposInt))
			{
				mBuilder.AppendLine("ALTER TABLE " + Name + "\n ADD CONSTRAINT " + mNome + " PRIMARY KEY(" + mCamposInt + ");");
			}

			return mBuilder.ToString();
		}
		#endregion
	}
}

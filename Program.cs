using System;
using NESTExportaDB;

namespace BKPNest
{
	class Program
	{
		static ClienteBanco client;
		static void Main(string[] args)
		{
			string line;
			string[] mParam;
			System.IO.StreamReader file =
			new System.IO.StreamReader(@"Parameters\NESTIndicadores.app");
			line = file.ReadLine();
			mParam = line.Split('|');
			file.Close();

			string mServe = mParam[0];
			int mPort = int.Parse(mParam[1]);
			string mUser = mParam[2];
			string mPass = mParam[3];
			string mDBName = mParam[4];
			string mDir = mParam[5];

			//client = new ClienteBanco(DbTypes.PgSql, "201.20.7.33", 5890, "postgres", "TjYz3m", null, "DES_NESTRIS");
			client = new ClienteBanco(DbTypes.PgSql, mServe, mPort, mUser, mPass, null, mDBName);
			string mScript = client.ScriptBanco();
			string mProcedure = client.ScriptProcedures();
			string mFileScript = string.Format(@mDir + "{0}_{1}_{2}.sql", "DB", mDBName.ToUpper(), DateTime.Now.ToString("yyyyMMddHHmmss"));
			string mFileProc = string.Format(@mDir + "{0}_{1}_{2}.sql", "PROC", mDBName.ToUpper(), DateTime.Now.ToString("yyyyMMddHHmmss"));
			System.IO.File.WriteAllText(mFileScript, mScript);
			System.IO.File.WriteAllText(mFileProc, mProcedure);

		}
	}
}

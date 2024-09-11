using Microsoft.SqlServer.Management.Smo;
using Microsoft.Win32;

namespace DemoApp.Utilities.SqlServerManagement
{
    public static class SqlServerManagement
    {
        #region Servers
        /*https://www.codeproject.com/Questions/1171012/Get-local-SQL-server-instances-using-Csharp*/
        /*
         YIELD - When you use the yield keyword in a statement, you indicate that the method, operator, or get accessor in 
         which it appears is an iterator.Using yield to define an iterator removes the need for an explicit extra class 
         (the class that holds the state for an enumeration, see IEnumerator<T> for an example) when you implement the
         IEnumerable and IEnumerator pattern for a custom collection type.
         */
        public static IEnumerable<string> GetServerList()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                using (var hive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    foreach (var item in GetServerList(hive))
                    {
                        yield return item;
                    }
                }

                using (var hive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
                {
                    foreach (var item in GetServerList(hive))
                    {
                        yield return item;
                    }
                }
            }
            else
            {
                foreach (var item in GetServerList(Registry.LocalMachine))
                {
                    yield return item;
                }
            }
        }
        private static IEnumerable<string> GetServerList(RegistryKey hive)
        {
            const string keyName = @"Software\Microsoft\Microsoft SQL Server";
            const string valueName = "InstalledInstances";
            const string defaultName = "MSSQLSERVER";

            using (var key = hive.OpenSubKey(keyName, false))
            {
                if (key == null) return Enumerable.Empty<string>();

                var value = key.GetValue(valueName) as string[];
                if (value == null) return Enumerable.Empty<string>();

                for (var index = 0; index < value.Length; index++)
                {
                    if (string.Equals(value[index], defaultName, StringComparison.OrdinalIgnoreCase))
                    {
                        value[index] = ".";
                    }
                    else
                    {
                        value[index] = @".\" + value[index];
                    }
                }

                return value;
            }
        }
        #endregion

        #region Databases
        public static List<string> GetDatabaseList(string serverName)
        {
            //try/catch
            var server = new Server(serverName);
            var databases = new List<string>();
            foreach (Database db in server.Databases)
            {
                databases.Add(db.Name);
            }
            return databases;
        }
        #endregion

        #region Tables
        public static List<Table> GetTableList(string serverName, string databaseName)
        {
            var server = new Server(serverName);
            var database = server.Databases[databaseName];
            var tables = new List<Table>();
            foreach (Table table in database.Tables)
            {
                if (!table.Name.Contains("_History") && table.Schema == "dbo")
                {
                    tables.Add(table);
                }
            }
            return tables;
        }
        #endregion

        #region Views
        public static List<View> GetViewlist(string serverName, string databaseName)
        {
            var server = new Server(serverName);
            var database = server.Databases[databaseName];
            var views = new List<View>();
            foreach (View view in database.Views)
            {
                views.Add(view);
            }
            return views;
        }
        #endregion

        #region Columns
        public static List<Column> GetColumnList(Table? table = null, View? view = null)
        {
            var columns = new List<Column>();
            if (table != null)
            {
                foreach (Column column in table.Columns)
                {
                    columns.Add(column);
                }
            }

            if (view != null)
            {
                foreach (Column column in view.Columns)
                {
                    columns.Add(column);
                }
            }

            return columns;
        }
        #endregion
    }
}

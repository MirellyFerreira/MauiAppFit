using System.Runtime;
using MauiAppFit.Helpers;


namespace MauiAppFit
{
    public partial class App : Application
    {
        static SQLiteDataBaseHelper database;

        public static SQLiteDataBaseHelper Database;
        {
            get
            {
                if (database == null)
                {
                    database = new SQLiteDataBaseHelper(
                        Patch.Combine(Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData), "XamAppFit.db3"));
                }

                return database;
            }
        }//Fecha Database...





        
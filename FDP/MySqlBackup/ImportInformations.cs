using System;
using System.Collections.Generic;
using System.Text;

namespace MySql.Data.MySqlClient
{
    /// <summary>
    /// Informations and Settings of MySQL Database Import Process
    /// </summary>
    public class ImportInformations
    {
        public enum CharSet
        {
            armscii8,
            ascii,
            big5,
            binary,
            cp1250,
            cp1251,
            cp1256,
            cp1257,
            cp850,
            cp852,
            cp866,
            cp932,
            dec8,
            eucjpms,
            euckr,
            gb2312,
            gbk,
            geostd8,
            greek,
            hebrew,
            hp8,
            keybcs2,
            koi8r,
            koi8u,
            latin1,
            latin2,
            latin5,
            latin7,
            macce,
            macroman,
            sjis,
            swe7,
            tis620,
            ucs2,
            ujis,
            utf8
        }

        private string _encryptionKey = "1234";
        private int _saltSize = 0;
        private string _database = "";
        private string _databaseCharSet = "";

        /// <summary>
        /// Gets or Sets a value indicates whether the Import process should run in Asynchronous Mode.
        /// </summary>
        public bool AsynchronousMode = false;

        /// <summary>
        /// Gets or Sets a value indicates whether the MySqlConnection and MySqlCommand used should close and dispose after import process finished.
        /// </summary>
        public bool AutoCloseConnection = true;

        /// <summary>
        /// Gets or Sets a value indicates whether the Imported Dump File is encrypted.
        /// </summary>
        public bool EnableEncryption = false;

        /// <summary>
        /// Gets or Sets the key or password used to decrypt the exported dump file.
        /// </summary>
        public string EncryptionKey
        {
            get
            {
                return _encryptionKey;
            }
            set
            {
                Methods methods = new Methods();
                _encryptionKey = methods.Sha2Hash(value);
                _saltSize = methods.GetSaltSize(_encryptionKey);
                methods = null;
            }
        }

        /// <summary>
        /// Gets the lenght of salt used in encryption.
        /// </summary>
        public int SaltSize
        {
            get
            {
                return _saltSize;
            }
        }

        /// <summary>
        /// Gets or Sets the full path and file name (dump file) that will be imported.
        /// </summary>
        public string FileName = "";

        /// <summary>
        /// Set the target database name that will be imported to and the default character set. If the database if not exists, it will be created. Warning: Select the exact charset as the source database, or else you might experience encoding problem while handling with unicode (non-latin) characters in future.
        /// </summary>
        /// <param name="databaseName">The name of database</param>
        /// <param name="defaultCharSet">The database's default character set</param>
        public void SetTargetDatabase(string databaseName, string defaultCharSet)
        {
            _database = databaseName;
            _databaseCharSet = defaultCharSet;
        }

        /// <summary>
        ///  Set the target database name that will be imported to. If the database if not exists, it will be created. The default character set of current connecting MySQL server will be used as default character set for this new target database.
        /// </summary>
        /// <param name="databaseName">The name of database</param>
        public void SetTargetDatabase(string databaseName)
        {
            _database = databaseName;
            _databaseCharSet = "";
        }

        /// <summary>
        /// Set the target database name that will be imported to and the default character set. If the database if not exists, it will be created. Warning: Select the exact charset as the source database, or else you might experience encoding problem while handling with unicode (non-latin) characters in future.
        /// </summary>
        /// <param name="databaseName">The name of database</param>
        /// <param name="charSet">The database's default character set</param>
        public void SetTargetDatabase(string databaseName, CharSet charSet)
        {
            SetTargetDatabase(databaseName, charSet.ToString());
        }

        /// <summary>
        /// Gets the CREATE DATABASE SQL statement of current database.
        /// </summary>
        public string CreateTargetDatabaseSql
        {
            get
            {
                if (_database != null && _database != "" && _databaseCharSet != null && _databaseCharSet != "")
                    return string.Format("CREATE DATABASE IF NOT EXISTS `{0}` DEFAULT CHARACTER SET {1};", _database, _databaseCharSet);
                else if (_database != null & _database != "")
                    return string.Format("CREATE DATABASE IF NOT EXISTS `{0}`;", _database);
                else
                    return "";
            }
        }

        /// <summary>
        /// Gets the name of target database.
        /// </summary>
        public string TargetDatabase
        {
            get
            {
                return _database;
            }
        }

        /// <summary>
        /// Gets or Sets a value indicates whether SQL errors occus in import process should be ignored. If true, all errors (exceptions) will be collected in ImportCompleteArg.Errors.
        /// </summary>
        public bool IgnoreSqlError = false;

        /// <summary>
        /// Gets or Sets the Collection of Informations about the completed import process.
        /// </summary>
        public ImportCompleteArg CompleteArg = null;

        public ImportInformations()
        { }
    }
}

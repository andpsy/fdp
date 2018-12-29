// The authors disclaims copyright of this project. Use at your own risk.
// For bugs report, feature request, discussions, supports, please visit:
// http://mysqlbackupnet.codeplex.com/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace MySql.Data.MySqlClient
{
    public class Methods
    {
        public string GetCreateTableSql(string table, MySqlCommand cmd)
        {
            cmd.CommandText = "SHOW CREATE TABLE `" + table + "`;";
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da = null;

            string createSql = "";
            object ob = dt.Rows[0][1];

            if (ob is System.String)
            {
                createSql = ob + "";
            }
            // It has been reported that, in some unknown cases, the query will return
            // byte array.
            // Report: http://www.codeproject.com/Messages/4450086/Small-changes-in-Code.aspx
            else if (ob is System.Byte[])
            {
                System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                createSql = enc.GetString((byte[])ob);
            }

            return createSql.Replace("CREATE TABLE", "CREATE TABLE IF NOT EXISTS").Replace("\n", "\r\n") + ";";

        }

        public string GetCreateDatabaseSql(MySqlCommand cmd)
        {
            cmd.CommandText = "SELECT DATABASE();";
            string databaseName = cmd.ExecuteScalar().ToString();
            cmd.CommandText = "SHOW CREATE DATABASE `" + databaseName + "`;";
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da = null;
            return dt.Rows[0][1].ToString().Replace("CREATE DATABASE", "CREATE DATABASE IF NOT EXISTS") + ";";
        }

        public Dictionary<string, long> GetTotalRowsInTables(string[] tableNames, ref MySqlCommand cmd)
        {
            var dic = new Dictionary<string, long>();
            foreach (string s in tableNames)
            {
                dic.Add(s, GetTotalRowsInTable(s, cmd));
            }
            return dic;
        }

        public long GetTotalRowsInTable(string tableName, MySqlCommand cmd)
        {
            cmd.CommandText = "SELECT COUNT(*) FROM `" + tableName + "`;";
            return (long)cmd.ExecuteScalar();
        }

        public string[] GetColumnNames(string table, MySqlCommand cmd)
        {
            cmd.CommandText = "SHOW COLUMNS FROM `" + table + "`;";
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da = null;
            string[] sa = new string[dt.Rows.Count];
            int count = -1;
            foreach (DataRow dr in dt.Rows)
            {
                count += 1;

                sa[count] = dr[0].ToString();
            }
            return sa;
        }

        public string[] GetTableNames(ref MySqlCommand cmd)
        {
            cmd.CommandText = "SHOW FULL TABLES WHERE Table_type LIKE 'BASE TABLE';";
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da = null;
            string[] sa = new string[dt.Rows.Count];
            int count = -1;

            foreach (DataRow dr in dt.Rows)
            {
                count += 1;
                sa[count] = dr[0] + "";
            }

            return sa;
        }

        public string GetServerVersion(ref MySqlCommand cmd, ref string version)
        {
            cmd.CommandText = "SHOW variables WHERE Variable_name = 'version';";
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            version = dt.Rows[0][1].ToString();
            cmd.CommandText = "SHOW variables WHERE Variable_name = 'version_comment';";
            da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            return version + " " + dt.Rows[0][1].ToString();
        }

        public long GetServerMaxAllowedPacket(ref MySqlCommand cmd)
        {
            cmd.CommandText = "SHOW variables WHERE Variable_name = 'max_allowed_packet';";
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return Convert.ToInt64(dt.Rows[0][1]);
        }

        public string GetDatabaseName(string ConnectionString)
        {
            string[] sa = ConnectionString.Split(';');
            foreach (string s in sa)
            {
                if (s.ToLower().StartsWith("database"))
                {
                    string[] sb = s.Split('=');
                    return sb[1];
                }
                if (s.ToLower().StartsWith("initial catalog"))
                {
                    string[] sb = s.Split('=');
                    return sb[1];
                }
            }
            throw new Exception("Database Name is not detected in Connection String.");
        }

        public string GetBLOBSqlDataStringFromBytes(byte[] ba)
        {
            // Method 1 (slower)
            //return "0x"+ BitConverter.ToString(bytes).Replace("-", string.Empty);

            // Method 2 (faster)
            char[] c = new char[ba.Length * 2 + 2];
            byte b;
            c[0] = '0'; c[1] = 'x';
            for (int y = 0, x = 2; y < ba.Length; ++y, ++x)
            {
                b = ((byte)(ba[y] >> 4));
                c[x] = (char)(b > 9 ? b + 0x37 : b + 0x30);
                b = ((byte)(ba[y] & 0xF));
                c[++x] = (char)(b > 9 ? b + 0x37 : b + 0x30);
            }
            return new string(c);
        }

        public string EncryptWithSalt(string input, string key, int saltSize)
        {
            string salt = RandomString(saltSize);
            return salt + AES_Encrypt(input, key + salt);
        }

        public string DecryptWithSalt(string input, string key, int saltSize)
        {
            try
            {
                string salt = input.Substring(0, saltSize);
                string data = input.Substring(saltSize);
                return AES_Decrypt(data, key + salt);
            }
            catch
            {
                string a = input;
                throw new Exception("Invalid Key.");
            }
        }

        public Random random = new Random((int)DateTime.Now.Ticks);

        public string RandomString(int size)
        {
            StringBuilder sb = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                sb.Append(ch);
            }

            return AES_Encrypt(sb.ToString(), random.Next(1, 1000000).ToString()).Substring(0, size);
        }

        public int GetSaltSize(string key)
        {
            int a = key.GetHashCode();
            string b = Convert.ToString(a);
            char[] ca = b.ToCharArray();
            int c = 0;
            foreach (char cc in ca)
            {
                if (char.IsNumber(cc))
                    c += Convert.ToInt32(cc.ToString());
            }
            return c;
        }

        public string Sha1Hash(string input)
        {
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            byte[] ba = Encoding.UTF8.GetBytes(input);
            byte[] ba2 = sha.ComputeHash(ba);
            return BitConverter.ToString(ba2).Replace("-", string.Empty).ToLower();
        }

        public string Sha2Hash(string input)
        {
            byte[] ba = Encoding.UTF8.GetBytes(input);
            return Sha2Hash(ba);
        }

        public string Sha2Hash(byte[] ba)
        {
            SHA256Managed sha2 = new SHA256Managed();
            byte[] ba2 = sha2.ComputeHash(ba);
            return BitConverter.ToString(ba2).Replace("-", string.Empty).ToLower();
        }

        public string AES_Encrypt(string input, string password)
        {
            byte[] clearBytes = System.Text.Encoding.UTF8.GetBytes(input);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            byte[] encryptedData = AES_Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return Convert.ToBase64String(encryptedData);
        }

        public string AES_Decrypt(string input, string password)
        {
            byte[] cipherBytes = Convert.FromBase64String(input);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 
            0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            byte[] decryptedData = AES_Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return System.Text.Encoding.UTF8.GetString(decryptedData);
        }

        private static byte[] AES_Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearData, 0, clearData.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }

        private static byte[] AES_Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }
    }
}

using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Resources;
using System.IO;
using System.Diagnostics;
using SmartAdmin.SessionStateProvider.Properties;

namespace SmartAdmin.SessionStateProvider.Common
{
  /// <summary>
  /// 
  /// </summary>
  public static class SchemaManager
  {
    private const int schemaVersion = 8;

    /// <summary>
    /// Gets the most recent version of the schema.
    /// </summary>
    /// <value>The most recent version number of the schema.</value>
    public static int Version
    {
      get { return schemaVersion; }
    }

    internal static void CheckSchema(string connectionString, NameValueCollection config)
    {
      int ver = GetSchemaVersion(connectionString);
      if (ver == Version) return;

      try
      {
        if (String.Compare(config["autogenerateschema"], "true", true) == 0)
          UpgradeToCurrent(connectionString, ver);
        else
          throw new ProviderException(Resources.MissingOrWrongSchema);

      }
      catch (Exception ex)
      {
        throw new ProviderException(Resources.MissingOrWrongSchema, ex);
      }
    }

    private static void UpgradeToCurrent(string connectionString, int version)
    {
      //ResourceManager r = new ResourceManager("MySql.Web.Properties.Resources", typeof(SchemaManager).Assembly);
        ResourceManager r = new ResourceManager("SmartAdmin.SessionStateProvider.Properties.Resources", typeof(SchemaManager).Assembly);

      if (version == Version) return;

      using (MySqlConnection connection = new MySqlConnection(connectionString))
      {
        connection.Open();

        for (int ver = version + 1; ver <= Version; ver++)
        {
          string schema = r.GetString(String.Format("schema{0}", ver));
          MySqlScript script = new MySqlScript(connection);
          script.Query = schema;

          try
          {
            script.Execute();
          }
          catch (MySqlException ex)
          {
            if (ex.Number == 1050 && ver == 7)
            {
              // Schema7 performs several renames of tables to their lowercase representation. 
              // If the current server OS does not support renaming to lowercase, then let's just continue.             
                script.Query = "UPDATE MY_ASPNET_SCHEMAVERSION SET VERSION=7";
              script.Execute();
              continue;
            }
            throw ex;
          }
        }
      }
    }

    private static int GetSchemaVersion(string connectionString)
    {
      // retrieve the current schema version
      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        conn.Open();

        MySqlCommand cmd = new MySqlCommand("SELECT * FROM MY_ASPNET_SCHEMAVERSION", conn);
        try
        {
          object ver = cmd.ExecuteScalar();
          if (ver != null)
            return (int)ver;
        }
        catch (MySqlException ex)
        {
          if (ex.Number != (int)MySqlErrorCode.NoSuchTable)
            throw;
          string[] restrictions = new string[4];
          restrictions[2] = "MYSQL_MEMBERSHIP";
          DataTable dt = conn.GetSchema("Tables", restrictions);
          if (dt.Rows.Count == 1)
            return Convert.ToInt32(dt.Rows[0]["TABLE_COMMENT"]);
        }
        return 0;
      }
    }

    /// <summary>
    /// Creates the or fetch user id.
    /// </summary>
    /// <param name="connection">The connection.</param>
    /// <param name="username">The username.</param>
    /// <param name="applicationId">The application id.</param>
    /// <param name="authenticated">if set to <c>true</c> [authenticated].</param>
    /// <returns></returns>
    internal static long CreateOrFetchUserId(MySqlConnection connection, string username,
        long applicationId, bool authenticated)
    {
      Debug.Assert(applicationId > 0);

      // first attempt to fetch an existing user id
      MySqlCommand cmd = new MySqlCommand(@"SELECT ID FROM MY_ASPNET_USERS WHERE APPLICATIONID = @APPID AND NAME = @NAME", connection);
      cmd.Parameters.AddWithValue("@APPID", applicationId);
      cmd.Parameters.AddWithValue("@NAME", username);
      object userId = cmd.ExecuteScalar();
      if (userId != null) return (int)userId;

      cmd.CommandText = @"INSERT INTO MY_ASPNET_USERS VALUES (NULL, @APPID, @NAME, @ISANON, NOW())";
      cmd.Parameters.AddWithValue("@ISANON", !authenticated);
      int recordsAffected = cmd.ExecuteNonQuery();
      if (recordsAffected != 1)
        throw new ProviderException(Resources.UnableToCreateUser);

      cmd.CommandText = "SELECT LAST_INSERT_ID()";
      return Convert.ToInt64(cmd.ExecuteScalar());
    }
  }
}
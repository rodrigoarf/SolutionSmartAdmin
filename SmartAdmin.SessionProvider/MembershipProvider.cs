using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;
using MySql.Data.MySqlClient;
using SmartAdmin.SessionStateProvider.Properties;
using SmartAdmin.SessionStateProvider.Profile;
using SmartAdmin.SessionStateProvider.Common;
using System.Text.RegularExpressions;
using SmartAdmin.SessionStateProvider.General;

namespace SmartAdmin.SessionStateProvider.Security
{
  /// <summary>
  /// Manages storage of membership information for an ASP.NET application in a MySQL database. 
  /// </summary>
  /// <remarks>
  /// <para>
  /// This class is used by the <see cref="Membership"/> and <see cref="MembershipUser"/> classes
  /// to provide membership services for ASP.NET applications using a MySQL database.
  /// </para>
  /// </remarks>
  /// <example>
  /// <code source="CodeExamples/MembershipCodeExample2.xml"/>
  /// </example>
  public sealed class MySQLMembershipProvider : MembershipProvider
  {
    private int newPasswordLength = 8;
    private string eventSource = "MySQLMembershipProvider";
    private string eventLog = "Application";
    private string exceptionMessage = "An exception occurred. Please check the Event Log.";
    private string connectionString;
    private int minRequiredPasswordLength;
    private bool writeExceptionsToEventLog;
    private bool enablePasswordReset;
    private bool enablePasswordRetrieval;
    private bool requiresQuestionAndAnswer;
    private bool requiresUniqueEmail;
    private int maxInvalidPasswordAttempts;
    private int passwordAttemptWindow;
    private MembershipPasswordFormat passwordFormat;
    private int minRequiredNonAlphanumericCharacters;
    private string passwordStrengthRegularExpression;
    private Application app;

    /// <summary>
    /// Initializes the MySQL membership provider with the property values specified in the 
    /// ASP.NET application's configuration file. This method is not intended to be used directly 
    /// from your code. 
    /// </summary>
    /// <param name="name">The name of the <see cref="MySQLMembershipProvider"/> instance to initialize.</param>
    /// <param name="config">A collection of the name/value pairs representing the 
    /// provider-specific attributes specified in the configuration for this provider.</param>
    /// <exception cref="T:System.ArgumentNullException">config is a null reference.</exception>
    /// <exception cref="T:System.InvalidOperationException">An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)"/> on a provider after the provider has already been initialized.</exception>
    /// <exception cref="T:System.Configuration.Provider.ProviderException"></exception>
    public override void Initialize(string name, NameValueCollection config)
    {
      if (config == null)
      {
        throw new ArgumentNullException("config");
      }
      if (name == null || name.Length == 0)
      {
        name = "MySQLMembershipProvider";
      }
      if (string.IsNullOrEmpty(config["description"]))
      {
        config.Remove("description");
        config.Add("description", "MySQL default application");
      }
      base.Initialize(name, config);

      string applicationName = GetConfigValue(config["applicationName"],
          HostingEnvironment.ApplicationVirtualPath);
      maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
      passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
      minRequiredNonAlphanumericCharacters =
          Convert.ToInt32(GetConfigValue(config["minRequiredNonalphanumericCharacters"], "1"));
      minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
      passwordStrengthRegularExpression =
          Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
      enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "True"));
      enablePasswordRetrieval = Convert.ToBoolean(
          GetConfigValue(config["enablePasswordRetrieval"], "False"));
      requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "False"));
      requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "True"));
      writeExceptionsToEventLog = Convert.ToBoolean(GetConfigValue(config["writeExceptionsToEventLog"], "True"));
      string temp_format = config["passwordFormat"];

      if (temp_format == null)
        temp_format = "hashed";
      else
        temp_format = temp_format.ToLowerInvariant();

      if (temp_format == "hashed")
        passwordFormat = MembershipPasswordFormat.Hashed;
      else if (temp_format == "encrypted")
        passwordFormat = MembershipPasswordFormat.Encrypted;
      else if (temp_format == "clear")
        passwordFormat = MembershipPasswordFormat.Clear;
      else
        throw new ProviderException("Password format not supported.");

      // if the user is asking for the ability to retrieve hashed passwords, then let
      // them know we can't
      if (PasswordFormat == MembershipPasswordFormat.Hashed)
      {
        if (EnablePasswordRetrieval)
          throw new ProviderException(Resources.CannotRetrieveHashedPasswords);
      }

      ConnectionStringSettings ConnectionStringSettings = ConfigurationManager.ConnectionStrings[
          config["connectionStringName"]];
      if (ConnectionStringSettings != null)
        connectionString = ConnectionStringSettings.ConnectionString.Trim();
      else
        connectionString = "";

      if (String.IsNullOrEmpty(connectionString)) return;

      // make sure we have the correct schema
      SchemaManager.CheckSchema(connectionString, config);

      app = new Application(applicationName, base.Description);
    }

    private static string GetConfigValue(string configValue, string defaultValue)
    {
      if (string.IsNullOrEmpty(configValue))
      {
        return defaultValue;
      }
      return configValue;
    }

    #region Properties

    /// <summary>
    /// The name of the application using the MySQL membership provider.
    /// </summary>
    /// <value>The name of the application using the MySQL membership provider.  The default is the 
    /// application virtual path.</value>
    /// <remarks>The ApplicationName is used by the MySqlMembershipProvider to separate 
    /// membership information for multiple applications.  Using different application names, 
    /// applications can use the same membership database.
    /// Likewise, multiple applications can make use of the same membership data by simply using
    /// the same application name.
    /// Caution should be taken with multiple applications as the ApplicationName property is not
    /// thread safe during writes.
    /// </remarks>
    /// <example>
    /// The following example shows the membership element being used in an applications web.config file.
    /// The application name setting is being used.
    /// <code source="CodeExamples/MembershipCodeExample1.xml"/>
    /// </example>
    public override string ApplicationName
    {
      get { return app.Name; }
      set
      {
        lock (this)
        {
          if (value.ToLowerInvariant() == app.Name.ToLowerInvariant()) return;
          app = new Application(value, String.Empty);
        }
      }
    }

    /// <summary>
    /// Indicates whether the membership provider is configured to allow users to reset their passwords.
    /// </summary>
    /// <value>true if the membership provider supports password reset; otherwise, false. The default is true.</value>
    /// <remarks>Allows the user to replace their password with a new, randomly generated password.  
    /// This can be especially handy when using hashed passwords since hashed passwords cannot be
    /// retrieved.</remarks>
    /// <example>
    /// The following example shows the membership element being used in an applications web.config file.
    /// <code source="CodeExamples/MembershipCodeExample1.xml"/>
    /// </example>
    public override bool EnablePasswordReset
    {
      get { return enablePasswordReset; }
    }

    /// <summary>
    /// Indicates whether the membership provider is configured to allow users to retrieve 
    /// their passwords.
    /// </summary>
    /// <value>true if the membership provider is configured to support password retrieval; 
    /// otherwise, false. The default is false.</value>
    /// <remarks>If the system is configured to use hashed passwords, then retrieval is not possible.  
    /// If the user attempts to initialize the provider with hashed passwords and enable password retrieval
    /// set to true then a <see cref="ProviderException"/> is thrown.</remarks>
    /// <example>
    /// The following example shows the membership element being used in an applications web.config file.
    /// <code source="CodeExamples/MembershipCodeExample1.xml"/>
    /// </example>
    public override bool EnablePasswordRetrieval
    {
      get { return enablePasswordRetrieval; }
    }

    /// <summary>
    /// Gets a value indicating whether the membership provider is 
    /// configured to require the user to answer a password question 
    /// for password reset and retrieval.
    /// </summary>
    /// <value>true if a password answer is required for password 
    /// reset and retrieval; otherwise, false. The default is false.</value>
    /// <example>
    /// The following example shows the membership element being used in an applications web.config file.
    /// <code source="CodeExamples/MembershipCodeExample1.xml"/>
    /// </example>
    public override bool RequiresQuestionAndAnswer
    {
      get { return requiresQuestionAndAnswer; }
    }

    /// <summary>
    /// Gets a value indicating whether the membership provider is configured 
    /// to require a unique e-mail address for each user name.
    /// </summary>
    /// <value>true if the membership provider requires a unique e-mail address; 
    /// otherwise, false. The default is true.</value>
    /// <example>
    /// The following example shows the membership element being used in an applications web.config file.
    /// <code source="CodeExamples/MembershipCodeExample1.xml"/>
    /// </example>
    public override bool RequiresUniqueEmail
    {
      get { return requiresUniqueEmail; }
    }

    /// <summary>
    /// Gets the number of invalid password or password-answer attempts allowed 
    /// before the membership user is locked out.
    /// </summary>
    /// <value>The number of invalid password or password-answer attempts allowed 
    /// before the membership user is locked out.</value>
    /// <example>
    /// The following example shows the membership element being used in an applications web.config file.
    /// <code source="CodeExamples/MembershipCodeExample1.xml"/>
    /// </example>
    public override int MaxInvalidPasswordAttempts
    {
      get { return maxInvalidPasswordAttempts; }
    }

    /// <summary>
    /// Gets the number of minutes in which a maximum number of invalid password or 
    /// password-answer attempts are allowed before the membership user is locked out.
    /// </summary>
    /// <value>The number of minutes in which a maximum number of invalid password or 
    /// password-answer attempts are allowed before the membership user is locked out.</value>
    /// <example>
    /// The following example shows the membership element being used in an applications web.config file.
    /// <code source="CodeExamples/MembershipCodeExample1.xml"/>
    /// </example>
    public override int PasswordAttemptWindow
    {
      get { return passwordAttemptWindow; }
    }

    /// <summary>
    /// Gets a value indicating the format for storing passwords in the membership data store.
    /// </summary>
    /// <value>One of the <see cref="T:System.Web.Security.MembershipPasswordFormat"/> 
    /// values indicating the format for storing passwords in the data store.</value>
    /// <example>
    /// The following example shows the membership element being used in an applications web.config file.
    /// <code source="CodeExamples/MembershipCodeExample1.xml"/>
    /// </example>
    public override MembershipPasswordFormat PasswordFormat
    {
      get { return passwordFormat; }
    }

    /// <summary>
    /// Gets the minimum number of special characters that must be present in a valid password.
    /// </summary>
    /// <value>The minimum number of special characters that must be present 
    /// in a valid password.</value>
    /// <example>
    /// The following example shows the membership element being used in an applications web.config file.
    /// <code source="CodeExamples/MembershipCodeExample1.xml"/>
    /// </example>
    public override int MinRequiredNonAlphanumericCharacters
    {
      get { return minRequiredNonAlphanumericCharacters; }
    }

    /// <summary>
    /// Gets the minimum length required for a password.
    /// </summary>
    /// <value>The minimum length required for a password. </value>
    /// <example>
    /// The following example shows the membership element being used in an applications web.config file.
    /// <code source="CodeExamples/MembershipCodeExample1.xml"/>
    /// </example>
    public override int MinRequiredPasswordLength
    {
      get { return minRequiredPasswordLength; }
    }

    /// <summary>
    /// Gets the regular expression used to evaluate a password.
    /// </summary>
    /// <value>A regular expression used to evaluate a password.</value>
    /// <example>
    /// The following example shows the membership element being used in an applications web.config file.
    /// In this example, the regular expression specifies that the password must meet the following
    /// criteria:
    /// <ul>
    /// <list>Is at least seven characters.</list>
    /// <list>Contains at least one digit.</list>
    /// <list>Contains at least one special (non-alphanumeric) character.</list>
    /// </ul>
    /// <code source="CodeExamples/MembershipCodeExample1.xml"/>
    /// </example>
    public override string PasswordStrengthRegularExpression
    {
      get { return passwordStrengthRegularExpression; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether exceptions are written to the event log.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if exceptions should be written to the log; otherwise, <c>false</c>.
    /// </value>
    public bool WriteExceptionsToEventLog
    {
      get { return writeExceptionsToEventLog; }
      set { writeExceptionsToEventLog = value; }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Changes the password.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="oldPassword">The old password.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns>true if the password was updated successfully, false if the supplied old password
    /// is invalid, the user is locked out, or the user does not exist in the database.</returns>
    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
      // this will return false if the username doesn't exist
      if (!(ValidateUser(username, oldPassword)))
        return false;

      ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, true);
      OnValidatingPassword(args);
      if (args.Cancel)
      {
        if (!(args.FailureInformation == null))
          throw args.FailureInformation;
        else
          throw new ProviderException(Resources.ChangePasswordCanceled);
      }

      // validate the password according to current guidelines
      if (!ValidatePassword(newPassword, "newPassword", true))
        return false;

      try
      {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
          connection.Open();

          // retrieve the existing key and format for this user
          string passwordKey;
          MembershipPasswordFormat passwordFormat;
          int userId = GetUserId(connection, username);

          GetPasswordInfo(connection, userId, out passwordKey, out passwordFormat);

          MySqlCommand cmd = new MySqlCommand(
              @"UPDATE MY_ASPNET_MEMBERSHIP
                        SET PASSWORD = @PASS, LASTPASSWORDCHANGEDDATE = @LASTPASSWORDCHANGEDDATE 
                        WHERE USERID=@USERID", connection);
          cmd.Parameters.AddWithValue("@pass",
              EncodePassword(newPassword, passwordKey, passwordFormat));
          cmd.Parameters.AddWithValue("@LASTPASSWORDCHANGEDDATE", DateTime.Now);
          cmd.Parameters.AddWithValue("@USERID", userId);
          return cmd.ExecuteNonQuery() > 0;
        }
      }
      catch (MySqlException e)
      {
        if (WriteExceptionsToEventLog)
          WriteToEventLog(e, "ChangePassword");
        throw new ProviderException(exceptionMessage, e);
      }
    }

    /// <summary>
    /// Changes the password question and answer.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    /// <param name="newPwdQuestion">The new password question.</param>
    /// <param name="newPwdAnswer">The new password answer.</param>
    /// <returns>true if the update was successful; otherwise, false. A value of false is 
    /// also returned if the password is incorrect, the user is locked out, or the user 
    /// does not exist in the database.</returns>
    public override bool ChangePasswordQuestionAndAnswer(string username,
        string password, string newPwdQuestion, string newPwdAnswer)
    {
      // this handles the case where the username doesn't exist
      if (!(ValidateUser(username, password)))
        return false;

      try
      {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
          connection.Open();

          string passwordKey;
          MembershipPasswordFormat passwordFormat;
          int userId = GetUserId(connection, username);

          GetPasswordInfo(connection, userId, out passwordKey, out passwordFormat);


          MySqlCommand cmd = new MySqlCommand(
              @"UPDATE MY_ASPNET_MEMBERSHIP 
                        SET PASSWORDQUESTION = @PASSWORDQUESTION, PASSWORDANSWER = @PASSWORDANSWER
                        WHERE USERID=@USERID", connection);
          cmd.Parameters.AddWithValue("@PASSWORDQUESTION", newPwdQuestion);
          cmd.Parameters.AddWithValue("@PASSWORDANSWER",
              EncodePassword(newPwdAnswer, passwordKey, passwordFormat));
          cmd.Parameters.AddWithValue("@USERID", userId);
          return cmd.ExecuteNonQuery() > 0;
        }
      }
      catch (MySqlException e)
      {
        if (WriteExceptionsToEventLog)
          WriteToEventLog(e, "ChangePasswordQuestionAndAnswer");
        throw new ProviderException(exceptionMessage, e);
      }
    }

    /// <summary>
    /// Adds a new membership user to the data source.
    /// </summary>
    /// <param name="username">The user name for the new user.</param>
    /// <param name="password">The password for the new user.</param>
    /// <param name="email">The e-mail address for the new user.</param>
    /// <param name="passwordQuestion">The password question for the new user.</param>
    /// <param name="passwordAnswer">The password answer for the new user</param>
    /// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
    /// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
    /// <param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus"/> enumeration value indicating whether the user was created successfully.</param>
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the information for the newly created user.
    /// </returns>
    public override MembershipUser CreateUser(string username, string password,
        string email, string passwordQuestion, string passwordAnswer,
        bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    {
      ValidatePasswordEventArgs Args = new ValidatePasswordEventArgs(username, password, true);
      OnValidatingPassword(Args);
      if (Args.Cancel)
      {
        status = MembershipCreateStatus.InvalidPassword;
        return null;
      }
      if (RequiresUniqueEmail && !String.IsNullOrEmpty(GetUserNameByEmail(email)))
      {
        status = MembershipCreateStatus.DuplicateEmail;
        return null;
      }

      ValidateQA(passwordQuestion, passwordAnswer);

      // now try to validate the password
      if (!ValidatePassword(password, "password", false))
      {
        status = MembershipCreateStatus.InvalidPassword;
        return null;
      }

      // now check to see if we already have a member by this name
      MembershipUser u = GetUser(username, false);
      if (u != null)
      {
        status = MembershipCreateStatus.DuplicateUserName;
        return null;
      }

      string passwordKey = GetPasswordKey();
      DateTime createDate = DateTime.Now;
      MySqlTransaction transaction = null;

      using (MySqlConnection connection = new MySqlConnection(connectionString))
      {
        try
        {
          connection.Open();
          transaction = connection.BeginTransaction();

          // either create a new user or fetch the existing user id
          long userId = SchemaManager.CreateOrFetchUserId(connection, username,
              app.EnsureId(connection), true);

          MySqlCommand cmd = new MySqlCommand(
              @"INSERT INTO MY_ASPNET_MEMBERSHIP 
                        VALUES(@USERID, @EMAIL, @COMMENT, @PASSWORD, @PASSWORDKEY, 
                        @PASSWORDFORMAT, @PASSWORDQUESTION, @PASSWORDANSWER, 
                        @ISAPPROVED, @LASTACTIVITYDATE, @LASTLOGINDATE,
                        @LASTPASSWORDCHANGEDDATE, @CREATIONDATE, 
                        @ISLOCKEDOUT, @LASTLOCKEDOUTDATE, @FAILEDPASSWORDATTEMPTCOUNT,
                        @FAILEDPASSWORDATTEMPTWINDOWSTART, @FAILEDPASSWORDANSWERATTEMPTCOUNT, 
                        @FAILEDPASSWORDANSWERATTEMPTWINDOWSTART)",
              connection);
          cmd.Parameters.AddWithValue("@USERID", userId);
          cmd.Parameters.AddWithValue("@EMAIL", email);
          cmd.Parameters.AddWithValue("@COMMENT", "");
          cmd.Parameters.AddWithValue("@PASSWORD", EncodePassword(password, passwordKey, PasswordFormat));
          cmd.Parameters.AddWithValue("@PASSWORDKEY", passwordKey);
          cmd.Parameters.AddWithValue("@PASSWORDFORMAT", PasswordFormat);
          cmd.Parameters.AddWithValue("@PASSWORDQUESTION", passwordQuestion);
          cmd.Parameters.AddWithValue("@PASSWORDANSWER", EncodePassword(passwordAnswer, passwordKey, PasswordFormat));
          cmd.Parameters.AddWithValue("@ISAPPROVED", isApproved);
          cmd.Parameters.AddWithValue("@LASTACTIVITYDATE", createDate);
          cmd.Parameters.AddWithValue("@LASTLOGINDATE", createDate);
          cmd.Parameters.AddWithValue("@LASTPASSWORDCHANGEDDATE", createDate);
          cmd.Parameters.AddWithValue("@CREATIONDATE", createDate);
          cmd.Parameters.AddWithValue("@ISLOCKEDOUT", false);
          cmd.Parameters.AddWithValue("@LASTLOCKEDOUTDATE", createDate);
          cmd.Parameters.AddWithValue("@FAILEDPASSWORDATTEMPTCOUNT", 0);
          cmd.Parameters.AddWithValue("@FAILEDPASSWORDATTEMPTWINDOWSTART", createDate);
          cmd.Parameters.AddWithValue("@FAILEDPASSWORDANSWERATTEMPTCOUNT", 0);
          cmd.Parameters.AddWithValue("@FAILEDPASSWORDANSWERATTEMPTWINDOWSTART", createDate);

          int recAdded = cmd.ExecuteNonQuery();
          if (recAdded > 0)
            status = MembershipCreateStatus.Success;
          else
            status = MembershipCreateStatus.UserRejected;
          transaction.Commit();
        }
        catch (MySqlException e)
        {
          if (WriteExceptionsToEventLog)
            WriteToEventLog(e, "CreateUser");
          status = MembershipCreateStatus.ProviderError;
          if (transaction != null)
            transaction.Rollback();
          return null;
        }
      }

      return GetUser(username, false);
    }

    /// <summary>
    /// Removes a user from the membership data source.
    /// </summary>
    /// <param name="username">The name of the user to delete.</param>
    /// <param name="deleteAllRelatedData">true to delete data related to the user from the database; false to leave data related to the user in the database.</param>
    /// <returns>
    /// true if the user was successfully deleted; otherwise, false.
    /// </returns>
    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
      try
      {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
          conn.Open();

          int userId = GetUserId(conn, username);
          if (-1 == userId) return false;

          // if we are supposed to delete all related data, then delegate that to those providers
          if (deleteAllRelatedData)
          {
            MySQLRoleProvider.DeleteUserData(conn, userId);
            MySQLProfileProvider.DeleteUserData(conn, userId);
          }

          string sql = @"DELETE {0}M 
                        FROM MY_ASPNET_USERS U, MY_ASPNET_MEMBERSHIP M
                        WHERE U.ID=M.USERID AND U.ID=@USERID";

          MySqlCommand cmd = new MySqlCommand(
              String.Format(sql, deleteAllRelatedData ? "u," : ""), conn);
          cmd.Parameters.AddWithValue("@APPID", app.FetchId(conn));
          cmd.Parameters.AddWithValue("@USERID", userId);
          return cmd.ExecuteNonQuery() > 0;
        }
      }
      catch (MySqlException e)
      {
        if (WriteExceptionsToEventLog)
          WriteToEventLog(e, "DeleteUser");
        throw new ProviderException(exceptionMessage, e);
      }
    }

    /// <summary>
    /// Gets a collection of all the users in the data source in pages of data.
    /// </summary>
    /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param>
    /// <param name="pageSize">The size of the page of results to return.</param>
    /// <param name="totalRecords">The total number of matched users.</param>
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
    /// </returns>
    public override MembershipUserCollection GetAllUsers(int pageIndex,
        int pageSize, out int totalRecords)
    {
      return GetUsers(null, null, pageIndex, pageSize, out totalRecords);
    }

    /// <summary>
    /// Gets the number of users currently accessing the application.
    /// </summary>
    /// <returns>
    /// The number of users currently accessing the application.
    /// </returns>
    public override int GetNumberOfUsersOnline()
    {
      TimeSpan onlineSpan = new TimeSpan(0, Membership.UserIsOnlineTimeWindow, 0);
      DateTime compareTime = DateTime.Now.Subtract(onlineSpan);

      try
      {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
          connection.Open();
          MySqlCommand cmd = new MySqlCommand(
              @"SELECT COUNT(*) FROM MY_ASPNET_MEMBERSHIP M JOIN MY_ASPNET_USERS U
                        ON M.USERID=U.ID WHERE M.LASTACTIVITYDATE > @DATE AND U.APPLICATIONID=@APPID",
              connection);
          cmd.Parameters.AddWithValue("@DATE", compareTime);
          cmd.Parameters.AddWithValue("@APPID", app.FetchId(connection));
          return Convert.ToInt32(cmd.ExecuteScalar());
        }
      }
      catch (MySqlException e)
      {
        if (WriteExceptionsToEventLog)
          WriteToEventLog(e, "GetNumberOfUsersOnline");
        throw new ProviderException(exceptionMessage, e);
      }
    }

    /// <summary>
    /// Gets the password for the specified user name from the data source.
    /// </summary>
    /// <param name="username">The user to retrieve the password for.</param>
    /// <param name="answer">The password answer for the user.</param>
    /// <returns>
    /// The password for the specified user name.
    /// </returns>
    public override string GetPassword(string username, string answer)
    {
      if (!EnablePasswordRetrieval)
        throw new ProviderException(Resources.PasswordRetrievalNotEnabled);

      try
      {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
          connection.Open();

          int userId = GetUserId(connection, username);
          if (-1 == userId)
            throw new ProviderException("Username not found.");

          string sql = @"SELECT PASSWORD, PASSWORDANSWER, PASSWORDKEY, PASSWORDFORMAT, 
                    ISLOCKEDOUT FROM MY_ASPNET_MEMBERSHIP WHERE USERID=@USERID";
          MySqlCommand cmd = new MySqlCommand(sql, connection);
          cmd.Parameters.AddWithValue("@USERID", userId);

          using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
          {
            reader.Read();
            if (reader.GetBoolean("IsLockedOut"))
              throw new MembershipPasswordException(Resources.UserIsLockedOut);

            string password = reader.GetString("Password");
            string passwordAnswer = reader.GetValue(reader.GetOrdinal("PasswordAnswer")).ToString();
            string passwordKey = reader.GetString("PasswordKey");
            MembershipPasswordFormat format = (MembershipPasswordFormat)reader.GetInt32(3);
            reader.Close();

            if (RequiresQuestionAndAnswer &&
                !(CheckPassword(answer, passwordAnswer, passwordKey, format)))
            {
              UpdateFailureCount(userId, "PasswordAnswer", connection);
              throw new MembershipPasswordException(Resources.IncorrectPasswordAnswer);
            }
            if (PasswordFormat == MembershipPasswordFormat.Encrypted)
            {
              password = UnEncodePassword(password, format);
            }
            return password;
          }
        }
      }
      catch (MySqlException e)
      {
        if (WriteExceptionsToEventLog)
          WriteToEventLog(e, "GetPassword");
        throw new ProviderException(exceptionMessage, e);
      }
    }

    /// <summary>
    /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
    /// </summary>
    /// <param name="username">The name of the user to get information for.</param>
    /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the specified user's information from the data source.
    /// </returns>
    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
      try
      {
        int userId = -1;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
          connection.Open();

          userId = GetUserId(connection, username);
          if (-1 == userId) return null;
        }

        return GetUser(userId, userIsOnline);
      }
      catch (MySqlException e)
      {
        if (WriteExceptionsToEventLog)
          WriteToEventLog(e, "GetUser(String, Boolean)");
        throw new ProviderException(exceptionMessage, e);
      }
    }

    /// <summary>
    /// Gets user information from the data source based on the unique identifier for the membership user. Provides an option to update the last-activity date/time stamp for the user.
    /// </summary>
    /// <param name="providerUserKey">The unique identifier for the membership user to get information for.</param>
    /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the specified user's information from the data source.
    /// </returns>
    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
      MySqlTransaction txn = null;

      try
      {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
          connection.Open();

          txn = connection.BeginTransaction();
          MySqlCommand cmd = new MySqlCommand("", connection);
          cmd.Parameters.AddWithValue("@userId", providerUserKey);

          if (userIsOnline)
          {
            cmd.CommandText =
                @"UPDATE MY_ASPNET_USERS SET LASTACTIVITYDATE = @DATE WHERE ID=@USERID";
            cmd.Parameters.AddWithValue("@DATE", DateTime.Now);
            cmd.ExecuteNonQuery();

            cmd.CommandText = "UPDATE MY_ASPNET_MEMBERSHIP SET LASTACTIVITYDATE=@DATE WHERE USERID=@USERID";
            cmd.ExecuteNonQuery();
          }

          cmd.CommandText = @"SELECT M.*,U.NAME 
                    FROM MY_ASPNET_MEMBERSHIP M JOIN MY_ASPNET_USERS U ON M.USERID=U.ID 
                    WHERE U.ID=@USERID";

          MembershipUser user;
          using (MySqlDataReader reader = cmd.ExecuteReader())
          {
            if (!reader.Read()) return null;
            user = GetUserFromReader(reader);
          }
          txn.Commit();
          return user;
        }
      }
      catch (MySqlException e)
      {
        if (txn != null)
          txn.Rollback();
        if (WriteExceptionsToEventLog)
          WriteToEventLog(e, "GetUser(Object, Boolean)");
        throw new ProviderException(exceptionMessage);
      }
    }

    /// <summary>
    /// Unlocks the user.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <returns>true if the membership user was successfully unlocked; 
    /// otherwise, false. A value of false is also returned if the user 
    /// does not exist in the database. </returns>
    public override bool UnlockUser(string username)
    {
      try
      {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
          conn.Open();

          int userId = GetUserId(conn, username);
          if (-1 == userId) return false;

          string sql = @"UPDATE MY_ASPNET_MEMBERSHIP  
                        SET ISLOCKEDOUT = FALSE, LASTLOCKEDOUTDATE = @LASTDATE 
                        WHERE USERID=@USERID";

          MySqlCommand cmd = new MySqlCommand(sql, conn);
          cmd.Parameters.AddWithValue("@LASTDATE", DateTime.Now);
          cmd.Parameters.AddWithValue("@USERID", userId);
          return cmd.ExecuteNonQuery() > 0;
        }
      }
      catch (MySqlException e)
      {
        if (WriteExceptionsToEventLog)
          WriteToEventLog(e, "UnlockUser");
        throw new ProviderException(exceptionMessage, e);
      }
    }

    /// <summary>
    /// Gets the user name associated with the specified e-mail address.
    /// </summary>
    /// <param name="email">The e-mail address to search for.</param>
    /// <returns>
    /// The user name associated with the specified e-mail address. If no match is found, return null.
    /// </returns>
    public override string GetUserNameByEmail(string email)
    {
      try
      {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
          conn.Open();

          string sql = @"SELECT U.NAME FROM MY_ASPNET_USERS U
                        JOIN MY_ASPNET_MEMBERSHIP M ON M.USERID=U.ID
                        WHERE M.EMAIL = @EMAIL AND U.APPLICATIONID=@APPID";
          MySqlCommand cmd = new MySqlCommand(sql, conn);
          cmd.Parameters.AddWithValue("@EMAIL", email);
          cmd.Parameters.AddWithValue("@APPID", app.FetchId(conn));
          return (string)cmd.ExecuteScalar();
        }
      }
      catch (MySqlException e)
      {
        if (WriteExceptionsToEventLog)
          WriteToEventLog(e, "GetUserNameByEmail");
        throw new ProviderException(exceptionMessage);
      }
    }

    /// <summary>
    /// Resets a user's password to a new, automatically generated password.
    /// </summary>
    /// <param name="username">The user to reset the password for.</param>
    /// <param name="answer">The password answer for the specified user.</param>
    /// <returns>The new password for the specified user.</returns>
    public override string ResetPassword(string username, string answer)
    {
      if (!(EnablePasswordReset))
        throw new NotSupportedException(Resources.PasswordResetNotEnabled);

      try
      {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
          connection.Open();

          // fetch the userid first
          int userId = GetUserId(connection, username);
          if (-1 == userId)
            throw new ProviderException(Resources.UsernameNotFound);

          if (answer == null && RequiresQuestionAndAnswer)
          {
            UpdateFailureCount(userId, "PasswordAnswer", connection);
            throw new ProviderException(Resources.PasswordRequiredForReset);
          }

          string newPassword = Membership.GeneratePassword(newPasswordLength, MinRequiredNonAlphanumericCharacters);
          ValidatePasswordEventArgs Args = new ValidatePasswordEventArgs(username, newPassword, true);
          OnValidatingPassword(Args);
          if (Args.Cancel)
          {
            if (!(Args.FailureInformation == null))
              throw Args.FailureInformation;
            else
              throw new MembershipPasswordException(Resources.PasswordResetCanceledNotValid);
          }

          MySqlCommand cmd = new MySqlCommand(@"SELECT PASSWORDANSWER, 
                    PASSWORDKEY, PASSWORDFORMAT, ISLOCKEDOUT 
                    FROM MY_ASPNET_MEMBERSHIP WHERE USERID=@USERID", connection);
          cmd.Parameters.AddWithValue("@userId", userId);

          string passwordKey = String.Empty;
          MembershipPasswordFormat format;
          using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
          {
            reader.Read();
            if (reader.GetBoolean("IsLockedOut"))
              throw new MembershipPasswordException(Resources.UserIsLockedOut);

            object passwordAnswer = reader.GetValue(reader.GetOrdinal("PasswordAnswer"));
            passwordKey = reader.GetString("PasswordKey");
            format = (MembershipPasswordFormat)reader.GetByte("PasswordFormat");
            reader.Close();

            if (RequiresQuestionAndAnswer)
            {
              if (!CheckPassword(answer, (string)passwordAnswer, passwordKey, format))
              {
                UpdateFailureCount(userId, "PasswordAnswer", connection);
                throw new MembershipPasswordException(Resources.IncorrectPasswordAnswer);
              }
            }
          }

          cmd.CommandText = @"UPDATE MY_ASPNET_MEMBERSHIP 
                        SET PASSWORD = @PASS, LASTPASSWORDCHANGEDDATE = @LASTPASSCHANGE
                        WHERE USERID=@USERID";

          cmd.Parameters.AddWithValue("@PASS",
              EncodePassword(newPassword, passwordKey, format));
          cmd.Parameters.AddWithValue("@LASTPASSCHANGE", DateTime.Now);
          int rowsAffected = cmd.ExecuteNonQuery();
          if (rowsAffected != 1)
            throw new MembershipPasswordException(Resources.ErrorResettingPassword);
          return newPassword;
        }
      }
      catch (MySqlException e)
      {
        if (WriteExceptionsToEventLog)
          WriteToEventLog(e, "ResetPassword");
        throw new ProviderException(exceptionMessage, e);
      }
    }

    /// <summary>
    /// Updates information about a user in the data source.
    /// </summary>
    /// <param name="user">A <see cref="T:System.Web.Security.MembershipUser"/> object 
    /// that represents the user to update and the updated information for the user.</param>
    public override void UpdateUser(MembershipUser user)
    {
      try
      {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
          conn.Open();

          int userId = GetUserId(conn, user.UserName);
          if (-1 == userId)
            throw new ProviderException(Resources.UsernameNotFound);

          string sql = @"UPDATE MY_ASPNET_MEMBERSHIP M, MY_ASPNET_USERS U 
                        SET M.EMAIL=@EMAIL, M.COMMENT=@COMMENT, M.ISAPPROVED=@ISAPPROVED,
                        M.LASTLOGINDATE=@LASTLOGINDATE, U.LASTACTIVITYDATE=@LASTACTIVITYDATE,
                        M.LASTACTIVITYDATE=@LASTACTIVITYDATE
                        WHERE M.USERID=U.ID AND U.NAME LIKE @NAME AND U.APPLICATIONID=@APPID";
          MySqlCommand cmd = new MySqlCommand(sql, conn);
          cmd.Parameters.AddWithValue("@EMAIL", user.Email);
          cmd.Parameters.AddWithValue("@COMMENT", user.Comment);
          cmd.Parameters.AddWithValue("@ISAPPROVED", user.IsApproved);
          cmd.Parameters.AddWithValue("@LASTLOGINDATE", user.LastLoginDate);
          cmd.Parameters.AddWithValue("@LASTACTIVITYDATE", user.LastActivityDate);
          cmd.Parameters.AddWithValue("@NAME", user.UserName);
          cmd.Parameters.AddWithValue("@APPID", app.FetchId(conn));
          cmd.ExecuteNonQuery();
        }
      }
      catch (MySqlException e)
      {
        if (WriteExceptionsToEventLog)
          WriteToEventLog(e, "UpdateUser");
        throw new ProviderException(exceptionMessage);
      }
    }

    /// <summary>
    /// Verifies that the specified user name and password exist in the data source.
    /// </summary>
    /// <param name="username">The name of the user to validate.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <returns>
    /// true if the specified username and password are valid; otherwise, false.
    /// </returns>
    public override bool ValidateUser(string username, string password)
    {
      bool isValid = false;
      try
      {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
          connection.Open();

          // first get the user id.  If that is -1, then the user doesn't exist
          // so we just return false since we can't bump any counters
          int userId = GetUserId(connection, username);
          if (-1 == userId) return false;

          string sql = @"SELECT PASSWORD, PASSWORDKEY, PASSWORDFORMAT, ISAPPROVED,
                            ISLOCKEDOUT FROM MY_ASPNET_MEMBERSHIP WHERE USERID=@USERID";
          MySqlCommand cmd = new MySqlCommand(sql, connection);
          cmd.Parameters.AddWithValue("@USERID", userId);

          using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
          {
            if (!reader.HasRows) return false;
            reader.Read();
            if (reader.GetBoolean("IsLockedOut")) return false;

            string pwd = reader.GetString(0);
            string passwordKey = reader.GetString(1);
            MembershipPasswordFormat format = (MembershipPasswordFormat)
                reader.GetInt32(2);
            bool isApproved = reader.GetBoolean(3);
            reader.Close();

            if (!CheckPassword(password, pwd, passwordKey, format))
              UpdateFailureCount(userId, "Password", connection);
            else if (isApproved)
            {
              isValid = true;
              DateTime currentDate = DateTime.Now;
              MySqlCommand updateCmd = new MySqlCommand(
                  @"UPDATE MY_ASPNET_MEMBERSHIP M, MY_ASPNET_USERS U 
                                SET M.LASTLOGINDATE = @LASTLOGINDATE, U.LASTACTIVITYDATE = @DATE,
                                M.LASTACTIVITYDATE=@DATE 
                                WHERE M.USERID=@USERID AND U.ID=@USERID", connection);
              updateCmd.Parameters.AddWithValue("@LASTLOGINDATE", currentDate);
              updateCmd.Parameters.AddWithValue("@DATE", currentDate);
              updateCmd.Parameters.AddWithValue("@USERID", userId);
              updateCmd.ExecuteNonQuery();
            }
          }
          return isValid;
        }
      }
      catch (MySqlException e)
      {
        if (WriteExceptionsToEventLog)
          WriteToEventLog(e, "ValidateUser");
        throw new ProviderException(exceptionMessage, e);
      }
    }

    /// <summary>
    /// Gets a collection of membership users where the user name contains the specified user name to match.
    /// </summary>
    /// <param name="usernameToMatch">The user name to search for.</param>
    /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param>
    /// <param name="pageSize">The size of the page of results to return.</param>
    /// <param name="totalRecords">The total number of matched users.</param>
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
    /// </returns>
    public override MembershipUserCollection FindUsersByName(string usernameToMatch,
                                     int pageIndex, int pageSize, out int totalRecords)
    {
      return GetUsers(usernameToMatch, null, pageIndex, pageSize, out totalRecords);
    }

    /// <summary>
    /// Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.
    /// </summary>
    /// <param name="emailToMatch">The e-mail address to search for.</param>
    /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param>
    /// <param name="pageSize">The size of the page of results to return.</param>
    /// <param name="totalRecords">The total number of matched users.</param>
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
    /// </returns>
    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex,
                                                              int pageSize, out int totalRecords)
    {
      return GetUsers(null, emailToMatch, pageIndex, pageSize, out totalRecords);
    }

    #endregion

    #region Private Methods

    private int GetUserId(MySqlConnection connection, string username)
    {
        MySqlCommand cmd = new MySqlCommand(
            "SELECT ID FROM MY_ASPNET_USERS WHERE NAME = @NAME AND APPLICATIONID=@APPID", connection);
        cmd.Parameters.AddWithValue("@NAME", username);
        cmd.Parameters.AddWithValue("@APPID", app.FetchId(connection));
        object id = cmd.ExecuteScalar();
        if (id == null) return -1;
        return (int)id;
    }

    private void WriteToEventLog(Exception e, string action)
    {
        using (EventLog log = new EventLog())
        {
            log.Source = eventSource;
            log.Log = eventLog;
            string message = "An exception occurred communicating with the data source." +
                             Environment.NewLine + Environment.NewLine;
            message += "Action: " + action + Environment.NewLine + Environment.NewLine;
            message += "Exception: " + e;
            log.WriteEntry(message);
        }
    }

    private MembershipUser GetUserFromReader(MySqlDataReader reader)
    {
        object providerUserKey = reader.GetInt32("USERID");
        string username = reader.GetString("name");

        string email = null;
        if (!reader.IsDBNull(reader.GetOrdinal("EMAIL")))
            email = reader.GetString("EMAIL");

        string passwordQuestion = "";
        if (!(reader.GetValue(reader.GetOrdinal("PasswordQuestion")) == DBNull.Value))
            passwordQuestion = reader.GetString("PasswordQuestion");

        string comment = "";
        if (!(reader.GetValue(reader.GetOrdinal("COMMENT")) == DBNull.Value))
            comment = reader.GetString("COMMENT");

        bool isApproved = reader.GetBoolean("ISAPPROVED");
        bool isLockedOut = reader.GetBoolean("ISLOCKEDOUT");
        DateTime creationDate = reader.GetDateTime("CREATIONDATE");
        DateTime lastLoginDate = new DateTime();
        if (!(reader.GetValue(reader.GetOrdinal("LASTLOGINDATE")) == DBNull.Value))
            lastLoginDate = reader.GetDateTime("LASTLOGINDATE");

        DateTime lastActivityDate = reader.GetDateTime("LASTACTIVITYDATE");
        DateTime lastPasswordChangedDate = reader.GetDateTime("LASTPASSWORDCHANGEDDATE");
        DateTime lastLockedOutDate = new DateTime();
        if (!(reader.GetValue(reader.GetOrdinal("LASTLOCKEDOUTDATE")) == DBNull.Value))
            lastLockedOutDate = reader.GetDateTime("LASTLOCKEDOUTDATE");

        MembershipUser u =
            new MembershipUser(Name, username, providerUserKey, email, passwordQuestion, comment, isApproved,
                               isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate,
                               lastLockedOutDate);
        return u;
    }

    private string UnEncodePassword(string encodedPassword, MembershipPasswordFormat format)
    {
        string password = encodedPassword;
        if (format == MembershipPasswordFormat.Clear)
            return encodedPassword;
        else if (format == MembershipPasswordFormat.Encrypted)
            return Encoding.Unicode.GetString(DecryptPassword(
                Convert.FromBase64String(password)));
        else if (format == MembershipPasswordFormat.Hashed)
            throw new ProviderException(Resources.CannotUnencodeHashedPwd);
        else
            throw new ProviderException(Resources.UnsupportedPasswordFormat);
    }

    private string GetPasswordKey()
    {
        RNGCryptoServiceProvider cryptoProvider =
            new RNGCryptoServiceProvider();
        byte[] key = new byte[16];
        cryptoProvider.GetBytes(key);
        return Convert.ToBase64String(key);
    }

    /// <summary>
    /// this method is only necessary because early versions of Mono did not support
    /// the HashAlgorithmType property
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    private string HashPasswordBytes(byte[] key, byte[] bytes)
    {
        HashAlgorithm hash = HashAlgorithm.Create(Membership.HashAlgorithmType);

        if (hash is KeyedHashAlgorithm)
        {
            KeyedHashAlgorithm keyedHash = hash as KeyedHashAlgorithm;
            keyedHash.Key = key;
        }
        return Convert.ToBase64String(hash.ComputeHash(bytes));
    }

    private string EncodePassword(string password, string passwordKey,
        MembershipPasswordFormat format)
    {
        if (password == null)
            return null;
        if (format == MembershipPasswordFormat.Clear)
            return password;

        byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
        byte[] keyBytes = Convert.FromBase64String(passwordKey);
        byte[] keyedBytes = new byte[passwordBytes.Length + keyBytes.Length];
        Array.Copy(keyBytes, keyedBytes, keyBytes.Length);
        Array.Copy(passwordBytes, 0, keyedBytes, keyBytes.Length, passwordBytes.Length);

        if (format == MembershipPasswordFormat.Encrypted)
        {
            byte[] encryptedBytes = EncryptPassword(passwordBytes);
            return Convert.ToBase64String(encryptedBytes);
        }
        else if (format == MembershipPasswordFormat.Hashed)
            return HashPasswordBytes(keyBytes, keyedBytes);
        else
            throw new ProviderException(Resources.UnsupportedPasswordFormat);
    }

    private void UpdateFailureCount(int userId, string failureType, MySqlConnection connection)
    {
        MySqlCommand cmd = new MySqlCommand(
            @"SELECT FAILEDPASSWORDATTEMPTCOUNT, 
                FAILEDPASSWORDATTEMPTWINDOWSTART, FAILEDPASSWORDANSWERATTEMPTCOUNT, 
                FAILEDPASSWORDANSWERATTEMPTWINDOWSTART FROM MY_ASPNET_MEMBERSHIP 
                WHERE USERID=@USERID", connection);
        cmd.Parameters.AddWithValue("@USERID", userId);

        DateTime windowStart = new DateTime();
        int failureCount = 0;
        try
        {
            using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
            {
                if (!reader.HasRows)
                    throw new ProviderException(Resources.UnableToUpdateFailureCount);

                reader.Read();
                if (failureType == "Password")
                {
                    failureCount = reader.GetInt32(0);
                    windowStart = reader.GetDateTime(1);
                }
                if (failureType == "PasswordAnswer")
                {
                    failureCount = reader.GetInt32(2);
                    windowStart = reader.GetDateTime(3);
                }
            }

            DateTime windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);
            if (failureCount == 0 || DateTime.Now > windowEnd)
            {
                if (failureType == "Password")
                {
                    cmd.CommandText =
                        @"UPDATE MY_ASPNET_MEMBERSHIP 
                            SET FAILEDPASSWORDATTEMPTCOUNT = @COUNT, 
                            FAILEDPASSWORDATTEMPTWINDOWSTART = @WINDOWSTART 
                            WHERE USERID=@USERID";
                }
                if (failureType == "PasswordAnswer")
                {
                    cmd.CommandText =
                        @"UPDATE MY_ASPNET_MEMBERSHIP 
                            SET FAILEDPASSWORDANSWERATTEMPTCOUNT = @COUNT, 
                            FAILEDPASSWORDANSWERATTEMPTWINDOWSTART = @WINDOWSTART 
                            WHERE USERID = @USERID";
                }
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@COUNT", 1);
                cmd.Parameters.AddWithValue("@WINDOWSTART", DateTime.Now);
                cmd.Parameters.AddWithValue("@USERID", userId);
                if (cmd.ExecuteNonQuery() < 0)
                    throw new ProviderException(Resources.UnableToUpdateFailureCount);
            }
            else
            {
                failureCount += 1;
                if (failureCount >= MaxInvalidPasswordAttempts)
                {
                    cmd.CommandText =
                        @"UPDATE MY_ASPNET_MEMBERSHIP SET ISLOCKEDOUT = @ISLOCKEDOUT, 
                            LASTLOCKEDOUTDATE = @LASTLOCKEDOUTDATE WHERE USERID=@USERID";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ISLOCKEDOUT", true);
                    cmd.Parameters.AddWithValue("@LASTLOCKEDOUTDATE", DateTime.Now);
                    cmd.Parameters.AddWithValue("@USERID", userId);
                    if (cmd.ExecuteNonQuery() < 0)
                        throw new ProviderException(Resources.UnableToLockOutUser);
                }
                else
                {
                    if (failureType == "Password")
                    {
                        cmd.CommandText =
                            @"UPDATE MY_ASPNET_MEMBERSHIP 
                                SET FAILEDPASSWORDATTEMPTCOUNT = @COUNT WHERE USERID=@USERID";
                    }
                    if (failureType == "PasswordAnswer")
                    {
                        cmd.CommandText =
                            @"UPDATE MY_ASPNET_MEMBERSHIP 
                                SET FAILEDPASSWORDANSWERATTEMPTCOUNT = @COUNT 
                                WHERE USERID=@USERID";
                    }
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@COUNT", failureCount);
                    cmd.Parameters.AddWithValue("@USERID", userId);
                    if (cmd.ExecuteNonQuery() < 0)
                        throw new ProviderException("Unable to update failure count.");
                }
            }
        }
        catch (MySqlException e)
        {
            if (WriteExceptionsToEventLog)
                WriteToEventLog(e, "UpdateFailureCount");
            throw new ProviderException(exceptionMessage, e);
        }
    }

    private bool CheckPassword(string password, string dbpassword,
        string passwordKey, MembershipPasswordFormat format)
    {
        password = EncodePassword(password, passwordKey, format);
        return password == dbpassword;
    }

    private void GetPasswordInfo(MySqlConnection connection, int userId,
        out string passwordKey, out MembershipPasswordFormat passwordFormat)
    {
        MySqlCommand cmd = new MySqlCommand(
            @"SELECT PASSWORDKEY, PASSWORDFORMAT FROM MY_ASPNET_MEMBERSHIP WHERE
                  USERID=@USERID", connection);
        cmd.Parameters.AddWithValue("@USERID", userId);
        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            reader.Read();
            passwordKey = reader.GetString(reader.GetOrdinal("PasswordKey"));
            passwordFormat = (MembershipPasswordFormat)reader.GetByte(
                reader.GetOrdinal("PasswordFormat"));
        }
    }

    private MembershipUserCollection GetUsers(string username, string email,
        int pageIndex, int pageSize, out int totalRecords)
    {
        MembershipUserCollection users = new MembershipUserCollection();
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;

                string sql = @"SELECT SQL_CALC_FOUND_ROWS U.NAME,M.* FROM MY_ASPNET_USERS U
                        JOIN MY_ASPNET_MEMBERSHIP M ON M.USERID=U.ID 
                        WHERE U.APPLICATIONID=@APPID";

                if (username != null)
                {
                    sql += " AND U.NAME LIKE @NAME";
                    cmd.Parameters.AddWithValue("@NAME", username);
                }
                else if (email != null)
                {
                    sql += " AND M.EMAIL LIKE @EMAIL";
                    cmd.Parameters.AddWithValue("@EMAIL", email);
                }
                sql += " ORDER BY U.ID ASC LIMIT {0},{1}";
                cmd.CommandText = String.Format(sql, pageIndex * pageSize, pageSize);
                cmd.Parameters.AddWithValue("@APPID", app.FetchId(connection));
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        users.Add(GetUserFromReader(reader));
                }
                cmd.CommandText = "SELECT FOUND_ROWS()";
                cmd.Parameters.Clear();
                totalRecords = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return users;
        }
        catch (MySqlException e)
        {
            if (WriteExceptionsToEventLog)
                WriteToEventLog(e, "GetUsers");
            throw new ProviderException(exceptionMessage);
        }
    }

    private void ValidateQA(string question, string answer)
    {
        if (RequiresQuestionAndAnswer && String.IsNullOrEmpty(question))
            throw new ArgumentException(Resources.PasswordQuestionInvalid);
        if (RequiresQuestionAndAnswer && String.IsNullOrEmpty(answer))
            throw new ArgumentException(Resources.PasswordAnswerInvalid);
    }

    private bool ValidatePassword(string password, string argumentName, bool throwExceptions)
    {
        string exceptionString = null;
        object correctValue = MinRequiredPasswordLength;

        if (password.Length < MinRequiredPasswordLength)
            exceptionString = Resources.PasswordNotLongEnough;
        else
        {
            int count = 0;
            foreach (char c in password)
                if (!char.IsLetterOrDigit(c))
                    count++;
            if (count < MinRequiredNonAlphanumericCharacters)
                exceptionString = Resources.NotEnoughNonAlphaNumericInPwd;
            correctValue = MinRequiredNonAlphanumericCharacters;
        }

        if (exceptionString != null)
        {
            if (throwExceptions)
                throw new ArgumentException(
                    string.Format(exceptionString, argumentName, correctValue),
                    argumentName);
            else
                return false;
        }

        if (PasswordStrengthRegularExpression.Length > 0)
            if (!Regex.IsMatch(password, PasswordStrengthRegularExpression))
                return false;

        return true;
    }

    #endregion
  }
}

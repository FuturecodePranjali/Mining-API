using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Net;
/// <summary>
/// Summary description for dal
/// </summary>
public class ClsDataAccess
{
    #region Public Constructor , takes one argument as sqlconnection string
    /// <summary>
    /// Public Constructor , takes one argument as sqlconnection string
    /// </summary>
    /// <param name="SQLconnectionString">SQLconnectionString</param>
    public ClsDataAccess(string SQLconnectionString)
    {
        ConnectionString1 = SQLconnectionString;
    }
    #endregion

    #region Dataset Method
    /// <summary>
    /// 
    /// </summary>
    /// <param name="SqlSelectCommamnd">SQL SELECT COMMAND</param>
    /// <returns>DataSet</returns>
    public DataSet GetDataSet(string SqlSelectCommamnd)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        SqlDataAdapter adp = new SqlDataAdapter(SqlSelectCommamnd, conc);
        try
        {
            if (conc.State == ConnectionState.Closed)
                conc.Open();
            DataSet ds = new DataSet();
            adp.Fill(ds);
            adp.Dispose();
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            adp.Dispose();
            conc.Close();
        }
    }
    #endregion

    #region "Data Table"
    /// <summary>
    /// Get datatable
    /// </summary>
    /// <param name="SqlSelectCommamnd">Sql Select Command</param>
    /// <param name="dataSettableName">DataTable name</param>
    /// <returns></returns>
    public DataTable GetDataSet(string SqlSelectCommamnd, string dataSettableName)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        SqlDataAdapter adp = new SqlDataAdapter(SqlSelectCommamnd, conc);
        try
        {
            if (conc.State == ConnectionState.Closed)
            conc.Open();
            DataTable dt = new DataTable(dataSettableName);
            adp.Fill(dt);
            adp.Dispose();
            return dt;
        }
        catch
        {
            return null;
        }
        finally
        {
            adp.Dispose();
            conc.Close();
        }
    }
    #endregion

    #region SqlDataReader, If Any error occured then it will returns NULL


    /// <summary>
    /// Takes One parameters as valid sql select command and returns SqlDataReader, in closed connection behavior mode
    /// </summary>
    /// <param name="SqlSelectCommand"></param>
    /// <returns>SqlDataReader, If Any error occured then it will returns NULL</returns>
    /// <exception cref="">Returns Null</exception>
    public SqlDataReader GetSqlDataReader(string SqlSelectCommand)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand(SqlSelectCommand, conc);
            if (conc.State == ConnectionState.Closed)
                conc.Open();
            SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return sdr;
        }
        catch (SqlException ex)
        {
            conc.Close();
            return null;
        }
    }
    #endregion

    ///////  second database records connection

    #region SqlDataReader_second, If Any error occured then it will returns NULL
    /// <summary>
    /// Takes One parameters as valid sql select command and returns SqlDataReader, in closed connection behavior mode
    /// </summary>
    /// <param name="SqlSelectCommand"></param>
    /// <returns>SqlDataReader, If Any error occured then it will returns NULL</returns>
    /// <exception cref="">Returns Null</exception>
    public SqlDataReader GetSqlDataReader_second(string SqlSelectCommand)
    {
        SqlConnection conc_old = new SqlConnection(ConfigurationManager.AppSettings["conc_old"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand(SqlSelectCommand, conc_old);
            if (conc_old.State == ConnectionState.Closed)
                conc_old.Open();
            SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return sdr;
        }
        catch (SqlException ex)
        {
            conc_old.Close();
            return null;
        }
    }
    #endregion

    #region Executes Non-Query Statements,Exeception :returns Exception Code, Returns int
    /// <summary>
    /// Executes Non-Query Statements,Exeception :returns Exception Code, Returns int
    /// </summary>
    /// <param name="SqlDMLcmd">String Sql DML Command</param>
    /// <returns>1 if Code Query is Executed Successfully Other wise returns Sql ErrorCode</returns>
    public int ExecuteDML(string SqlDMLcmd)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            int returnval = 0;
            SqlCommand cmd = new SqlCommand(SqlDMLcmd, conc);
            if (conc.State == ConnectionState.Closed)
                conc.Open();
            returnval = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conc.Close();
            return returnval;
        }
        catch (SqlException ex)
        {
            return ex.ErrorCode;
        }
        finally
        {
            conc.Close();
        }
    }
    #endregion

    #region ExecuteScalerQuery(string strSqlSelectCommand)

    /// <summary>
    /// Takes one Parameter as sql select command and retruns only one column and one row
    /// </summary>
    /// <param name="strSqlSelectCommand">Valid SqlSelect Command , Only For Aggrigate Functions</param>
    /// <returns>Aggrigate Value(String)</returns>
    /// <exception cref="">Returns Error Message</exception>
    public string ExecuteScalerQuery(string strSqlSelectCommand)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            string returnval = string.Empty;
            SqlCommand cmd = new SqlCommand(strSqlSelectCommand, conc);
            if (conc.State == ConnectionState.Closed)
                conc.Open();
            returnval = cmd.ExecuteScalar().ToString();
            cmd.Dispose();
            conc.Close();
            return returnval;
        }
        catch (SqlException ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            conc.Close();
        }
    }
    #endregion

    #region New Member Registration Method

    /// <summary>
    /// New User Registration
    /// </summary>
    /// <param name="Cust_SponserID">Invitation ID</param>
    /// <param name="Email">E-mailID</param>
    /// <param name="Cust_Address">Address</param>
    /// <param name="Cust_Answer">Security Answer</param>
    /// <param name="Cust_Question">Security Question</param>
    /// <param name="Cust_City">City</param>
    /// <param name="Cust_State">State</param>
    /// <param name="Cust_Country">Country</param>
    /// <param name="Cust_Password">Password</param>
    /// <param name="Cust_Title">Title</param>
    /// <param name="Cust_Name">Name</param>
    /// <param name="Cust_Gender">Gender</param>
    /// <param name="Cust_FatherName">Mother name</param>
    /// <param name="Cust_DOB">Date of Birth</param>
    /// <param name="Cust_Pincode">Zip Code</param>
    /// <param name="Cust_mobileNo">Mobile Np</param>
    /// <param name="Cust_nominee">Nominee Name</param>
    /// <param name="Cust_Relation">Relation</param>
    /// <param name="Cust_Package">Joing package</param>
    /// <param name="Cust_Location">Position</param>
    /// <param name="Cust_TempPinID">Used pinno</param>
    /// <param name="PayMode">payment mode</param>
    /// <param name="NewID">Returns Newly generated ID</param>
    /// <param name="ErrCode">Error Message</param>
    /// <returns>INT</returns>
    public int InsertCustRecords(string Cust_SponserID, string Email, string Cust_Address, string Cust_Answer, string Cust_Question, string Cust_City, string Cust_State, string Cust_Country, string Cust_Password, string Cust_Title, string Cust_Name, string Cust_Gender, string Cust_FatherName, DateTime Cust_DOB, string Cust_Pincode, string Cust_mobileNo, string Cust_nominee, string Cust_Relation, string Cust_Package, string Cust_Location, string Cust_TempPinID, int PayMode, string Cust_BankName, string Cust_BankAcc, string Cust_BankIFSC, string Cust_BankBranch, string Cust_PanID, string _custusername, out string NewID, out string ErrCode)

    {

        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {

            SqlCommand cmd = new SqlCommand("[dbo].[InsertNewMember_virthnext_onlyprereg]", conc);
            //SqlCommand cmd = new SqlCommand("[dbo].[reliable_InsertNewMember]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Cust_SponserID", Cust_SponserID));
            cmd.Parameters.Add(new SqlParameter("@Email", Email));
            cmd.Parameters.Add(new SqlParameter("@Cust_Address", Cust_Address));
            cmd.Parameters.Add(new SqlParameter("@Cust_Answer", Cust_Answer));
            cmd.Parameters.Add(new SqlParameter("@Cust_Question", Cust_Question));
            cmd.Parameters.Add(new SqlParameter("@Cust_City", Cust_City));
            cmd.Parameters.Add(new SqlParameter("@Cust_State", Cust_State));
            cmd.Parameters.Add(new SqlParameter("@Cust_Country", Cust_Country));
            cmd.Parameters.Add(new SqlParameter("@Cust_Password", Cust_Password));
            cmd.Parameters.Add(new SqlParameter("@Cust_Title", Cust_Title));
            cmd.Parameters.Add(new SqlParameter("@Cust_Name", Cust_Name));
            cmd.Parameters.Add(new SqlParameter("@Cust_Gender", Cust_Gender));
            cmd.Parameters.Add(new SqlParameter("@Cust_FatherName", Cust_FatherName));
            cmd.Parameters.Add(new SqlParameter("@Cust_DOB", Cust_DOB));
            cmd.Parameters.Add(new SqlParameter("@Cust_Pincode", Cust_Pincode));
            cmd.Parameters.Add(new SqlParameter("@Cust_mobileNo", Cust_mobileNo));
            cmd.Parameters.Add(new SqlParameter("@Cust_nominee", Cust_nominee));
            cmd.Parameters.Add(new SqlParameter("@Cust_Relation", Cust_Relation));
            cmd.Parameters.Add(new SqlParameter("@Cust_Package", Cust_Package));
            cmd.Parameters.Add(new SqlParameter("@Cust_Location", Cust_Location));
            cmd.Parameters.Add(new SqlParameter("@Cust_TempPinID", Cust_TempPinID));

            cmd.Parameters.Add(new SqlParameter("@PayMode", PayMode));

            // here bank information use for transfer Pin Value Kindly refer from registration page textbox value
            //dr["Cust_BankName"] = txt_security_pinslno.Text;
            //dr["Cust_BankAcc"] = txt_security_pinno.Text;

            cmd.Parameters.Add(new SqlParameter("@Cust_BankName", Cust_BankName));
            cmd.Parameters.Add(new SqlParameter("@Cust_BankAcc", Cust_BankAcc));
            cmd.Parameters.Add(new SqlParameter("@Cust_BankIFSC", Cust_BankIFSC));
            cmd.Parameters.Add(new SqlParameter("@Cust_BankBranch", Cust_BankBranch));
            cmd.Parameters.Add(new SqlParameter("@Cust_PanID", Cust_PanID));
            cmd.Parameters.Add(new SqlParameter("@custusername", _custusername));

            //cmd.Parameters.Add(new SqlParameter("@e_wallet_userid", e_wallet_userid));
            //cmd.Parameters.Add(new SqlParameter("@e_wallet_userid_password", e_wallet_userid_password));
            //cmd.Parameters.Add(new SqlParameter("@e_wallet_userid_chargeamt", e_wallet_userid_chargeamt));

            SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.Float);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrCode = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
            SqlParaErrCode.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrCode).Value = "";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int order = cmd.ExecuteNonQuery();
            double NewID1 = (double)SqlParaNewID.Value;
            ErrCode = (string)SqlParaErrCode.Value;
            cmd.Dispose();
            conc.Close();
            NewID = NewID1.ToString();
            return order;

        }
        catch (Exception ex)
        {
            NewID = "0";
            ErrCode = ex.Message.ToString();
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    //#region New Member Registration Method

    ///// <summary>
    ///// New User Registration
    ///// </summary>
    ///// <param name="Cust_SponserID">Invitation ID</param>
    ///// <param name="Email">E-mailID</param>
    ///// <param name="Cust_Address">Address</param>
    ///// <param name="Cust_Answer">Security Answer</param>
    ///// <param name="Cust_Question">Security Question</param>
    ///// <param name="Cust_City">City</param>
    ///// <param name="Cust_State">State</param>
    ///// <param name="Cust_Country">Country</param>
    ///// <param name="Cust_Password">Password</param>
    ///// <param name="Cust_Title">Title</param>
    ///// <param name="Cust_Name">Name</param>
    ///// <param name="Cust_Gender">Gender</param>
    ///// <param name="Cust_FatherName">Mother name</param>
    ///// <param name="Cust_DOB">Date of Birth</param>
    ///// <param name="Cust_Pincode">Zip Code</param>
    ///// <param name="Cust_mobileNo">Mobile Np</param>
    ///// <param name="Cust_nominee">Nominee Name</param>
    ///// <param name="Cust_Relation">Relation</param>
    ///// <param name="Cust_Package">Joing package</param>
    ///// <param name="Cust_Location">Position</param>
    ///// <param name="Cust_TempPinID">Used pinno</param>
    ///// <param name="PayMode">payment mode</param>
    ///// <param name="NewID">Returns Newly generated ID</param>
    ///// <param name="ErrCode">Error Message</param>
    ///// <returns>INT</returns>
    //public int InsertCustRecords(string Cust_SponserID, string Email, string Cust_Address, string Cust_Answer, string Cust_Question, string Cust_City, string Cust_State, string Cust_Country, string Cust_Password, string Cust_Title, string Cust_Name, string Cust_Gender, string Cust_FatherName, DateTime Cust_DOB, string Cust_Pincode, string Cust_mobileNo, string Cust_nominee, string Cust_Relation, string Cust_Package, string Cust_Location, string Cust_TempPinID, int PayMode, string Cust_BankName, string Cust_BankAcc, string Cust_BankIFSC, string Cust_BankBranch, string Cust_PanID, string e_wallet_userid, string e_wallet_userid_password, string  e_wallet_userid_chargeamt, out string NewID, out string ErrCode)
    //{

    //    SqlConnection conc = new SqlConnection(ConnectionStringValue);
    //    try
    //    {   
    //        SqlCommand cmd = new SqlCommand("[dbo].[lorgan_InsertNewMember]", conc);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.Add(new SqlParameter("@Cust_SponserID", Cust_SponserID));
    //        cmd.Parameters.Add(new SqlParameter("@Email", Email));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_Address", Cust_Address));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_Answer", Cust_Answer));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_Question", Cust_Question));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_City", Cust_City));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_State", Cust_State));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_Country", Cust_Country));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_Password", Cust_Password));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_Title", Cust_Title));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_Name", Cust_Name));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_Gender", Cust_Gender));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_FatherName", Cust_FatherName));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_DOB", Cust_DOB));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_Pincode", Cust_Pincode));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_mobileNo", Cust_mobileNo));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_nominee", Cust_nominee));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_Relation", Cust_Relation));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_Package", Cust_Package));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_Location", Cust_Location));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_TempPinID", Cust_TempPinID));
    //        cmd.Parameters.Add(new SqlParameter("@PayMode", PayMode));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_BankName", Cust_BankName));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_BankAcc", Cust_BankAcc));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_BankIFSC", Cust_BankIFSC));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_BankBranch", Cust_BankBranch));
    //        cmd.Parameters.Add(new SqlParameter("@Cust_PanID", Cust_PanID));

    //        cmd.Parameters.Add(new SqlParameter("@e_wallet_userid", e_wallet_userid));
    //        cmd.Parameters.Add(new SqlParameter("@e_wallet_userid_password", e_wallet_userid_password));
    //        cmd.Parameters.Add(new SqlParameter("@e_wallet_userid_chargeamt", e_wallet_userid_chargeamt));

    //        SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.Float);
    //        SqlParaNewID.Direction = ParameterDirection.Output;
    //        cmd.Parameters.Add(SqlParaNewID).Value = 0;

    //        SqlParameter SqlParaErrCode = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
    //        SqlParaErrCode.Direction = ParameterDirection.Output;
    //        cmd.Parameters.Add(SqlParaErrCode).Value = "";

    //        if (conc.State == ConnectionState.Closed)
    //            conc.Open();
    //        int order = cmd.ExecuteNonQuery();
    //        double NewID1 = (double)SqlParaNewID.Value;
    //        ErrCode = (string)SqlParaErrCode.Value;
    //        cmd.Dispose();
    //        conc.Close();
    //        NewID = NewID1.ToString();
    //        return order;

    //    }
    //    catch (Exception ex)
    //    {
    //        NewID = "0";
    //        ErrCode = ex.Message.ToString();
    //        return 0;
    //    }
    //    finally
    //    {
    //        conc.Close();
    //    }
    //}

    //#endregion

    #region New Member Registration Method-2

    /// <summary>
    /// Takes 31 Parameter And Executes New Join  Process 
    /// </summary>
    /// <param name="Cust_SponserID">Introducer Or SponserID</param>
    /// <param name="Email">Customer E-Mail Address</param>
    /// <param name="Entry_Date">Current DateTime</param>
    /// <param name="Cust_Address">Customer Address</param>
    /// <param name="Cust_Answer">Security Answer</param>
    /// <param name="Cust_Question">Security Question</param>
    /// <param name="Cust_City">Customers City</param>
    /// <param name="Cust_Country">Customer State</param>
    /// <param name="Cust_Password">Login Password</param>
    /// <param name="Cust_Title">Name Title</param>
    /// <param name="Cust_Name">Customer First Name</param>
    /// <param name="Cust_Lastname">Customer Last Name</param>
    /// <param name="Cust_Gender">Martial Status Or Company Type</param>
    /// <param name="Cust_FatherName">Customer Father Name</param>
    /// <param name="Cust_DOB">Customer Date of Birth</param>
    /// <param name="Cust_Pincode">Customer Pincode</param>
    /// <param name="Cust_PhoneNo">Customer Phone No</param>
    /// <param name="Cust_mobileNo">Customer Mobile No</param>
    /// <param name="Cust_nominee">Customer Nominee Name</param>
    /// <param name="Cust_Relation">Customers Relation With nominee</param>
    /// <param name="Cust_PanID">Customer PanID</param>
    /// <param name="Cust_Package">Customer Joining package</param>
    /// <param name="Cust_Location">Joining Position according to sponserID</param>
    /// <param name="Cust_TempPinID">Joining PINNO</param>
    /// <param name="Cust_BankName">Customers bank Name where his/her account exists</param>
    /// <param name="Cust_BankAdd">Customers bank IFSC Code</param>
    /// <param name="Cust_BankBranch">Customers bank Branch</param>
    /// <param name="Cust_BankAcc">Customers Bank Account no</param>
    /// <param name="plannerId">Paymentmode must be zero(0) if Mode is ECS else 1 </param>
    /// <param name="Product_category">Joining package Category eg. BASIC,STANDARD etc</param>
    /// <param name="NewID">Output type parameter, Which returns Newly Generated Customer ID</param>
    /// <param name="ErrCode">Success if Execution was sucessful else returns Error Messages</param>
    /// <returns>Status Code:1 if Successful else 0</returns>
    /// <exception cref="">Returns Exception message with Out Parameter</exception>
    public int InsertCustjoining(string Cust_SponserID, string Email, DateTime Entry_Date, string Cust_Address, string Cust_Answer, string Cust_Question, string Cust_City, string Cust_Country, string Cust_Password, string Cust_Title, string Cust_Name, string Cust_Lastname, string Cust_Gender, string Cust_FatherName, DateTime Cust_DOB, string Cust_Pincode, string Cust_PhoneNo, string Cust_mobileNo, string Cust_nominee, string Cust_Relation, string Cust_PanID, string Cust_Package, string Cust_Location, string Cust_TempPinID, string Cust_BankName, string Cust_BankAdd, string Cust_BankBranch, string Cust_BankAcc, int plannerId, string Product_category, out string NewID, out string ErrCode)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[spZondadailyInsertNewMember]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Cust_SponserID", Cust_SponserID));
            cmd.Parameters.Add(new SqlParameter("@Email", Email));
            cmd.Parameters.Add(new SqlParameter("@Entry_Date", Entry_Date));
            cmd.Parameters.Add(new SqlParameter("@Cust_Address", Cust_Address));
            cmd.Parameters.Add(new SqlParameter("@Cust_Answer", Cust_Answer));
            cmd.Parameters.Add(new SqlParameter("@Cust_Question", Cust_Question));
            cmd.Parameters.Add(new SqlParameter("@Cust_City", Cust_City));
            cmd.Parameters.Add(new SqlParameter("@Cust_Country", Cust_Country));
            cmd.Parameters.Add(new SqlParameter("@Cust_Password", Cust_Password));
            cmd.Parameters.Add(new SqlParameter("@Cust_Title", Cust_Title));
            cmd.Parameters.Add(new SqlParameter("@Cust_Name", Cust_Name));
            cmd.Parameters.Add(new SqlParameter("@Cust_Lastname", Cust_Lastname));
            cmd.Parameters.Add(new SqlParameter("@Cust_Gender", Cust_Gender));
            cmd.Parameters.Add(new SqlParameter("@Cust_FatherName", Cust_FatherName));
            cmd.Parameters.Add(new SqlParameter("@Cust_DOB", Cust_DOB));
            cmd.Parameters.Add(new SqlParameter("@Cust_Pincode", Cust_Pincode));
            cmd.Parameters.Add(new SqlParameter("@Cust_PhoneNo", Cust_PhoneNo));
            cmd.Parameters.Add(new SqlParameter("@Cust_mobileNo", Cust_mobileNo));
            cmd.Parameters.Add(new SqlParameter("@Cust_nominee", Cust_nominee));
            cmd.Parameters.Add(new SqlParameter("@Cust_Relation", Cust_Relation));
            cmd.Parameters.Add(new SqlParameter("@Cust_PanID", Cust_PanID));
            cmd.Parameters.Add(new SqlParameter("@Cust_Package", Cust_Package));
            cmd.Parameters.Add(new SqlParameter("@Cust_Location", Cust_Location));
            cmd.Parameters.Add(new SqlParameter("@Cust_TempPinID", Cust_TempPinID));
            cmd.Parameters.Add(new SqlParameter("@Cust_BankName", Cust_BankName));
            cmd.Parameters.Add(new SqlParameter("@Cust_BankAdd", Cust_BankAdd));
            cmd.Parameters.Add(new SqlParameter("@Cust_BankBranch", Cust_BankBranch));
            cmd.Parameters.Add(new SqlParameter("@Cust_BankAcc", Cust_BankAcc));
            cmd.Parameters.Add(new SqlParameter("@plannerId", plannerId));
            cmd.Parameters.Add(new SqlParameter("@Product_category", Product_category));

            SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.Float);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrCode = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
            SqlParaErrCode.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrCode).Value = "";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int order = cmd.ExecuteNonQuery();
            double NewID1 = (double)SqlParaNewID.Value;
            ErrCode = (string)SqlParaErrCode.Value;
            cmd.Dispose();
            conc.Close();
            NewID = NewID1.ToString();
            return order;
        }
        catch (Exception ex)
        {
            NewID = "0";
            ErrCode = ex.Message.ToString();
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    #region New Member AlertPay Registration Method

    /// <summary>
    /// New User Registration
    /// </summary>
    /// <param name="Cust_SponserID">Invitation ID</param>
    /// <param name="Email">E-mailID</param>
    /// <param name="Cust_Address">Address</param>
    /// <param name="Cust_Answer">Security Answer</param>
    /// <param name="Cust_Question">Security Question</param>
    /// <param name="Cust_City">City</param>
    /// <param name="Cust_State">State</param>
    /// <param name="Cust_Country">Country</param>
    /// <param name="Cust_Password">Password</param>
    /// <param name="Cust_Title">Title</param>
    /// <param name="Cust_Name">Name</param>
    /// <param name="Cust_Gender">Gender</param>
    /// <param name="Cust_FatherName">Mother name</param>
    /// <param name="Cust_DOB">Date of Birth</param>
    /// <param name="Cust_Pincode">Zip Code</param>
    /// <param name="Cust_mobileNo">Mobile Np</param>
    /// <param name="Cust_nominee">Nominee Name</param>
    /// <param name="Cust_Relation">Relation</param>
    /// <param name="Cust_Package">Joing package</param>
    /// <param name="Cust_Location">Position</param>
    /// <param name="Cust_TempPinID">Used pinno</param>
    /// <param name="PayMode">payment mode</param>
    /// <param name="NewID">Returns Newly generated ID</param>
    /// <param name="ErrCode">Error Message</param>
    /// <returns>INT</returns>
    public int InsertalertCustRecords(string Cust_SponserID, string Email, string Cust_Address, string Cust_Answer, string Cust_Question, string Cust_City, string Cust_State, string Cust_Country, string Cust_Password, string Cust_Title, string Cust_Name, string custlastname, string Cust_Gender, string Cust_FatherName, DateTime Cust_DOB, string Cust_Pincode, string Cust_PhoneNo, string Cust_mobileNo, string Cust_nominee, string Cust_Relation, string _cust_panid, string Cust_Package, string Cust_Location, string Cust_TempPinID, int PayMode, string Alertpayacc, string Alertpayaccname, string depositdate, string tranid, out string NewID, out string ErrCode)
    {

        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[spinserttempcustrecords]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Cust_SponserID", Cust_SponserID));
            cmd.Parameters.Add(new SqlParameter("@Email", Email));
            cmd.Parameters.Add(new SqlParameter("@Cust_Address", Cust_Address));
            cmd.Parameters.Add(new SqlParameter("@Cust_Answer", Cust_Answer));
            cmd.Parameters.Add(new SqlParameter("@Cust_Question", Cust_Question));
            cmd.Parameters.Add(new SqlParameter("@Cust_City", Cust_City));
            cmd.Parameters.Add(new SqlParameter("@Cust_State", Cust_State));
            cmd.Parameters.Add(new SqlParameter("@Cust_Country", Cust_Country));
            cmd.Parameters.Add(new SqlParameter("@Cust_Password", Cust_Password));
            cmd.Parameters.Add(new SqlParameter("@Cust_Title", Cust_Title));
            cmd.Parameters.Add(new SqlParameter("@Cust_Name", Cust_Name));
            cmd.Parameters.Add(new SqlParameter("@Cust_Lastname", custlastname));
            cmd.Parameters.Add(new SqlParameter("@Cust_Gender", Cust_Gender));
            cmd.Parameters.Add(new SqlParameter("@Cust_FatherName", Cust_FatherName));
            cmd.Parameters.Add(new SqlParameter("@Cust_DOB", Cust_DOB));
            cmd.Parameters.Add(new SqlParameter("@Cust_Pincode", Cust_Pincode));
            cmd.Parameters.Add(new SqlParameter("@Cust_PhoneNo", Cust_PhoneNo));
            cmd.Parameters.Add(new SqlParameter("@Cust_mobileNo", Cust_mobileNo));
            cmd.Parameters.Add(new SqlParameter("@Cust_nominee", Cust_nominee));
            cmd.Parameters.Add(new SqlParameter("@Cust_Relation", Cust_Relation));
            cmd.Parameters.Add(new SqlParameter("@Cust_PanID", _cust_panid));
            cmd.Parameters.Add(new SqlParameter("@Cust_Package", Cust_Package));
            cmd.Parameters.Add(new SqlParameter("@Cust_Location", Cust_Location));
            cmd.Parameters.Add(new SqlParameter("@PayMode", PayMode));
            cmd.Parameters.Add(new SqlParameter("@alertpayacc", Alertpayacc));
            cmd.Parameters.Add(new SqlParameter("@accholdername", Alertpayaccname));
            cmd.Parameters.Add(new SqlParameter("@depositdate", depositdate));
            cmd.Parameters.Add(new SqlParameter("@transactionid", tranid));

            SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.Float);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrCode = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
            SqlParaErrCode.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrCode).Value = "";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int order = cmd.ExecuteNonQuery();
            double NewID1 = (double)SqlParaNewID.Value;
            ErrCode = (string)SqlParaErrCode.Value;
            cmd.Dispose();
            conc.Close();
            NewID = NewID1.ToString();
            return order;
        }
        catch (Exception ex)
        {
            NewID = "0";
            ErrCode = ex.Message.ToString();
            return 0;
        }
        finally
        {
            conc.Close();
            conc.Dispose();
        }
    }

    #endregion

    #region buyshare(string cusid, string txt_ews_pws,string m_shareqty, w_wallettype, out int flag, out string errMsg)

    /// <summary>
    /// Topup User Account
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="pcode"></param>
    /// <param name="multiple"></param>
    /// <param name="EwalletID"></param>
    /// <param name="Topupdate"></param>
    /// <param name="flag"></param>
    /// <param name="errMsg"></param>
    /// <returns></returns>
    public int buyshare(string cusid, string txt_ews_pws, string m_shareqty, string Validity, string category, string w_wallettype, string p_code, string btc_amt,string orderno, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[temp_buyshare_hashpower]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cusid", cusid));
            cmd.Parameters.Add(new SqlParameter("@txt_ews_pws", txt_ews_pws));
            cmd.Parameters.Add(new SqlParameter("@m_shareqty", m_shareqty));
            cmd.Parameters.Add(new SqlParameter("@validity", Validity));
            cmd.Parameters.Add(new SqlParameter("@category", category));
            cmd.Parameters.Add(new SqlParameter("@w_wallettype", w_wallettype));
            cmd.Parameters.Add(new SqlParameter("@p_code", p_code));
            cmd.Parameters.Add(new SqlParameter("@btc_amt", btc_amt));
			cmd.Parameters.Add(new SqlParameter("@orderno", orderno));

			SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
    #endregion

    #region loanAccount( string EwalletID, out int flag,  out string errMsg)

    /// <summary>
    /// Topup User Account
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="pcode"></param>
    /// <param name="multiple"></param>
    /// <param name="EwalletID"></param>
    /// <param name="Topupdate"></param>
    /// <param name="flag"></param>
    /// <param name="errMsg"></param>
    /// <returns></returns>
    public int loanAccount(string sessionid, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[TRADEONFAME_RANKCALC]", conc);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@sessionid", sessionid));
            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
    #endregion

    #region Payemnt_addfund_gateway(string gateway, string orderid, string cusidtomerid, string or_curr_amt,string return_status,  out int flag, out string errMsg)

    /// <summary>
    /// Topup User Account
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="pcode"></param>
    /// <param name="multiple"></param>
    /// <param name="EwalletID"></param>
    /// <param name="Topupdate"></param>
    /// <param name="flag"></param>
    /// <param name="errMsg"></param>
    /// <returns></returns>
    public int Payemnt_addfund_gateway(string gateway, string orderid, string cusidtomerid, string or_curr_amt, string return_status, string BTC_Rate, string CoinName, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[Payemnt_addfund_gateway]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@gateway", gateway));
            cmd.Parameters.Add(new SqlParameter("@orderid", orderid));
            cmd.Parameters.Add(new SqlParameter("@cusidtomerid", cusidtomerid));
            cmd.Parameters.Add(new SqlParameter("@or_curr_amt", or_curr_amt));
            cmd.Parameters.Add(new SqlParameter("@return_status", return_status));
            cmd.Parameters.Add(new SqlParameter("@Coin_Rate", BTC_Rate));
            cmd.Parameters.Add(new SqlParameter("@CoinName", CoinName));
            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
    #endregion

    #region Payemnt_callback_gateway(string gateway,string re_order, string re_status, string amount_btc, string amount_usd_val,  string re_userid,  string crypto_curr,  string txno, string confirmed_count, out int flag, out string errMsg)

    /// <summary>
    /// Topup User Account
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="pcode"></param>
    /// <param name="multiple"></param>
    /// <param name="EwalletID"></param>
    /// <param name="Topupdate"></param>
    /// <param name="flag"></param>
    /// <param name="errMsg"></param>
    /// <returns></returns>
    public int Payemnt_callback_gateway(string gateway, string re_orderid, string re_status, string amount_btc, string amount_usd_val, string re_userid, string crypto_curr, string txno, string confirmed_count, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {

            //string gateway,string re_order, string re_status, string amount_btc, string amount_usd_val,  string re_userid,  string crypto_curr,  string txno, string confirmed_count, out int flag, out string errMsg
            SqlCommand cmd = new SqlCommand("[dbo].[Payment_callback_gateway]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@gateway", gateway));
            cmd.Parameters.Add(new SqlParameter("@re_orderid", re_orderid));
            cmd.Parameters.Add(new SqlParameter("@re_status", re_status));
            cmd.Parameters.Add(new SqlParameter("@amount_btc", amount_btc));
            cmd.Parameters.Add(new SqlParameter("@amount_usd_val", amount_usd_val));
            cmd.Parameters.Add(new SqlParameter("@re_userid", re_userid));
            cmd.Parameters.Add(new SqlParameter("@crypto_curr", crypto_curr));
            cmd.Parameters.Add(new SqlParameter("@txno", txno));
            cmd.Parameters.Add(new SqlParameter("@confirmed_count", confirmed_count));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
    #endregion

    #region topupaccount_advt(string cusid, string pcode,string advtno, int multiple, string EwalletID, DateTime Topupdate, out int flag, out string errMsg)

    /// <summary>
    /// Topup User Account
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="pcode"></param>
    /// <param name="multiple"></param>
    /// <param name="EwalletID"></param>
    /// <param name="Topupdate"></param>
    /// <param name="flag"></param>
    /// <param name="errMsg"></param>
    /// <returns></returns>
    public int topupaccount_advt(string cusid, string pinno, string advtno, string sessionid, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[sptopupaccount_advt]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cusid", cusid));
            cmd.Parameters.Add(new SqlParameter("@pinno", pinno));
            cmd.Parameters.Add(new SqlParameter("@advtno", advtno));
            cmd.Parameters.Add(new SqlParameter("@sessionid", sessionid));
            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
    #endregion

    #region topupAccount_choreservices(string cusid, string pcode,string advtno, int multiple, string EwalletID, DateTime Topupdate, out int flag, out string errMsg)

    /// <summary>
    /// Topup User Account
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="pcode"></param>
    /// <param name="multiple"></param>
    /// <param name="EwalletID"></param>
    /// <param name="Topupdate"></param>
    /// <param name="flag"></param>
    /// <param name="errMsg"></param>
    /// <returns></returns>
    public int topupAccount_choreservices(string cusid, string pinno, string advtno, string sessionid, string m_demandorderno, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[topupAccount_choreservices]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cusid", cusid));
            cmd.Parameters.Add(new SqlParameter("@pinno", pinno));
            cmd.Parameters.Add(new SqlParameter("@advtno", advtno));
            cmd.Parameters.Add(new SqlParameter("@demandorder", m_demandorderno));
            cmd.Parameters.Add(new SqlParameter("@sessionid", sessionid));
            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
    #endregion

    #region topupAccount_advt_sahaj(string cusid, string pcode, int multiple, string EwalletID, DateTime Topupdate, out int flag, out string errMsg)

    /// <summary>
    /// Topup User Account
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="pcode"></param>
    /// <param name="multiple"></param>
    /// <param name="EwalletID"></param>
    /// <param name="Topupdate"></param>
    /// <param name="flag"></param>
    /// <param name="errMsg"></param>
    /// <returns></returns>
    public int topupAccount_advt_sahaj(string cusid, string pinno, string sessionid, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[spActionMedia_topupaccount_advt]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cusid", cusid));
            cmd.Parameters.Add(new SqlParameter("@pinno", pinno));
            cmd.Parameters.Add(new SqlParameter("@sessionid", sessionid));
            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
    #endregion

    #region topupaccount(string cusid, string pcode, int multiple, string EwalletID, DateTime Topupdate, out int flag, out string errMsg)

    /// <summary>
    /// Topup User Account
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="pcode"></param>
    /// <param name="multiple"></param>
    /// <param name="EwalletID"></param>
    /// <param name="Topupdate"></param>
    /// <param name="flag"></param>
    /// <param name="errMsg"></param>
    /// <returns></returns>
    public int topupaccount(string cusid, string pinno, string sessionid, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[topupaccount_rechargemania]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cusid", cusid));
            cmd.Parameters.Add(new SqlParameter("@pinno", pinno));
            cmd.Parameters.Add(new SqlParameter("@sessionid", sessionid));
            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
    #endregion

    #region change_password(string CusID1, string oldpassword, string newPassword, string remark, out string retunmsg,  out int flag)
    /// <summary>
    /// Topup User Account
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="pcode"></param>
    /// <param name="multiple"></param>
    /// <param name="EwalletID"></param>
    /// <param name="Topupdate"></param>
    /// <param name="flag"></param>
    /// <param name="errMsg"></param>
    /// <returns></returns>
    public int change_password(string CusID1, string oldpassword, string newPassword, string remark, out string retunmsg, out int flag)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[update_password]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@sessionid", CusID1));
            cmd.Parameters.Add(new SqlParameter("@old_password", oldpassword));
            cmd.Parameters.Add(new SqlParameter("@new_password", newPassword));
            cmd.Parameters.Add(new SqlParameter("@remark", remark));
            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@retunmsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            retunmsg = Convert.ToString(SqlParaErrMsg.Value);
            return value;

        }
        catch (Exception ex)
        {
            flag = 0;
            retunmsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
    #endregion

    #region topupaccount1(string cusid, string pinno, string sendercomment, string tier,  out int flag, out string errMsg)

    /// <summary>
    /// Topup User Account
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="pcode"></param>
    /// <param name="multiple"></param>
    /// <param name="EwalletID"></param>
    /// <param name="Topupdate"></param>
    /// <param name="flag"></param>
    /// <param name="errMsg"></param>
    /// <returns></returns>
    public int topupaccount1(string cusid, string pinno, string sendercomment, string tier, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[HarDinDouble_topupaccount]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cusid", cusid));
            cmd.Parameters.Add(new SqlParameter("@pinno", pinno));
            cmd.Parameters.Add(new SqlParameter("@sendercomment", sendercomment));
            cmd.Parameters.Add(new SqlParameter("@tier", tier));
            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
    #endregion

    #region Connecting String Property
    /// <summary>
    /// Connecting String Property,That can Be set and get
    /// </summary>
    private string ConnectionString1;

    public string ConnectionStringValue
    {
        get
        {
            return ConnectionString1;
        }
        set
        {
            ConnectionString1 = value;
        }
    }
    #endregion

    #region fundtransfer_clubtomain(string CusID1, string CusID2, string amount, int transferFrom)

    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">ID From</param>
    /// <param name="CusID2">ID to</param>
    /// <param name="amount">Transfer Amount</param>
    /// <returns>Transaction ID</returns>
    public double fundtransfer_clubtomain(string CusID1, string CusID2, string amount, int transferFrom)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            double trID = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[fundtransfer_clubtomain]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@CusID1", CusID1));
            cmd1.Parameters.Add(new SqlParameter("@CusID2", CusID2));
            cmd1.Parameters.Add(new SqlParameter("@amount", amount));
            cmd1.Parameters.Add(new SqlParameter("@transferFrom", transferFrom));
            cmd1.Parameters.Add(new SqlParameter("@trID", 0)).Direction = ParameterDirection.Output;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.ExecuteNonQuery();
            connc.Close();

            trID = Convert.ToDouble(cmd1.Parameters["@trID"].Value);
            cmd1.Dispose();
            return trID;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            connc.Close();
        }

    }

    #endregion

    #region fundtransfer_binarytomain(string CusID1, string CusID2, string amount, int transferFrom)

    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">ID From</param>
    /// <param name="CusID2">ID to</param>
    /// <param name="amount">Transfer Amount</param>
    /// <returns>Transaction ID</returns>
    public double fundtransfer_binarytomain(string CusID1, string CusID2, string amount, int transferFrom)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            double trID = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[fundtransfer_binarytomain]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@CusID1", CusID1));
            cmd1.Parameters.Add(new SqlParameter("@CusID2", CusID2));
            cmd1.Parameters.Add(new SqlParameter("@amount", amount));
            cmd1.Parameters.Add(new SqlParameter("@transferFrom", transferFrom));
            cmd1.Parameters.Add(new SqlParameter("@trID", 0)).Direction = ParameterDirection.Output;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.ExecuteNonQuery();
            connc.Close();

            trID = Convert.ToDouble(cmd1.Parameters["@trID"].Value);
            cmd1.Dispose();
            return trID;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            connc.Close();
        }

    }

    #endregion

    #region InternalFundTransfer(string CusID1, string CusID2, string amount, int transferFrom)


    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">ID From</param>
    /// <param name="CusID2">ID to</param>
    /// <param name="amount">Transfer Amount</param>
    /// <returns>Transaction ID</returns>
    public double InternalFundTransfer(string CusID1, string CusID2, string amount, int transferFrom)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            double trID = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[spActionMedia_TransferInternalFund_ok]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@CusID1", CusID1));
            cmd1.Parameters.Add(new SqlParameter("@CusID2", CusID2));
            cmd1.Parameters.Add(new SqlParameter("@amount", amount));
            cmd1.Parameters.Add(new SqlParameter("@transferFrom", transferFrom));
            cmd1.Parameters.Add(new SqlParameter("@trID", 0)).Direction = ParameterDirection.Output;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.ExecuteNonQuery();
            connc.Close();

            trID = Convert.ToDouble(cmd1.Parameters["@trID"].Value);
            cmd1.Dispose();
            return trID;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            connc.Close();
        }

    }

    #endregion

    #region InternalFundTransfer to own ID(string CusID1, string CusID2, string amount)


    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">ID From</param>
    /// <returns>Transaction ID</returns>
    public double InternalFundTransfer(string CusID1, string amount)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            double trID = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[spActionMedia_TransferInternalFund_toownwallet_ok]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@CusID1", CusID1));
            cmd1.Parameters.Add(new SqlParameter("@amount", amount));
            cmd1.Parameters.Add(new SqlParameter("@trID", 0)).Direction = ParameterDirection.Output;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.ExecuteNonQuery();
            connc.Close();

            trID = Convert.ToDouble(cmd1.Parameters["@trID"].Value);
            cmd1.Dispose();
            return trID;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            connc.Close();
        }

    }

    #endregion

    #region Proctransferfundmainwallettopinwallet to own ID ewallet to pin wallet(string CusID1, string CusID2, string amount)


    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">ID From</param>
    /// <returns>Transaction ID</returns>
    public double Proctransferfundmainwallettopinwallet(string CusID1, string amount)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            double trID = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[transferfundmainwallettopinwallet]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@CusID1", CusID1));
            cmd1.Parameters.Add(new SqlParameter("@amount", amount));
            cmd1.Parameters.Add(new SqlParameter("@trID", 0)).Direction = ParameterDirection.Output;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.ExecuteNonQuery();
            connc.Close();

            trID = Convert.ToDouble(cmd1.Parameters["@trID"].Value);
            cmd1.Dispose();
            return trID;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            connc.Close();
        }

    }

    #endregion

    #region TransferEpin(int qty, string pcode, float amount, string idfrom, string idto,out int flag)

    public int TransferEpin(int qty, string pcode, float amount, string idfrom, string idto, out int flag)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            int trID = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[spActionMedia_transferpinu2u]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@pinqntity", qty));
            cmd1.Parameters.Add(new SqlParameter("@product", pcode));
            cmd1.Parameters.Add(new SqlParameter("@pinamt", amount));
            cmd1.Parameters.Add(new SqlParameter("@idfrom", idfrom));
            cmd1.Parameters.Add(new SqlParameter("@idTo", idto));
            cmd1.Parameters.Add(new SqlParameter("@flag", 0)).Direction = ParameterDirection.Output;
            cmd1.CommandType = CommandType.StoredProcedure;
            trID = cmd1.ExecuteNonQuery();
            connc.Close();

            flag = Convert.ToInt32(cmd1.Parameters["@flag"].Value);
            cmd1.Dispose();
            return trID;
        }
        catch (Exception ex)
        {
            flag = 0;
            return 0;
        }
        finally
        {
            connc.Close();
        }
    }
    #endregion

    #region  public int updateUserPersonalDetails(string cusid, string Cust_City, string Cust_PhoneNo, string Cust_Address, string Cust_State, string Cust_Pincode, string email, string Cust_DOB)

    /// <summary>
    /// update Users Personal Details
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="Cust_City"></param>
    /// <param name="Cust_PhoneNo"></param>
    /// <param name="Cust_Address"></param>
    /// <param name="Cust_State"></param>
    /// <param name="Cust_Pincode"></param>
    /// <param name="email"></param>
    /// <param name="Cust_DOB"></param>
    /// <param name="Cust_Country"></param>
    /// <returns></returns>
    public int updateUserPersonalDetails(string cusid, string Cust_City, string Cust_PhoneNo, string Cust_Address, string Cust_Country, string email, string Cust_DOB)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        //try
        //{
        int trID = 0;
        SqlCommand cmd1 = new SqlCommand("[dbo].[spZondadaily_updateUsersDetails]", connc);
        connc.Open();
        cmd1.Parameters.Add(new SqlParameter("@Cust_City", Cust_City));
        cmd1.Parameters.Add(new SqlParameter("@Cust_PhoneNo", Cust_PhoneNo));
        cmd1.Parameters.Add(new SqlParameter("@Cust_Address", Cust_Address));
        cmd1.Parameters.Add(new SqlParameter("@Cust_State", Cust_Country));
        //cmd1.Parameters.Add(new SqlParameter("@Cust_Pincode", Cust_Pincode));
        cmd1.Parameters.Add(new SqlParameter("@email", email));
        cmd1.Parameters.Add(new SqlParameter("@Cust_DOB", Cust_DOB));
        cmd1.Parameters.Add(new SqlParameter("@cusid", cusid));

        cmd1.CommandType = CommandType.StoredProcedure;
        trID = cmd1.ExecuteNonQuery();
        connc.Close();
        return trID;
        //}
        //catch (Exception ex)
        //{
        //    return 0;
        //}
        //finally
        //{
        //    connc.Close();
        //}
    }
    #endregion

    #region public int updateUsersNominee(string cusid, string name, string Relationship, string City, string State, string Mobile, string Address)

    /// <summary>
    /// Update Nominee Details
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="name"></param>
    /// <param name="Relationship"></param>
    /// <param name="City"></param>
    /// <param name="State"></param>
    /// <param name="Mobile"></param>
    /// <param name="Address"></param>
    /// <param name="Cust_DOB"></param>
    /// <returns></returns>
    public int updateUsersNominee(string cusid, string name, string Relationship, string City, string State, string Mobile, string Address)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            int trID = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[spZondadaily_updateUsersNominee]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@ReferenceID", cusid));
            cmd1.Parameters.Add(new SqlParameter("@name", name));
            cmd1.Parameters.Add(new SqlParameter("@Relationship", Relationship));
            cmd1.Parameters.Add(new SqlParameter("@City", City));
            cmd1.Parameters.Add(new SqlParameter("@State", State));
            cmd1.Parameters.Add(new SqlParameter("@Mobile", Mobile));
            cmd1.Parameters.Add(new SqlParameter("@Address", Address));
            cmd1.CommandType = CommandType.StoredProcedure;
            trID = cmd1.ExecuteNonQuery();
            connc.Close();
            return trID;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            connc.Close();
        }
    }
    #endregion
	
    #region update_mob_appdetail(string cusid, string txt_app_name_m, string txt_mob_app_name_m, string txt_mob_acno_m, string txt_remark_m)

    /// <summary>
    /// Update Users Bank Details
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="Cust_PanID"></param>
    /// <param name="bankachodename"></param>
    /// <param name="Cust_BankBranch"></param>
    /// <param name="BankState"></param>
    /// <param name="Cust_BankName"></param>
    /// <param name="Cust_BankAcc"></param>
    /// <param name="BankCity"></param>
    /// <param name="Cust_BankIFSC"></param>
    /// <returns></returns>
    public int update_mob_appdetail(string cusid, string txt_app_name_m, string txt_mob_app_name_m, string txt_mob_acno_m, string txt_remark_m)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            int trID = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[sp_mobileapp_update]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@cusid", cusid));
            cmd1.Parameters.Add(new SqlParameter("@txt_app_name", txt_app_name_m));
            cmd1.Parameters.Add(new SqlParameter("@txt_mob_app_name", txt_mob_app_name_m));
            cmd1.Parameters.Add(new SqlParameter("@txt_mob_acno", txt_mob_acno_m));
            cmd1.Parameters.Add(new SqlParameter("@txt_remark", txt_remark_m));
            cmd1.CommandType = CommandType.StoredProcedure;
            trID = cmd1.ExecuteNonQuery();
            connc.Close();
            return trID;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            connc.Close();
        }
    }
    #endregion
	
    #region updateUsersBankDetails(string cusid, string Cust_PanID, string bankachodename, string Cust_BankBranch, string BankState, string Cust_BankName, string Cust_BankAcc, string BankCity, string Cust_BankIFSC)

    /// <summary>
    /// Update Users Bank Details
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="Cust_PanID"></param>
    /// <param name="bankachodename"></param>
    /// <param name="Cust_BankBranch"></param>
    /// <param name="BankState"></param>
    /// <param name="Cust_BankName"></param>
    /// <param name="Cust_BankAcc"></param>
    /// <param name="BankCity"></param>
    /// <param name="Cust_BankIFSC"></param>
    /// <returns></returns>
    public int updateUsersBankDetails(string cusid, string Cust_PanID, string bankachodename, string Cust_BankBranch, string BankState, string Cust_BankName, string Cust_BankAcc, string BankCity, string Cust_BankIFSC)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            int trID = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[spZondadaily_updateUsersBankDetails]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@cusid", cusid));
            cmd1.Parameters.Add(new SqlParameter("@Cust_PanID", Cust_PanID));
            cmd1.Parameters.Add(new SqlParameter("@bankachodename", bankachodename));
            cmd1.Parameters.Add(new SqlParameter("@Cust_BankBranch", Cust_BankBranch));
            cmd1.Parameters.Add(new SqlParameter("@BankState", BankState));
            cmd1.Parameters.Add(new SqlParameter("@Cust_BankName", Cust_BankName));
            cmd1.Parameters.Add(new SqlParameter("@Cust_BankAcc", Cust_BankAcc));
            cmd1.Parameters.Add(new SqlParameter("@BankCity", BankCity));
            cmd1.Parameters.Add(new SqlParameter("@Cust_BankIFSC", Cust_BankIFSC));
            cmd1.CommandType = CommandType.StoredProcedure;
            trID = cmd1.ExecuteNonQuery();
            connc.Close();
            return trID;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            connc.Close();
        }
    }
    #endregion

    #region RequestForPtbWidrawl(string cusid, float RqAmount,float percantage, string appwalletno, out string rqno, out string errMsg)

    /// <summary>
    /// PTB Point Widrawl Request
    /// </summary>
    /// <param name="cusid">Request ID</param>
    /// <param name="RqAmount">Request Points</param>
    /// <param name="rqno">Request No</param>
    /// <returns>Request No</returns>
    public int RequestForPtbWidrawl(string cusid, string RqAmount, string percantage, string appwalletno, string address, out string rqno, out string errMsg)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            int ret = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[spActionMedia_RequestPTBWidrawl]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@custid", cusid));
            cmd1.Parameters.Add(new SqlParameter("@RqAmount", RqAmount));
            cmd1.Parameters.Add(new SqlParameter("@percantage", percantage));
            cmd1.Parameters.Add(new SqlParameter("@appwalletno", appwalletno));
            cmd1.Parameters.Add(new SqlParameter("@address", address));
            cmd1.Parameters.Add(new SqlParameter("@rqno", 0)).Direction = ParameterDirection.Output;


            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "0";

            cmd1.CommandType = CommandType.StoredProcedure;
            ret = cmd1.ExecuteNonQuery();
            connc.Close();

            rqno = Convert.ToDouble(cmd1.Parameters["@rqno"].Value).ToString();
            errMsg = (string)SqlParaErrMsg.Value;
            connc.Close();
            return ret;
        }
        catch (Exception ex)
        {
            rqno = ex.Message;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            connc.Close();
        }
    }
    #endregion
	
    #region RequestForPtbWidrawl_1(string cusid, float RqAmount,float Txno, out string rqno, out string errMsg)

    /// <summary>
    /// PTB Point Widrawl Request
    /// </summary>
    /// <param name="cusid">Request ID</param>
    /// <param name="RqAmount">Request Points</param>
    /// <param name="rqno">Request No</param>
    /// <returns>Request No</returns>
    public int RequestForPtbWidrawl_1(string cusid, float RqAmount, float Txno, out string rqno, out string errMsg)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            int ret = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[spwithdraw_helpbonus_1]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@custid", cusid));
            cmd1.Parameters.Add(new SqlParameter("@RqAmount", RqAmount));
            cmd1.Parameters.Add(new SqlParameter("@txnNo", Txno));
            cmd1.Parameters.Add(new SqlParameter("@rqno", 0)).Direction = ParameterDirection.Output;


            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "0";

            cmd1.CommandType = CommandType.StoredProcedure;
            ret = cmd1.ExecuteNonQuery();
            connc.Close();

            rqno = Convert.ToDouble(cmd1.Parameters["@rqno"].Value).ToString();
            errMsg = (string)SqlParaErrMsg.Value;
            connc.Close();
            return ret;
        }
        catch (Exception ex)
        {
            rqno = ex.Message;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            connc.Close();
        }
    }
    #endregion

    #region makecommit(string cusid, string pcode, int multiple, string EwalletID, DateTime Topupdate, out int flag, out string errMsg)

    /// <summary>
    /// Topup User Account
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="pcode"></param>
    /// <param name="multiple"></param>
    /// <param name="EwalletID"></param>
    /// <param name="Topupdate"></param>
    /// <param name="flag"></param>
    /// <param name="errMsg"></param>
    /// <returns></returns>
    public int makecommit(string cusid, string pcode, int multiple, string EwalletID, DateTime Topupdate, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[helpyug_makecommit]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cusid", cusid));
            cmd.Parameters.Add(new SqlParameter("@pcode", pcode));
            cmd.Parameters.Add(new SqlParameter("@multiple", multiple));
            cmd.Parameters.Add(new SqlParameter("@EwalletID", EwalletID));
            cmd.Parameters.Add(new SqlParameter("@Topupdate", Topupdate));
            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "0";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
    #endregion
	
    #region Proctransfermainwallettorechargewallet to own ID ewallet to pin wallet(string CusID1, string CusID2, string amount)

    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">ID From</param>
    /// <returns>Transaction ID</returns>
    public double Proctransfermainwallettorechargewallet(string CusID1, string amount)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            double trID = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[transfermainwallettorechargewallet]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@CusID1", CusID1));
            cmd1.Parameters.Add(new SqlParameter("@amount", amount));
            cmd1.Parameters.Add(new SqlParameter("@trID", 0)).Direction = ParameterDirection.Output;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.ExecuteNonQuery();
            connc.Close();

            trID = Convert.ToDouble(cmd1.Parameters["@trID"].Value);
            cmd1.Dispose();
            return trID;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            connc.Close();
        }

    }

    #endregion
	
    #region shop_sessioncart_payment(string cusid,  string EwalletID, string proId,string m_qty,string m_shipping, delivery_add, mobileno, out int flag, out string errMsg)


    public int shop_sessioncart_payment(string cusid, string sessionid, string proId, string m_qty, string m_shipping, string delivery_add, string mobileno, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[shop_sessioncart_payment]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cusid", cusid));
            cmd.Parameters.Add(new SqlParameter("@sessionid", sessionid));
            cmd.Parameters.Add(new SqlParameter("@walletType ", proId));
            cmd.Parameters.Add(new SqlParameter("@qty", m_qty));
            cmd.Parameters.Add(new SqlParameter("@Shipping", m_shipping));
            cmd.Parameters.Add(new SqlParameter("@DeliveryAdd", delivery_add));
            cmd.Parameters.Add(new SqlParameter("@MobileNo", mobileno));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
    #endregion

    #region shop_sessioncart(string cusid,  string EwalletID, string proId,string m_qty,  out int flag, out string errMsg)


    public int shop_sessioncart(string cusid, string sessionid, string proId, string m_qty, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[shop_sessioncart]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cusid", cusid));
            cmd.Parameters.Add(new SqlParameter("@sessionid", sessionid));
            cmd.Parameters.Add(new SqlParameter("@proId", proId));
            cmd.Parameters.Add(new SqlParameter("@qty", m_qty));
            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
    #endregion
	
    #region transferotherwallettomainwallet to own ID ewallet to pin wallet(string CusID1, string CusID2, string amount)

    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">ID From</param>
    /// <returns>Transaction ID</returns>
    public double transferotherwallettomainwallet(string CusID1, string amount)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            double trID = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[transferotherwallettomainwallet]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@CusID1", CusID1));
            cmd1.Parameters.Add(new SqlParameter("@amount", amount));
            cmd1.Parameters.Add(new SqlParameter("@trID", 0)).Direction = ParameterDirection.Output;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.ExecuteNonQuery();
            connc.Close();

            trID = Convert.ToDouble(cmd1.Parameters["@trID"].Value);
            cmd1.Dispose();
            return trID;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            connc.Close();
        }

    }

    #endregion

    #region club wallet to main wallet(string CusID1, string CusID2, string amount)

    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">ID From</param>
    /// <returns>Transaction ID</returns>
    public double transfer_club_walletto_mainwallet(string CusID1, string amount)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            double trID = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[transfer_club_walletto_mainwallet]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@CusID1", CusID1));
            cmd1.Parameters.Add(new SqlParameter("@amount", amount));
            cmd1.Parameters.Add(new SqlParameter("@trID", 0)).Direction = ParameterDirection.Output;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.ExecuteNonQuery();
            connc.Close();

            trID = Convert.ToDouble(cmd1.Parameters["@trID"].Value);
            cmd1.Dispose();
            return trID;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            connc.Close();
        }

    }

    #endregion
	
    #region updatemessagedetail(float slno, string payment_detail)

    /// <summary>
    /// Update Users Bank Details
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="Cust_PanID"></param>
    /// <param name="bankachodename"></param>
    /// <param name="Cust_BankBranch"></param>
    /// <param name="BankState"></param>
    /// <param name="Cust_BankName"></param>
    /// <param name="Cust_BankAcc"></param>
    /// <param name="BankCity"></param>
    /// <param name="Cust_BankIFSC"></param>
    /// <returns></returns>
    public int updatemessagedetail(string toid, string fromid, string advtno, string payment_detail)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            int trID = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[msgsystem]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@toid", toid));
            cmd1.Parameters.Add(new SqlParameter("@fromid", fromid));
            cmd1.Parameters.Add(new SqlParameter("@PaymentDetail", payment_detail));
            cmd1.Parameters.Add(new SqlParameter("@advtid", advtno));

            //cmd1.Parameters.Add(new SqlParameter("@bankachodename", bankachodename));
            //cmd1.Parameters.Add(new SqlParameter("@Cust_BankBranch", Cust_BankBranch));
            //cmd1.Parameters.Add(new SqlParameter("@BankState", BankState));
            //cmd1.Parameters.Add(new SqlParameter("@Cust_BankName", Cust_BankName));
            //cmd1.Parameters.Add(new SqlParameter("@Cust_BankAcc", Cust_BankAcc));
            //cmd1.Parameters.Add(new SqlParameter("@BankCity", BankCity));
            //cmd1.Parameters.Add(new SqlParameter("@Cust_BankIFSC", Cust_BankIFSC));
            cmd1.CommandType = CommandType.StoredProcedure;
            trID = cmd1.ExecuteNonQuery();
            connc.Close();
            return trID;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            connc.Close();
        }
    }
    #endregion

    #region webpromotion(float sender, string typeofcontnet, string urldtl, string messagedtl, string remark, out int ticketno, out string errMsg)
    public int webpromotion(float sender, string typeofcontnet, string urldtl, string messagedtl, string remark, out int ticketno, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[webpromotion_add_proc]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@sender", sender));
            cmd.Parameters.Add(new SqlParameter("@typeofcontnet", typeofcontnet));
            cmd.Parameters.Add(new SqlParameter("@urldtl", urldtl));
            cmd.Parameters.Add(new SqlParameter("@messagedtl", messagedtl));
            cmd.Parameters.Add(new SqlParameter("@remark", remark));
            SqlParameter SqlParaNewID = new SqlParameter("@ticketno", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            @ticketno = (int)SqlParaNewID.Value;
            errMsg = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            @ticketno = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }


    }

    #endregion

    #region support_ticket(float sender, float receiver, string messagedtl, out int ticketno, out string errMsg)

    public int support_ticket(float sender, float receiver, string messagedtl, out int ticketno, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[Help_ticket_system]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@sender", sender));
            cmd.Parameters.Add(new SqlParameter("@receiver", receiver));
            cmd.Parameters.Add(new SqlParameter("@messagedtl", messagedtl));
            SqlParameter SqlParaNewID = new SqlParameter("@ticketno", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            @ticketno = (int)SqlParaNewID.Value;
            errMsg = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            @ticketno = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }


    }
	#endregion
	
	#region update_multi_emailverify(string cusid, out int flag, out string errMsg)

	/// <summary>
	/// Topup User Account
	/// </summary>
	/// <param name="cusid"></param>
	/// <param name="pcode"></param>
	/// <param name="multiple"></param>
	/// <param name="EwalletID"></param>
	/// <param name="Topupdate"></param>
	/// <param name="flag"></param>
	/// <param name="errMsg"></param>
	/// <returns></returns>
	public int update_multi_emailverify(string cusid,string msgtpe, out int NewID, out string ErrCode)
	{
		SqlConnection conc = new SqlConnection(ConnectionStringValue);
		try
		{
			SqlCommand cmd = new SqlCommand("[dbo].[update_multi_emailverify]", conc);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter("@cusid", cusid));
			cmd.Parameters.Add(new SqlParameter("@msgid", msgtpe));

			SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.Int);
			SqlParaNewID.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID).Value = 0;

			SqlParameter SqlParaErrMsg = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
			SqlParaErrMsg.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

			if (conc.State == ConnectionState.Closed)
				conc.Open();
			int value = cmd.ExecuteNonQuery();
			NewID = (int)SqlParaNewID.Value;
			ErrCode = Convert.ToString(SqlParaErrMsg.Value);
			return value;
		}
		catch (Exception ex)
		{
			NewID = 0;
			ErrCode = ex.Message;
			return 0;
		}
		finally
		{
			conc.Close();
		}

	}
	#endregion
	
	#region verification_byregistration(string cusid, out int flag, out string errMsg)
	/// <summary>
	/// Topup User Account
	/// </summary>
	/// <param name="cusid"></param>
	/// <param name="pcode"></param>
	/// <param name="multiple"></param>
	/// <param name="EwalletID"></param>
	/// <param name="Topupdate"></param>
	/// <param name="flag"></param>
	/// <param name="errMsg"></param>
	/// <returns></returns>
	public int verification_byregistration(string cusid, out int NewID, out string ErrCode)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[InsertNewMember_kalmanserve_alinkver]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@cusid", cusid));

            SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            NewID = (int)SqlParaNewID.Value;
            ErrCode = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            NewID = 0;
            ErrCode = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
	#endregion

	#region Check_balance_detail(string emailid,string pws,out m_sponsor, out string m_password, out int flag,out string Name_msg,out string m_walletbal, out string errMsg)


	public int Check_balance_detail(string emailid, string pws, out string m_sponsor, out string m_password, out int NewID, out string Name_msg, out string m_walletbal, out string ErrCode)
	{
		SqlConnection conc = new SqlConnection(ConnectionStringValue);
		try
		{ 
			SqlCommand cmd = new SqlCommand("[dbo].[Check_balance_detail]", conc);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter("@emailid", emailid));
			cmd.Parameters.Add(new SqlParameter("@pws", pws));

			SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.Int);
			SqlParaNewID.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID).Value = 0;

			 
				SqlParameter SqlParaNewID_m_sponsor = new SqlParameter("@m_sponsor", SqlDbType.VarChar, 500);
			SqlParaNewID_m_sponsor.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID_m_sponsor).Value = "Sponsor Not Found";

			SqlParameter SqlParaNewID_name = new SqlParameter("@Name_msg", SqlDbType.VarChar, 500);
			SqlParaNewID_name.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID_name).Value = "Wrong Name";

			SqlParameter SqlParaNewID_m_password = new SqlParameter("@m_password", SqlDbType.VarChar, 500);
			SqlParaNewID_m_password.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID_m_password).Value = "Wrong Password";

			SqlParameter SqlParaNewID_balance = new SqlParameter("@m_walletbal", SqlDbType.VarChar, 50);
			SqlParaNewID_balance.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID_balance).Value = "0";

			SqlParameter SqlParaErrMsg = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
			SqlParaErrMsg.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

			if (conc.State == ConnectionState.Closed)
				conc.Open();

			int value = cmd.ExecuteNonQuery();

			NewID = (int)SqlParaNewID.Value;

			m_sponsor = Convert.ToString(SqlParaNewID_m_sponsor.Value);
			Name_msg = Convert.ToString(SqlParaNewID_name.Value);
			m_password = Convert.ToString(SqlParaNewID_m_password.Value);
			m_walletbal = Convert.ToString(SqlParaNewID_balance.Value);
			ErrCode = Convert.ToString(SqlParaErrMsg.Value);
			return value;
		}
		catch (Exception ex)
		{
			NewID = 0;
			m_sponsor = "n/a";
			Name_msg = "n/a";
			m_password = "n/a";
			m_walletbal = "0";
			ErrCode = ex.Message;
			return 0;
		}
		finally
		{
			conc.Close();
		}

	}
	#endregion


	#region Check_login_detail(string username,string pws,  out int flag,out string name,out string m_walletbal, out string errMsg)


	public int Check_login_detail(string username, string pws, out int NewID, out string name, out string m_walletbal, out string ErrCode)
	{
		SqlConnection conc = new SqlConnection(ConnectionStringValue);
		try
		{
			SqlCommand cmd = new SqlCommand("[dbo].[Check_login_detail]", conc);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter("@username", username));
			cmd.Parameters.Add(new SqlParameter("@pws", pws));

			SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.Int);
			SqlParaNewID.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID).Value = 0;

			SqlParameter SqlParaNewID_name = new SqlParameter("@Name", SqlDbType.VarChar, 500);
			SqlParaNewID_name.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID_name).Value = "Wrong Name";
 

			SqlParameter SqlParaNewID_balance = new SqlParameter("@m_walletbal", SqlDbType.VarChar, 50);
			SqlParaNewID_balance.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID_balance).Value = "0";

			SqlParameter SqlParaErrMsg = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
			SqlParaErrMsg.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

			if (conc.State == ConnectionState.Closed)
				conc.Open();

			int value = cmd.ExecuteNonQuery();

			NewID = (int)SqlParaNewID.Value;
			name = Convert.ToString(SqlParaNewID_name.Value); 
			m_walletbal = Convert.ToString(SqlParaNewID_balance.Value);
			ErrCode = Convert.ToString(SqlParaErrMsg.Value);
			return value;
		}
		catch (Exception ex)
		{
			NewID = 0;
			name = "n/a";
			m_walletbal = "0";
			ErrCode = ex.Message;
			return 0;
		}
		finally
		{
			conc.Close();
		}

	}
	#endregion

	public double Updatebid(string id, string pname, string pdetail, string cat, string status, string pp, string fname, string brate, DateTime sdate, DateTime edate, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[SPAction_BidUpdate]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@Product_ID", id));
            cmd1.Parameters.Add(new SqlParameter("@Product_Name", pname));
            cmd1.Parameters.Add(new SqlParameter("@Product_detail", pdetail));
            cmd1.Parameters.Add(new SqlParameter("@Cat", cat));
            cmd1.Parameters.Add(new SqlParameter("@status", status));
            cmd1.Parameters.Add(new SqlParameter("@PP", pp));
            cmd1.Parameters.Add(new SqlParameter("@fname", fname));
            cmd1.Parameters.Add(new SqlParameter("@BRATE", brate));
            cmd1.Parameters.Add(new SqlParameter("@sdate", sdate));
            cmd1.Parameters.Add(new SqlParameter("@edate", edate));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "0";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    public double Insertbid(string pname, string pdetail, string cat, string status, string pp, string fname, string brate, DateTime sdate, DateTime edate, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[SPAction_BidInsert]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@Product_Name", pname));
            cmd1.Parameters.Add(new SqlParameter("@Product_detail", pdetail));
            cmd1.Parameters.Add(new SqlParameter("@Cat", cat));
            cmd1.Parameters.Add(new SqlParameter("@status", status));
            cmd1.Parameters.Add(new SqlParameter("@PP", pp));
            cmd1.Parameters.Add(new SqlParameter("@fname", fname));
            cmd1.Parameters.Add(new SqlParameter("@BRATE", brate));
            cmd1.Parameters.Add(new SqlParameter("@sdate", sdate));
            cmd1.Parameters.Add(new SqlParameter("@edate", edate));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "0";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #region InternalFundTransfer to bid ewallet own ID(string CusID1, string CusID2, string amount)


    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">ID From</param>
    /// <returns>Transaction ID</returns>
    public double InternalFundTransfer_bidwallet(string CusID1, string amount)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            double trID = 0;
            // SqlCommand cmd1 = new SqlCommand("[dbo].[spActionMedia_TransferInternalFund_toownwallet]", connc); 
            SqlCommand cmd1 = new SqlCommand("[dbo].[spActionMedia_TransferInternalFund_bidwallet]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@CusID1", CusID1));
            cmd1.Parameters.Add(new SqlParameter("@amount", amount));
            cmd1.Parameters.Add(new SqlParameter("@trID", 0)).Direction = ParameterDirection.Output;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.ExecuteNonQuery();
            connc.Close();

            trID = Convert.ToDouble(cmd1.Parameters["@trID"].Value);
            cmd1.Dispose();
            return trID;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            connc.Close();
        }

    }

    #endregion

    #region InternalFundPointsPTBtoRTB(string CusID1, string CusID2, string amount)


    /// <summary>
    /// Internal Transfer Points PTB to RTB
    /// </summary>
    /// <param name="CusID1">ID From</param>
    /// <param name="amount">Transfer Amount</param>
    /// <returns>Transaction ID</returns>
    public double InternalTransferPointsPTBtoRTB(string CusID, int mode, string amount)
    {
        SqlConnection connc = new SqlConnection(ConnectionStringValue);
        try
        {
            double trID = 0;
            SqlCommand cmd1 = new SqlCommand("[dbo].[spZonda_TransferInternalPointsPTBtoRTB]", connc);
            connc.Open();
            cmd1.Parameters.Add(new SqlParameter("@CusID", CusID));
            cmd1.Parameters.Add(new SqlParameter("@amount", amount));
            cmd1.Parameters.Add(new SqlParameter("@mode", mode));
            cmd1.Parameters.Add(new SqlParameter("@trID", 0)).Direction = ParameterDirection.Output;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.ExecuteNonQuery();
            connc.Close();

            trID = Convert.ToDouble(cmd1.Parameters["@trID"].Value);
            cmd1.Dispose();
            return trID;
        }
        catch (Exception ex)
        {
            return 0;
        }
        finally
        {
            connc.Close();
        }

    }

    #endregion

    #region datatransfer_oldtonew(string session_username, string old_cusid, string new_cusid, out int flag, out string errMsg)

    /// <summary>
    /// Topup User Account
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="pcode"></param>
    /// <param name="multiple"></param>
    /// <param name="EwalletID"></param>
    /// <param name="Topupdate"></param>
    /// <param name="flag"></param>
    /// <param name="errMsg"></param>
    /// <returns></returns>
    public int datatransfer_oldtonew(string session_username, string old_cusid, string new_cusid, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[datatransfer_oldtonew1]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@session_username", session_username));
            cmd.Parameters.Add(new SqlParameter("@old_cusid", old_cusid));
            cmd.Parameters.Add(new SqlParameter("@new_cusid", new_cusid));
            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = Convert.ToString(SqlParaErrMsg.Value);
            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
    #endregion

    #region Insert_CoinAddress

    public int Insert_CoinAddress(string CustID, string CoinAddress, string Coins, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[spInsert_CoinAddress]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@CustID", CustID));
            cmd1.Parameters.Add(new SqlParameter("@CoinAddress", CoinAddress));
            cmd1.Parameters.Add(new SqlParameter("@Coins", Coins));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;

        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion 

    #region Update_BIXC_Entry

    /// <summary>
    /// To Withdraw Money from Customer e_wallet_main
    /// </summary>
    public int Update_BIXC_Entry(string CustID, string Category, string TxID, string Amount, string TimeReceived, string Confirmations, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[spUpdate_BIXC_Entry]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@CustID", CustID));
            cmd1.Parameters.Add(new SqlParameter("@Category", Category));
            cmd1.Parameters.Add(new SqlParameter("@TxID", TxID));
            cmd1.Parameters.Add(new SqlParameter("@Amount", Amount));
            cmd1.Parameters.Add(new SqlParameter("@TimeReceived", TimeReceived));
            cmd1.Parameters.Add(new SqlParameter("@Confirmations", Confirmations));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;

        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    #region Update_BIXC_Entry_New_Success_Pending

    /// <summary>
    /// To Withdraw Money from Customer e_wallet_main
    /// </summary>
    public int Update_BIXC_Entry_New_Success_Pending(string CustID, string Category, string TxID, string Amount, string Time, string Confirmations, string TransferStatus, string Address, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[spUpdate_BIXC_Entry_New_Success_Pending]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@CustID", CustID));
            cmd1.Parameters.Add(new SqlParameter("@Category", Category));
            cmd1.Parameters.Add(new SqlParameter("@TxID", TxID));
            cmd1.Parameters.Add(new SqlParameter("@Amount", Amount));
            cmd1.Parameters.Add(new SqlParameter("@Time", Time));
            cmd1.Parameters.Add(new SqlParameter("@Confirmations", Confirmations));
            cmd1.Parameters.Add(new SqlParameter("@TransferStatus", TransferStatus));
            cmd1.Parameters.Add(new SqlParameter("@Address", Address));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;

        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    // JAVID CODE

    #region ETHCoin_Purchase

    /// <summary>
    /// To Withdraw Money from Customer e_wallet_main
    /// </summary>
    public int ETHCoin_Purchase(string CusID, string txt_ews_pws, string pcode, string w_wallettype, string m_shareqty, string Category, float btc_amt, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[billionairefxtraders_buyshare_ETHCoin_ETHRevised]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@CusID", CusID));
            cmd1.Parameters.Add(new SqlParameter("@txt_ews_pws", txt_ews_pws));
            cmd1.Parameters.Add(new SqlParameter("@p_code", pcode));
            cmd1.Parameters.Add(new SqlParameter("@w_wallettype", w_wallettype));
            cmd1.Parameters.Add(new SqlParameter("@m_shareqty", m_shareqty));
            cmd1.Parameters.Add(new SqlParameter("@Category", Category));
            cmd1.Parameters.Add(new SqlParameter("@btc_amt", btc_amt));


            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;

        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    #region ETCCoin_Purchase

    /// <summary>
    /// To Withdraw Money from Customer e_wallet_main
    /// </summary>
    public int ETCCoin_Purchase(string CusID, string txt_ews_pws, string pcode, string w_wallettype, string m_shareqty, string Category, float btc_amt, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[billionairefxtraders_buyshare_ETCCoin]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@CusID", CusID));
            cmd1.Parameters.Add(new SqlParameter("@txt_ews_pws", txt_ews_pws));
            cmd1.Parameters.Add(new SqlParameter("@p_code", pcode));
            cmd1.Parameters.Add(new SqlParameter("@w_wallettype", w_wallettype));
            cmd1.Parameters.Add(new SqlParameter("@m_shareqty", m_shareqty));
            cmd1.Parameters.Add(new SqlParameter("@Category", Category));
            cmd1.Parameters.Add(new SqlParameter("@btc_amt", btc_amt));


            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;

        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    #region AddFund_GLTC_Address_Delete_Previous

    /// <summary>
    /// To Withdraw Money from Customer e_wallet_main
    /// </summary>
    public int AddFund_GLTC_Address_Delete_Previous(string SerialNO, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[spAddFund_GLTC_Address_Delete_Previous]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@SerialNO", SerialNO));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;

        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    #region Generate_BIXC_Address

    /// <summary>
    /// To Withdraw Money from Customer e_wallet_main
    /// </summary>
    public int Generate_BIXC_Address(string BIXC_Address, string CustID, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[spInsertBIXC_Address]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@BIXCAddress", BIXC_Address));
            cmd1.Parameters.Add(new SqlParameter("@CustID", CustID));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;

        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    #region AddFund_GLTC_Address_AutoState_Update

    /// <summary>
    /// To Withdraw Money from Customer e_wallet_main
    /// </summary>
    public int AddFund_GLTC_Address_AutoState_Update(string SerialNO, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[spAddFund_GLTC_Address_AutoState_Update]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@SerialNO", SerialNO));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;

        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    #region Update_GLTC_Address_Status_New

    /// <summary>
    /// To Withdraw Money from Customer e_wallet_main
    /// </summary>
    public int Update_GLTC_Address_Status_New(string adminStaffId, string GLTC_Address, string CustID, string Coin_Balance, string m_shareqty, string ValidStatus, string TXN, string Dates, string SerialNO, string UsedType, string LVPS_Rate, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[spAddFund_GLTC_Address_Status]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@adminStaffId", adminStaffId));
            cmd1.Parameters.Add(new SqlParameter("@GLTC_Address", GLTC_Address));
            cmd1.Parameters.Add(new SqlParameter("@CustID", CustID));
            cmd1.Parameters.Add(new SqlParameter("@Coin_Balance", Coin_Balance));
            cmd1.Parameters.Add(new SqlParameter("@m_shareqty", m_shareqty));
            cmd1.Parameters.Add(new SqlParameter("@ValidStatus", ValidStatus));
            cmd1.Parameters.Add(new SqlParameter("@UsedType", UsedType));
            cmd1.Parameters.Add(new SqlParameter("@GLTC_TXN", TXN));
            cmd1.Parameters.Add(new SqlParameter("@GLTC_Payement_Date", Dates));
            cmd1.Parameters.Add(new SqlParameter("@SerialNO", SerialNO));
            cmd1.Parameters.Add(new SqlParameter("@LVPS_Rate", LVPS_Rate));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;

        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    #region AddFund_GLTC_Address_Update_Previous

    /// <summary>
    /// To Withdraw Money from Customer e_wallet_main
    /// </summary>
    public int AddFund_GLTC_Address_Update_Previous(string SerialNO, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[spAddFund_GLTC_Address_Update_Previous]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@SerialNO", SerialNO));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;

        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    #region AddFund_GLTC_Address

    /// <summary>
    /// To Withdraw Money from Customer e_wallet_main
    /// </summary>
    public int AddFund_GLTC_Address(string GLTC_Address, string CustID, string Coin_Balance, string Share_Qty, string UsedType, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[spAddFund_GLTC_Address]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@GLTC_Address", GLTC_Address));
            cmd1.Parameters.Add(new SqlParameter("@CustID", CustID));
            cmd1.Parameters.Add(new SqlParameter("@Coin_Balance", Coin_Balance));
            cmd1.Parameters.Add(new SqlParameter("@Share_Qty", Share_Qty));
            cmd1.Parameters.Add(new SqlParameter("@UsedType", UsedType));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;

        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    #region Update_GLTC_Address_Status_PackageUpgrade

    /// <summary>
    /// To Withdraw Money from Customer e_wallet_main
    /// </summary>
    public int Update_GLTC_Address_Status_PackageUpgrade(string adminStaffId, string GLTC_Address, string CustID, string Coin_Balance, string m_shareqty, string ValidStatus, string UsedType, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[spAddFund_GLTC_Address_Status_Upgrade]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@adminStaffId", adminStaffId));
            cmd1.Parameters.Add(new SqlParameter("@GLTC_Address", GLTC_Address));
            cmd1.Parameters.Add(new SqlParameter("@CustID", CustID));
            cmd1.Parameters.Add(new SqlParameter("@Coin_Balance", Coin_Balance));
            cmd1.Parameters.Add(new SqlParameter("@m_shareqty", m_shareqty));
            cmd1.Parameters.Add(new SqlParameter("@ValidStatus", ValidStatus));
            cmd1.Parameters.Add(new SqlParameter("@UsedType", UsedType));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;

        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    #region spUpdate_BIXC_Entry_New_Success_Pending_Coin_New_BTC

    /// <summary>
    /// To Withdraw Money from Customer e_wallet_main
    /// </summary>
    public int spUpdate_BIXC_Entry_New_Success_Pending_Coin_New_BTC(string CustID, string TrID, string TxID, string Confirmations, string MarketID, out int flag, out string errMsg)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[spUpdate_BIXC_Entry_New_Success_Pending_Coin_New_BTC]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@CustID", CustID));
            cmd1.Parameters.Add(new SqlParameter("@TrID", TrID));
            cmd1.Parameters.Add(new SqlParameter("@TxID", TxID));
            cmd1.Parameters.Add(new SqlParameter("@Confirmations", Confirmations));
            cmd1.Parameters.Add(new SqlParameter("@MarketID", MarketID));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;

        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion

    #region Update_BIXC_Entry_New_Success_Pending_Coin_BTC

    /// <summary>
    /// To Withdraw Money from Customer e_wallet_main
    /// </summary>
    public int Update_BIXC_Entry_New_Success_Pending_Coin_BTC(string CustID, string Category, string TxID, string Amount, string Time, string Confirmations, string MarketID , string USDAmt, out int flag, out string errMsg)
    {
        //decimal Amount1;
        //Amount1 = Convert.ToDecimal(Amount);

        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd1 = new SqlCommand("[dbo].[spUpdate_BIXC_Entry_New_Success_Pending_Coin_BTC]", conc);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@CustID", CustID));
            cmd1.Parameters.Add(new SqlParameter("@Category", Category));
            cmd1.Parameters.Add(new SqlParameter("@TxID", TxID));
            cmd1.Parameters.Add(new SqlParameter("@Amount", Amount));
            cmd1.Parameters.Add(new SqlParameter("@Time", Time));
            cmd1.Parameters.Add(new SqlParameter("@Confirmations", Confirmations));
            cmd1.Parameters.Add(new SqlParameter("@MarketID", MarketID));
            cmd1.Parameters.Add(new SqlParameter("@USDAmt", USDAmt));


            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd1.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = (string)SqlParaErrMsg.Value;
            return value;

        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            return 0;
        }
        finally
        {
            conc.Close();
        }
    }

    #endregion   

    #region Insert_OTP_Code

    public int Insert_OTP_Code(string CustID, string Type, out int flag, out string errMsg, out string Result_OTP_Code)
    {
        SqlConnection conc = new SqlConnection(ConnectionStringValue);
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[Create_Withdrawal_OTP]", conc);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CustID", CustID));
            cmd.Parameters.Add(new SqlParameter("@Type", Type));

            SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
            SqlParaNewID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaNewID).Value = 0;

            SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
            SqlParaErrMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

            SqlParameter SqlParaResult_OTP_Code = new SqlParameter("@Result_OTP_Code", SqlDbType.VarChar, 500);
            SqlParaResult_OTP_Code.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(SqlParaResult_OTP_Code).Value = "TRY";

            if (conc.State == ConnectionState.Closed)
                conc.Open();
            int value = cmd.ExecuteNonQuery();
            flag = (int)SqlParaNewID.Value;
            errMsg = Convert.ToString(SqlParaErrMsg.Value);
            Result_OTP_Code = Convert.ToString(SqlParaResult_OTP_Code.Value);

            return value;
        }
        catch (Exception ex)
        {
            flag = 0;
            errMsg = ex.Message;
            Result_OTP_Code = "";
            return 0;
        }
        finally
        {
            conc.Close();
        }

    }
	#endregion

	#region ORDERID(string pno,   out int flag, out string errMsg)

	/// <summary>
	/// Topup User Account
	/// </summary>
	/// <param name="cusid"></param>
	/// <param name="pcode"></param>
	/// <param name="multiple"></param>
	/// <param name="EwalletID"></param>
	/// <param name="Topupdate"></param>
	/// <param name="flag"></param>
	/// <param name="errMsg"></param>
	/// <returns></returns>
	public int ORDERID(string pno,  out int flag, out string errMsg)
	{
		SqlConnection conc = new SqlConnection(ConnectionStringValue);
		try
		{
			SqlCommand cmd = new SqlCommand("[dbo].[sp_createOrdertransacition_no]", conc);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter("@pno", pno));
			 
			SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
			SqlParaNewID.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID).Value = 0;

			SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
			SqlParaErrMsg.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

			if (conc.State == ConnectionState.Closed)
				conc.Open();
			int value = cmd.ExecuteNonQuery();
			flag = (int)SqlParaNewID.Value;
			errMsg = Convert.ToString(SqlParaErrMsg.Value);
			return value;
		}
		catch (Exception ex)
		{
			flag = 0;
			errMsg = ex.Message;
			return 0;
		}
		finally
		{
			conc.Close();
		}

	}
	#endregion

	#region ORDERIDIVESTMENT(string cusid, string txt_ews_pws,string m_shareqty, w_wallettype, out int flag, out string errMsg)

	/// <summary>
	/// Topup User Account
	/// </summary>
	/// <param name="cusid"></param>
	/// <param name="pcode"></param>
	/// <param name="multiple"></param>
	/// <param name="EwalletID"></param>
	/// <param name="Topupdate"></param>
	/// <param name="flag"></param>
	/// <param name="errMsg"></param>
	/// <returns></returns>
	public int ORDERIDIVESTMENT(string cusid, string txt_ews_pws, string m_shareqty, string Validity, string category, string w_wallettype, string p_code, string btc_amt,string orderno, out int flag, out string errMsg)
	{
		SqlConnection conc = new SqlConnection(ConnectionStringValue);
		try
		{
			SqlCommand cmd = new SqlCommand("[dbo].[buyshare_hashpower_orderinvest]", conc);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter("@cusid", cusid));
			cmd.Parameters.Add(new SqlParameter("@txt_ews_pws", txt_ews_pws));
			cmd.Parameters.Add(new SqlParameter("@m_shareqty", m_shareqty));
			cmd.Parameters.Add(new SqlParameter("@validity", Validity));
			cmd.Parameters.Add(new SqlParameter("@category", category));
			cmd.Parameters.Add(new SqlParameter("@w_wallettype", w_wallettype));
			cmd.Parameters.Add(new SqlParameter("@p_code", p_code));
			cmd.Parameters.Add(new SqlParameter("@btc_amt", btc_amt));
			cmd.Parameters.Add(new SqlParameter("@orderno", orderno));

			SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
			SqlParaNewID.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID).Value = 0;

			SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
			SqlParaErrMsg.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

			if (conc.State == ConnectionState.Closed)
				conc.Open();
			int value = cmd.ExecuteNonQuery();
			flag = (int)SqlParaNewID.Value;
			errMsg = Convert.ToString(SqlParaErrMsg.Value);
			return value;
		}
		catch (Exception ex)
		{
			flag = 0;
			errMsg = ex.Message;
			return 0;
		}
		finally
		{
			conc.Close();
		}

	}
	#endregion

	#region reset password

	/// <returns>INT</returns>
	public int inserrttemppass(string username, string id, string name, string strHostName, string ipaddress, string pass, string email, string mobileno, out string NewID, out string ErrCode)
    {
		SqlConnection conc = new SqlConnection(ConnectionStringValue);
		try
		{

			SqlCommand cmd = new SqlCommand("[dbo].[Insertresetpass_onlyprepass]", conc);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter("@CusID", id));
			cmd.Parameters.Add(new SqlParameter("@custusername", username));
			cmd.Parameters.Add(new SqlParameter("@Cust_Name", name));
			cmd.Parameters.Add(new SqlParameter("@HostName", strHostName));
			cmd.Parameters.Add(new SqlParameter("@ipaddress", ipaddress));
			cmd.Parameters.Add(new SqlParameter("@Cust_Password", pass));
			cmd.Parameters.Add(new SqlParameter("@Email", email));
			cmd.Parameters.Add(new SqlParameter("@Cust_mobileNo", mobileno));
			

			SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.Float);
			SqlParaNewID.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID).Value = 0;

			SqlParameter SqlParaErrCode = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
			SqlParaErrCode.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaErrCode).Value = "";

			if (conc.State == ConnectionState.Closed)
				conc.Open();
			int order = cmd.ExecuteNonQuery();
			double NewID1 = (double)SqlParaNewID.Value;
			ErrCode = (string)SqlParaErrCode.Value;
			cmd.Dispose();
			conc.Close();
			NewID = NewID1.ToString();
			return order;

		}
		catch (Exception ex)
		{
			NewID = "0";
			ErrCode = ex.Message.ToString();
			return 0;
		}
		finally
		{
			conc.Close();
		}
	}

	#endregion
	
	#region New Member Registration Method
	public int inserrtnewtempEntry(string Cust_SponserID, string Email, string Cust_Address, string Cust_Answer, string Cust_Question, string Cust_City, string Cust_State, string Cust_Country, string Cust_Password, string Cust_Title, string Cust_Name, string Cust_Gender, string Cust_FatherName, DateTime Cust_DOB, string Cust_Pincode, string Cust_mobileNo, string Cust_nominee, string Cust_Relation, string Cust_Package, string Cust_Location, string Cust_TempPinID, int PayMode, string Cust_BankName, string Cust_BankAcc, string Cust_BankIFSC, string Cust_BankBranch, string Cust_PanID, string _custusername, out string NewID, out string ErrCode)

	{

		SqlConnection conc = new SqlConnection(ConnectionStringValue);
		try
		{

			SqlCommand cmd = new SqlCommand("[dbo].[InsertNewMember_temp_onlyprereg]", conc);
			//SqlCommand cmd = new SqlCommand("[dbo].[reliable_InsertNewMember]", conc);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter("@Cust_SponserID", Cust_SponserID));
			cmd.Parameters.Add(new SqlParameter("@Email", Email));
			cmd.Parameters.Add(new SqlParameter("@Cust_Address", Cust_Address));
			cmd.Parameters.Add(new SqlParameter("@Cust_Answer", Cust_Answer));
			cmd.Parameters.Add(new SqlParameter("@Cust_Question", Cust_Question));
			cmd.Parameters.Add(new SqlParameter("@Cust_City", Cust_City));
			cmd.Parameters.Add(new SqlParameter("@Cust_State", Cust_State));
			cmd.Parameters.Add(new SqlParameter("@Cust_Country", Cust_Country));
			cmd.Parameters.Add(new SqlParameter("@Cust_Password", Cust_Password));
			cmd.Parameters.Add(new SqlParameter("@Cust_Title", Cust_Title));
			cmd.Parameters.Add(new SqlParameter("@Cust_Name", Cust_Name));
			cmd.Parameters.Add(new SqlParameter("@Cust_Gender", Cust_Gender));
			cmd.Parameters.Add(new SqlParameter("@Cust_FatherName", Cust_FatherName));
			cmd.Parameters.Add(new SqlParameter("@Cust_DOB", Cust_DOB));
			cmd.Parameters.Add(new SqlParameter("@Cust_Pincode", Cust_Pincode));
			cmd.Parameters.Add(new SqlParameter("@Cust_mobileNo", Cust_mobileNo));
			cmd.Parameters.Add(new SqlParameter("@Cust_nominee", Cust_nominee));
			cmd.Parameters.Add(new SqlParameter("@Cust_Relation", Cust_Relation));
			cmd.Parameters.Add(new SqlParameter("@Cust_Package", Cust_Package));
			cmd.Parameters.Add(new SqlParameter("@Cust_Location", Cust_Location));
			cmd.Parameters.Add(new SqlParameter("@Cust_TempPinID", Cust_TempPinID));

			cmd.Parameters.Add(new SqlParameter("@PayMode", PayMode));

			// here bank information use for transfer Pin Value Kindly refer from registration page textbox value
			//dr["Cust_BankName"] = txt_security_pinslno.Text;
			//dr["Cust_BankAcc"] = txt_security_pinno.Text;

			cmd.Parameters.Add(new SqlParameter("@Cust_BankName", Cust_BankName));
			cmd.Parameters.Add(new SqlParameter("@Cust_BankAcc", Cust_BankAcc));
			cmd.Parameters.Add(new SqlParameter("@Cust_BankIFSC", Cust_BankIFSC));
			cmd.Parameters.Add(new SqlParameter("@Cust_BankBranch", Cust_BankBranch));
			cmd.Parameters.Add(new SqlParameter("@Cust_PanID", Cust_PanID));
			cmd.Parameters.Add(new SqlParameter("@custusername", _custusername));

			//cmd.Parameters.Add(new SqlParameter("@e_wallet_userid", e_wallet_userid));
			//cmd.Parameters.Add(new SqlParameter("@e_wallet_userid_password", e_wallet_userid_password));
			//cmd.Parameters.Add(new SqlParameter("@e_wallet_userid_chargeamt", e_wallet_userid_chargeamt));

			SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.Float);
			SqlParaNewID.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID).Value = 0;

			SqlParameter SqlParaErrCode = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
			SqlParaErrCode.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaErrCode).Value = "";

			if (conc.State == ConnectionState.Closed)
				conc.Open();
			int order = cmd.ExecuteNonQuery();
			double NewID1 = (double)SqlParaNewID.Value;
			ErrCode = (string)SqlParaErrCode.Value;
			cmd.Dispose();
			conc.Close();
			NewID = NewID1.ToString();
			return order;

		}
		catch (Exception ex)
		{
			NewID = "0";
			ErrCode = ex.Message.ToString();
			return 0;
		}
		finally
		{
			conc.Close();
		}
	}

	#endregion

	#region New Member Registration Method
	public int InsertNewMember_instent_registration(string Cust_SponserID, string Email, string Cust_Address, string Cust_Answer, string Cust_Question, string Cust_City, string Cust_State, string Cust_Country, string Cust_Password, string Cust_Title, string Cust_Name, string Cust_Gender, string Cust_FatherName, DateTime Cust_DOB, string Cust_Pincode, string Cust_mobileNo, string Cust_nominee, string Cust_Relation, string Cust_Package, string Cust_Location, string Cust_TempPinID, int PayMode, string Cust_BankName, string Cust_BankAcc, string Cust_BankIFSC, string Cust_BankBranch, string Cust_PanID, string _custusername, out string NewID, out string ErrCode)

	{

		SqlConnection conc = new SqlConnection(ConnectionStringValue);
		try
		{

			SqlCommand cmd = new SqlCommand("[dbo].[InsertNewMember_instent_registration]", conc);
			//SqlCommand cmd = new SqlCommand("[dbo].[reliable_InsertNewMember]", conc);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter("@Cust_SponserID", Cust_SponserID));
			cmd.Parameters.Add(new SqlParameter("@Email", Email));
			cmd.Parameters.Add(new SqlParameter("@Cust_Address", Cust_Address));
			cmd.Parameters.Add(new SqlParameter("@Cust_Answer", Cust_Answer));
			cmd.Parameters.Add(new SqlParameter("@Cust_Question", Cust_Question));
			cmd.Parameters.Add(new SqlParameter("@Cust_City", Cust_City));
			cmd.Parameters.Add(new SqlParameter("@Cust_State", Cust_State));
			cmd.Parameters.Add(new SqlParameter("@Cust_Country", Cust_Country));
			cmd.Parameters.Add(new SqlParameter("@Cust_Password", Cust_Password));
			cmd.Parameters.Add(new SqlParameter("@Cust_Title", Cust_Title));
			cmd.Parameters.Add(new SqlParameter("@Cust_Name", Cust_Name));
			cmd.Parameters.Add(new SqlParameter("@Cust_Gender", Cust_Gender));
			cmd.Parameters.Add(new SqlParameter("@Cust_FatherName", Cust_FatherName));
			cmd.Parameters.Add(new SqlParameter("@Cust_DOB", Cust_DOB));
			cmd.Parameters.Add(new SqlParameter("@Cust_Pincode", Cust_Pincode));
			cmd.Parameters.Add(new SqlParameter("@Cust_mobileNo", Cust_mobileNo));
			cmd.Parameters.Add(new SqlParameter("@Cust_nominee", Cust_nominee));
			cmd.Parameters.Add(new SqlParameter("@Cust_Relation", Cust_Relation));
			cmd.Parameters.Add(new SqlParameter("@Cust_Package", Cust_Package));
			cmd.Parameters.Add(new SqlParameter("@Cust_Location", Cust_Location));
			cmd.Parameters.Add(new SqlParameter("@Cust_TempPinID", Cust_TempPinID));

			cmd.Parameters.Add(new SqlParameter("@PayMode", PayMode));

			// here bank information use for transfer Pin Value Kindly refer from registration page textbox value
			//dr["Cust_BankName"] = txt_security_pinslno.Text;
			//dr["Cust_BankAcc"] = txt_security_pinno.Text;

			cmd.Parameters.Add(new SqlParameter("@Cust_BankName", Cust_BankName));
			cmd.Parameters.Add(new SqlParameter("@Cust_BankAcc", Cust_BankAcc));
			cmd.Parameters.Add(new SqlParameter("@Cust_BankIFSC", Cust_BankIFSC));
			cmd.Parameters.Add(new SqlParameter("@Cust_BankBranch", Cust_BankBranch));
			cmd.Parameters.Add(new SqlParameter("@Cust_PanID", Cust_PanID));
			cmd.Parameters.Add(new SqlParameter("@custusername", _custusername));

			//cmd.Parameters.Add(new SqlParameter("@e_wallet_userid", e_wallet_userid));
			//cmd.Parameters.Add(new SqlParameter("@e_wallet_userid_password", e_wallet_userid_password));
			//cmd.Parameters.Add(new SqlParameter("@e_wallet_userid_chargeamt", e_wallet_userid_chargeamt));

			SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.Float);
			SqlParaNewID.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID).Value = 0;

			SqlParameter SqlParaErrCode = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
			SqlParaErrCode.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaErrCode).Value = "";

			if (conc.State == ConnectionState.Closed)
				conc.Open();
			int order = cmd.ExecuteNonQuery();
			double NewID1 = (double)SqlParaNewID.Value;
			ErrCode = (string)SqlParaErrCode.Value;
			cmd.Dispose();
			conc.Close();
			NewID = NewID1.ToString();
			return order;

		}
		catch (Exception ex)
		{
			NewID = "0";
			ErrCode = ex.Message.ToString();
			return 0;
		}
		finally
		{
			conc.Close();
		}
	}

	#endregion

	#region temverification_withouteverify(string cusid, out int flag, out string errMsg)


	public int temverification_withouteverify(string cusid, out int NewID, out string ErrCode)
	{
		SqlConnection conc = new SqlConnection(ConnectionStringValue);
		try
		{
			SqlCommand cmd = new SqlCommand("[dbo].[InsertNewMember_withoutemailverify]", conc);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter("@cusid", cusid));

			SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.Int);
			SqlParaNewID.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID).Value = 0;

			SqlParameter SqlParaErrMsg = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
			SqlParaErrMsg.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaErrMsg).Value = "TRY";

			if (conc.State == ConnectionState.Closed)
				conc.Open();
			int value = cmd.ExecuteNonQuery();
			NewID = (int)SqlParaNewID.Value;
			ErrCode = Convert.ToString(SqlParaErrMsg.Value);
			return value;
		}
		catch (Exception ex)
		{
			NewID = 0;
			ErrCode = ex.Message;
			return 0;
		}
		finally
		{
			conc.Close();
		}

	}
	#endregion

	#region UpdateCustRecords_UserName

	/// <summary>
	/// To Withdraw Money from Customer e_wallet_main
	/// </summary>
	public int UpdateCustRecords_UserName(string CusID, string Cust_UserName1, out int flag, out string errMsg)
	{
		SqlConnection conc = new SqlConnection(ConnectionStringValue);
		try
		{
			
			   SqlCommand cmd1 = new SqlCommand("[dbo].[spUpdateCustUserName]", conc);
			cmd1.CommandType = CommandType.StoredProcedure;
			cmd1.Parameters.Add(new SqlParameter("@CusID", CusID));
			cmd1.Parameters.Add(new SqlParameter("@Cust_UserName1", Cust_UserName1));

			SqlParameter SqlParaNewID = new SqlParameter("@flag", SqlDbType.Int);
			SqlParaNewID.Direction = ParameterDirection.Output;
			cmd1.Parameters.Add(SqlParaNewID).Value = 0;

			SqlParameter SqlParaErrMsg = new SqlParameter("@errMsg", SqlDbType.VarChar, 500);
			SqlParaErrMsg.Direction = ParameterDirection.Output;
			cmd1.Parameters.Add(SqlParaErrMsg).Value = "TRY";

			if (conc.State == ConnectionState.Closed)
				conc.Open();
			int value = cmd1.ExecuteNonQuery();
			flag = (int)SqlParaNewID.Value;
			errMsg = (string)SqlParaErrMsg.Value;
			return value;

		}
		catch (Exception ex)
		{
			flag = 0;
			errMsg = ex.Message;
			return 0;
		}
		finally
		{
			conc.Close();
		}
	}

	#endregion

	#region loginuser

	public int loginuser(string Cust_Username, string Cust_Password, out string NewID, out string ErrCode)
	{
		SqlConnection conc = new SqlConnection(ConnectionString1);
		try
		{

			SqlCommand cmd = new SqlCommand("login", conc);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "login";
			cmd.Parameters.AddWithValue("@Cust_Username", Cust_Username);
			cmd.Parameters.AddWithValue("@Cust_Password", Cust_Password);
			//cmd.Parameters.AddWithValue("@device_id", device_id);
			cmd.Connection = conc;


			SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.VarChar, 500);
			SqlParaNewID.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID).Value = "";

			SqlParameter SqlParaErrCode = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
			SqlParaErrCode.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaErrCode).Value = "";

			if (conc.State == ConnectionState.Closed)
				conc.Open();
			int value = cmd.ExecuteNonQuery();
			NewID = (string)SqlParaNewID.Value;
			ErrCode = (string)SqlParaErrCode.Value;
			return value;




		}
		catch (Exception ex)
		{
			NewID = "0";
			ErrCode = ex.Message;
			return 0;
		}
		finally
		{
			conc.Close();
		}

	}

	#endregion

	#region otpverification

	public int otpverification(string CustID, string otp, string device_id, string remark, out string NewID, out string ErrCode)
	{
		SqlConnection conc = new SqlConnection(ConnectionString1);
		try
		{
			SqlCommand cmd = new SqlCommand("otpverify", conc);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "otpverify";
			cmd.Parameters.AddWithValue("@CustID", CustID);
			cmd.Parameters.AddWithValue("@otp", otp);
			cmd.Parameters.AddWithValue("@device_id", device_id);
			cmd.Parameters.AddWithValue("@remark", remark);
			cmd.Parameters.AddWithValue("@status", 1);
			cmd.Connection = conc;


			SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.VarChar, 500);
			SqlParaNewID.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID).Value = 0;

			SqlParameter SqlParaErrCode = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
			SqlParaErrCode.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaErrCode).Value = "";

			if (conc.State == ConnectionState.Closed)
				conc.Open();
			int value = cmd.ExecuteNonQuery();
			NewID = (string)SqlParaNewID.Value;
			ErrCode = (string)SqlParaErrCode.Value;
			return value;

		}
		catch (Exception ex)
		{
			NewID = "0";
			ErrCode = ex.Message;
			return 0;
		}
		finally
		{
			conc.Close();
		}

	}

	#endregion

	#region RegitrationInsertTemp

	public int RegitrationInsertTemp(string Email, string Cust_Password, string FullName, string Mobile_No, string Country, string Cust_Type, out string NewID, out string ErrCode)
	{
		SqlConnection conc = new SqlConnection(ConnectionString1);
		try
		{
			SqlCommand cmd = new SqlCommand("RegitrationInsert", conc);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@Email", Email);
			cmd.Parameters.AddWithValue("@Cust_Password", Cust_Password);
			cmd.Parameters.AddWithValue("@FullName", FullName);
			// cmd.Parameters.AddWithValue("@Address", Address);
			cmd.Parameters.AddWithValue("@Mobile_No", Mobile_No);
			cmd.Parameters.AddWithValue("@Country", Country);
			cmd.Parameters.AddWithValue("@Cust_Type", Cust_Type);
			//cmd.Parameters.AddWithValue("@Cust_Photo", Cust_Photo);
			// cmd.Parameters.AddWithValue("@Referral_Code", Referral_Code);
			//cmd.Parameters.AddWithValue("@Referral", Referral);

			SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.VarChar, 500);
			SqlParaNewID.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID).Value = "";

			SqlParameter SqlParaErrMsg = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
			SqlParaErrMsg.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaErrMsg).Value = "";

			if (conc.State == ConnectionState.Closed)
				conc.Open();
			int value = cmd.ExecuteNonQuery();
			NewID = (string)SqlParaNewID.Value;
			ErrCode = (string)SqlParaErrMsg.Value;
			return value;

		}
		catch (Exception ex)
		{
			NewID = "0";
			ErrCode = ex.Message;
			return 0;
		}
		finally
		{
			conc.Close();
		}

	}

	#endregion

	#region RegitrationInsert

	public int RegitrationInsert( string Referral_Code, string Cust_Username, string Email, string Cust_Name, string Password, string Confirm_Password, out string NewID, out string ErrCode)
	{

		//float CusID = 0;
		//CusID = float.Parse(CustID.ToString());

		SqlConnection conc = new SqlConnection(ConnectionString1);
		try
		{
			SqlCommand cmd = new SqlCommand("RegitrationInsert_User", conc);
			cmd.CommandType = CommandType.StoredProcedure;
			//cmd.Parameters.AddWithValue("@CustID", CustID);
			cmd.Parameters.AddWithValue("@Cust_SponserID", Referral_Code);
			cmd.Parameters.AddWithValue("@custusername", Cust_Username);
			cmd.Parameters.AddWithValue("@Email", Email);
			cmd.Parameters.AddWithValue("@Cust_Name", Cust_Name);
			cmd.Parameters.AddWithValue("@Cust_Password", Password);

			SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.VarChar, 500);
			SqlParaNewID.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID).Value = "";

			SqlParameter SqlParaErrMsg = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
			SqlParaErrMsg.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaErrMsg).Value = "";

			if (conc.State == ConnectionState.Closed)
				conc.Open();
			int value = cmd.ExecuteNonQuery();
			NewID = (string)SqlParaNewID.Value;
			ErrCode = (string)SqlParaErrMsg.Value;
			return value;
		}
		catch (Exception ex)
		{
			NewID = "0";
			ErrCode = ex.Message;
			return 0;
		}
		finally
		{
			conc.Close();
		}

	}

	#endregion

	#region Activity_log_detail

	public int Activity_log_detail(string Cust_id, string Task, out string NewID, out string ErrCode)
	{
		SqlConnection conc = new SqlConnection(ConnectionString1);
		try
		{

			SqlCommand cmd = new SqlCommand("Activity_log", conc);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "Activity_log";
			cmd.Parameters.AddWithValue("@Cust_id", Cust_id);
			cmd.Parameters.AddWithValue("@Task", Task);
			cmd.Connection = conc;

			SqlParameter SqlParaNewID = new SqlParameter("@NewID", SqlDbType.VarChar, 500);
			SqlParaNewID.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaNewID).Value = "";

			SqlParameter SqlParaErrCode = new SqlParameter("@ErrCode", SqlDbType.VarChar, 500);
			SqlParaErrCode.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaErrCode).Value = "";

			if (conc.State == ConnectionState.Closed)
				conc.Open();
			int value = cmd.ExecuteNonQuery();
			NewID = (string)SqlParaNewID.Value;
			ErrCode = (string)SqlParaErrCode.Value;
			return value;
		}
		catch (Exception ex)
		{
			NewID = "0";
			ErrCode = ex.Message;
			return 0;
		}
		finally
		{
			conc.Close();
		}

	}

	#endregion

	#region otp_manage

	public int otp_manage(string CustID, string OTP, out string ErrCode1)
	{
		SqlConnection conc = new SqlConnection(ConnectionString1);
		try
		{
			SqlCommand cmd = new SqlCommand("OTP_Inset_manage", conc);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "OTP_Inset_manage";
			cmd.Parameters.AddWithValue("@CustID", CustID);
			cmd.Parameters.AddWithValue("@OTP", OTP);

			SqlParameter SqlParaErrCode = new SqlParameter("@ErrCode1", SqlDbType.VarChar, 500);
			SqlParaErrCode.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(SqlParaErrCode).Value = "";

			if (conc.State == ConnectionState.Closed)
				conc.Open();
			int value = cmd.ExecuteNonQuery();

			// NewIDSub = value;
			ErrCode1 = (string)SqlParaErrCode.Value;
			return value;

		}
		catch (Exception ex)
		{
			ErrCode1 = ex.Message;
			return 0;
		}
		finally
		{
			conc.Close();
		}

	}

	#endregion
}
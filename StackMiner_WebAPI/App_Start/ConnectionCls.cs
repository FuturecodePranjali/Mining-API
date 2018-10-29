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
using System.Net;
using System.IO;
using System.Data.SqlClient;


/// <summary>
/// Summary description for ConnectionCls
/// </summary>
public class ConnectionCls
{
	private ClsDataAccess objClsDataccess = new ClsDataAccess(@"server=45.58.40.201; uid=usa_stackminer_db; pwd=usa_stackminer_db;Connect Timeout = 300");
	//private ClsDataAccess objClsDataccess = new ClsDataAccess(@"Server =45.58.40.201;Database=usa_stackminer_db;integrated security=true");
   // private ClsDataAccess objClsDataccess = new ClsDataAccess(ConfigurationManager.AppSettings[0].ToString());
    public DataSet Countrow;
    public SqlDataAdapter adpt;


    public SqlConnection connc = new SqlConnection(ConfigurationManager.AppSettings["conc"]);
    public SqlCommand cmd_connc = new SqlCommand();
    public SqlDataReader cmd_reader;


    public ConnectionCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// bounds dropdownlists
    /// </summary>
    /// <param name="sqlSelect">SQL Select Statement, first column shuld be Text Field and second one value</param>
    /// <param name="Drop1">DropDownList1</param> 
    public void BoundDropdownWithValue(string sqlSelect, DropDownList Drop1)
    {
        try
        {
            DataTable ds = new DataTable();
            ds = objClsDataccess.GetDataSet(sqlSelect, "table1");
            if (ds != null)
            {
                Drop1.Items.Clear();
                Drop1.DataSource = ds;
                Drop1.DataValueField = ds.Columns[0].ColumnName;
                Drop1.DataTextField = ds.Columns[1].ColumnName;
                
                Drop1.DataBind();

            }
            
            ds.Dispose();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public bool ValidateSqlClause(string sql)
    {
        bool validate = false;
        sql = sql.Replace("'", "");
        if (sql.ToLower().Contains("drop") || sql.ToLower().Contains("delete") || sql.ToLower().Contains("union") || sql.ToLower().Contains("select") || sql.ToLower().Contains("insert") || sql.ToLower().Contains("=") || sql.ToLower().Contains("sys") || sql.ToLower().Contains("update"))
        {
            validate = false;
        }
        else
        {
            validate = true;
        }
        return validate;
    }
     

    /// <summary>
    /// 
    /// </summary>
    /// <param name="strSqlSelectCommand"></param>
    /// <returns></returns>
    public SqlDataReader GetdataReader(string strSqlSelectCommand)
    {

        SqlDataReader sdr = objClsDataccess.GetSqlDataReader(strSqlSelectCommand);
        return sdr;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="strSqlSelectCommand"></param>
    /// <returns></returns>
    public SqlDataReader GetSqlDataReader_second(string strSqlSelectCommand)
    {

        SqlDataReader sdr = objClsDataccess.GetSqlDataReader_second(strSqlSelectCommand);
        return sdr;
    }


    /// <summary>
    /// Used to execute non query
    /// </summary>
    /// <param name="strSqldmlCommand">Sql manupulation command</param>
    /// <returns>int</returns>
    public int ExecuteSqlnonQuery(string strSqldmlCommand)
    {
        return objClsDataccess.ExecuteDML(strSqldmlCommand);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sqlSelectCommand"></param>
    /// <returns></returns>
    public DataSet getdataSet(string sqlSelectCommand)
    {
        DataSet ds = new DataSet();
        ds = objClsDataccess.GetDataSet(sqlSelectCommand);
        return ds;
    }
    /// <summary>
    /// Used to get Aggrigate Value
    /// </summary>
    /// <param name="strSqldmlCommand"></param>
    /// <returns></returns>
    public string getScalerValue(string strSqldmlCommand)
    {
        return objClsDataccess.ExecuteScalerQuery(strSqldmlCommand);
    }
    /// <summary>
    /// jbjk
    /// </summary>
    /// <param name="sqlSelectCommand"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public DataTable getdataSet(string sqlSelectCommand, string tableName)
    {
        return objClsDataccess.GetDataSet(sqlSelectCommand, tableName);
    }
    public void RowCount(string str)
    {
        try
        {
            SqlConnection.ClearAllPools();
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings[0].ToString());
            SqlCommand cmd = new SqlCommand(str, conn);

            cmd.CommandTimeout = 1000;
            conn.Open();
            adpt = new SqlDataAdapter(cmd);
            Countrow = new DataSet();
            adpt.Fill(Countrow);
            // modified march 2015
            //--------------------
            conn.Close();
            //--------------------
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public DataSet getdatanews(string sqlSelectCommand)
    {
        SqlConnection conn = new SqlConnection("Server=184.106.243.186;Database=webcubenews_db;Uid=webcubenews_db;Pwd=webcubenews_db;");
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adpt = new SqlDataAdapter(sqlSelectCommand, conn);
        try
        {


            DataSet ds = new DataSet();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = sqlSelectCommand;
            cmd.CommandType = CommandType.Text;
            adpt.Fill(ds);
            // modified march 2015
            //--------------------
            conn.Close();
            //--------------------

            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            adpt.Dispose();
        }


    }
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
    public int inserrtnewcustrecords(string Cust_SponserID, string Email, string Cust_Address, string Cust_Answer, string Cust_Question, string Cust_City, string Cust_State, string Cust_Country, string Cust_Password, string Cust_Title, string Cust_Name, string Cust_Gender, string Cust_FatherName, DateTime Cust_DOB, string Cust_Pincode, string Cust_mobileNo, string Cust_nominee, string Cust_Relation, string Cust_Package, string Cust_Location, string Cust_TempPinID, int PayMode, string Cust_BankName, string Cust_BankAcc, string Cust_BankIFSC, string Cust_BankBranch, string Cust_PanID,string _custusername , out string NewID, out string ErrCode)
    {

        int i = objClsDataccess.InsertCustRecords(Cust_SponserID, Email, Cust_Address, Cust_Answer, Cust_Question, Cust_City, Cust_State, Cust_Country, Cust_Password, Cust_Title, Cust_Name, Cust_Gender, Cust_FatherName, Cust_DOB, Cust_Pincode, Cust_mobileNo, Cust_nominee, Cust_Relation, Cust_Package, Cust_Location, Cust_TempPinID, PayMode, Cust_BankName, Cust_BankAcc, Cust_BankIFSC, Cust_BankBranch, Cust_PanID,_custusername, out   NewID, out   ErrCode);
        return i;
    }
    /// <summary>
    /// Topup Account
    /// </summary>
    /// <param name="cusid">Topup ID</param>
    /// <param name="pcode">Product COde</param>
    /// <param name="multiple">Multiple of Product Amount</param>
    /// <param name="EwalletID">E-Wallet ID</param>
    /// <param name="flag">out type INT</param>
    /// <param name="errMsg">out Type STRING</param>
    /// <returns></returns>
    ///

    public int loanAccount( string sessionid, out int flag, out string errMsg)
    {
        return objClsDataccess.loanAccount(sessionid, out flag, out errMsg);
    }

    public int topupAccount_Advt(string cusid, string pinno, string advtno, string sessionid, out int flag, out string errMsg)
    {
        return objClsDataccess.topupaccount_advt(cusid, pinno, advtno, sessionid, out flag, out errMsg);
    }
    
    public int Payemnt_addfund_gateway(string gateway, string orderid, string cusidtomerid, string or_curr_amt,string return_status,string BTC_Rate, string CoinName,  out int flag, out string errMsg)
    {
        return objClsDataccess.Payemnt_addfund_gateway(gateway, orderid, cusidtomerid, or_curr_amt, return_status,BTC_Rate,CoinName, out flag, out errMsg);
    }

    public int Payemnt_callback_gateway(string gateway,string re_orderid, string re_status, string amount_btc, string amount_usd_val,  string re_userid,  string crypto_curr,  string txno, string confirmed_count, out int flag, out string errMsg)
    {
        return objClsDataccess.Payemnt_callback_gateway(gateway, re_orderid, re_status, amount_btc, amount_usd_val, re_userid, crypto_curr, txno, confirmed_count, out flag, out errMsg);
    }

    
    public int topupAccount_choreservices(string cusid, string pinno, string advtno, string sessionid, string m_demandorderno, out int flag, out string errMsg)
    {
        return objClsDataccess.topupAccount_choreservices(cusid, pinno, advtno, sessionid,  m_demandorderno, out flag, out errMsg);
    }

    
    public int topupAccount_advt_sahaj(string cusid, string pinno, string sessionid, out int flag, out string errMsg)
    {
        return objClsDataccess.topupAccount_advt_sahaj(cusid, pinno, sessionid, out flag, out errMsg);
    }
    public int topupAccount(string cusid, string pinno, string sessionid, out int flag, out string errMsg)
    {
        return objClsDataccess.topupaccount(cusid, pinno,sessionid, out flag, out errMsg);
    }
     public int datatransfer_oldtonew(string session_username, string old_cusid, string new_cusid, out int flag, out string errMsg)
    {
        return objClsDataccess.datatransfer_oldtonew(session_username, old_cusid, new_cusid, out flag, out errMsg);
    }

     public int buyshare(string cusid, string txt_ews_pws, string m_shareqty, string Validity, string category, string w_wallettype, string p_code, string btc_amt,string orderno, out int flag, out string errMsg)
     {
         return objClsDataccess.buyshare(cusid, txt_ews_pws, m_shareqty, Validity, category, w_wallettype, p_code, btc_amt, orderno, out flag, out errMsg);
     }

    
    public int topupAccount1(string cusid, string pinno, string sendercomment, string tier, out int flag, out string errMsg)
    {
        return objClsDataccess.topupaccount1(cusid, pinno, sendercomment,   tier, out flag, out errMsg);
    }
    public int inserrtalertpaycustrecords(string Cust_SponserID, string Email, string Cust_Address, string Cust_Answer, string Cust_Question, string Cust_City, string Cust_State, string Cust_Country, string Cust_Password, string Cust_Title, string Cust_Name, string lastname, string Cust_Gender, string Cust_FatherName, DateTime Cust_DOB, string Cust_Pincode, string Cust_PhoneNo, string Cust_mobileNo, string Cust_nominee, string Cust_Relation, string _cust_panid, string Cust_Package, string Cust_Location, string Cust_TempPinID, int PayMode, string Alertpayacc, string Alertpayaccname, string depositdate, string tranid, out string NewID, out string ErrCode)
    {

        int i = objClsDataccess.InsertalertCustRecords(Cust_SponserID, Email, Cust_Address, Cust_Answer, Cust_Question, Cust_City, Cust_State, Cust_Country, Cust_Password, Cust_Title, Cust_Name, lastname, Cust_Gender, Cust_FatherName, Cust_DOB, Cust_Pincode, Cust_PhoneNo, Cust_mobileNo, Cust_nominee, Cust_Relation, _cust_panid, Cust_Package, Cust_Location, Cust_TempPinID, PayMode, Alertpayacc, Alertpayaccname, depositdate, tranid, out NewID, out   ErrCode);
        return i;
    }

    public static DateTime getIndianDateTime()
    {
        DateTime utc, dt;
        dt = DateTime.Now;
        utc = dt.ToUniversalTime();
        dt = utc.AddMinutes(330);
        return dt;
    }
    /// <summary>
    /// Send SMS
    /// </summary>
    /// <param name="baseurl"></param>
    public string FireSMS(string baseurl)
    {
        try
        {
            WebClient client1 = new WebClient();
            Stream data1 = client1.OpenRead(baseurl);
            StreamReader reader1 = new StreamReader(data1);
            string s1 = reader1.ReadToEnd();
            data1.Close();
            reader1.Close();
            return s1;
        }
        catch (Exception ex)
        {
            ExecuteSqlnonQuery("INSERT INTO [dbo].[ErrLog]  ([Panel],[errDate] ,[ErrMessage])VALUES('User-SMS' ,'" + ConnectionCls.getIndianDateTime() + "','" + ex.Message.ToString() + "'");
            return ex.Message;
        }
    }



    #region Internal Fund Transfer fundtransfer_clubtomain

    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">Session User ID</param>
    /// <param name="CusID2">ID to Transfer</param>
    /// <param name="amount">Transfer Amount</param>
    /// <returns>TransactionID</returns>
    public double fundtransfer_clubtomain(string CusID1, string CusID2, string amount, int transferFrom)
    {
        return objClsDataccess.fundtransfer_clubtomain(CusID1, CusID2, amount, transferFrom);
    }
    #endregion

    #region Internal Fund Transfer fundtransfer_binarytomain

    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">Session User ID</param>
    /// <param name="CusID2">ID to Transfer</param>
    /// <param name="amount">Transfer Amount</param>
    /// <returns>TransactionID</returns>
    public double fundtransfer_binarytomain(string CusID1, string CusID2, string amount, int transferFrom)
    {
        return objClsDataccess.fundtransfer_binarytomain(CusID1, CusID2, amount, transferFrom);
    }
    #endregion


    #region change in password

    /// <summary>
    /// change in password
    /// </summary>
    /// <param name="CusID1">Session User ID</param>
    /// <param name="CusID2">ID to Transfer</param>
    /// <param name="amount">Transfer Amount</param>
    /// <returns>TransactionID</returns>
    public double change_password(string CusID1, string oldpassword, string newPassword, string remark, out string retunmsg,  out int flag)
    {
        return objClsDataccess.change_password(CusID1,oldpassword, newPassword, remark, out retunmsg,  out flag);
    }
    #endregion
 

    #region Internal Fund Transfer

    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">Session User ID</param>
    /// <param name="CusID2">ID to Transfer</param>
    /// <param name="amount">Transfer Amount</param>
    /// <returns>TransactionID</returns>
    public double InternalFundTransfer(string CusID1, string CusID2, string amount, int transferFrom)
    {
        return objClsDataccess.InternalFundTransfer(CusID1, CusID2, amount, transferFrom);
    }
    #endregion


    #region Internal Fund Transfer own account

    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">Session User ID</param>
    /// <param name="CusID2">ID to Transfer</param>
    /// <param name="amount">Transfer Amount</param>
    /// <returns>TransactionID</returns>
    public double InternalFundTransfer(string CusID1, string amount)
    {
        return objClsDataccess.InternalFundTransfer(CusID1, amount);
    }
    #endregion


    #region Internal Fund Transfer own account ewallet to pinwallet

    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">Session User ID</param>
    /// <param name="CusID2">ID to Transfer</param>
    /// <param name="amount">Transfer Amount</param>
    /// <returns>TransactionID</returns>
    public double Proctransferfundmainwallettopinwallet(string CusID1, string amount)
    {
        return objClsDataccess.Proctransferfundmainwallettopinwallet(CusID1, amount);
    }
    #endregion

    /// <summary>
    /// Transfer E-Pin User To User
    /// </summary>
    /// <param name="qty"></param>
    /// <param name="pcode"></param>
    /// <param name="idfrom"></param>
    /// <param name="idto"></param>
    /// <returns></returns>
    public int TransferEpin(int qty, string pcode, float  amount,  string idfrom, string idto, out int flag)
    {
        return objClsDataccess.TransferEpin(qty, pcode, amount, idfrom, idto, out flag);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="Cust_City"></param>
    /// <param name="Cust_PhoneNo"></param>
    /// <param name="Cust_Address"></param>
    /// <param name="Cust_State"></param>
    /// <param name="Cust_Pincode"></param>
    /// <param name="email"></param>
    /// <param name="Cust_DOB"></param>
    /// <returns></returns>
    public int updateUserPersonalDetails(string cusid, string Cust_City, string Cust_PhoneNo, string Cust_Address, string Cust_Country,  string email, string Cust_DOB)
    {
        return objClsDataccess.updateUserPersonalDetails(cusid, Cust_City, Cust_PhoneNo, Cust_Address, Cust_Country, email, Cust_DOB);
    }
    /// <summary>
    /// Update User nominee Datails
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="name"></param>
    /// <param name="Relationship"></param>
    /// <param name="City"></param>
    /// <param name="State"></param>
    /// <param name="Mobile"></param>
    /// <param name="Address"></param>
    /// <returns></returns>
    public int updateUsersNominee(string cusid, string name, string Relationship, string City, string State, string Mobile, string Address)
    {
        return objClsDataccess.updateUsersNominee(cusid, name, Relationship, City, State, Mobile, Address);
    }
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
        return objClsDataccess.updateUsersBankDetails(cusid, Cust_PanID, bankachodename, Cust_BankBranch, BankState, Cust_BankName, Cust_BankAcc, BankCity, Cust_BankIFSC);
    }

    public int update_mob_appdetail(string cusid, string txt_app_name_m, string txt_mob_app_name_m, string txt_mob_acno_m, string txt_remark_m)
    {
        return objClsDataccess.update_mob_appdetail(cusid, txt_app_name_m, txt_mob_app_name_m, txt_mob_acno_m, txt_remark_m);
    }


    /// <summary>
    /// PTB Point Widrawl requests
    /// </summary>
    /// <param name="cusid"></param>
    /// <param name="RqAmount"></param>
    /// <param name="rqno"></param>
    /// <returns></returns>
    public int RequestForPtbWidrawl(string cusid, string RqAmount, string percantage, string appwalletno, string address, out string rqno, out string errMsg)
    {
        return objClsDataccess.RequestForPtbWidrawl(cusid, RqAmount, percantage, appwalletno, address, out  rqno, out  errMsg);
    }
    public int RequestForPtbWidrawl_1(string cusid, float RqAmount, float txno ,out string rqno, out string errMsg)
    {
        return objClsDataccess.RequestForPtbWidrawl_1(cusid, RqAmount, txno, out  rqno, out  errMsg);
    }
    /// <summary>
    ///TransferPointsPTBtoRTB 
    /// </summary>
    /// <param name="CusID"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public double InternalTransferPointsPTBtoRTB(string CusID, int mode, string amount)
    {
        return objClsDataccess.InternalTransferPointsPTBtoRTB(CusID, mode, amount);
    }

    public int shop_sessioncart(string cusid, string sessionid, string proId, string m_qty, out int flag, out string errMsg)
    {
        return objClsDataccess.shop_sessioncart(cusid, sessionid, proId, m_qty, out flag, out errMsg);
    }

    public int shop_sessioncart_payment(string cusid, string sessionid, string proId, string m_qty, string m_shipping, string delivery_add, string mobileno, out int flag, out string errMsg)
    {
        return objClsDataccess.shop_sessioncart_payment(cusid, sessionid, proId, m_qty, m_shipping, delivery_add, mobileno, out flag, out errMsg);
    }
    public int makecommit(string cusid, string pcode, int multiple, string EwalletID, out int flag, out string errMsg)
    {
        return objClsDataccess.makecommit(cusid, pcode, multiple, EwalletID, getIndianDateTime(), out flag, out errMsg);
    }

    public double BidInsert(string pname, string pdetail, string cat, string status, string pp, string fname, string brate, DateTime sdate, DateTime edate, out int flag, out string errMsg)
    {
        return objClsDataccess.Insertbid(pname, pdetail, cat, status, pp, fname, brate, sdate, edate, out flag, out errMsg);
    }
    
    public double BidUpdate(string id, string pname, string pdetail, string cat, string status, string pp, string fname, string brate, DateTime sdate, DateTime edate, out int flag, out string errMsg)
    {
        return objClsDataccess.Updatebid(id, pname, pdetail, cat, status, pp, fname, brate, sdate, edate, out flag, out errMsg);
    }
    
    public int updatemessagedetail(string toid, string fromid, string advtno, string payment_detail)
    {
        return objClsDataccess.updatemessagedetail(toid,fromid,advtno, payment_detail);
    }

    public int support_ticket(float sender, float receiver, string messagedtl, out int ticketno, out string errMsg)
    {
        return objClsDataccess.support_ticket(sender, receiver, messagedtl, out ticketno, out errMsg);
    }

    public int webpromotion(float sender, string typeofcontnet, string urldtl, string messagedtl, string remark, out int ticketno, out string errMsg)
    {
        return objClsDataccess.webpromotion(sender, typeofcontnet, urldtl,messagedtl,remark, out ticketno, out errMsg);
    }



	public int update_multi_emailverify(string cusid, string msgtpe, out int flag, out string errMsg)
	{
		return objClsDataccess.update_multi_emailverify(cusid, msgtpe, out flag, out errMsg);
	}



	public int Check_login_detail(string username, string pws, out int flag, out string Name, out string m_walletbal,  out string errMsg)
	{
		return objClsDataccess.Check_login_detail(username, pws, out flag, out Name,out m_walletbal,  out errMsg);
	}

	public int Check_balance_detail(string emailid, string pws, out string m_sponsor,out string m_password, out int flag, out string Name_msg, out string m_walletbal, out string errMsg)
	{
		return objClsDataccess.Check_balance_detail(emailid, pws, out m_sponsor,out m_password, out flag, out Name_msg, out m_walletbal, out errMsg);
	}



	public int verification_byregistration(string cusid, out int flag, out string errMsg)
    {
        return objClsDataccess.verification_byregistration(cusid, out flag, out errMsg);
    } 



    #region Internal Fund Transfer own account ewallet to rechrage

    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">Session User ID</param>
    /// <param name="CusID2">ID to Transfer</param>
    /// <param name="amount">Transfer Amount</param>
    /// <returns>TransactionID</returns>
    public double Proctransfermainwallettorechargewallet(string CusID1, string amount)
    {
        return objClsDataccess.Proctransfermainwallettorechargewallet(CusID1, amount);
    }
    #endregion


    #region other wallet to main wallet

    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">Session User ID</param>
    /// <param name="CusID2">ID to Transfer</param>
    /// <param name="amount">Transfer Amount</param>
    /// <returns>TransactionID</returns>
    public double transferotherwallettomainwallet(string CusID1, string amount)
    {
        return objClsDataccess.transferotherwallettomainwallet(CusID1, amount);
    }

    //public void ETCCoin_Purchase(string v1, string txt_ews_pws, string pcode, string w_wallettype, string m_shareqty, string v2, float v3, out int flag, out string errMsg)
    //{
    //    throw new NotImplementedException();
    //}
    #endregion


    #region club wallet to main wallet

    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">Session User ID</param>
    /// <param name="CusID2">ID to Transfer</param>
    /// <param name="amount">Transfer Amount</param>
    /// <returns>TransactionID</returns>
    public double transfer_club_walletto_mainwallet(string CusID1, string amount)
    {
        return objClsDataccess.transfer_club_walletto_mainwallet(CusID1, amount);
    }
    #endregion

    #region Internal Fund Transfer own account to bidwallet

    /// <summary>
    /// Internal Fund Transfer
    /// </summary>
    /// <param name="CusID1">Session User ID</param>
    /// <param name="CusID2">ID to Transfer</param>
    /// <param name="amount">Transfer Amount</param>
    /// <returns>TransactionID</returns>
    public double InternalFundTransfer_bidwallet(string CusID1, string amount)
    {
        return objClsDataccess.InternalFundTransfer_bidwallet(CusID1, amount);
    }
    #endregion




    #region "Topup   TopupPingeneration_advt_Ewallet"
    /// <summary>
    /// Topup Pin generation
    /// </summary>
    /// <param name="qty"></param>
    /// <param name="CusID"></param>
    /// <param name="flag"></param>
    /// <returns></returns>
    /// m_qty, advtno, amtval, customerid, DropDownList1.Text, franid
    public double TopupPingeneration_advt_Ewallet(string qty, string advtno, string amtval,string customerid, string downval,  string franid, out int flag)
    {
        try
        {
            double trID = 0;
            SqlConnection conc = new SqlConnection(objClsDataccess.ConnectionStringValue);
            SqlCommand cmd1 = new SqlCommand("[dbo].[franchisee_pinGen_advt_transfer]", conc);
            conc.Open();
            cmd1.Parameters.Add(new SqlParameter("@pinQnty", qty));
            cmd1.Parameters.Add(new SqlParameter("@amount1", amtval));
            cmd1.Parameters.Add(new SqlParameter("@Product_Code", downval));
            cmd1.Parameters.Add(new SqlParameter("@Advtno", advtno));
            cmd1.Parameters.Add(new SqlParameter("@TransfferedTo", customerid));
            cmd1.Parameters.Add(new SqlParameter("@franchiseeID", franid));
            cmd1.Parameters.Add(new SqlParameter("@activatestatus", "TOPUP"));
            cmd1.Parameters.Add(new SqlParameter("@des", "Topup E-Pin generation (" + qty + ")"));
            cmd1.Parameters.Add(new SqlParameter("@status", 0)).Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(new SqlParameter("@TrID", 0)).Direction = ParameterDirection.Output;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.ExecuteNonQuery();
            conc.Close();

            flag = Convert.ToInt32(cmd1.Parameters["@status"].Value);
            trID = Convert.ToDouble(cmd1.Parameters["@trID"].Value);
            cmd1.Dispose();
            return trID;
        }
        catch (Exception ex)
        {
            flag = 0;
            return 0;
        }
    }

    #endregion

    #region "Topup Pin generation"
    /// <summary>
    /// Topup Pin generation
    /// </summary>
    /// <param name="qty"></param>
    /// <param name="CusID"></param>
    /// <param name="flag"></param>
    /// <returns></returns>
    public double TopupPingeneration_Ewallet(string qty, string amount1, string Product_Code, string CusID, out int flag)
    {
        try
        {
            double trID = 0;
            SqlConnection conc = new SqlConnection(objClsDataccess.ConnectionStringValue);
            SqlCommand cmd1 = new SqlCommand("[dbo].[spActionMedia_pinGeneration_topupbyfranchisee]", conc);
            conc.Open();
            cmd1.Parameters.Add(new SqlParameter("@pinQnty", qty));
            cmd1.Parameters.Add(new SqlParameter("@amount1", amount1)); 
            cmd1.Parameters.Add(new SqlParameter("@Product_Code", Product_Code));
            cmd1.Parameters.Add(new SqlParameter("@TransfferedTo", CusID));
            cmd1.Parameters.Add(new SqlParameter("@activatestatus", "TOPUP"));
            cmd1.Parameters.Add(new SqlParameter("@des", "Topup E-Pin generation (" + qty + ")"));
            cmd1.Parameters.Add(new SqlParameter("@status", 0)).Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(new SqlParameter("@TrID", 0)).Direction = ParameterDirection.Output;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.ExecuteNonQuery();
            conc.Close();

            flag = Convert.ToInt32(cmd1.Parameters["@status"].Value);
            trID = Convert.ToDouble(cmd1.Parameters["@trID"].Value);
            cmd1.Dispose();
            return trID;
        }
        catch (Exception ex)
        {
            flag = 0;
            return 0;
        }
    }

    #endregion


    #region "Registration Pin generation"
    /// <summary>
    /// Registration Pin generation
    /// </summary>
    /// <param name="qty">qUANTITY</param>
    /// <param name="CusID"></param>
    /// <param name="flag"></param>
    /// <returns></returns>
    public double RegistrationPingeneration_Ewallet(string qty, string Product_Code, string CusID, out int flag)
    {
        try
        {
            double trID = 0;
            SqlConnection conc = new SqlConnection(objClsDataccess.ConnectionStringValue);
            SqlCommand cmd1 = new SqlCommand("[dbo].[spActionMedia_pinGeneration_registration]", conc);
            conc.Open();
            cmd1.Parameters.Add(new SqlParameter("@pinQnty", qty));
            cmd1.Parameters.Add(new SqlParameter("@Product_Code", Product_Code));
            cmd1.Parameters.Add(new SqlParameter("@TransfferedTo", CusID));
            cmd1.Parameters.Add(new SqlParameter("@activatestatus", "REGISTRATION"));
            cmd1.Parameters.Add(new SqlParameter("@des", "Registration E-Pin generation (" + qty + ")"));
            cmd1.Parameters.Add(new SqlParameter("@status", 0)).Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(new SqlParameter("@TrID", 0)).Direction = ParameterDirection.Output;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.ExecuteNonQuery();
            conc.Close();

            flag = Convert.ToInt32(cmd1.Parameters["@status"].Value);
            trID = Convert.ToDouble(cmd1.Parameters["@trID"].Value);
            cmd1.Dispose();
            return trID;
        }
        catch (Exception ex)
        {
            flag = 0;
            return 0;
        }
    }

    #endregion


    #region "Promotion Pin generation"
    /// <summary>
    /// Promotion Pin generation
    /// </summary>
    /// <param name="qty"></param>
    /// <param name="CusID"></param>
    /// <param name="flag"></param>
    /// <returns></returns>
    public double PromotionPingeneration_Ewallet(string qty, string Product_Code, string CusID, out int flag)
    {
        try
        {
            double trID = 0;
            SqlConnection conc = new SqlConnection(objClsDataccess.ConnectionStringValue);
            SqlCommand cmd1 = new SqlCommand("[dbo].[spActionMedia_pinGeneration_promotion]", conc);
            conc.Open();
            cmd1.Parameters.Add(new SqlParameter("@pinQnty", qty));
            cmd1.Parameters.Add(new SqlParameter("@Product_Code", Product_Code));
            cmd1.Parameters.Add(new SqlParameter("@TransfferedTo", CusID));
            cmd1.Parameters.Add(new SqlParameter("@activatestatus", "PROMOTION"));
            cmd1.Parameters.Add(new SqlParameter("@des", "Promotion E-Pin generation (" + qty + ")"));
            cmd1.Parameters.Add(new SqlParameter("@status", 0)).Direction = ParameterDirection.Output;
            cmd1.Parameters.Add(new SqlParameter("@TrID", 0)).Direction = ParameterDirection.Output;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.ExecuteNonQuery();
            conc.Close();

            flag = Convert.ToInt32(cmd1.Parameters["@status"].Value);
            trID = Convert.ToDouble(cmd1.Parameters["@trID"].Value);
            cmd1.Dispose();
            return trID;
        }
        catch (Exception ex)
        {
            flag = 0;
            return 0;
        }
    }

    #endregion

    /// <summary>
    /// jhghj
    /// </summary>
    /// <param name="Cust_SponserID"></param>
    /// <param name="Email"></param>
    /// <param name="Cust_Address"></param>
    /// <param name="Cust_Answer"></param>
    /// <param name="Cust_Question"></param>
    /// <param name="Cust_City"></param>
    /// <param name="Cust_Country"></param>
    /// <param name="Cust_Password"></param>
    /// <param name="Cust_Title"></param>
    /// <param name="Cust_Name"></param>
    /// <param name="Cust_Lastname"></param>
    /// <param name="Cust_Gender"></param>
    /// <param name="Cust_FatherName"></param>
    /// <param name="Cust_DOB"></param>
    /// <param name="Cust_Pincode"></param>
    /// <param name="Cust_PhoneNo"></param>
    /// <param name="Cust_mobileNo"></param>
    /// <param name="Cust_nominee"></param>
    /// <param name="Cust_Relation"></param>
    /// <param name="Cust_PanID"></param>
    /// <param name="Cust_Package"></param>
    /// <param name="Cust_Location"></param>
    /// <param name="Cust_TempPinID"></param>
    /// <param name="Cust_BankName"></param>
    /// <param name="Cust_BankAdd"></param>
    /// <param name="Cust_BankBranch"></param>
    /// <param name="Cust_BankAcc"></param>
    /// <param name="plannerId"></param>
    /// <param name="Product_category"></param>
    /// <param name="NewID"></param>
    /// <param name="ErrCode"></param>
    /// <returns></returns>
    public int inserrtnewjoining(string Cust_SponserID, string Email, string Cust_Address, string Cust_Answer, string Cust_Question, string Cust_City, string Cust_Country, string Cust_Password, string Cust_Title, string Cust_Name, string Cust_Lastname, string Cust_Gender, string Cust_FatherName, DateTime Cust_DOB, string Cust_Pincode, string Cust_PhoneNo, string Cust_mobileNo, string Cust_nominee, string Cust_Relation, string Cust_PanID, string Cust_Package, string Cust_Location, string Cust_TempPinID, string Cust_BankName, string Cust_BankAdd, string Cust_BankBranch, string Cust_BankAcc, int plannerId, string Product_category, out string NewID, out string ErrCode)
    {

        int i = objClsDataccess.InsertCustjoining(Cust_SponserID, Email, getIndianDateTime(), Cust_Address, Cust_Answer, Cust_Question, Cust_City, Cust_Country, Cust_Password, Cust_Title, Cust_Name, Cust_Lastname, Cust_Gender, Cust_FatherName, Cust_DOB, Cust_Pincode, Cust_PhoneNo, Cust_mobileNo, Cust_nominee, Cust_Relation, Cust_PanID, Cust_Package, Cust_Location, Cust_TempPinID, Cust_BankName, Cust_BankAdd, Cust_BankBranch, Cust_BankAcc, plannerId, Product_category, out NewID, out ErrCode);
        return 1;
    }


    /// <summary>
    /// ///////////////////Encrypt////////////////////////////////
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    /// 


    public string Encrypt(string str)
    {
        try
        {
            string encodedString;

            encodedString = (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(str)));

            return encodedString;
        }
        catch (SqlException decodedString1)
        {
            return decodedString1.ToString();
        }
    }
    //public string Encrypt(string val)
    //{
    //    var bytes = System.Text.Encoding.UTF8.GetBytes(val);
    //    var encBytes = System.Security.Cryptography.ProtectedData.Protect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
    //    return Convert.ToBase64String(encBytes);
    //}





    /// <summary>
    /// ///////////////////////Decrypt////////////////////////////
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>

    public string Decrypt(string str)
    {
        try
        {
            string decodedString;

            decodedString = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(str));

            return decodedString;
        }
        catch (SqlException decodedString1)
        {
            return decodedString1.ToString();
        }
    }



    //public string Decrypt(string val)
    //{
    //    var bytes = Convert.FromBase64String(val);
    //    var encBytes = System.Security.Cryptography.ProtectedData.Unprotect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
    //    return System.Text.Encoding.UTF8.GetString(encBytes);
    //}
    ////////////////////////////////////////////////////////////////

    public string _siteurl1; public string _siteurl2; public string _siteurl3; public string _siteurl4; public string _siteurl5; public string _siteurl6; public string _siteurl7; public string _siteurl8; public string _siteurl9; public string _siteurl10; public string _siteurl11; public string _siteurl12; public string _siteurl13; public string _siteurl14;

    public string _bannerurl1; public string _bannerurl2; public string _bannerurl3; public string _bannerurl4; public string _bannerurl5; public string _bannerurl6; public string _bannerurl7; public string _bannerurl8; public string _bannerurl9; public string _bannerurl10; public string _bannerurl11; public string _bannerurl12; public string _bannerurl13; public string _bannerurl14;








    /// <summary>
    /// method to get Client ip address
    /// </summary>
    /// <param name="GetLan"> set to true if want to get local(LAN) Connected ip address</param>
    /// <returns></returns>
    public static string GetVisitorIPAddress(bool GetLan)
    // public static string GetVisitorIPAddress()
    {
        string visitorIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (String.IsNullOrEmpty(visitorIPAddress))
            visitorIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

        if (string.IsNullOrEmpty(visitorIPAddress))
            visitorIPAddress = HttpContext.Current.Request.UserHostAddress;

        if (string.IsNullOrEmpty(visitorIPAddress) || visitorIPAddress.Trim() == "::1")
        {
            GetLan = true;
            visitorIPAddress = string.Empty;
        }

        if (GetLan)
        {
            if (string.IsNullOrEmpty(visitorIPAddress))
            {
                //This is for Local(LAN) Connected ID Address
                string stringHostName = Dns.GetHostName();
                //Get Ip Host Entry
                IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);
                //Get Ip Address From The Ip Host Entry Address List
                IPAddress[] arrIpAddress = ipHostEntries.AddressList;

                try
                {
                    visitorIPAddress = arrIpAddress[arrIpAddress.Length - 2].ToString();
                }
                catch
                {
                    try
                    {
                        visitorIPAddress = arrIpAddress[0].ToString();
                    }
                    catch
                    {
                        try
                        {
                            arrIpAddress = Dns.GetHostAddresses(stringHostName);
                            visitorIPAddress = arrIpAddress[0].ToString();
                        }
                        catch
                        {
                            visitorIPAddress = "127.0.0.1";
                        }
                    }
                }
            }
        }
        return visitorIPAddress;
    }

    #region Insert_CoinAddress

    public int Insert_CoinAddress(string CustID, string CoinAddress, string Coins, out int flag, out string errMsg)
    {
        return objClsDataccess.Insert_CoinAddress(CustID, CoinAddress, Coins, out flag, out errMsg);
    }

    #endregion
    #region Update_BIXC_Entry

    public int Update_BIXC_Entry(string CustID, string Category, string TxID, string Amount, string TimeReceived, string Confirmations, out int flag, out string errMsg)
    {
        return objClsDataccess.Update_BIXC_Entry(CustID, Category, TxID, Amount, TimeReceived, Confirmations, out flag, out errMsg);
    }

    #endregion
    #region Update_BIXC_Entry_New_Success_Pending

    public int Update_BIXC_Entry_New_Success_Pending(string CustID, string Category, string TxID, string Amount, string Time, string Confirmations, string TransferStatus, string Address, out int flag, out string errMsg)
    {
        return objClsDataccess.Update_BIXC_Entry_New_Success_Pending(CustID, Category, TxID, Amount, Time, Confirmations, TransferStatus, Address, out flag, out errMsg);
    }

    #endregion



    // JAVID CODE


    #region ETHCoin_Purchase

    public int ETHCoin_Purchase(string CusID, string txt_ews_pws, string pcode, string w_wallettype, string m_shareqty, string Category, float btc_amt, out int flag, out string errMsg)
    {
        return objClsDataccess.ETHCoin_Purchase(CusID, txt_ews_pws, pcode, w_wallettype, m_shareqty, Category, btc_amt, out flag, out errMsg);
    }

    #endregion


    #region AddFund_GLTC_Address_Delete_Previous

    public int AddFund_GLTC_Address_Delete_Previous(string SerialNO, out int flag, out string errMsg)
    {
        return objClsDataccess.AddFund_GLTC_Address_Delete_Previous(SerialNO, out flag, out errMsg);
    }

    #endregion


    #region Generate_BIXC_Address

    public int Generate_BIXC_Address(string BIXC_Address, string CustID, out int flag, out string errMsg)
    {
        return objClsDataccess.Generate_BIXC_Address(BIXC_Address, CustID, out flag, out errMsg);
    }

    #endregion

    #region AddFund_GLTC_Address_AutoState_Update

    public int AddFund_GLTC_Address_AutoState_Update(string SerialNO, out int flag, out string errMsg)
    {
        return objClsDataccess.AddFund_GLTC_Address_AutoState_Update(SerialNO, out flag, out errMsg);
    }

    #endregion

    #region Update_GLTC_Address_Status_New

    public int Update_GLTC_Address_Status_New(string adminStaffId, string GLTC_Address, string CustID, string Coin_Balance, string m_shareqty, string ValidStatus, string TXN, string Dates, string SerialNO, string UsedType, string LVPS_Rate, out int flag, out string errMsg)
    {
        return objClsDataccess.Update_GLTC_Address_Status_New(adminStaffId, GLTC_Address, CustID, Coin_Balance, m_shareqty, ValidStatus, TXN, Dates, SerialNO, UsedType, LVPS_Rate, out flag, out errMsg);
    }

    #endregion

    #region AddFund_GLTC_Address_Update_Previous

    public int AddFund_GLTC_Address_Update_Previous(string SerialNO, out int flag, out string errMsg)
    {
        return objClsDataccess.AddFund_GLTC_Address_Update_Previous(SerialNO, out flag, out errMsg);
    }

    #endregion

    #region AddFund_GLTC_Address

    public int AddFund_GLTC_Address(string GLTC_Address, string CustID, string Coin_Balance, string Share_Qty, string UsedType, out int flag, out string errMsg)
    {
        return objClsDataccess.AddFund_GLTC_Address(GLTC_Address, CustID, Coin_Balance, Share_Qty, UsedType, out flag, out errMsg);
    }

    #endregion

    #region Update_GLTC_Address_Status_PackageUpgrade

    public int Update_GLTC_Address_Status_PackageUpgrade(string adminStaffId, string GLTC_Address, string CustID, string Coin_Balance, string m_shareqty, string ValidStatus, string UsedType, out int flag, out string errMsg)
    {
        return objClsDataccess.Update_GLTC_Address_Status_PackageUpgrade(adminStaffId, GLTC_Address, CustID, Coin_Balance, m_shareqty, ValidStatus, UsedType, out flag, out errMsg);
    }

    #endregion   


    #region spUpdate_BIXC_Entry_New_Success_Pending_Coin_New_BTC

    public int spUpdate_BIXC_Entry_New_Success_Pending_Coin_New_BTC(string CustID, string TrID, string TxID, string Confirmations, string MarketID, out int flag, out string errMsg)
    {
        return objClsDataccess.spUpdate_BIXC_Entry_New_Success_Pending_Coin_New_BTC(CustID, TrID, TxID, Confirmations, MarketID, out flag, out errMsg);
    }

    #endregion  



    #region Update_BIXC_Entry_New_Success_Pending_Coin_BTC

    public int Update_BIXC_Entry_New_Success_Pending_Coin_BTC(string CustID, string Category, string TxID, string Amount, string Time, string Confirmations, string MarketID,string USDAmt, out int flag, out string errMsg)
    {
        return objClsDataccess.Update_BIXC_Entry_New_Success_Pending_Coin_BTC(CustID, Category, TxID, Amount, Time, Confirmations, MarketID, USDAmt, out flag, out errMsg);
    }

    #endregion


    #region Insert_OTP_Code

    public int Insert_OTP_Code(string CustID, string Type, out int flag, out string errMsg, out string Result_OTP_Code)
    {
        return objClsDataccess.Insert_OTP_Code(CustID, Type, out flag, out errMsg, out Result_OTP_Code);
    }

	#endregion


	public int ORDERID(string pno,  out int flag, out string errMsg)
	{
		return objClsDataccess.ORDERID(pno, out flag, out errMsg);
	}

	public int ORDERIDIVESTMENT(string cusid, string txt_ews_pws, string m_shareqty, string Validity, string category, string w_wallettype, string p_code, string btc_amt,string orderno, out int flag, out string errMsg)
	{
		return objClsDataccess.ORDERIDIVESTMENT(cusid, txt_ews_pws, m_shareqty, Validity, category, w_wallettype, p_code, btc_amt, orderno, out flag, out errMsg);
	}


	public int inserrttemppass(string username, string id, string name, string strHostName, string ipaddress, string pass, string email, string mobileno, out string NewID, out string ErrCode)
	{

		int i = objClsDataccess.inserrttemppass(username, id, name, strHostName, ipaddress, pass, email, mobileno, out NewID, out ErrCode);
		return i;
	}


	public int inserrtnewtempEntry(string Cust_SponserID, string Email, string Cust_Address, string Cust_Answer, string Cust_Question, string Cust_City, string Cust_State, string Cust_Country, string Cust_Password, string Cust_Title, string Cust_Name, string Cust_Gender, string Cust_FatherName, DateTime Cust_DOB, string Cust_Pincode, string Cust_mobileNo, string Cust_nominee, string Cust_Relation, string Cust_Package, string Cust_Location, string Cust_TempPinID, int PayMode, string Cust_BankName, string Cust_BankAcc, string Cust_BankIFSC, string Cust_BankBranch, string Cust_PanID, string _custusername, out string NewID, out string ErrCode)
	{

		int i = objClsDataccess.inserrtnewtempEntry(Cust_SponserID, Email, Cust_Address, Cust_Answer, Cust_Question, Cust_City, Cust_State, Cust_Country, Cust_Password, Cust_Title, Cust_Name, Cust_Gender, Cust_FatherName, Cust_DOB, Cust_Pincode, Cust_mobileNo, Cust_nominee, Cust_Relation, Cust_Package, Cust_Location, Cust_TempPinID, PayMode, Cust_BankName, Cust_BankAcc, Cust_BankIFSC, Cust_BankBranch, Cust_PanID, _custusername, out NewID, out ErrCode);
		return i;
	}
	public int InsertNewMember_instent_registration(string Cust_SponserID, string Email, string Cust_Address, string Cust_Answer, string Cust_Question, string Cust_City, string Cust_State, string Cust_Country, string Cust_Password, string Cust_Title, string Cust_Name, string Cust_Gender, string Cust_FatherName, DateTime Cust_DOB, string Cust_Pincode, string Cust_mobileNo, string Cust_nominee, string Cust_Relation, string Cust_Package, string Cust_Location, string Cust_TempPinID, int PayMode, string Cust_BankName, string Cust_BankAcc, string Cust_BankIFSC, string Cust_BankBranch, string Cust_PanID, string _custusername, out string NewID, out string ErrCode)
	{

		int i = objClsDataccess.InsertNewMember_instent_registration(Cust_SponserID, Email, Cust_Address, Cust_Answer, Cust_Question, Cust_City, Cust_State, Cust_Country, Cust_Password, Cust_Title, Cust_Name, Cust_Gender, Cust_FatherName, Cust_DOB, Cust_Pincode, Cust_mobileNo, Cust_nominee, Cust_Relation, Cust_Package, Cust_Location, Cust_TempPinID, PayMode, Cust_BankName, Cust_BankAcc, Cust_BankIFSC, Cust_BankBranch, Cust_PanID, _custusername, out NewID, out ErrCode);
		return i;
	}
	
	public int temverification_withouteverify(string cusid, out int flag, out string errMsg)
	{
		return objClsDataccess.temverification_withouteverify(cusid, out flag, out errMsg);
	}

	#region UpdateCustRecords_UserName

	public int UpdateCustRecords_UserName(string CusID, string Cust_UserName1, out int flag, out string errMsg)
	{
		return objClsDataccess.UpdateCustRecords_UserName(CusID, Cust_UserName1, out flag, out errMsg);
	}

	#endregion


	#region loginuser

	public int loginuser(string Cust_Username, string Cust_Password, out string NewID, out string ErrCode)
	{
		return objClsDataccess.loginuser(Cust_Username, Cust_Password, out NewID, out ErrCode);
	}

	#endregion



	#region otpverification

	public int otpverification(string CustID, string otp, string device_id, string remark, out string NewID, out string ErrCode)
	{
		return objClsDataccess.otpverification(CustID, otp, device_id, remark, out NewID, out ErrCode);
	}

	#endregion


	#region RegitrationInsertTemp

	public int RegitrationInsertTemp(string Email, string Cust_Password, string FullName, string Mobile_No, string Country, string Cust_Type, out string NewID, out string ErrCode)
	{
		return objClsDataccess.RegitrationInsertTemp(Email, Cust_Password, FullName, Mobile_No, Country, Cust_Type, out NewID, out ErrCode);
	}

	#endregion

	#region RegitrationInsert

	public int RegitrationInsert(string Referral_Code, string Cust_Username, string Email, string Cust_Name, string Password, string Confirm_Password, out string NewID, out string ErrCode)
	{                                 
		return objClsDataccess.RegitrationInsert(Referral_Code, Cust_Username, Email, Cust_Name, Password, Confirm_Password, out NewID, out ErrCode);
	}

	#endregion


	#region Activity_log_detail

	public int Activity_log_detail(string Cust_id, string Task, out string NewID, out string ErrCode)
	{
		return objClsDataccess.Activity_log_detail(Cust_id, Task, out NewID, out ErrCode);
	}

	#endregion

	#region otp_manage

	public int otp_manage(string CustID, string OTP, out string ErrCode1)
	{
		return objClsDataccess.otp_manage(CustID, OTP, out ErrCode1);
	}

	#endregion




}

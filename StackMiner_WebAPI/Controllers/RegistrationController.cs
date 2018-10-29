using StackMiner_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
using System.Text;
using StackMiner_WebAPI.Helper;
using StackMiner_WebAPI.Utility;
namespace StackMiner_WebAPI.Controllers
{

	public class RegistrationController : ApiController
	{
		ConnectionCls conCls = new ConnectionCls();
		UserRootModel user = new UserRootModel();

		SqlDataReader sdr;
		/// <summary>
		/// New Member registration.
		/// </summary>
		/// <remarks>
		/// ### REMARKS ###
		/// The following codes are returned
		/// - "ErrorCode : 0" - If registration Failed
		/// - "ErrorCode : 1" - If registration Successful
		/// - "ErrorMessage " - Return True or False result
		/// - "error: email_already_exist" - If user already exist with email
		/// - "error: numeric_name_not_allowed" - Name numeric not allowed
		/// - "error: name_cant_be_null_or_numeric" - Name cant be null or numeric 
		/// - "error: sponser_id_invalid " - If provided SponserID is Invalid
		/// - "error: please_fill_mandatory_filled" - Please fill the mandatory filed these are the name, email, password and Mobile No 
		/// </remarks>
		/// <param name="registrationmodel"></param>
		/// <returns> </returns>
		[HttpPost]
		[Route("api/Registration/NewRegistrationUsers")]
		public IHttpActionResult NewRegistrationUsers(RegistrationModel registrationmodel)
		{
			try
			{
				if (string.IsNullOrEmpty(registrationmodel.Name) || string.IsNullOrEmpty(registrationmodel.Email) || string.IsNullOrEmpty(registrationmodel.Password) || string.IsNullOrEmpty(registrationmodel.MobileNo))
				{
					user.ErrorCode = 0;
					user.ErrorMessage = "false";
					//return Ok("error: please_fill_mandatory_filled");
				}
				else
				{
					string sqlQuery = "";
					string valuexx, email_valuexx;
					if (string.IsNullOrEmpty(registrationmodel.SponserID = ""))
					{
						registrationmodel.SponserID = "admin";
					}


					sqlQuery = "SELECT cusid FROM Custrecords WHERE loginusername='" + registrationmodel.SponserID + "'";
					sdr = conCls.GetdataReader(sqlQuery);
					if (sdr.HasRows)
					{
						if (sdr.Read())
						{
							valuexx = sdr["cusid"].ToString();
						}
						else
						{
							valuexx = "HELLO";
						}
						sdr = conCls.GetdataReader("SELECT count (*) as countemail   FROM  Custrecords_prereg WHERE email= '" + registrationmodel.Email.Replace(" ", "") + "'");
						if (sdr.HasRows)
						{
							if (sdr.Read())
							{
								email_valuexx = sdr["countemail"].ToString(); // sdr["coun"].ToString();
							}
							else
							{
								email_valuexx = "1";
							}

						}
						else
						{
							email_valuexx = "1";
						}
						sdr.Close();

						if (registrationmodel.Name.Trim().Length > 4)
						{
							if (!string.IsNullOrEmpty(registrationmodel.Name))
							{
								decimal val;
								var isNumericName = decimal.TryParse(registrationmodel.Name.ToString(), out val);
								if (!isNumericName)
								{
									if (email_valuexx == "0")
									{
										string Newid = "0", Errocode = "";
										string _cust_sponserID, _name, _email, _cust_Password, _cust_address, _cust_answer, _cust_question, _cust_city, _cust_state, _cust_country, _Cust_Title, _Cust_Name, _Cust_Gender, _Cust_FatherName, _Cust_Pincode, _Cust_mobileNo, _Cust_nominee, _Cust_Relation, _Cust_Package, _Cust_Location, _Cust_TempPinID, _Cust_BankName, _Cust_BankAcc, _Cust_BankIFSC, _Cust_BankBranch, _Cust_PanID, _custusername;
										int _PayMode = 0;
										DateTime _Cust_DOB;
										string msgSuccess = "";
										_cust_sponserID = valuexx;
										_name = registrationmodel.Name;
										_email = registrationmodel.Email;
										_Cust_mobileNo = registrationmodel.MobileNo;
										_cust_Password = registrationmodel.Password;
										_cust_address = "";
										_cust_answer = "";
										_cust_question = "";
										_cust_city = "";
										_cust_state = "";
										_cust_country = "";
										_Cust_Title = "";
										_Cust_Name = registrationmodel.Name;
										_Cust_Gender = "";
										_Cust_FatherName = "";
										_Cust_DOB = registrationmodel.Cust_DOB;
										_Cust_Pincode = "";
										_Cust_nominee = "";
										_Cust_Relation = "";
										_Cust_Package = registrationmodel.Cust_Package;
										_Cust_Location = registrationmodel.Cust_Location;
										_Cust_TempPinID = registrationmodel.Cust_tempPinID;
										_PayMode = registrationmodel.PayMode;
										_Cust_BankName = "";
										_Cust_BankAcc = "";
										_Cust_BankIFSC = "";
										_Cust_BankBranch = "";
										_Cust_PanID = "";
										_custusername = registrationmodel.custusername;

										var resultString = conCls.inserrtnewcustrecords(_cust_sponserID, _email, _cust_address, _cust_answer, _cust_question, _cust_city, _cust_state, _cust_country, _cust_Password, _Cust_Title, _Cust_Name, _Cust_Gender, _Cust_FatherName, _Cust_DOB, _Cust_Pincode, _Cust_mobileNo, _Cust_nominee, _Cust_Relation, _Cust_Package, _Cust_Location, _Cust_TempPinID, _PayMode, _Cust_BankName, _Cust_BankAcc, _Cust_BankIFSC, _Cust_BankBranch, _Cust_PanID, _custusername, out Newid, out Errocode);
										if (Newid.Length > 4)
										{
											////****************OTP Gnereation Code*********************
											////var Sendsms = new sendsms();
											//string numbers = "1234567890";
											//string characters = numbers;
											//// OTP LENGTH
											//int length = 6;

											////INITIAL OTP
											//string otp = string.Empty;

											////GENERATING OTP
											//for (int i = 0; i < length; i++)
											//{
											//	string character = string.Empty;
											//	do
											//	{
											//		int index = new Random().Next(0, characters.Length);
											//		character = characters.ToCharArray()[index].ToString();
											//	} while (otp.IndexOf(character) != -1);
											//	otp += character;
											//}
											
											SendSMS(Newid, _Cust_Name, _cust_sponserID, _Cust_mobileNo, _cust_Password, _email, _custusername, _cust_Password, _Cust_Package);
											string otpstatus = conCls.ExecuteSqlnonQuery("Update [OTP_data_Code] Set Status = 1 Where CustID = '"+ Newid  + "' AND Type = 'Registration' AND Status = 0").ToString();
											string result = Convert.ToString(resultString);
											if (!string.IsNullOrEmpty(result))
											{
												if (result == "1")
												{
													//return new RegistrationModel(_Cust_Name, Request);
													//return Ok("true");
													user.ErrorCode = 1;
													user.ErrorMessage = "true";
													return Ok(registrationmodel);

												}
												else
												{
													user.ErrorCode = 0;
													user.ErrorMessage = "false";
													user.registeruser = null;
												}
											}
											else
											{
												user.ErrorCode = 0;
												user.ErrorMessage = "false";
												user.registeruser = null;
												//result = "false";

											}
										}
									}
									else
									{
										user.ErrorCode = 0;
										user.ErrorMessage = "false";
										user.registeruser = null;
										//return Ok("error: email_already_exist");
									}
								}
								else
								{
									user.ErrorCode = 0;
									user.ErrorMessage = "false";
									user.registeruser = null;
									//return Ok("error: numeric_name_not_allowed");
								}
							}
							else
							{
								user.ErrorCode = 0;
								user.ErrorMessage = "false";
								user.registeruser = null;
								//return Ok("error: name_cant_be_null_or_numeric");
							}
						}
						else
						{
							user.ErrorCode = 0;
							user.ErrorMessage = "false";
							//user.registeruser = null;
							//return Ok("error: name_cant_be_less_than_five_characters");
						}
					}
					else
					{
						user.ErrorCode = 0;
						user.ErrorMessage = "false";
						user.registeruser = null;
						//return Ok("error: sponser_id_invalid ");
					}

				}
				return Ok(user);

			}
			catch (Exception ex)
			{
				return Ok(ex.Message.ToString());
			}

		}


		private void SendSMS(string id, string name, string sponserID, string mob, string pass, string email, string username, string Cust_Password, string Cust_Package)
		{
			try
			{
				int flag = 0;
				string errmsg = "";
				string otp = "";
				string Type = "Registration";
				string Cust_Package_amt;
				//Cust_Package_amt = conCls.getScalerValue("select Product_Price from Product_chart where Product_Code ='" + Cust_Package + "'");
				Cust_Package_amt = "0";
				WebClient client = new WebClient();
				DataSet ds = new DataSet();
				ds = conCls.getdataSet("select Baseurl1,Baseurl2 from SMS_Services where Status=1");
				DataTable dt = ds.Tables[0];
				if (dt != null)
					if (dt.Rows.Count > 0)
					{
						string baseurl = dt.Rows[0][0].ToString() + mob + dt.Rows[0][1].ToString() + "Congratulations! Dear " + name + ",Thanks For Joining With Us Your userName (" + username + ") and Password: " + pass + " - trans pws :" + pass + " with amount " + Cust_Package_amt + " for detail pls visit aps-mining.com";

						Stream data = client.OpenRead(baseurl);
						StreamReader reader = new StreamReader(data);
						string s = reader.ReadToEnd();
						data.Close();
						reader.Close();
					}
				ds.Dispose();

				conCls.Insert_OTP_Code(id, Type, out flag, out errmsg, out otp);

				if (flag == 1)
				{
					string body = this.PopulateBody1(id, name, sponserID, mob, pass, email, username, Cust_Password, otp);
					DateTime letterdt = ConnectionCls.getIndianDateTime();
					string dat = letterdt.ToShortDateString();
					// Gmail Address from where you send the mail
					//var fromAddress = ConfigurationManager.AppSettings["smtpsenderEmail"].ToString();
					// any address where the email will be sending
					var toAddress = email;
					//Password of your gmail address
					//string fromPassword = ConfigurationManager.AppSettings["smtpPassword"].ToString();
					// Passing the values and make a email formate to display
					//string subject = "aps-mining.com Welcome to website...!";
					string subject = "Verification Mail from stack miner";
					// body = bodyy; 
					MailMessage mailMsg = new MailMessage();
					// To
					mailMsg.To.Add(new MailAddress(email));
					// From
					mailMsg.From = new MailAddress("noreply@aps-mining.com", "Email Verification");
					//mailMsg.From = new MailAddress("alert@aps-mining.com", " Registration  Detail");
					//mailMsg.From = new MailAddress(ConfigurationManager.AppSettings["smtpsenderEmail"].ToString(), "Email Verification - aps-mining Pool ");
					// Subject and multipart/alternative Body
					mailMsg.Subject = subject;
					string text = "text body";
					string html = body;
					mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
					mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
					// Init SmtpClient and send
					SmtpClient smtpClient1 = new SmtpClient("smtp.mailgun.org", Convert.ToInt32(587));
					smtpClient1.EnableSsl = true;
					System.Net.NetworkCredential credentials1 = new System.Net.NetworkCredential("noreply@aps-mining.com", "b5fe91c3d559323d663987a96d22fbcf");
					smtpClient1.Credentials = credentials1;
					smtpClient1.Send(mailMsg);
				}
				else
				{

				}
			}
			catch (Exception)
			{

				throw;
			}
		}

		private string PopulateBody1(string id, string name, string sponserID, string mob, string pass, string email, string username, string Cust_Password, string otp)
		{
			string body = string.Empty;

			//using (StreamReader reader = new StreamReader("~/Email/email_verify.htm")
			//{
			//	body = reader.ReadToEnd();
			//}
			string Path = System.Web.HttpContext.Current.Request.MapPath("~/Email/email_verify.htm");
			body = File.ReadAllText(Path);

			string _dateti = DateTime.Now.ToString();
			body = body.Replace("{date}", _dateti);
			body = body.Replace("{name}", name);
			body = body.Replace("{email}", email);
			body = body.Replace("{Sponsorid}", sponserID);
			body = body.Replace("{idno}", id);
			body = body.Replace("{password}", Cust_Password);
			body = body.Replace("{username}", username);
			body = body.Replace("{otp}", otp);
			// Creating the query string to pass
			//string technology = HttpUtility.UrlEncode(Encrypt(ddlTechnology.SelectedItem.Value));
			//Response.Redirect(string.Format("~/CS2.aspx?name={0}&technology={1}", name, technology));  
			var encyptionHelper = new encryption();
			string name111 = encyptionHelper.Encrypt(id);
			//string name111 = HttpUtility.UrlEncode(Encrypt(id));
			body = body.Replace("{encoderegno}", name111);
			//lbl_test.Text = name111.ToString();
			return body;
		}
	}
}

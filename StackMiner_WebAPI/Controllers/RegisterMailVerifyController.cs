using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using StackMiner_WebAPI.Helper;
using StackMiner_WebAPI.Models;

namespace StackMiner_WebAPI.Controllers
{

	public class RegisterMailVerifyController : ApiController
	{
		ConnectionCls conCls = new ConnectionCls();
		UserRootModel user = new UserRootModel();
		RegistrationModel registrationmodel =new RegistrationModel();

		/// <summary>
		///  Mail Verification of Member Registration
		/// </summary>
		/// <remarks>
		/// 	/// ### REMARKS ###
		/// The following codes are returned
		/// - "ErrorCode : 0" - If registration Failed
		/// - "ErrorCode : 1" - If registration Successful
		/// - "ErrorMessage " - Return True or False result
		/// - "error: invalid_otp_code" - If Entered invalid OTP code
		/// - "error: something_went_wrong - If mailer link not found
		/// - "error: user_verification_id_not_found" - If user verfication code not found
		/// - "error:email_already_exist_or_sponserid_invalid" - If Email already exist
		/// - "error: please_contact_admin_or_create_support_ticket_for_activate_account" - If Failed Registration
		/// </remarks>
		/// <param name="otpcode"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("api/RegisterMailVerify/RegisterMailConfirmation{otpcode}")]
		public IHttpActionResult RegisterMailConfirmation(string otpcode)
		{
			try
			{
				decryption Decryption = new decryption();
				if (!string.IsNullOrEmpty(otpcode))
				{
					checksecuritypin checksecuritypin = new checksecuritypin();

					if (!checksecuritypin.CheckSecurityPin(otpcode))
					{
						user.ErrorCode = 0;
						user.ErrorMessage = "false";
						//return Ok("error: invalid_otp_code");
					}
					else if(checksecuritypin.CheckSecurityPin(otpcode) == false)
					{
						user.ErrorCode = 0;
						user.ErrorMessage = "false";
						//return Ok("error: invalid_otp_code");
					}
					else
					{
						string custid = "";
						SqlDataReader sdr = conCls.GetdataReader("Select CustID from OTP_data_Code Where  Status=0 AND Type='Registration' AND OTP_Code=" + otpcode);
						if (sdr.HasRows)
						{
							if (sdr.Read())
							{
								custid = sdr["CustID"].ToString();
							}
						}
						sdr.Close();
						sdr.Dispose();

						int flag = 0; string errMsg = "0";
						conCls.verification_byregistration(custid, out flag, out errMsg);
						string NewID = "";
						NewID = flag.ToString();

						if (NewID.Length > 4)
						{
							string regsucc = "";
							regsucc = errMsg.ToString().Trim();
							if (regsucc.ToString() == "SUCCESS")
							{
								string _Cust_Name = "";
								string _email = "";
								string _username = "";
								string sqlStr1 = "";
								

								sqlStr1 = "SELECT Cust_UserName, Email, cusid, cust_name from CustRecords_preReg where cusid = " + custid;
								SqlDataReader sdr1 = conCls.GetdataReader(sqlStr1);
								if (sdr1.HasRows)
								{
									if (sdr1.Read())
									{
										_username = sdr1["Cust_UserName"].ToString();
										_email = sdr1["Email"].ToString();
										_Cust_Name = sdr1["cust_name"].ToString();
									}
									else
									{
										user.ErrorCode = 0;
										user.ErrorMessage = "false";
										//return Ok("error: record_not_available");
									}
								}
								else
								{
									user.ErrorCode = 0;
									user.ErrorMessage = "false";
									//return Ok("error: record_not_available");
								}

								sdr1.Close();
								sdr1.Dispose();

								SendSMS(NewID, _Cust_Name, _email, _username);
								user.ErrorCode = 1;
								user.ErrorMessage = "true";
								return Ok(registrationmodel);
								//return Ok("true");

								
							}
							else
							{
								user.ErrorCode = 0;
								user.ErrorMessage = "false";
								//return Ok("error: please_contact_admin_or_create_support_ticket_for_activate_account");
							}

						}
						else
						{
							user.ErrorCode = 0;
							user.ErrorMessage = "false";
							//return Ok("error:email_already_exist_or_sponserid_invalid");
						}

					}

				}
				else
				{
					user.ErrorCode = 0;
					user.ErrorMessage = "false";
					//return Ok("error: otp_code_not_found");
				}

				
			}
			catch (Exception ex)
			{
				return Ok(ex.Message.ToString());
			}
		}
		
		private void SendSMS(string id, string name, string email, string username)
		{
			try
			{
				string body = this.PopulateBody(id, name, email, username);
				DateTime letterdt = ConnectionCls.getIndianDateTime();
				string dat = letterdt.ToShortDateString();
				// Gmail Address from where you send the mail
				var fromAddress = "noreply@aps-mining.com";
				// any address where the email will be sending
				var toAddress = email;
				//Password of your gmail address
				const string fromPassword = "b5fe91c3d559323d663987a96d22fbcf";
				// Passing the values and make a email formate to display
				//string subject = "aps-mining.com Welcome to website...!";
				string subject = "Registration aps-mining.com";
				// body = bodyy; 
				MailMessage mailMsg = new MailMessage();
				// To
				mailMsg.To.Add(new MailAddress(email));
				// From
				//mailMsg.From = new MailAddress("alert@aps-mining.com", " Registration  Detail");
				mailMsg.From = new MailAddress("noreply@aps-mining.com", " Registration");
				// Subject and multipart/alternative Body
				mailMsg.Subject = subject;
				string text = "text body";
				string html = body;
				mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
				mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
				// Init SmtpClient and send
				SmtpClient smtpClient = new SmtpClient("smtp.mailgun.org", Convert.ToInt32(587));
				smtpClient.EnableSsl = true;
				System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("noreply@aps-mining.com", "b5fe91c3d559323d663987a96d22fbcf");
				smtpClient.Credentials = credentials;
				smtpClient.Send(mailMsg);
				// sending mail to sponsor

				string sp_cusid = "";
				string sp_cus_name = "";
				string sp_cus_email = "";
				string sp_cus_username = "";
				string sp_cus_sponsorid = "";
				

				SqlDataReader sdr12 = conCls.GetdataReader("select Cust_SponserID   from custrecords where  cusid  =" + id.ToString());

				if (sdr12.HasRows)
				{
					if (sdr12.Read())
					{
						sp_cus_sponsorid = sdr12["Cust_SponserID"].ToString();
					}
				}
				sdr12.Close();
				sdr12.Dispose();


				SqlDataReader sdr11 = conCls.GetdataReader("select cusid, cust_name, cust_username, email from custrecords where  cusid  =" + sp_cus_sponsorid.ToString());

				if (sdr11.HasRows)
				{
					if (sdr11.Read())
					{
						//sss = Int32.Parse(sdr11[0].ToString()); 
						try
						{
							sp_cusid = sdr11["cusid"].ToString();
							sp_cus_name = sdr11["cust_name"].ToString();
							sp_cus_username = sdr11["cust_username"].ToString();
							sp_cus_email = sdr11["email"].ToString();
						}
						catch
						{


						}

					}
				}

				sdr11.Close();
				sdr11.Dispose();


				//id, name, email, username

				string sp_name = "";
				//string body_sponsor = this.PopulateBody_sponsor(sp_cusid, sp_cus_name, sp_cus_email, sp_cus_username, sp_name);
				string body_sponsor = this.PopulateBody_sponsor(sp_cusid, name, email, username, sp_name);
				DateTime letterdt_sponsor = ConnectionCls.getIndianDateTime();
				string date_sponsor = letterdt.ToShortDateString();
				// Gmail Address from where you send the mail
				var fromAddress_sponsor = "noreply@aps-mining.com";
				// any address where the email will be sending
				var toAddress_sponsor = sp_cus_email;
				//Password of your gmail address
				const string fromPassword_sponsor = "b5fe91c3d559323d663987a96d22fbcf";
				// Passing the values and make a email formate to display
				//string subject = "aps-mining.com Welcome to website...!";


				string subject_sponsor = "Great Job..." + sp_cus_name + ", " + name + " just signed up as your direct referral in your team";
				// body = bodyy; 
				MailMessage mailMsg_sponsor = new MailMessage();
				// To
				mailMsg_sponsor.To.Add(new MailAddress(toAddress_sponsor));
				// From
				//mailMsg.From = new MailAddress("alert@aps-mining.com", " Registration  Detail");
				mailMsg_sponsor.From = new MailAddress("noreply@aps-mining.com", subject_sponsor.ToString());
				// Subject and multipart/alternative Body
				mailMsg_sponsor.Subject = subject_sponsor;
				string text_sponsor = "text body";
				string html_sponsor = body_sponsor;
				mailMsg_sponsor.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text_sponsor, null, MediaTypeNames.Text.Plain));
				mailMsg_sponsor.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html_sponsor, null, MediaTypeNames.Text.Html));
				// Init SmtpClient and send
				SmtpClient smtpClient_sponsor = new SmtpClient("smtp.mailgun.org", Convert.ToInt32(587));
				smtpClient_sponsor.EnableSsl = true;
				System.Net.NetworkCredential credentials_sponsor = new System.Net.NetworkCredential("noreply@aps-mining.com", "b5fe91c3d559323d663987a96d22fbcf");
				smtpClient_sponsor.Credentials = credentials_sponsor;
				smtpClient_sponsor.Send(mailMsg_sponsor);
				//SMTP Server: smtp-pulse.com 
				//Port: 2525 (SSL port: 465) 
				//Login: postmaster@ethpool.io 
				//Password: 06526edd28b7ec716cc2bef941cb0587 Change password 
				//Your IP: 78.41.200.13 
				//Sender Email:  postmaster@ethpool.io  
			}
			catch (Exception ex)
			{

			}
		}

		private string PopulateBody(string id, string name, string email, string username)
		{
			// Name:{Name}Email-Id:{email}Sponsor Id:{Sponsorid}ID No: {idno}Password:{pass}
			string body = string.Empty;
			//using (StreamReader reader = new StreamReader(Server.MapPath("~/backoffice/welcomemail.htm")))
			//{
			//	body = reader.ReadToEnd();
			//}

			string Path = System.Web.HttpContext.Current.Request.MapPath("~/Email/welcomemail.htm");
			body = File.ReadAllText(Path);
			string _dateti = DateTime.Now.ToString();
			body = body.Replace("{date}", _dateti);
			body = body.Replace("{name}", name);
			body = body.Replace("{email}", email);
			// body = body.Replace("{Sponsorid}", sponserID);
			body = body.Replace("{idno}", id);
			// body = body.Replace("{password}", Cust_Password);
			body = body.Replace("{username}", username);
			return body;
		}
		
		private string PopulateBody_sponsor(string id, string name, string email, string username, string sp_name)
		{

			// Name:{Name}Email-Id:{email}Sponsor Id:{Sponsorid}ID No: {idno}Password:{pass}

			string body = string.Empty;
			//using (StreamReader reader = new StreamReader(Server.MapPath("~/backoffice/welcomemail_sponsor.htm")))
			//{
			//	body = reader.ReadToEnd();
			//}

			string Path = System.Web.HttpContext.Current.Request.MapPath("~/Email/welcomemail_sponsor.htm");
			body = File.ReadAllText(Path);

			string _dateti = DateTime.Now.ToString();
			body = body.Replace("{name_sp_name}", sp_name);
			body = body.Replace("{date}", _dateti);
			body = body.Replace("{name}", name);
			body = body.Replace("{email}", email);
			// body = body.Replace("{Sponsorid}", sponserID);
			body = body.Replace("{idno}", id);
			// body = body.Replace("{password}", Cust_Password);
			body = body.Replace("{username}", username);
			return body;
		}

	

	}
}

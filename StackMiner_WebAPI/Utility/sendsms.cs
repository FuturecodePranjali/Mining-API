using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using StackMiner_WebAPI.Models;
using StackMiner_WebAPI.Utility;
namespace StackMiner_WebAPI.Utility
{
	public class sendsms
	{
		ConnectionCls conCls = new ConnectionCls();
		public void SendSMS(string id, string name, string sponserID, string mob, string pass, string email, string username, string Cust_Password, string Cust_Package)
		{
			try
			{
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
				var mailbody = new MailVerifyPopulatedBody();
				string body = mailbody.PopulateBody(id, name, sponserID, mob, pass, email, username, Cust_Password);
				DateTime letterdt = ConnectionCls.getIndianDateTime();
				string dat = letterdt.ToShortDateString();
				var toAddress = email;
				string subject = "Verification Mail from aps-mining ";
				MailMessage mailMsg = new MailMessage();
				// To
				mailMsg.To.Add(new MailAddress(email));
				// From
				mailMsg.From = new MailAddress("noreply@aps-mining.com", "Email Verification");
				// Subject and multipart/alternative Body
				mailMsg.Subject = subject;
				string text = "text body";
				string html = body;
				mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
				mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
				SmtpClient smtpClient1 = new SmtpClient("smtp.mailgun.org", Convert.ToInt32(587));
				smtpClient1.EnableSsl = true;
				System.Net.NetworkCredential credentials1 = new System.Net.NetworkCredential("noreply@aps-mining.com", "b5fe91c3d559323d663987a96d22fbcf");
				smtpClient1.Credentials = credentials1;
				smtpClient1.Send(mailMsg);
			}
			catch (Exception)
			{

				throw;
			}
		}

	}
}
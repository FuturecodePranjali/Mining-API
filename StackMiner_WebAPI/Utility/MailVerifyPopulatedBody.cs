using StackMiner_WebAPI.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StackMiner_WebAPI.Utility
{
	public class MailVerifyPopulatedBody
	{
		public string PopulateBody(string id, string name, string sponserID, string mob, string pass, string email, string username, string Cust_Password)
		{
			string body = string.Empty;
			string Path = System.Web.HttpContext.Current.Request.MapPath(@"\\Email\\email_verify.htm");
		    body = File.ReadAllText(Path);
			//using (StreamReader reader = new StreamReader(("~/backoffice/email_verify.htm")))
			//{
			//	body = reader.ReadToEnd();
			//}
			string _dateti = DateTime.Now.ToString();
			body = body.Replace("{date}", _dateti);
			body = body.Replace("{name}", name);
			body = body.Replace("{email}", email);
			body = body.Replace("{Sponsorid}", sponserID);
			body = body.Replace("{idno}", id);
			body = body.Replace("{password}", Cust_Password);
			body = body.Replace("{username}", username);
			// Creating the query string to pass
			var encyptionHelper = new encryption();
			string name111 = encyptionHelper.Encrypt(id);
			//string name111 = HttpUtility.UrlEncode(Encrypt(id));
			body = body.Replace("{encoderegno}", name111);
			return body;
		}

	}
}
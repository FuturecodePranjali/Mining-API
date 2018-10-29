using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StackMiner_WebAPI.Helper
{
	public class checksecuritypin
	{
		ConnectionCls conCls = new ConnectionCls();
		public bool CheckSecurityPin(string SecurityPin)
		{
			string Type = "Registration";
			bool flag = false;
			string OTP_Code = "";

			if (SecurityPin == "")
			{
				SecurityPin = "0";
			}
			SqlDataReader sdr = conCls.GetdataReader("Select OTP_Code from OTP_data_Code Where  Status=0 AND Type='" + Type + "' AND OTP_Code=" + SecurityPin);
			if (sdr.HasRows)
			{
				if (sdr.Read())
				{
					OTP_Code = sdr["OTP_Code"].ToString();
				}
			}
			sdr.Close();
			sdr.Dispose();

			if (OTP_Code == "")
			{
				flag = false;
			}
			else
			{
				string sessvalue = OTP_Code;
				if (sessvalue == SecurityPin)
				{
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

	}
}
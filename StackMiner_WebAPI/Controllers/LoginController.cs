using StackMiner_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;


namespace StackMiner_WebAPI.Controllers
{
	public class LoginController : ApiController
	{
		ConnectionCls Concls = new ConnectionCls();
		SqlDataReader sdr;

		[HttpGet]
		[Route("api/Login/GetSampleUsers")]
		public IHttpActionResult GetSampleUsers()
		{
			LoginModel loginmodel = new LoginModel();
			loginmodel.Email = "Pranjali";
			loginmodel.Password = "123123";

			return Ok(loginmodel);
		}


		/// <summary>
		/// Get authentication login users.
		/// </summary>
		/// /// <remarks>
		/// ### REMARKS ###
		///  The following codes are returned
		/// - "true" - If Login Success 
		/// - "false" - If Login Falied
		///  </remarks>
		/// <param name="loginModel">Check Login Users</param>
		/// <returns>Get authentication login users</returns>
		[HttpPost]
		[Route("api/Login/GetLoginUsers")]
		public IHttpActionResult GetLoginUsers(LoginModel loginModel)
		{
			string sqlQuery = "";
			string resultString = "";
			try
			{
				if (loginModel.Email != null || loginModel.Password != "")
				{
					sqlQuery ="select CusID from Custrecords where Email='" + loginModel.Email + "' and Cust_Password='" + loginModel.Password + "' " ;
					sdr = Concls.GetdataReader(sqlQuery);
					if (sdr.HasRows)
					{
						if (sdr.Read())
						{
							resultString = sdr["CusID"].ToString();
						}
						else
						{
							resultString = "";
						}
					}
					else
					{
						resultString = "";
					}
					sdr.Close();

					if (string.IsNullOrEmpty(resultString))
					{
						resultString = "false";
					}
					else
					{
						resultString = "true";
					}

					return Ok(resultString);
				}
				else
				{
					return Ok("Something went Wrong...");
				}

			}
			catch (Exception ex)
			{
				throw (ex);
			}
		}
	}
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackMiner_WebAPI.Models
{
	public class RegistrationModel
	{
		public string SponserID { get; set; }

		[JsonProperty ("Name")]
		[Required(ErrorMessage = "Please enter Name")]
		public string Name { get; set; }

		[JsonProperty("Email")]
		[Required(ErrorMessage = "Please enter Email")]
		public string Email { get; set; }

		[JsonProperty("MobileNo")]
		[Required(ErrorMessage = "Please enter MobileNo")]
		[RegularExpression("[^0-9]", ErrorMessage = "Mobile No must be Numeric")]
		public string MobileNo { get; set; }

		[JsonProperty("Password")]
		[Required(ErrorMessage = "Please enter Password")]
		public string Password { get; set; }

		public string Cust_Address { get; set; }
		public string Cust_Question { get; set; }
		public string Cust_Answer { get; set; }
		public string Cust_City { get; set; }
		public string Cust_State { get; set; }
		public string Cust_Country { get; set; }
		public string Cust_Title { get; set; }
		public string Cust_Name { get; set; }
		public string Cust_Gender { get; set; }
		public string Cust_FatherName { get; set; }
		public DateTime Cust_DOB { get; set; }
		public string Cust_PinCode { get; set; }
		public string Cust_Nominee { get; set; }
		public string Cust_Relation { get; set; }
		public string Cust_Package { get; set; }
		public string Cust_Location { get; set; }
		public string Cust_tempPinID { get; set; }
		public int PayMode { get; set; }
		public string Cust_BankName { get; set; }
		public string Cust_BankAcc { get; set; }
		public string Cust_BankIFSC { get; set; }
		public string Cust_BankBranch { get; set; }
		public string Cust_PanID { get; set; }
		public string custusername { get; set; }

		
	}

	public class UserRootModel
	{
		public int ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
		public RegistrationModel registeruser { get; set; }
	}

}
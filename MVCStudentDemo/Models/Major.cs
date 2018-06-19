using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCStudentDemo.Models {
	public class Major {

		public int Id { get; set; }
		public string Description { get; set; }
		public int MinSAT { get; set; }

		public Major() {

		}
	}
}
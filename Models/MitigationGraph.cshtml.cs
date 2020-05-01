using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace program.Pages
{
    public class MitigationGraphModel : PageModel
    {
        public List<Models.StateCensus> StateList {get; set;}
        public string Input {get; set;}
        public string st {get; set;}
        public List<string> x_axis {get; set;}
        public List<string> news {get; set;}
        public List<int> y_axis {get; set;}
        public List<Tuple<string, string>> annotations {get; set;}
        
        public Exception EX {get; set;}
    
        public void OnGet(string input)
        {
            StateList = new List<Models.StateCensus>();
            x_axis = new List<string>();
            news = new List<string>();
            y_axis = new List<int>();
            annotations = new List<Tuple<string, string>>();
            
            Input = input;
            EX = null;
            
            try{
              if(input == null) {
                
              } else {
                string sql;
                input = input.Replace("'", "''");

                sql = string.Format(@"
                select date as DATE, Sum(total) as _total
                from us_states_covid19_daily where state like '{0}'
                group by date
                order by date;", input); 
                
                if(input == "AZ")
                  st = "US: Arizona";
                else if(input == "FL")
                  st = "US: Florida";
                else if(input == "IL")
                  st = "US: Illinois";
                else if(input == "IN")
                  st = "US: Indiana";
                else if(input == "NV")
                  st = "US: Nevada";
                else if(input == "NJ")
                  st = "US: New Jersey";
                else if(input == "OR")
                  st = "US: Oregon";
                else if(input == "SC")
                  st = "US: S Carolina";
                else if(input == "VA")
                  st = "US: Virginia";
                else if(input == "WI")
                  st = "US: Wisconsin";
                else if(input == "AL")
                  st = "US:Alabama";
                else if(input == "AK")
                  st = "US:Alaska";
                else if(input == "CA")
                  st = "US:California";
                else if(input == "DE")
                  st = "US:Delaware";
                else if(input == "GA")
                  st = "US:Georgia";
                else if(input == "ID")
                  st = "US:Idaho";
                else if(input == "MD")
                  st = "US:Maryland";
                else if(input == "NC")
                  st = "US:N Carolina";
                else if(input == "NJ")
                  st = "US:New Jersey";
                else if(input == "UT")
                  st = "US:Utah";
                else if(input == "WA")
                  st = "US:Washington";
                else 
                  st = "No Data Found For This State";
                 
                /*
                if(input == "AK")
                  st = "US:Alaska";
                else if(input == "AL")
                  st = "US:Alabama";
                else if(input == "AR")
                  st = "US:Arkansas";
                else if(input == "AS")
                  st = ("????");
                else if(input == "AZ")
                  st = "US:Arizona";
                else if(input == "CA")
                  st = "US:California";
                else if(input == "CO")
                  st = "US:Colorado";
                else if(input == "CT")
                  st = "US:Connecticut";
                else if(input == "DC")
                  st = "US:????";
                else if(input == "DE")
                  st = "US:Delaware";
                else if(input == "FL")
                  st = "US:Florida";
                else if(input == "GA")
                  st = "US:Georgia";
                else if(input == "GU")
                  st = "US:???";
                else if(input == "HI")
                  st = "US:Hawaii";
                else if(input == "IA")
                  st = "US:Iowa";
                else if(input == "ID")
                  st = "US:Idaho";
                else if(input == "IL")
                  st = "US: Illinois";
                else if(input == "IN")
                  st = "US:Indiana";
                else if(input == "KS")
                  st = "US:Kansas";
                else if(input == "KY")
                  st = "US:Kentucky";
                else if(input == "LA")
                  st = "US:Los Angeles";
                */
                  
                string sql2;
                sql2 = string.Format(@"
                select StartDate as DATE, DescriptionOfMeasure as dom
                from covid_mitigation_usa where Country like '{0}'
                order by StartDate", st);
                
                DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);
                DataSet ds2 = DataAccessTier.DB.ExecuteNonScalarQuery(sql2);
                
                string label, label1, str;
                int val;
                int weekday = 0;
                int week = 0;
                
                foreach(DataRow row in ds.Tables[0].Rows) {
                  if(row["DATE"] != DataAccessTier.DB.nullvalue) {
                    label = Convert.ToString(row["DATE"]);
                    x_axis.Add(label);
                    val = Convert.ToInt32(row["_total"]);
                    y_axis.Add(val);
                  }
                }
                
                string dt;
                foreach(DataRow row in ds2.Tables[0].Rows) {
                  if(row["DATE"] != DataAccessTier.DB.nullvalue) {
                    label1 = Convert.ToString(row["DATE"]);
                    dt = DateTime.ParseExact(label1, "MMM dd, yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                    str = Convert.ToString(row["dom"]);
                    news.Add(str);
                    //++weekday;
                    //if(weekday >= 7) {
                      //++week;
                      annotations.Add(new Tuple<string, string>(dt, "News:" + str));
                      //weekday = 0;
                    //}
                  }
                }
              }
            } 
            catch(Exception ex) {
              EX = ex;
            }
            finally {
              
            }            
        }
    }
}

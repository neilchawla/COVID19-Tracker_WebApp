using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace program.Pages
{
    public class HospitalizationGraphModel : PageModel
    {
        public List<Models.StateCensus> StateList {get; set;}
        public string Input {get; set;}
        public List<int> hospitalizations {get; set;}
        public List<int> deaths {get; set;}
        public List<int> y_axis {get; set;}
        public List<string> dates {get; set;}
        
        public Exception EX {get; set;}
    
        public void OnGet(string input)
        {
            StateList = new List<Models.StateCensus>();
            hospitalizations = new List<int>();
            deaths = new List<int>();
            dates = new List<string>();
            y_axis = new List<int>();
            
            Input = input;
            EX = null;
            
            try{
              if(input == null) {
                
              } else {
                string sql;
                input = input.Replace("'", "''");

                sql = string.Format(@"
                select date as DATE, Sum(hospitalized) as hospt, Sum(death) as dth
                from us_states_covid19_daily where state like '{0}'
                group by date
                order by date;", input); 
                
                DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);
                
                string label;
                int hosp, _deaths;
                
                foreach(DataRow row in ds.Tables[0].Rows) {
                  if(row["DATE"] != DataAccessTier.DB.nullvalue) {
                    label = Convert.ToString(row["DATE"]);
                    dates.Add(label);
                    hosp = Convert.ToInt32(row["hospt"]);
                    hospitalizations.Add(hosp);
                    _deaths = Convert.ToInt32(row["dth"]);
                    deaths.Add(_deaths);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace program.Pages
{
    public class CensusGraphModel : PageModel
    {
        public List<Models.StateCensus> StateList {get; set;}
        public string Input {get; set;}
        public List<string> x_axis {get; set;}
        public List<int> y_axis {get; set;}
        
        public Exception EX {get; set;}
    
        public void OnGet(string input)
        {
            StateList = new List<Models.StateCensus>();
            x_axis = new List<string>();
            y_axis = new List<int>();
            
            Input = input;
            EX = null;
            
            try{
              if(input == null) {
                
              } else {
                string sql;
                input = input.Replace("'", "''");

                sql = string.Format(@"
                select year, population
                from census where state like '{0}'
                order by year;", input); 
                
                DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);
                
                string label;
                int val;
                
                foreach(DataRow row in ds.Tables[0].Rows) {
                  if(row["Year"] != DataAccessTier.DB.nullvalue) {
                    label = Convert.ToString(row["Year"]);
                    x_axis.Add(label);
                    val = Convert.ToInt32(row["Population"]);
                    y_axis.Add(val);
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

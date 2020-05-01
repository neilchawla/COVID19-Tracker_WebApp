using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
  
namespace program.Pages  
{  
    public class NumbersByDateModel : PageModel  
    {  
        public List<string> dates { get; set; }
        public List<int> positive { get; set; }
        public List<int> negative { get; set; }
        public List<int> deaths { get; set; }
        
        public Exception EX { get; set; }
  
        public void OnGet()  
        {
          dates = new List<string>();
          positive = new List<int>();
          negative = new List<int>();
          deaths = new List<int>();
          
          EX = null;
          
          try
          {
            string sql = string.Format(@"
SELECT date AS DATE, Sum(positive) AS positive, Sum(negative) as negative, Sum(death) as deaths
FROM us_states_covid19_daily
GROUP BY date
ORDER BY date;
");
          
            DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
              string date = Convert.ToString(row["DATE"]);
              dates.Add(date);
              int posi = Convert.ToInt32(row["positive"]);
              positive.Add(posi);
              int neg = Convert.ToInt32(row["negative"]);
              negative.Add(neg);
              int death = Convert.ToInt32(row["deaths"]);
              deaths.Add(death);
            }
		      }
		      catch(Exception ex)
		      {
            EX = ex;
		      }
		      finally
		      { 
            // nothing at the moment
          } 
        }  
        
    }//class
}//namespace
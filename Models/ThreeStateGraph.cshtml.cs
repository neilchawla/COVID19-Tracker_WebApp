using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace program.Pages
{
    public class ThreeStateGraphModel : PageModel
    {
        public List<Models.StateCensus> StateList {get; set;}
        public string Input {get; set;}
        public string Input2 {get; set;}
        public string Input3 {get; set;}
        public List<int> s1_pos {get; set;}
        public List<int> s1_neg {get; set;}
        public List<int> s2_pos {get; set;}
        public List<int> s2_neg {get; set;}
        public List<int> s3_pos {get; set;}
        public List<int> s3_neg {get; set;}
        public List<string> dates {get; set;}
        public List<string> d {get; set;}
        
        public Exception EX {get; set;}
    
        public void OnGet(string input, string input2, string input3)
        {
            StateList = new List<Models.StateCensus>();
            s1_pos = new List<int>();
            s1_neg = new List<int>();
            s2_pos = new List<int>();
            s2_neg = new List<int>();
            s3_pos = new List<int>();
            s3_neg = new List<int>();
            dates = new List<string>();
            d = new List<string>();
            
            
            Input = input;
            Input2 = input2;
            Input3 = input3;
            
            EX = null;
            
            try{
              if(input == null) {
                
              } else {
                string sql, sql2, sql3;
                input = input.Replace("'", "''");
                input2 = input2.Replace("'", "''");
                input3 = input3.Replace("'", "''");

                /*
                sql = string.Format(@"
                select date as DATE, positiveIncrease as positive, negativeIncrease as negative
                from us_states_covid19_daily where state like '{0}'
                order by date;", input);
                
                sql2 = string.Format(@"
                select date as DATE, positiveIncrease as positive, negativeIncrease as negative
                from us_states_covid19_daily where state like '{0}'
                order by date;", input2);
                
                sql3 = string.Format(@"
                select date as DATE, positiveIncrease as positive, negativeIncrease as negative
                from us_states_covid19_daily where state like '{0}'
                order by date;", input3);
                */

                sql = string.Format(@"
                select date as DATE, Sum(positiveIncrease) as positive, Sum(negativeIncrease) as negative
                from us_states_covid19_daily where state like '{0}'
                group by date
                order by date;", input);

                sql2 = string.Format(@"
                select date as DATE, Sum(positiveIncrease) as positive, Sum(negativeIncrease) as negative
                from us_states_covid19_daily where state like '{0}'
                group by date
                order by date;", input2);
                
                sql3 = string.Format(@"
                select date as DATE, Sum(positiveIncrease) as positive, Sum(negativeIncrease) as negative
                from us_states_covid19_daily where state like '{0}'
                group by date
                order by date;", input3);
                
                
                DataSet ds = DataAccessTier.DB.ExecuteNonScalarQuery(sql);
                DataSet ds2 = DataAccessTier.DB.ExecuteNonScalarQuery(sql2);
                DataSet ds3 = DataAccessTier.DB.ExecuteNonScalarQuery(sql3);
                
                string label;
                int pos, neg;
                
                foreach(DataRow row in ds.Tables[0].Rows) {
                  if(row["DATE"] != DataAccessTier.DB.nullvalue) {
                    label = Convert.ToString(row["DATE"]);
                    dates.Add(label);
                    pos = Convert.ToInt32(row["positive"]);
                    s1_pos.Add(pos);
                    neg = Convert.ToInt32(row["negative"]);
                    s1_neg.Add(neg);
                  }
                }
                    
                string label2;
                int pos2, neg2;
                
                foreach(DataRow row in ds2.Tables[0].Rows) {
                  if(row["DATE"] != DataAccessTier.DB.nullvalue) {
                    label2 = Convert.ToString(row["DATE"]);
                    dates.Add(label2);
                    pos2 = Convert.ToInt32(row["positive"]);
                    s2_pos.Add(pos2);
                    neg2 = Convert.ToInt32(row["negative"]);
                    s2_neg.Add(neg2);
                  }
                }
                
                string label3;
                int pos3, neg3;
                
                foreach(DataRow row in ds3.Tables[0].Rows) {
                  if(row["DATE"] != DataAccessTier.DB.nullvalue) {
                    label3 = Convert.ToString(row["DATE"]);
                    dates.Add(label3);
                    pos3 = Convert.ToInt32(row["positive"]);
                    s3_pos.Add(pos3);
                    neg3 = Convert.ToInt32(row["negative"]);
                    s3_neg.Add(neg3);
                  }
                }
                
                dates.Sort();
                d = dates.Distinct().ToList();
                
                foreach(DataRow row in ds.Tables[0].Rows) {
                  if(row["DATE"] != DataAccessTier.DB.nullvalue) {
                    while(d.Count() > s1_pos.Count()) {
                      s1_pos.Insert(0, 0);
                      s1_neg.Insert(0, 0);
                    }
                    /*
                    if(d.Count() > s1_pos.Count())
                      s1_pos.Insert(0, 0);
                      
                    if(d.Count() > s1_neg.Count())
                      s1_neg.Insert(0, 0);
                    */
                  }
                }
                
                foreach(DataRow row in ds2.Tables[0].Rows) {
                  if(row["DATE"] != DataAccessTier.DB.nullvalue) {
                    while(d.Count() > s2_pos.Count()) {
                      s2_pos.Insert(0, 0);
                      s2_neg.Insert(0, 0);
                    }
                    /*
                    if(d.Count() > s2_pos.Count())
                      s2_pos.Insert(0, 0);
                      
                    if(d.Count() > s2_neg.Count())
                      s2_neg.Insert(0, 0);
                    */                        
                  }
                }
                
                foreach(DataRow row in ds3.Tables[0].Rows) {
                  if(row["DATE"] != DataAccessTier.DB.nullvalue) {
                    while(d.Count() > s3_pos.Count()) {
                      s3_pos.Insert(0, 0);
                      s3_neg.Insert(0, 0);
                    }
                    /*
                    if(d.Count() > s3_pos.Count())
                      s3_pos.Insert(0, 0);
                      
                    if(d.Count() > s3_neg.Count())
                      s3_neg.Insert(0, 0);
                    */
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

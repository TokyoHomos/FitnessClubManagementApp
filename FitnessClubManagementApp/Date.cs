using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Road6Bills
{
    public class Date
    {
        private string day;
        private string month;
        private string year;

        //--------------------------------------------------
        public Date(string day, string month, string year)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }
        public Date()
        {
            
        }
        //--------------------------------------------------
        public override string ToString()
        {
            return $"{this.day}/{this.month}/{this.year}";
        }
        //--------------------------------------------------
        public string GetDay()
        {
            return day ;
        }
        public string GetMonth()
        {
            return month ;
        }
        public string GetYear()
        {
            return year ;
        }
        //--------------------------------------------------
        public void SetDay(string day)
        {
            this.day = day ;

        }
        public void SetMonth(string month)
        {
            this.month = month ;
        }
        public void SetYear(string year)
        {
            this.year = year ;
        }
    }
}

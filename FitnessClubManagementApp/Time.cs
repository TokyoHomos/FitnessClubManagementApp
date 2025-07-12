using Road6Bills;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementApp
{
    public class Time
    {
        private string hour;
        private string minute;
        private string second;

        public Time() { }
        public Time(string hour, string minute, string second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }
        public override string ToString()
        {
            return $"{this.hour}:{this.minute}:{this.second}";
        }
        public string getHour() { return hour; }
        public string getMinute() { return minute; }
        public string getSecond() { return second; }
        public void setHour(string hour) { this.hour = hour; }
        public void setMinute(string minute) { this.minute = minute; }
        public void setSecond(string second) { this.second = second; }
        public void SetTime(string time)
        {
            string[] parts = time.Split(' ', ':', '/', '.');

            if(parts.Length == 3 )
            {
                this.hour=parts[0];
                this.minute=parts[1];
                this.second=parts[2];
            }
            else
            {
                Console.WriteLine("Bug Time Format");
            }
        }
        

    }
}

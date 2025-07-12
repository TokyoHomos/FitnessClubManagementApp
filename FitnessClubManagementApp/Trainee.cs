using Microsoft.SqlServer.Server;
using Road6Bills;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FitnessClubManagementApp
{
    public class Trainee
    {
        private string traineeID;
        private string firstName;
        private string lastName;
        private string birthDate;
        private string city;
        private string contactInfo;
        private TrainingGroups groupID;

        //----------------------------------------

        public ProgressTracker progressTracker;

        public Trainee()
        {

        }
       public Trainee(string traineeID, string firstName, string lastName, string birthDate, string city, string contactInfo, TrainingGroups groupID)
        {
            this.traineeID = traineeID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
            this.city = city;
            this.contactInfo = contactInfo;
            this.groupID = groupID;
        }
        //--------------------------------------------------
        public override string ToString()
        {
            return $"{this.traineeID},{this.firstName} {this.lastName},{this.birthDate},{this.city},{this.contactInfo},{this.groupID}";
        }
        //----------------------------------------
        public string GetFirstName()
        {
            return firstName;
        }
        public string GetLastName()
        {
            return lastName;
        }
        public string GetBirthDate()
        {
            return birthDate;
        }
        public string GetCity()
        {
            return city;
        }
        public string GetContactInfo()
        {
            return contactInfo;
        }
        public TrainingGroups GetGroupID()
        {
            return groupID;
        }
        public string GetTraineeID()
        {
            return traineeID;
        }
        //----------------------------------------
        public void SetTraineID(string traineeID)
        {
            this.traineeID = traineeID;
        }
        public void SetFirstName(string firstName)
        {
            this.firstName = firstName;
        }
        public void SetLastName(string lastName)
        {
            this.lastName = lastName;
        }
        public void SetBirthDate(string birthDate)
        {
            string[] parts = birthDate.Split(' ', '-', '/', '.');
            string day = parts[0];
            string month = parts[1];
            string year = parts[2];

            Date date = new Date(day, month, year);
            string a = date.ToString();

            this.birthDate = a;
        }
        public void SetCity(string city)
        {
            this.city = city;
        }
        public void SetContactInfo(string contactInfo)
        {
            this.contactInfo = contactInfo;
        }
        public void SetGroupID(TrainingGroups groupID)
        {
            this.groupID = groupID;
        }

        //----------------------------------------
        public void SaveTraineeData(string id, string fName, string lName, string date, string city, string contactInfo, string groupID, Trainee.ProgressTracker progressTracker)
        {
            //This method for save and write the trainee information in file
            bool checker = false;

            string folderPath = @"./Data/Trainees";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, $"{id}.txt");//to create text file in correct folder and name it ID

            string[] a = date.Split(',', ' ', '-', '/');
            string d = a[0];
            string c = a[1];
            string e = a[2];

            Date dateS = new Date(d, c, e);

            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine($"ID: {id}");
                streamWriter.WriteLine($"First Name: {fName}");
                streamWriter.WriteLine($"Last Name: {lName}");
                streamWriter.WriteLine($"BirthOFDate: {dateS.ToString()}");
                streamWriter.WriteLine($"City: {city}");
                streamWriter.WriteLine($"Contact: {contactInfo}");
                streamWriter.WriteLine($"GroupID: {groupID}");

                if(progressTracker != null)//to cheach if trainee enter measured information
                {
                    streamWriter.WriteLine($"Body Measurement: {progressTracker.ToString()}");
                }
                streamWriter.WriteLine($"=============================");

                checker = true;
            }
            Console.WriteLine();
            if (checker == true)
            {
                Console.WriteLine("Seccessful, Information of Trainee Saved");
            }
            
        }
        public Trainee[] ReadTraineeInfo(Trainee[] traineeInfo)
        {
            //this method for read trainees files for return all trainees information in trainee array
            Trainee[] trainee = new Trainee[100];
            int i = 0;


            string mainFolderPath = @"./Data";//main folder path
            string[] subFolderPath = Directory.GetDirectories(mainFolderPath, "Trainees");//to go direct in to currect folder

            foreach (string subFolder in subFolderPath) //this for move between text files
            {
                string[] traineeFiles = Directory.GetFiles(subFolder, "*.txt"); //get files
                foreach (string trainees in traineeFiles)
                {
                    string fileName = Path.GetFileName(trainees);

                    if (File.Exists(trainees))
                    {
                        string line, lineID, lineNAME, lineFNAME, lineLNAME, lineBOD, lineCITY, lineContactInfo, lineGroupID;

                        using (StreamReader sr = new StreamReader(trainees))
                        {
                            while (!sr.EndOfStream)
                            {
                                
                                line = sr.ReadLine();
                                if (line == "")
                                {
                                    line = sr.ReadLine() ;
                                }
                                string[] a = line.Split(':');
                                lineID = a[1].Trim();

                                line = sr.ReadLine() ;
                                string[] b = line.Split(':');
                                lineFNAME = b[1].Trim();

                                line = sr.ReadLine();
                                string[] c = line.Split(':');
                                lineLNAME = c[1].Trim();

                                line = sr.ReadLine();
                                string[] d = line.Split(':');
                                lineBOD = d[1].Trim();

                                line = sr.ReadLine();
                                string[] f = line.Split(':');
                                lineCITY = f[1].Trim();

                                line = sr.ReadLine();
                                string[] r = line.Split(':');
                                lineContactInfo = r[1].Trim();

                                line = sr.ReadLine();
                                string[] w = line.Split(':');
                                lineGroupID = w[1].Trim();

                                

                                TrainingGroups trainingGroups = new TrainingGroups(lineGroupID);

                                trainee[i] = new Trainee(lineID, lineFNAME, lineLNAME, lineBOD, lineCITY, lineContactInfo, trainingGroups);
                                i++;
                                break;
                            }
                        }
                    }

                }
            }            
            return trainee;
        }
        public int CountHowMuchTrainees(int count)
        {
            //this method for count how much trainees in program put i dont use it becase there are a simple way to know it from length folder Trainee
            string folderPath = @"./Data/Trainees";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine($"{folderPath}.txt");

            bool checker = false;
            if (File.Exists(filePath))
            {
                using(StreamReader sr = new StreamReader(filePath))
                {
                    while (!checker)
                    {
                        string line = sr.ReadLine();
                        string[] a = line.Split(':');
                        
                        if (a[0] == "ID")
                        {
                            count++;
                        }
                        if(line == null)
                        {
                            checker = true;
                        }
                    }
                }
            }
            return count;
        }
        //----------------------------------------
        public class ProgressTracker
        {
            
            private double neck;
            private double chest;
            private double waist;
            private double weight;
            private string dateMeasured;
            //--------------------------------------------------
            public ProgressTracker()
            {
                
            }
            public ProgressTracker(double neck, double chest, double waist, double weight, string dateMeasured)
            {
                this.neck = neck;
                this.chest = chest;
                this.waist = waist;
                this.weight = weight;
                this.dateMeasured = dateMeasured;
            }
            //--------------------------------------------------
            public string SaverType()
            {
                return $"{this.neck},{this.chest},{this.waist},{this.weight},{this.dateMeasured}";
            }
            public override string ToString()
            {
                return $"Neck: {this.neck}, Chest: {this.chest}, Waist: {this.waist}, Weight: {this.weight}, Measurement Date: {this.dateMeasured}";
            }
            //--------------------------------------------------
            public double GetNeck()
            {
                return neck;
            }

            public double GetChest()
            {
                return chest;
            }
            public double GetWaist()
            {
                return waist;
            }
            public double GetWeight()
            {
                return weight;
            }
            public string GetDateMeasured()
            {
                return dateMeasured;
            }
            //--------------------------------------------------
            public void SetNeck(double neck)
            {
                this.neck = neck;
            }
            public void SetChest(double chest)
            {
                this.chest = chest;
            }
            public void SetWaist(double waist)
            {
                this.waist = waist;
            }
            public void SetWeight(double weight)
            {
                this.weight = weight;
            }
            public string SetDateMeasured(string dateMeasured)
            {

                string[] parts = dateMeasured.Split(' ','-','/','.');
                string day = parts[0];
                string month = parts[1];
                string year = parts[2];
                 
                Date date = new Date(day, month, year);
                string a = date.ToString();

                return this.dateMeasured = a;
            }
            //--------------------------------------------------
            public void SaveTraineeMeasurmentByID(ProgressTracker progressTracker,string id)
            {
                //this method for save and write measiured trainee information in file
                string folderPath = @"./Data/Measurements";

                if(!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, $"{id}.txt");

                using(StreamWriter sw = new StreamWriter(filePath,true))
                {
                    sw.WriteLine(progressTracker.SaverType());
                }
            }
        }
    }
}

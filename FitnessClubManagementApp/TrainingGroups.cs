using FitnessClubManagementApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementApp
{
    
    public class TrainingGroups
    {
        private int groupID;
        private string category;
        private string ageCategory;
        //----------------------------------------
        public TrainingGroups(int groupID, string category, string ageCategory)
        {
            this.groupID = groupID;
            this.category = category;
            this.ageCategory = ageCategory;
        }
        public TrainingGroups(string category)
        {
            this.category = category;
        }
        public TrainingGroups()
        {

        }
        //----------------------------------------
        public override string ToString()
        {
            return $"{this.category}";
        }
        public string TS()
        {
            return $"{this.groupID},{this.category},{this.ageCategory}";
        }
        //----------------------------------------
        public int GetGroupID()
        {
            return groupID; 
        }
        public string GetCategory() 
        {
            return category;
        }
        public string GetAgeCategory() 
        {
            return ageCategory;
        }
        //----------------------------------------
        public void SetGroupID(int groupID)
        {
            this.groupID = groupID;
        }
        public void SetCategory(string category)
        {
            this.category = category;
        }
        public void AddAgeCategory(string category)
        {
            this.ageCategory = category;
        }
        //----------------------------------------
        public void SaveCategoryInfo(TrainingGroups[] trainingGroups)
        {
            //This method for save and write the training groups information in file
            string folderPath = @"./Data/Groups";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, "TrainingGroups.txt");

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                for (int i = 0; i < trainingGroups.Length; i++)
                {
                    if (trainingGroups[i] != null)
                    {
                        sw.WriteLine(trainingGroups[i].TS());
                    }
                }
                sw.Close();

                Console.WriteLine("Seccessful, Information of Groups Saved");
            }
        }
        public TrainingGroups[] ReadCategoryInfoFile()
        {
            //This method for read and return array the training groups information in file
            TrainingGroups[] trainingGroupsBackUP = new TrainingGroups[100];
            int index = 0;

            string folderPath = @"./Data/Groups";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, "TrainingGroups.txt");

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null && index < 100)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3   )
                    {
                        int id = int.Parse(parts[0]);
                        string category = parts[1];
                        string ageCategory = parts[2];

                        trainingGroupsBackUP[index] = new TrainingGroups(id, category, ageCategory);
                        index++;
                    }
                }
            }
           

            return trainingGroupsBackUP;
        }
        public string[] SaveINArrWorkoutGroups(string[] inArrWorkoutGroups)
        {
            string folderPath = @"./Data/Groups";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, "TrainingGroups.txt");
            int i = 0;
            if (File.Exists(filePath))
            {
                using(StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] a = line.Split(',');

                        if (a.Length < 3 )
                        {
                            continue;
                        }
                        string id = a[0];
                        string category = a[1];
                        string ageCategory = a[2];

                        inArrWorkoutGroups[i] = category;
                        i++;
                    }
                }
            }
            return inArrWorkoutGroups;
        }
        public string ReturnWorkoutCategoryByID(string w)
        {
            string folderPath = @"./Data/Groups";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, "TrainingGroups.txt");
            int i = 1;
            bool checher = false;

            string id, category = "", ageCategory;
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine("Line: " + line);
                        string[] a = line.Split(',');

                        if (a.Length < 3)
                        {
                            continue;
                        }
                        id = a[0];
                        category = a[1];
                        ageCategory = a[2];

                        Console.WriteLine($"Comparing ID: {id} to w: {w}");

                        if (w != null)
                        {
                            if (id == w)
                            {
                                Console.WriteLine("Found Match! Category: " + category); // ✅ تتبع
                                return category;
                            }
                        }
                        i++;
                    }
                }
            }
            return null;
        }
        public int CountHowMuchGroups()
        {
            int count = 0;
            string folderPath = @"./Data/Groups";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, "TrainingGroups.txt");
            bool checker = false;
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!checker)
                    {
                        string line = sr.ReadLine();
                        
                        if(line != null)
                        {
                            count++;
                        }
                        else
                        {
                            checker = true;
                        }
                    }
                }
            }
            return count;
        }
    }
}

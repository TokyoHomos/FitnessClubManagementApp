using Road6Bills;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static FitnessClubManagementApp.Trainee;

namespace FitnessClubManagementApp
{
    internal class Program
    {
        //static Trainee[] traineesArr = new Trainee[100];
        //static Date[] datesArr = new Date[100];
        static Trainee trainee = new Trainee();                                             //To Access to all Trainee Class methods features
        static TrainingGroups[] trainingGroupsArr = new TrainingGroups[100];                //To Create Array from TrainingGroups type
        static TrainingGroups trainingGroups = new TrainingGroups();                        //To Access to all TrainingGroups Class methods features
        //static Time time = new Time();
        static string[] trainingGroupsNames = new string[100];                              //To Save in array all categoey names in array to print it in AddTrainee Section for choose which category need
        static Trainee.ProgressTracker progressTracker = new Trainee.ProgressTracker();     //To Access to all ProgressTracker Class methods features


        static string id, workoutCategory;                                                  //this values identifiears her (because to reach theit values anywhere in the program) 
        static DateTime timeEntry = DateTime.Now;                                           //this values identifiears her (because to reach theit values anywhere in the program) 
        static DateTime timeExit = DateTime.Now;                                            //this values identifiears her (because to reach theit values anywhere in the program)
        static double longTraining;                                                         //this values identifiears her (because to reach theit values anywhere in the program)


        static void ServiceMenu() //Done
        {
            trainingGroups.SaveINArrWorkoutGroups(trainingGroupsNames);  //to read and return category names as the program run
            Console.Clear();
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t\t\t==================================\r\n\t\t\t\t\t   Fitness Club Management App\r\n\t\t\t\t\t==================================");
            
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t\t\tTrainee Section:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t\t\t1. Add New ");
            Console.WriteLine("\t\t\t\t\t2. View All ");
            Console.WriteLine("\t\t\t\t\t3. Register Entry");
            Console.WriteLine("\t\t\t\t\t4. Register Exit");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t\t\tTraining Groups Section:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t\t\t5. Add New");
            Console.WriteLine("\t\t\t\t\t6. View All");
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\t7. Generate System Report");
            Console.WriteLine("\t\t\t\t\t8. Exit");
            Console.WriteLine();
            Console.Write("\t\t\t\t\tPlease enter your choice: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            int choice = int.Parse(Console.ReadLine());
            Console.ResetColor();
            switch(choice)
            {
                case 1:
                    AddTrainee();
                    break;
                case 2:
                    ShowTrainees();
                    break;
                case 3:
                    RegisterEntry();
                    break;
                case 4:
                    RegisterExit();
                    break;
                case 5:
                    AddGroup();
                    break;
                case 6:
                    ShowGroups();
                    break;
                case 7:
                    PrintReport();
                    break;
                case 8:
                    Console.WriteLine("\t\t\t\t\t\tTo Exit Press Double Enter");
                    break;
            }
        }
        //------------------- Service Section --------------
        static void AddTrainee()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==================================");
            Console.WriteLine("      Add Yur Informaition");
            Console.WriteLine("==================================");
            string id,fName, lName, birthDate, city, contactInfo;  //this input values for personal trainee informaion
            double neckSize, chestSize, waistSize, weight;         //this input values for body measured trainee informaion
            string dateMeasured;                                   //this input values for body measured trainee date
            bool checker = false;
            Date date1 = new Date();                               //to create new date object in memory


            Console.Write("ID (9-Digits): ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            id = Console.ReadLine();                                //trainee id input value

            if(id.Length != 9)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("==================================");
                Console.WriteLine("      Add Yur Informaition");
                Console.WriteLine("==================================");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalidate");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("ID (9-Digits): ");
                Console.ForegroundColor = ConsoleColor.Magenta;

                id = Console.ReadLine();
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("First Name: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            fName = Console.ReadLine();                             //trainee first name input value
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Last Name: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            lName = Console.ReadLine();                             //trainee last name input value
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Notice you can use any format you nee (' ' ',' '-' '/')");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("BirthDate: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            birthDate = Console.ReadLine();                         //trainee birth of date input value
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("City: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            city = Console.ReadLine();                              //trainee adress input value
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Contatct (EmailPhone Number): ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            contactInfo = Console.ReadLine();                       //trainee contact information input value
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (contactInfo.Length != 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalidate");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Contatct (Email / Phone Number '10 Nums'): ");
                Console.ForegroundColor = ConsoleColor.Magenta;

                contactInfo = Console.ReadLine();
            }
            Console.ForegroundColor = ConsoleColor.Yellow;

            DisplayTrainingGroups();                                //for display category workout name to choose which one need

            Console.Write("Select Workout Group: ");
            Console.ForegroundColor = ConsoleColor.Magenta;

            workoutCategory = Console.ReadLine();                   //trainee workout category input value
            workoutCategory = trainingGroups.ReturnWorkoutCategoryByID(workoutCategory);//to return workout category id trainee choosed



            //Trainee Measured Informatin Saction
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("---------------------------");
            Console.WriteLine("Now Need your body measurements for track progress");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Neck Size: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            neckSize = double.Parse(Console.ReadLine());             //trainee neck size input value

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Chest Size: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            chestSize = double.Parse(Console.ReadLine());            //trainee chest size input value
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Waist Size: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            waistSize = double.Parse(Console.ReadLine());            //trainee waist size input value
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Weight: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            weight = double.Parse(Console.ReadLine());               //trainee weight input value

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Notice you can use any format you nee (' ' ',' '-' '/')");
            Console.Write("Measurement Date: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            dateMeasured = Console.ReadLine();                       //date measured date input value

            Console.ForegroundColor = ConsoleColor.Yellow;
            string date = progressTracker.SetDateMeasured(dateMeasured);//to return correct date format

            progressTracker = new Trainee.ProgressTracker(neckSize,chestSize,waistSize,weight,date);//save this information in ProgressTracker class format

            trainee.SaveTraineeData(id, fName, lName, birthDate, city, contactInfo, workoutCategory, progressTracker);//to move all this informaion to save and write it in file

            progressTracker.SaveTraineeMeasurmentByID(progressTracker,id);//to save measured informaion in file


            Console.WriteLine();
            Console.Write("Do you want to back to service menu: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string choice = Console.ReadLine();


            if (choice == "Yes" || choice == "yes" || choice == "y" || choice == "Y")
            {
                ServiceMenu();
            }
            if (choice == "No" || choice == "no" || choice == "n" || choice == "N")
            {
                Environment.Exit(0);
            }
        } //Done
        static void ShowTrainees()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==================================");
            Console.WriteLine("      Show All Trainee Details");
            Console.WriteLine("==================================");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("+---------+------------+-----------+-------------+--------------+-----------------------+");
            Console.WriteLine("| ID      | First Name | Last Name | Birth Date  | ContactInfo  | Group                 |");
            Console.WriteLine("+---------+------------+-----------+-------------+--------------+-----------------------+");

            Trainee[] traineesBackIPRead = new Trainee[100];                  
            traineesBackIPRead = trainee.ReadTraineeInfo(traineesBackIPRead);//this array for read trainee faile and save all informaion and return in to print in table for show all trainees in my program


            foreach (Trainee t in traineesBackIPRead)//use foreach loob for move and print all trainees in traineesBackIPRead array in table
            {
                if(t == null) continue;
                string group = t.GetGroupID().ToString();
                Console.WriteLine($"|{t.GetTraineeID()}| {t.GetFirstName().PadRight(11)}| {t.GetLastName().PadRight(10)}| {t.GetBirthDate().PadRight(12)}| {t.GetContactInfo().PadRight(13)}| {group.PadRight(22)}|");
                //i used the PadRight method for arrange dimensions table
            
            }
            Console.WriteLine("+---------+------------+-----------+-------------+--------------+-----------------------+");


            Console.WriteLine();
            Console.Write("Do you want to back to service menu: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string choice = Console.ReadLine();


            if (choice == "Yes" || choice == "yes" || choice == "y" || choice == "Y")
            {
                ServiceMenu();
            }
            if (choice == "No" || choice == "no" || choice == "n" || choice == "N")
            {
                Environment.Exit(0);
            }
        } //Done
        static void AddGroup() //Done
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==================================");
            Console.WriteLine("      Add Group Informaition");
            Console.WriteLine("==================================");

            string category, ageCategory;

            int i = 0;
            bool check = false;

            TrainingGroups[] trainingGroupsArr = new TrainingGroups[100];                //Use it like a mediator to save in it workout groups and write it in text file

            while (!check)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Workout Category: ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                category = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Age Workout Category: ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                ageCategory = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                TrainingGroups[] existingGroups = trainingGroups.ReadCategoryInfoFile();//Use this array for check workout category file whats category in it

                int count = 0;
                while (count < existingGroups.Length && existingGroups[count] != null)  //for count number of category to make id for new category
                {
                    count++;
                }

                trainingGroupsArr[i] = new TrainingGroups(count + 2, category, ageCategory);//Give array new category to save and write it in file

                trainingGroups.SaveCategoryInfo(trainingGroupsArr);

                Console.Write("Do you want to add more workout category (y/n): ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                string choice = Console.ReadLine();

                if (choice == "y" || choice == "Y" || choice == "yes" || choice == "Yes")
                {
                    i++;
                    AddGroup();
                }
                if (choice == "n" || choice == "N" || choice == "No" || choice == "no")
                {
                    check = true;
                    ServiceMenu();
                }
            }
        }
        static void ShowGroups() //Done
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==================================");
            Console.WriteLine("      Show Groups ");
            Console.WriteLine("==================================");
            int i = 0;
            bool check = false;

            DisplayTrainingGroups(); //to shaw grou

            Console.Write("Do you want to add new Group Workout (y/n): ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string choice = Console.ReadLine();

            if (choice == "y" || choice == "Y" || choice == "yes" || choice == "Yes")
            {
                i++;
                AddGroup();
            }
            if (choice == "n" || choice == "N" || choice == "No" || choice == "no")
            {
                check = true;
                ServiceMenu();
            }
        }
        static void RegisterEntry()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==================================");
            Console.WriteLine("          Entry Time");
            Console.WriteLine("==================================");

            Console.WriteLine();
            Console.Write("ID (9-Digits): ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            id = Console.ReadLine(); //to get a id to identified the trainee

            if (id.Length != 9)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("==================================");
                Console.WriteLine("          Entry Time");
                Console.WriteLine("==================================");

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalidate");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("ID (9-Digits): ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                id = Console.ReadLine();
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"DateTime (DD/MM/YYYY HH:MM:SS): {timeEntry}");//I Use here DateTime Class for give me the correct time


            Console.WriteLine();
            Console.Write("Do you want to back to service menu: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string choice = Console.ReadLine();


            if (choice == "Yes" || choice == "yes" || choice == "y" || choice == "Y")
            {
                ServiceMenu();
            }
            if (choice == "No" || choice == "no" || choice == "n" || choice == "N")
            {
                Environment.Exit(0);
            }

        } //Done
        static void RegisterExit()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==================================");
            Console.WriteLine("          Exit Time");
            Console.WriteLine("==================================");

            Console.WriteLine();
            Console.Write("ID: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            id = Console.ReadLine();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"DateTime (DD/MM/YYYY HH:MM:SS): {timeExit}");
            Console.WriteLine();
            Console.WriteLine($"You Trained in {longTraining} Houres Today");


            SaveEntryExitTime(id); //method for save and write time informatin in file

            Console.WriteLine();
            Console.Write("Do you want to back to service menu: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string choice = Console.ReadLine();


            if (choice == "Yes" || choice == "yes" || choice == "y" || choice == "Y")
            {
                ServiceMenu();
            }
            if (choice == "No" || choice == "no" || choice == "n" || choice == "N")
            {
                Environment.Exit(0);
            }
        }//Done
        static void PrintReport()
        {
            DateTime dateTime = DateTime.Now;
            int totalTrainees = 0;
            int counterGroups = 0;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("========== Club Daily Report ==========");

            string[] traineeFiles = Directory.GetFiles("./Data/Trainees");//direct folder path
            totalTrainees = traineeFiles.Length; //to get how much trainees in program

            counterGroups = trainingGroups.CountHowMuchGroups(); //to get how much TrainingGroups in program
            Console.WriteLine();
            Console.WriteLine($"Date: {dateTime}");               //print real time
            Console.WriteLine($"Total Trainees: {totalTrainees}");//print Total Trainees Number
            Console.WriteLine($"Total Groups: {counterGroups}");  //print Total Groups Number

            Console.WriteLine();

            CountHowMuchTraineePerGroup();                        //this method for count how much trainees in each training groups



            Console.WriteLine();
            Console.Write("Do you want to back to service menu: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string choice = Console.ReadLine();


            if (choice == "Yes" || choice == "yes" || choice == "y" || choice == "Y")
            {
                ServiceMenu();
            }
            if (choice == "No" || choice == "no" || choice == "n" || choice == "N")
            {
                Environment.Exit(0);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;


        }
        //------------------- Methods --------------
        static void SaveEntryExitTime(string id)
        {
            string timeDate1 = timeEntry.ToString(); //Entry Time
            string timeDate2 = timeExit.ToString();  //Exit Time


            double calculatorMinutes;
            double calculatorHoures;

            string folderPath = @"./Data/EntryExit";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, $"{id}.txt");

            using (StreamWriter sw = new StreamWriter(filePath,true))
            {
                //Calculate long time trainee it took
                string[] a = timeDate1.Split(' '); //entry time

                //splite between date and time
                string entryDate = a[0].Trim();
                string entryTime = a[1].Trim();

                string[] b = timeDate2.Split(' ');//exit time

                //splite between date and time
                string exitDate = b[0].Trim();
                string exitTime = b[1].Trim();


                //split entry time between hour ,minutes and seconde
                string[] timeEntryParts = entryTime.Split(':');

                double hourEn = double.Parse(timeEntryParts[0]);
                double minuteEn = double.Parse(timeEntryParts[1]);
                double secondeEn = double.Parse(timeEntryParts[2]);


                //split exit time between hour ,minutes and seconde
                string[] timeExitParts = exitTime.Split(':');

                double hourEx = double.Parse(timeExitParts[0]);
                double minuteEx = double.Parse(timeExitParts[1]);
                double secondeEx = double.Parse(timeExitParts[2]);


                calculatorMinutes = (hourEx - hourEn) * 60 + (minuteEx - minuteEn); // Calculate minutes function ((seconde hour negative first hour) * 60 + (seconde minutes negative first minites)
                calculatorHoures = calculatorMinutes / 60;                          //To Convert from minutes to hours

                longTraining = calculatorHoures; //To return the lont time trained to trainee screen

                sw.WriteLine($"{calculatorHoures},{timeEntry} - {timeExit} "); //To write it in file
                sw.Close();
            }
            
        }
        static void DisplayTrainingGroups()
        {
            //this method for display category workout names for trainee to choose which category need
            Console.WriteLine("----------------------------------------------");

            for (int i = 0; i < trainingGroupsNames.Length; i++)
            {
                if (trainingGroupsNames[i] != null)
                {
                    Console.WriteLine("| " + $"{i + 1}. " + trainingGroupsNames[i].PadRight(37) + "   |");
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("----------------------------------------------");
        }
        static void CountHowMuchTraineePerGroup()
        {
            //this method for chech an read how much trainees in each category
            string[] categoryNames = new string[100];
            int[] counterArr = new int[100];
            int[] placeIndex = new int[100];

            trainingGroups.SaveINArrWorkoutGroups(categoryNames);

            for(int i = 0;i < counterArr.Length; i++) // To Fill Array 0
            {
                counterArr[i] = 0;
            }

            
            string mainFolderPath = @"./Data"; //main folder path
            string[] subFolderPath = Directory.GetDirectories(mainFolderPath, "Trainees"); //to go direct in to currect folder

            foreach (string subFolder in subFolderPath) //this for move between text files
            {
                string[] traineeFiles = Directory.GetFiles(subFolder, "*.txt");//get files
                foreach (string trainee in traineeFiles)
                {
                    string fileName = Path.GetFileName(trainee);

                    
                    bool checker = false;
                    //her for move into test files to read and check category each trainee have
                    using (StreamReader sr = new StreamReader(trainee))
                    {
                        while (!checker && !sr.EndOfStream)
                        {
                            string line = sr.ReadLine(); //read line
                            if (line == null)//check if lie is null
                                continue;

                            string[] lineParts = line.Split(':');//split line for get the direct line need
                            if (lineParts[0].Trim() == "GroupID")//check if the first part of line is "GroupID" (notice: i use .Tirm() method for remove spaces)
                            {
                                string tempValue = lineParts[1].Trim();//get the category name

                                for (int j = 0; j < categoryNames.Length; j++)
                                {
                                    if (categoryNames[j] != null && tempValue == categoryNames[j])
                                    {
                                        counterArr[j]++;
                                        break;
                                    }
                                }

                                checker = true;
                            }
                        }
                    }
                }
                //Notice: in this method i use algorithm idea, i will took about in in WORD document
            }

            //this for print result
            Console.WriteLine("Trainees Per Group:");
            for (int i = 0; i < categoryNames.Length; i++)
            {
                if (categoryNames[i] != null && counterArr[i] > 0)
                {
                    Console.WriteLine($"- {categoryNames[i]}: {counterArr[i]}");
                }
            }

        }


        static void Main(string[] args)
        {
            ServiceMenu();
            
            Console.ReadKey();
        }
    }
}

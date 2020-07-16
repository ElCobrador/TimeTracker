using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace TimeTracker
{


    class Program
    {
        static void Main(string[] args)
        {
            
            bool KeepAppRunning = true;
            GuiDrawer GuiDrawer;
            GuiDrawer = new GuiDrawer(100, 15);

            while(KeepAppRunning)
            {
                Console.Clear();
                var items = new List<string>() { "Value1", "Value2", "Value3"};
                GuiDrawer.DrawInterface(items);
                string userMessage =  Console.ReadLine();

                if(userMessage == "quit")
                    KeepAppRunning = false;

                TimeRepository timeRepository = new TimeRepository("C:\\Users\\Barbe Rousse\\Documents\\_TimeSpentOnProjects\\Today.txt");
                timeRepository.Save(new TimeSpentOnProject() {StartTime = DateTime.Now, EndTime = DateTime.Now, "FileWriter"});
            }
        }
    }

    class TimeRepository
    {   
        private string PathToFile;
        public TimeRepository( string pathToFile )
        {
            PathToFile = pathToFile;

            JSonSerializer = new JSonSerializer();
        }

        public void Save(string command)
        {
            using (FileStream fileStream = new FileStream(PathToFile, FileMode.OpenOrCreate))
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                /* Write at the end of the file
                var endOfFile = fileStream.Length;
                fileStream.Seek(endOfFile, SeekOrigin.Begin );
                */
                streamWriter.WriteLine(command);
            }
        }
    }

    class WorkDay
    {
        public List<TimeSpentOnProject> TimeSpentOnProjects;
    }

    class TimeSpentOnProject
    {
        public DateTime StartTime;
        public DateTime EndTime;
        public string project;
    }

    class TimeMapper
    {

    }

    class GuiDrawer
    {
        private int GuiWidth;
        private int GuiHeight;
        private int FirstItemsAtY = 2;
        private int FirstItemAtX = 10;
        private const int UserInputOffset = 30;
        private string TopDelimiter = "";
        private string sidesDelimiter = "";

        public GuiDrawer(int guiWidth, int guiHeight)
        {
            GuiWidth = guiWidth;
            GuiHeight = guiHeight;
        }

        public void DrawInterface(List<string> itemsToDraw )
        {
            // Header
            DrawTopDelimiter();

            // Content
            DrawContent(itemsToDraw);

            
        }

        public void DrawContent(List<string> itemsToDraw)
        {
            for(int i = 0; i <= FirstItemsAtY; i++)
            {
                DrawSide();
            }

            foreach(var item in itemsToDraw)
            {
                DrawItem(item);
            }

            int numbersOfLinesToFill = GuiHeight - itemsToDraw.Count() - FirstItemsAtY - 1;
            for(int i = 0; i <= numbersOfLinesToFill; i++)
            {
                DrawSide();
            }

            string userInput = "+";
            for(int i = 0; i < UserInputOffset; i++)
            {
                userInput += "-";
            }
            userInput += " ";
            Console.Write(userInput);
        }

        public void DrawItem(string item)
        {
            string lineToDraw = "|";
            for(int i = 0; i <= FirstItemAtX; i++)
            {
                lineToDraw += " ";
            }
            lineToDraw += item;
            
            int numbersOfCharsToFill = (GuiWidth - lineToDraw.Length) - 1;
            for(int i = 0; i < numbersOfCharsToFill; i++)
            {
                lineToDraw += " ";
            }

            lineToDraw += "|";
            Console.WriteLine(lineToDraw);
        }

        public void DrawTopDelimiter()
        {
            if(TopDelimiter == "")
            {
                TopDelimiter += "+";
                for(int i = 0; i < GuiWidth - 2; i++)
                {
                    this.TopDelimiter += "-";
                }
                TopDelimiter += "+";
            }

            Console.WriteLine(TopDelimiter);            
        }

        public void DrawSide()
        {
            if(sidesDelimiter == "")
            {
                sidesDelimiter += '|';
                for(int i = 0; i < GuiWidth - 2; i++)
                {
                    this.sidesDelimiter += " ";
                }
                sidesDelimiter += "|";
            }

            Console.WriteLine(sidesDelimiter);
        }        
        
    }
}

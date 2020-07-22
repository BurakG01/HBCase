using System;
using System.Collections.Generic;
using System.IO;

namespace HBCase.Scenario
{
    public interface IScenarioReader
    {
        List<string> ReadScenario(string filePath);
    }
    public class ScenarioReader : IScenarioReader
    {
        private readonly int _fileNameCharacterSize=13;
        public List<string> ReadScenario(string filePath)
        {
            var fileName = filePath.Substring(filePath.Length - _fileNameCharacterSize);

            Console.WriteLine($"Output of {fileName} File");

            Console.WriteLine("\n");

            var commands=ReadCommandsFromFile(filePath);

            return commands;
        }
        private List<string> ReadCommandsFromFile(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            StreamReader sw = new StreamReader(fs);

            var commands = new List<string>();

            string commandLine = sw.ReadLine();

            while (commandLine != null)
            {
                commands.Add(commandLine);

                commandLine = sw.ReadLine();
            }

            fs.Close();

            return commands;
        }
    }
}

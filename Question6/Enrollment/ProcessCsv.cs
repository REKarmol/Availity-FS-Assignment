using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Enrollment
{
    class ProcessCsv
    {
        private string filePath;
        private string fileDirectory;
        private Dictionary<string, Dictionary<string,CsvRecord>> insuranceDictionary;

        public ProcessCsv(string filePath)
        {
            this.filePath = filePath;
            FileInfo fileInfo = new FileInfo(filePath);
            fileDirectory = fileInfo.DirectoryName;
            insuranceDictionary = new Dictionary<string, Dictionary<string, CsvRecord>>();
        }

        public void ProcessFile()
        {
            ReadFile();
            WriteFiles();
        }

        private void ReadFile()
        {
            int lineCounter = 0;
            string[] csvLines = System.IO.File.ReadAllLines(filePath);
            foreach (string csvLine in csvLines)
            {
                lineCounter++;
                ParseLine(csvLine, lineCounter);
            }
        }

        private void ParseLine(string csvLine, int lineCounter)
        {
            string[] csvValue = csvLine.Split(',');

            if (csvLine.Length == 0)
            {
                return;
            }

            if (csvValue.Length != 5)
            {
                throw new Exception(string.Format("Bad line format in line {0}", lineCounter));
            }

            string userId = csvValue[0].Trim();
            string firstName = csvValue[1].Trim();
            string lastName = csvValue[2].Trim();
            string insurance = csvValue[4].Trim();
            int version;
            if (int.TryParse(csvValue[3], out version) == false)
            {
                throw new Exception(string.Format("Bad version in line {0}", lineCounter));
            }

            CsvRecord csvRecord = new CsvRecord(userId, firstName, lastName, version);

            // each insurance entry in dictionary builds its own sub dictionary of csv records
            Dictionary<string, CsvRecord> userDictionary;
            if (!insuranceDictionary.TryGetValue(insurance, out userDictionary))
            {
                // new insurance entry, need to add csvRecord to userDictionary and then add that to insuranceDictionary
                userDictionary = new Dictionary<string, CsvRecord>();
                userDictionary.Add(userId, csvRecord);
                insuranceDictionary.Add(insurance, userDictionary);
            }
            else
            {
                // existing insurance; update userDictionary record if needed, else add
                CsvRecord csvRecordInDictionary;
                if (userDictionary.TryGetValue(userId, out csvRecordInDictionary))
                {
                    // want the higher version if already existed, else leave old one
                    if (csvRecordInDictionary.Version > csvRecord.Version)
                    {
                        userDictionary[userId] = csvRecordInDictionary;
                    }
                    else
                    {
                        userDictionary[userId] = csvRecord;
                    }
                }
                else
                {
                    // add this new csvRecord to this userDictionary 
                    userDictionary.Add(userId, csvRecord);
                }
            }
        }

        private void WriteFiles()
        {
            foreach(KeyValuePair<string,Dictionary<string, CsvRecord>> kv in insuranceDictionary)
            {
                string newFile = fileDirectory + "\\" + kv.Key.Replace(" ","") + ".enr";

                // our userDictionary converted to List<csvRecord>
                var newList = kv.Value.ToList();
                newList.Sort((a, b) => ((a.Value.LastName+"."+a.Value.FirstName).CompareTo((b.Value.LastName + "." + b.Value.FirstName))));

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(newFile))
                {
                    foreach (KeyValuePair<string,CsvRecord> listItem in newList)
                    {
                        file.WriteLine("{0},{1},{2},{3},{4}", listItem.Value.UserId, listItem.Value.FirstName, listItem.Value.LastName, listItem.Value.Version.ToString(), kv.Key);
                    }
                }
            }
        }
    }
}

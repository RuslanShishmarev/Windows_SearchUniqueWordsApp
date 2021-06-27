using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace SearchUniqueWordsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //get text file
            string file = Console.ReadLine();
            //string file = "voyna-i-mir_sm.txt";
            string separatedStr = "------------------------";
            if (File.Exists(file))
            {
                Console.WriteLine("Wait...");
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var enc1251 = Encoding.GetEncoding(1251);
                using (StreamReader sr = new StreamReader(file, enc1251))
                {
                    //get text from file
                    string allText = sr.ReadToEnd().Replace(Environment.NewLine, " ").Trim(new Char[] { ' ', '*', '.', ',', }).Replace("-", "");
                    string[] inputTextAsList = allText.Split(" ");

                    //start method
                    var start = DateTime.Now;
                    var uniqueWords = GetDublicatesWithIndexList(inputTextAsList);

                    //show 20 words
                    Console.WriteLine("\nTOP 20 words");
                    int maxCount = 20;
                    int startCount = 0;
                    foreach (var selectedWord in uniqueWords.OrderBy(pair => pair.Value.Count()).Reverse())
                    {
                        if(startCount < maxCount)
                        {
                            string newLineResult = String.Format("\nMain word: {0}, count: {1}", selectedWord.Key, selectedWord.Value.Count());
                            Console.WriteLine(newLineResult);

                            //find 5 closed words 
                            int maxCountForAdded = 5;
                            int startCountForAdded = 0;

                            var allAddWords = new List<string>();
                            foreach(int i in uniqueWords[selectedWord.Key])
                            {
                                allAddWords.Add(inputTextAsList[i - 1]);
                                allAddWords.Add(inputTextAsList[i + 1]);
                            }
                            var dublicatesForSelectedWord = GetDublicatesWithIndexList(allAddWords);
                            foreach (var addWord in dublicatesForSelectedWord.OrderBy(newPair => newPair.Value.Count()).Reverse())
                            {
                                if (startCountForAdded < maxCountForAdded)
                                {
                                    string secondLineResult = String.Format("Additional word: {0}, count: {1}", addWord.Key, addWord.Value.Count());
                                    Console.WriteLine(secondLineResult);
                                    startCountForAdded++;
                                }
                                else { break; }
                            }
                            Console.WriteLine(separatedStr);
                            //--------------------------------------
                            startCount++;
                        }
                        else { break; }
                    }
                    var end = DateTime.Now;
                    var speed = end - start;
                    
                    Console.WriteLine("\nComplete! Total duration = " + speed);
                }
            }
            else
            {
                Console.WriteLine("File is not exist");
            }
        }
        private static Dictionary<string, List<int>> GetDublicatesWithIndexList(IEnumerable<string> textAsArray)
        {
            var duplicates = textAsArray.Select((x, i) => new { i, x })
              .GroupBy(x => x.x)
              .Where(g => g.Count() > 1)
              .Where(g => g.Key != "")
              .ToDictionary(g => g.Key, g => g.Select(x => x.i).ToList());
            return duplicates;
        }
    }
}

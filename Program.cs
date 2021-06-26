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
            //var inputTextFilePath = Console.ReadLine();
            string file = "C://Users//Admin//Desktop//Programming//c#//AllTests//voyna-i-mir.txt";

            if (File.Exists(file))
            {
                Console.WriteLine("Wait...");
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var enc1251 = Encoding.GetEncoding(1251);
                using (StreamReader sr = new StreamReader(file, enc1251))
                {
                    //get input text
                    string allText = sr.ReadToEnd().Replace(Environment.NewLine, " ").Replace(",", "").Replace(".", "").Replace("-", "");
                    string[] inputTextAsList = allText.Split(" ");

                    #region old method
                    //var start = DateTime.Now;
                    //var uniqueWords = inputTextAsList.Distinct();

                    //sort unique words by count
                    //uniqueWords = uniqueWords.OrderBy(word => GetCountOfWord(inputTextAsList, word)).Reverse();
                    //var end = DateTime.Now;
                    //var speed = end - start;

                    //show 20 words
                    //Console.WriteLine("TOP 20 words");
                    //for (int i = 0; i < 20; i++)
                    //{
                    //    string oneLineResult = uniqueWords.ToList()[i] + " " + GetCountOfWord(inputTextAsList, uniqueWords.ToList()[i]);
                    //    Console.WriteLine(oneLineResult);
                    //}
                    //Console.WriteLine("Complete! TotalDuration = " + speed);
                    //Console.WriteLine();
                    #endregion

                    //other method
                    var start = DateTime.Now;
                    var uniqueWords = GetDublicatesWithIndexList(inputTextAsList);

                    //show 20 words
                    Console.WriteLine("TOP 20 words");
                    int maxCount = 20;
                    int startCount = 0;
                    foreach (var pair in uniqueWords.OrderBy(pair => pair.Value.Count()).Reverse())
                    {
                        if(startCount < maxCount)
                        {
                            string newLineResult = String.Format("Word: {0}, Count: {1}", pair.Key, pair.Value.Count());
                            Console.WriteLine(newLineResult);
                            //find 5 

                            startCount++;
                        }
                        else { break; }
                    }
                    var end = DateTime.Now;
                    var speed = end - start;
                    Console.WriteLine();
                    
                    Console.WriteLine("Complete! Total duration = " + speed);
                }
            }
            else
            {
                Console.WriteLine("File is not exist");
            }
        }
        private static void Get5Words(IEnumerable<string> textAsArray, string word)
        {

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

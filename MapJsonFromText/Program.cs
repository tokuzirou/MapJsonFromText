using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace MapJsonFromText
{
    class Program
    {
        static void Main(string[] args)
        {
            //テキストファイルを読み込む
            string filePath = @"C:\Users\user\json\quiz.txt";
            string dataText = "";
            try
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    dataText = streamReader.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("テキストファイルが見つかりませんでした。");
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
                Environment.Exit(0);
            }
            //テキストファイルの中身をリストに格納(空白とnull要素は削除)
            string[] dataSet = dataText.Split(',');
            List<string> dataSetList = dataSet.ToList<string>();
            dataSetList.RemoveAll(data => string.IsNullOrEmpty(data));
            //DataクラスのWordプロパティにそれぞれ代入
            Data dataClasses = new Data(dataSetList.Count);
            dataSetList.ForEach(data => dataClasses.Word.Add(data));
            //シリアライズして、json文字列を取得
            string json = JsonSerializer.Serialize(dataClasses, new JsonSerializerOptions { WriteIndented = true });
            //json文字列をJSONファイルを書き込む
            string jsonFilePath = @"C:\Users\user\json\quiz.json";
            try
            {
                using(StreamWriter streamWriter = new StreamWriter(jsonFilePath))
                {
                    streamWriter.WriteLine(json);
                }
            }catch(Exception ex)
            {
                Console.WriteLine("JSONファイル作成に失敗しました。");
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("作業終了します。");
            //入力待ち
            Console.ReadLine();
        }
    }

    public class Data
    {
        public List<string> Word { get; set; }

        internal Data(int count) => this.Word = new List<string>(count);
    }
}

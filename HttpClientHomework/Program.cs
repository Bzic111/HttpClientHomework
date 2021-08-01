using System;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HttpClientHomework
{
    class Program
    {
        static async Task Main()
        {
            string uri = "https://jsonplaceholder.typicode.com/posts/";
            string path = @"temp.txt";
            int postId = 4;
            List<string> ResponseList = new List<string>();

            for (int i = 0; i < 10; i++) ResponseList.Add(SafeHttpResponceMessage($"{uri}{postId + i}").Result);
            foreach (var item in ResponseList) Console.WriteLine(item);
            await WriteTxtFile(path, ResponseList);

        }
        /// <summary>
        /// Создаёт Http Client и отправляет Get запрос
        /// </summary>
        /// <param name="uri">URI вызова</param>
        /// <returns>string</returns>
        static async Task<string> HttpResponseMessage(string uri)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(uri);
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        /// <summary>
        /// Метод безопасного запроса HttpResponseMessage
        /// </summary>
        /// <param name="uri">URI вызова</param>
        /// <returns>Task<string></returns>
        static async Task<string> SafeHttpResponceMessage(string uri)
        {
            try
            {
                return await HttpResponseMessage(uri);
            }
            catch (AggregateException e)
            {
                return $"Response fail:\n {e.Message}";
            }
        }
        /// <summary>
        /// Записывает коллекцию строк в файл
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <param name="input">коллекция строк</param>
        /// <returns></returns>
        static async Task WriteTxtFile(string path, List<string> input)
        {
            await File.AppendAllLinesAsync(path, input);
        }
    }
}

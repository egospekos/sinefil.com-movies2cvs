using System;
using HtmlAgilityPack;
using System.Net;
using System.Text;


namespace MissionSaveTheCinema
{
    class Program
    {

        static void Main(string[] args)
        {
            string html;
            Uri url;
            //Mission 01 Düzgün csv format çıktı

            Console.Write("sinefil.com Kullanıcı adı: ");
            string usurname = Console.ReadLine();
            Console.WriteLine();

            // https://www.sinefil.com/user/bozradagast/izledikleri/2 


            int sayfa_sayisi;
            url = new Uri($"https://www.sinefil.com/user/{usurname}/izledikleri");
            WebClient client = new WebClient();

            client.Encoding = Encoding.UTF8;
            html = client.DownloadString(url);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            sayfa_sayisi = Convert.ToInt32(doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[1]/div[4]/div/div/div/div[2]/div/div[2]/div/div[3]/ul/li[5]/a").InnerText);
            Console.WriteLine($"Ulaşılan sayfa sayısı {sayfa_sayisi}");
            string temp_title;
            string temp_date;
            int movie_count = 0;
            for (int x = 0; x < sayfa_sayisi; x++)
            {
                Console.WriteLine($"Sayfa {x + 1}/{sayfa_sayisi}");
                url = new Uri($"https://www.sinefil.com/user/{usurname}/izledikleri/{x + 1}");
                WebClient client2 = new WebClient();

                client2.Encoding = Encoding.UTF8;
                html = client2.DownloadString(url);
                HtmlAgilityPack.HtmlDocument doc2 = new HtmlAgilityPack.HtmlDocument();
                doc2.LoadHtml(html);
                for (int i = 0; i < 16; i++)
                {

                    //title için
                    try
                    {
                        temp_title = doc2.DocumentNode.SelectSingleNode(
                   $"html/body/div[1]/div[1]/div[4]/div/div/div/div[2]/div/div[2]/div/div[2]/div[{i + 1}]/div/div[2]/div[1]").InnerText;
                    }
                    catch (System.NullReferenceException)
                    {

                        break;
                    }
                    movie_count++;

                    //System.NullReferenceException
                    temp_title = temp_title.Trim();
                    if (temp_title == "&nbsp;")
                    {
                        temp_title = doc2.DocumentNode.SelectSingleNode(
                   $"html/body/div[1]/div[1]/div[4]/div/div/div/div[2]/div/div[2]/div/div[2]/div[{i + 1}]/div/div[2]/h3/a").InnerText;
                    }

                    Console.Write(temp_title + ",");
                    //System.IO.File.WriteAllText(@"C:\Users\user\Desktop\ratings.txt",temp_title);
                    System.IO.File.AppendAllText(@$"C:\Users\user\Desktop\{usurname}_movies.txt", temp_title + " ");
                    // date

                    temp_date = doc2.DocumentNode.SelectSingleNode(
                   $"html/body/div[1]/div[1]/div[4]/div/div/div/div[2]/div/div[2]/div/div[2]/div[{i + 1}]/div/div[2]/h3/span").InnerText;
                    temp_date = temp_date.Trim();
                    //burayı açcazzzzzz
                    //Console.WriteLine(temp_date);

                    System.IO.File.AppendAllText(@$"C:\Users\user\Desktop\{usurname}_movies.txt", temp_date + "\n");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"\n\n Başarıyla tamamlandı. {movie_count} tane film yazıldı.\n Çıkmak için herhangi bir tuşa basınız...");
            Console.ReadLine();







        }
    }
}

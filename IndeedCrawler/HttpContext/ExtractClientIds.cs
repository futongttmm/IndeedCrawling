using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndeedCrawler.HttpContext
{
    public class ExtractClientIds
    {
        public static string getClientIds(HtmlDocument htmlSnippet, string xpath)
        {
            List<string> hrefTags = new List<string>();
            try
            {
                //get clinet id by xpath
                //string futongpath = "//*[@id=\"content\"]/div/div[2]/div/div[2]/div[2]/div[2]/div/div[1]/span[1]";
                HtmlNodeCollection nodesCol = htmlSnippet.DocumentNode.SelectNodes("//*[@class='rezemp-ResumeSearchCard-contents']/div/span[1]"); 

                if (nodesCol == null)
                    return null;
                else
                {
                    IEnumerable<HtmlNode> nodes = nodesCol.ToArray();

                    StringBuilder returnClientId = new StringBuilder();

                    foreach (HtmlNode node in nodes)
                    {
                        HtmlAttribute att = node.Attributes["id"];
                        returnClientId.Append(att.Value + ";");
                    }

                    return returnClientId.Remove(returnClientId.Length - 1, 1).ToString();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Picture_Gallery
{
    public partial class _Default : Page
    {
        string url = "http://www.africankabob.com/PictureGallery/";
        string partialURL;

        protected string RootURL
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
            }
        }

        protected string PartialURL
        {
            get
            {
                return partialURL;
            }
        }

        protected string FullURL
        {
            get
            {
                return RootURL + partialURL + "/Camera%20JPEGs/Thumbs/";
            }
        }

        protected string ZipURL
        {
            get
            {
                return RootURL + partialURL + "/" + partialURL + ".zip";
            }
        }

        public static string GetDirectoryListingRegexForUrl(string url)
        {
            return "<a href=\"(?<name>[^\\?][^\\\\].*)\">";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Galleries -";
            List<string> dirList = new List<string>();


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RootURL);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string html = reader.ReadToEnd();

                    Regex regex = new Regex(GetDirectoryListingRegexForUrl(RootURL), RegexOptions.IgnoreCase);
                    MatchCollection matches = regex.Matches(html);
                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                        {
                            if (match.Success)
                            {
                                if (!match.Groups["name"].ToString().Contains("Parent Directory") && !match.Groups["name"].ToString().Contains("Thumbs") && 
                                    !match.Groups["name"].ToString().Contains("!"))
                                    dirList.Add(HttpUtility.UrlDecode(match.Groups["name"].ToString().Replace("/", "")));
                            }
                        }
                    }
                }
            }

            lstGalleries.DataSource = dirList;
            lstGalleries.DataBind();

            if (!String.IsNullOrEmpty(Convert.ToString(Request.QueryString["galleryName"])))
            {
                partialURL = Request.QueryString["galleryName"];
                BuildPicList();
            }
        }

        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect(((ImageButton)sender).ImageUrl.Replace("Thumbs/", ""));
        }

        protected void BuildPicList()
        {
            lnkZip.Visible = true;
            lnkZip.Text = "Click to Download ZIP of ALL Pictures - " + PartialURL + ".zip";
            lnkZip.CommandArgument = ZipURL;

            this.Page.Title = PartialURL + " -";
            List<string> fileList = new List<string>();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(FullURL);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string html = reader.ReadToEnd();
                        Regex regex = new Regex(GetDirectoryListingRegexForUrl(FullURL));
                        MatchCollection matches = regex.Matches(html);
                        if (matches.Count > 0)
                        {
                            foreach (Match match in matches)
                            {
                                if (match.Success)
                                {
                                    if (match.Groups["name"].ToString().Contains(".") && !match.Groups["name"].ToString().Contains("Thumbs"))
                                        fileList.Add(match.Groups["name"].ToString());
                                }
                            }
                        }
                    }
                }

                lnkZip.Visible = true;

                lstPics.DataSource = fileList;
                lstPics.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkGallery_Click(object sender, EventArgs e)
        {
            partialURL = ((LinkButton)sender).Text;
        }

        protected void lstPics_DataBinding(object sender, EventArgs e)
        {

        }

        protected void lstPics_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            ((ImageButton)(e.Item.FindControl("ImageButton1"))).CommandArgument = ((ImageButton)(e.Item.FindControl("ImageButton1"))).ImageUrl;
        }

        protected void lstPics_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void lstGalleries_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            ((LinkButton)e.Item.FindControl("lnkGallery")).CommandArgument = ((LinkButton)e.Item.FindControl("lnkGallery")).Text;
            ((LinkButton)e.Item.FindControl("lnkGallery")).PostBackUrl = "~/Default.aspx?galleryName=" + ((LinkButton)e.Item.FindControl("lnkGallery")).Text;
        }

        protected void lstGalleries_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void lnkZip_Click(object sender, EventArgs e)
        {
            Response.Redirect(((LinkButton)sender).CommandArgument);
        }

        protected void ImageButton1_Command(object sender, CommandEventArgs e)
        {

        }
    }
}
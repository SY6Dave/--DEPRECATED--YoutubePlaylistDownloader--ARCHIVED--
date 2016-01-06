using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using YoutubeExtractor;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.IO;
using System.Windows.Forms;

namespace YoutubePlaylistDownloader
{
    public class Downloadable
    {
        private Object toLock;
        public Object ToLock
        {
            get
            {
                return toLock;
            }
            set
            {
                toLock = value;
            }
        }
        private bool pending;
        public bool Pending
        {
            get
            {
                return pending;
            }
            set
            {
                pending = value;
            }
        }
        private bool completed;
        public bool Completed
        {
            get
            {
                return completed;
            }
            set
            {
                completed = value;
            }
        }
        private bool preparing;
        public bool Preparing
        {
            get
            {
                return preparing;
            }
            set
            {
                preparing = value;
            }
        }
        private string url;
        public string Url
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
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        private string displayName;
        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
            }
        }
        private int index;
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }
        private string id;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        private double percentCompletion;
        public double PercentCompletion
        {
            get
            {
                return percentCompletion;
            }
            set
            {
                percentCompletion = value;
            }
        }

        public Downloadable(string inURL, string inName, int inIndex, string inId)
        {
            this.pending = true;
            this.completed = false;
            this.url = inURL;
            this.toLock = new Object();
            this.name = inName;
            this.index = inIndex;
            this.id = inId;
            this.percentCompletion = 0;
            this.preparing = false;

            if (inName.Length > 42)
            {
                char[] newName = new char[45];
                for (int i = 0; i < 42; i = i + 1)
                {
                    newName[i] = inName[i];
                }
                for (int i = 42; i < 45; i = i + 1)
                {
                    newName[i] = '.';
                }
                displayName = new string(newName);
            }
            else
            {
                displayName = inName;
            }
        }

        public void SetPercent(double percentValue)
        {
            this.PercentCompletion = percentValue;
        }
    }

    public class PlaylistDownloader
    {
        public string savePath = "";
        public bool incremental = true;
        public int maxThreads = 1;
        public int maxVids = 100;
        public double globalPercentage = 0;
        object lockGlobalPerc = new object();
        int vidCount = 0;
        int threadFinishedCount = 0;
        string emptyPath = "https://www.youtube.com/watch?v=";
        string apikey = "AIzaSyDIqkGknC_2L1nZNraZItSeVSSL_liWbvg";
        int padLeft = 0;
        public List<Downloadable> videos;
        object threadLocker = new object();
        string region = System.Globalization.RegionInfo.CurrentRegion.TwoLetterISORegionName;

        public void StartThreads(List<Downloadable> videos)
        {
            for (int i = 0; i < maxThreads; i = i + 1)
            {
                Thread newThread = new Thread(this.DownloadAll);
                newThread.Name = (i + 1).ToString();
                newThread.Start(videos);
            }
        }

        public string GetPlaylistIDFromURL(string inUrl)
        {
            string playlistID = "";

            if (!inUrl.Contains("list="))
            {
                return null;
            }

            string[] splitUrl = inUrl.Split(new[] { "list=" }, StringSplitOptions.None);

            try
            {
                playlistID = splitUrl[1];
                if (playlistID.Contains('&'))
                {
                    char[] playlisttochar = playlistID.ToCharArray();
                    playlistID = "";
                    foreach (char c in playlisttochar)
                    {
                        if (c == '&')
                        {
                            break;
                        }

                        playlistID += c;
                    }
                }
            }
            catch
            {
                return null;
            }

            return playlistID;
        }

        public List<Downloadable> GetVideosByPlaylist(string playlistID)
        {
            List<Downloadable> videos = new List<Downloadable>();

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apikey,
                ApplicationName = "VideoGrabber"
            });

            var nextPageToken = "";
            vidCount = 0;
            while (nextPageToken != null)
            {
                var playlistRequest = youtubeService.PlaylistItems.List("snippet,contentDetails");

                playlistRequest.PlaylistId = playlistID;
                playlistRequest.MaxResults = 50;
                playlistRequest.PageToken = nextPageToken;

                try
                {
                    var playlistResponse = playlistRequest.Execute();
                    int indexcount = 0;
                    foreach (var playlistItem in playlistResponse.Items)
                    {
                        if (playlistItem.Snippet.Description == "This video is unavailable." && playlistItem.Snippet.Title == "Deleted video")
                        {
                            continue;
                        }

                        Downloadable newVideo = new Downloadable(emptyPath + playlistItem.ContentDetails.VideoId, playlistItem.Snippet.Title, indexcount, playlistItem.ContentDetails.VideoId);
                        videos.Add(newVideo);
                        indexcount++;
                        vidCount++;

                        if (vidCount >= maxVids) break;
                    }

                    if (vidCount >= maxVids) break;
                    nextPageToken = playlistResponse.NextPageToken;
                }
                catch
                {
                    //a wrong playlist id got entered
                    return null;
                }
            }

            if (videos.Count < 100)
            {
                padLeft = 2;
            }
            else if (videos.Count < 999)
            {
                padLeft = 3;
            }
            else
            {
                padLeft = 4;
            }

            return videos;
        }

        public List<Downloadable> GetVideosByQuery(string searchQuery)
        {
            List<Downloadable> videos = new List<Downloadable>();

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apikey,
                ApplicationName = "VideoGrabber"
            });

            var nextPageToken = "";
            vidCount = 0;
            while (nextPageToken != null)
            {
                var searchRequest = youtubeService.Search.List("snippet");
                searchRequest.Q = searchQuery;
                searchRequest.MaxResults = 50;

                try
                {
                    var searchResponse = searchRequest.Execute();
                    int indexcount = 0;
                    foreach (var searchItem in searchResponse.Items)
                    {
                        if (searchItem.Snippet.Description == "This video is unavailable." && searchItem.Snippet.Title == "Deleted video")
                        {
                            continue;
                        }
                        if (searchItem.Id.VideoId != null)
                        {
                            Downloadable newVideo = new Downloadable(emptyPath + searchItem.Id.VideoId, searchItem.Snippet.Title, indexcount, searchItem.Id.VideoId);
                            videos.Add(newVideo);
                            indexcount++;
                            vidCount++;

                            if (vidCount >= maxVids) break;
                        }
                    }

                    if (vidCount >= maxVids) break;
                    nextPageToken = searchResponse.NextPageToken;
                }
                catch
                {
                    //something went wrong
                    return null;
                }
            }

            if (videos.Count < 100)
            {
                padLeft = 2;
            }
            else if (videos.Count < 999)
            {
                padLeft = 3;
            }
            else
            {
                padLeft = 4;
            }

            return videos;
        }

        public string GetPlaylistIDFromChannel(string channelName)
        {

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apikey,
                ApplicationName = "VideoGrabber"
            });

            var channelRequest = youtubeService.Channels.List("contentDetails");
            channelRequest.ForUsername = channelName;
            channelRequest.MaxResults = 1;

            try
            {
                var channelResponse = channelRequest.Execute();
                var channel = channelResponse.Items[0];
                string uploadsPlaylistID = channel.ContentDetails.RelatedPlaylists.Uploads;
                return uploadsPlaylistID;
            }
            catch
            {
                return null;
            }
        }

        void DownloadAll(object inVideo)
        {
            videos = inVideo as List<Downloadable>;
            for (int i = 0; i < videos.Count; i = i + 1)
            {
                var currentVid = videos[i];
                try
                {
                    var locked = !Monitor.TryEnter(currentVid.ToLock, 100);
                    if (locked)
                    {
                        continue;
                    }

                    lock (currentVid.ToLock)
                    {
                        if (!currentVid.Completed)
                        {
                            if (currentVid.Pending)
                            {
                                currentVid.Pending = false;

                                for (int n = 0; n < 10; n = n + 1)
                                {
                                    try
                                    {
                                        DownloadAudioFrom(currentVid, i + 1);
                                        break;
                                    }
                                    catch
                                    {
                                        var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                                        {
                                            ApiKey = apikey,
                                            ApplicationName = "VideoGrabber"
                                        });

                                        var videoRequest = youtubeService.Videos.List("contentDetails");
                                        videoRequest.MaxResults = 1;
                                        videoRequest.Id = currentVid.Id;
                                        var videoResponse = videoRequest.Execute();
                                        var vid = videoResponse.Items[0];

                                        try
                                        {
                                            if (vid.ContentDetails.RegionRestriction.Blocked.Contains(region))
                                            {
                                                lock (threadLocker)
                                                {
                                                    currentVid.SetPercent(-1);
                                                    Program.frm.errors++;
                                                    Program.frm.remaining--;
                                                }
                                                break;
                                            }
                                        }
                                        catch
                                        {
                                            if (n == 9)
                                            {
                                                lock (threadLocker)
                                                {
                                                    currentVid.SetPercent(-1);
                                                    Program.frm.errors++;
                                                    Program.frm.remaining--;
                                                }
                                                break;
                                            }
                                            continue;
                                        }
                                    }
                                }
                                currentVid.Completed = true;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    continue;
                }
            }

            lock (threadLocker)
            {
                threadFinishedCount++;
                if (threadFinishedCount >= maxThreads)
                {
                    Program.frm.threadsComplete();
                    threadFinishedCount = 0;
                }
            }
        }

        void DownloadAudioFrom(Downloadable InVideo, int count)
        {
            InVideo.Preparing = true;
            ListDelegate ld = new ListDelegate(Program.frm.UpdateList);
            Program.frm.Invoke(ld);

            string link = InVideo.Url;
            string countPrefix = count.ToString();
            countPrefix = countPrefix.PadLeft(padLeft, '0');

            VideoInfo video = DownloadUrlResolver.GetDownloadUrls(link);

            if (video.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(video);
            }

            char[] legalTitleArray = video.Title.ToCharArray();
            string legalTitle = "";

            foreach (char c in legalTitleArray)
            {
                if (!(c == '/' || c == '\\' || c == ':' || c == '*' || c == '?' || c == '"' || c == '<' || c == '>' || c == '|'))
                {
                    legalTitle += c;
                }
            }

            if (incremental)
            {
                var audioDownloader = new AudioDownloader(video, savePath + @"\" + countPrefix + " " + legalTitle + video.AudioExtension);
                audioDownloader.DownloadProgressChanged += (sender, _args) => DownloadProgress(_args.ProgressPercentage, InVideo);
                audioDownloader.AudioExtractionProgressChanged += (sender, _args) => ExtractionProgress(_args.ProgressPercentage, InVideo);
                audioDownloader.Execute();
            }
            else
            {
                var audioDownloader = new AudioDownloader(video, savePath + @"\" + legalTitle + video.AudioExtension);
                audioDownloader.DownloadProgressChanged += (sender, _args) => DownloadProgress(_args.ProgressPercentage, InVideo);
                audioDownloader.AudioExtractionProgressChanged += (sender, _args) => ExtractionProgress(_args.ProgressPercentage, InVideo);
                audioDownloader.Execute();
            }

            lock (threadLocker)
            {
                Program.frm.remaining--;
            }
        }

        delegate void ListDelegate();
        object listlock = new object();

        void DownloadProgress(double ProgressPercentage, Downloadable d)
        {
            d.Preparing = false;
            string title = d.DisplayName;
            double percentcalc = Math.Round(ProgressPercentage * 0.85, 2);
            if (percentcalc > 99.9) percentcalc = 100;

            lock (listlock)
            {
                videos[d.Index].SetPercent(percentcalc);
                globalPercentage = 0;
                for (int i = 0; i < videos.Count; i = i + 1)
                {
                    if (videos[i].PercentCompletion == -1)
                    {
                        globalPercentage += 100;
                    }
                    else
                    {
                        globalPercentage += videos[i].PercentCompletion;
                    }
                }
                globalPercentage /= (videos.Count * 100);
                globalPercentage *= 100;
            }
        }

        void ExtractionProgress(double ProgressPercentage, Downloadable d)
        {
            d.Preparing = false;
            string title = d.DisplayName;
            double percentcalc = Math.Round(85 + ProgressPercentage * 0.15, 2);
            if (percentcalc > 99.9) percentcalc = 100;

            lock (listlock)
            {
                videos[d.Index].SetPercent(percentcalc);
                globalPercentage = 0;
                for (int i = 0; i < videos.Count; i = i + 1)
                {
                    if (videos[i].PercentCompletion == -1)
                    {
                        globalPercentage += 100;
                    }
                    else
                    {
                        globalPercentage += videos[i].PercentCompletion;
                    }
                }
                globalPercentage /= (videos.Count * 100);
                globalPercentage *= 100;
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace PC3
{
    class BitMexAPI
    {
        private string domain = "https://testnet.bitmex.com";
        private string apiKey;
        private string apiSecret;
        private int rateLimit;

        public BitMexAPI(string bitmexKey = "", string bitmexSecret = "", string bitmexDomain = "", int rateLimit = 5000)
        {
            this.apiKey = bitmexKey;
            this.apiSecret = bitmexSecret;
            this.rateLimit = rateLimit;
            this.domain = bitmexDomain;
        }
        #region BitMex API
        private string BuildQueryData(Dictionary<string, string> param)
        {
            if (param == null)
                return "";

            StringBuilder b = new StringBuilder();
            foreach (var item in param)
                b.Append(string.Format("&{0}={1}", item.Key, WebUtility.UrlEncode(item.Value)));

            try { return b.ToString().Substring(1); }
            catch (Exception) { return ""; }
        }

        private string BuildJSON(Dictionary<string, string> param)
        {
            if (param == null)
                return "";

            var entries = new List<string>();
            foreach (var item in param)
                entries.Add(string.Format("\"{0}\":\"{1}\"", item.Key, item.Value));

            return "{" + string.Join(",", entries) + "}";
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private long GetNonce()
        {
            DateTime yearBegin = new DateTime(1990, 1, 1);
            return DateTime.UtcNow.Ticks - yearBegin.Ticks;
        }

        private string Query(string method, string function, Dictionary<string, string> param = null, bool auth = false, bool json = false)
        {
            string paramData = json ? BuildJSON(param) : BuildQueryData(param);
            string url = "/api/v1" + function + ((method == "GET" && paramData != "") ? "?" + paramData : "");
            string postData = (method != "GET") ? paramData : "";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(domain + url);
            webRequest.Method = method;

            if (auth)
            {
                string nonce = GetNonce().ToString();
                string message = method + url + nonce + postData;
                byte[] signatureBytes = hmacsha256(Encoding.UTF8.GetBytes(apiSecret), Encoding.UTF8.GetBytes(message));
                string signatureString = ByteArrayToString(signatureBytes);

                webRequest.Headers.Add("api-nonce", nonce);
                webRequest.Headers.Add("api-key", apiKey);
                webRequest.Headers.Add("api-signature", signatureString);
            }

            try
            {
                if (postData != "")
                {
                    webRequest.ContentType = json ? "application/json" : "application/x-www-form-urlencoded";
                    var data = Encoding.UTF8.GetBytes(postData);
                    using (var stream = webRequest.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }

                using (WebResponse webResponse = webRequest.GetResponse())
                using (Stream str = webResponse.GetResponseStream())
                using (StreamReader sr = new StreamReader(str))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (WebException wex)
            {
                using (HttpWebResponse response = (HttpWebResponse)wex.Response)
                {
                    if (response == null)
                        throw;

                    using (Stream str = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(str))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        private byte[] hmacsha256(byte[] keyByte, byte[] messageBytes)
        {
            using (var hash = new HMACSHA256(keyByte))
            {
                return hash.ComputeHash(messageBytes);
            }
        }

        #region RateLimiter

        private long lastTicks = 0;
        private object thisLock = new object();

        private void RateLimit()
        {
            lock (thisLock)
            {
                long elapsedTicks = DateTime.Now.Ticks - lastTicks;
                var timespan = new TimeSpan(elapsedTicks);
                if (timespan.TotalMilliseconds < rateLimit)
                    Thread.Sleep(rateLimit - (int)timespan.TotalMilliseconds);
                lastTicks = DateTime.Now.Ticks;
            }
        }

        #endregion RateLimiter
        #endregion

        #region Examples
        //public List<OrderBookItem> GetOrderBook(string symbol, int depth)
        //{
        //    var param = new Dictionary<string, string>();
        //    param["symbol"] = symbol;
        //    param["depth"] = depth.ToString();
        //    string res = Query("GET", "/orderBook", param);
        //    return JsonSerializer.DeserializeFromString<List<OrderBookItem>>(res);
        //}

        /*public string GetOrders(string Symbol)
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = Symbol;
            //param["filter"] = "{\"open\":true}";
            //param["columns"] = "";
            //param["count"] = 100.ToString();
            //param["start"] = 0.ToString();
            //param["reverse"] = false.ToString();
            //param["startTime"] = "";
            //param["endTime"] = "";
            return Query("GET", "/order", param, true);
        }*/

        public string PostOrders()
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = "XBTUSD";
            param["side"] = "Buy";
            param["orderQty"] = "1";
            param["ordType"] = "Market";
            return Query("POST", "/order", param, true);
        }

        public string DeleteOrders()
        {
            var param = new Dictionary<string, string>();
            param["orderID"] = "de709f12-2f24-9a36-b047-ab0ff090f0bb";
            param["text"] = "cancel order by ID";
            return Query("DELETE", "/order", param, true, true);
        }
        #endregion


        #region Our Calls
        public List<OrderBook> GetOrderBook(string symbol, int depth)
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = symbol;
            param["depth"] = depth.ToString();
            string res = Query("GET", "/orderBook/L2", param);
            return JsonConvert.DeserializeObject<List<OrderBook>>(res);
        }

        public string PostOrdersClosePos(string Symbol, string Side, int Price)
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = Symbol;
            param["side"] = Side;
            param["ordType"] = "Limit";
            param["execInst"] = "Close";
            param["price"] = Price.ToString();
            return Query("POST", "/order", param, true);
        }

        public string PostLimitOrders(string Symbol, string Side, string Price, int Quantity)
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = Symbol;
            param["side"] = Side;
            param["ordType"] = "Limit";
            param["orderQty"] = Quantity.ToString();
            //param["execInst"] = "FillOrKill";
            param["price"] = Price;
            return Query("POST", "/order", param, true);
        }

        public string CancelAllOpenOrders(string Symbol, string Note = "")
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = Symbol;
            param["text"] = Note;
            return Query("DELETE", "/order/all", param, true, true);
        }

        public string MarketOrders(string Symbol, string Side, int Quantity, string clOrdID)
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = Symbol;
            param["side"] = Side;
            param["orderQty"] = Quantity.ToString();
            param["ordType"] = "Market";
            param["clOrdID"] = clOrdID;
            return Query("POST", "/order", param, true);
        }

        public List<Instrument> GetActiveInstruments()
        {
            string res = Query("GET", "/instrument/active");
            return JsonConvert.DeserializeObject<List<Instrument>>(res);
        }

        public List<Instrument> GetInstrument(string Symbol)
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = Symbol;
            string res = Query("GET", "/instrument");
            return JsonConvert.DeserializeObject<List<Instrument>>(res);
        }

        public Wallet WalletAmmount()
        {
            var param = new Dictionary<string, string>();
            param["currency"] = "XBt";
            string res = Query("GET", "/user/wallet", param, true);
            return JsonConvert.DeserializeObject<Wallet>(res);
        }

        public List<Candles> GetCandleHistory(string Symbol, string Size)
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = Symbol;
            param["count"] = 500.ToString();
            param["reverse"] = true.ToString();
            param["partial"] = true.ToString();
            param["binSize"] = Size;
            string res = Query("GET", "/trade/bucketed", param);
            return JsonConvert.DeserializeObject<List<Candles>>(res).OrderByDescending(a => a.Timestamp).ToList();
        }

        public List<Positions> GetOpenPositions(string Symbol)
        {
            var param = new Dictionary<string, string>();
            string res = Query("GET", "/position", param, true);
            return
                JsonConvert.DeserializeObject<List<Positions>>(res)
                    .Where(a => a.Symbol == Symbol && a.IsOpen == true)
                    .OrderByDescending(a => a.Timestamp)
                    .ToList();
        }

        public List<Order> GetOpenOrders(string Symbol)
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = Symbol;
            param["reverse"] = true.ToString();
            string res = Query("GET", "/order", param, true);
            return
                JsonConvert.DeserializeObject<List<Order>>(res)
                    .Where(a => a.OrdStatus == "New" || a.OrdStatus == "PartiallyFilled")
                    .OrderByDescending(a => a.Timestamp)
                    .ToList();
        }

        public List<Order> GetOrders(string Symbol, string reff)
        {
            var param = new Dictionary<string, string>();
            param["symbol"] = Symbol;
            param["reverse"] = true.ToString();
            string res = Query("GET", "/order", param, true);
            return
                JsonConvert.DeserializeObject<List<Order>>(res)
                    .Where(a => a.ClOrdId.Contains(reff))
                    .OrderByDescending(a => a.Timestamp)
                    .ToList();
        }
        #endregion Our Calls
    } // public class BitMEXApi

    #region Working Classes
    public class OrderBookItem
    {
        public string Symbol { get; set; }
        public int Level { get; set; }
        public int BidSize { get; set; }
        public decimal BidPrice { get; set; }
        public int AskSize { get; set; }
        public decimal AskPrice { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class OrderBook
    {
        public string Side { get; set; }
        public double Price { get; set; }
        public int Size { get; set; }
    }

    public class Instrument
    {
        public string Symbol { get; set;}
        public double TickSize { get; set; }
    }

    public class Wallet
    {
        public double? Amount { get; set; }
    }

    public class Candles
    {
        public DateTime Timestamp { get; set; }
        public int PCC { get; set; }
        public double? UpCh { get; set; }
        public double? High { get; set; }
        public double? Close { get; set; }
        public double? Low { get; set; }
        public double? DownCh { get; set; }
        //public double? FairPrice { get; set; }
        public double? Up_averPC { get; set; }
        public double? Down_averPC { get; set; }
    }

    public class Positions
    {
        public DateTime Timestamp { get; set; }
        public double? Leverage { get; set; }
        public int? CurrentQty { get; set; }
        public double? CurrentCost { get; set; }
        public bool IsOpen { get; set; }
        public double? MarkPrice { get; set; }
        public double? MarkValue { get; set; }
        public double? UnrealisedPnl { get; set; }
        public double? UnrealisedPnlPcnt { get; set; }
        public double? AvgEntryPrice { get; set; }
        public double? BreakEvenPrice { get; set; }
        public double? LiquidationPrice { get; set; }
        public string Symbol { get; set; }
    }

    public class Order
    {
        public DateTime Timestamp { get; set; }
        public string Symbol { get; set; }
        public string OrdStatus { get; set; }
        public string OrdType { get; set; }
        public string OrderId { get; set; }
        public string Side { get; set; }
        public string ClOrdId { get; set; }
        public double? Price { get; set; }
        public int? OrderQty { get; set; }
        public int? DisplayQty { get; set; }
    }
    #endregion Working Classes
}

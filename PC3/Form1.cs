using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PC3
{
    public partial class Form1 : Form
    {
        #region Keys
        #region testnet
        private static string bitmexKey = "";
        private static string bitmexSecret = "";
        private static string bitmexDomain = "https://testnet.bitmex.com";
        #endregion testnet
        #endregion Keys

        BitMexAPI bitmex = new BitMexAPI(bitmexKey, bitmexSecret, bitmexDomain); 
        List<Instrument> ActiveInstruments = new List<Instrument>();
        Instrument ActiveInstrument = new Instrument();
        Wallet wallet = new Wallet();
        List<Candles> Candels = new List<Candles>();
        List<Positions> OpenPositions = new List<Positions>();
        List<Order>  Orders = new List<Order>();
        

       public Form1()
       {
            InitializeComponent();
            InitializeSymbolInformation();
            InitializeDropDowns();
       }

        private void InitializeDropDowns()
        {
            ddlStratagy.SelectedIndex = 0;
            ddlTF.SelectedIndex = 0;
        }

        private void InitializeSymbolInformation()
        {
            ActiveInstruments = bitmex.GetActiveInstruments().OrderBy(a => a.Symbol).ToList();
            ddlSymbols.DataSource = ActiveInstruments;
            ddlSymbols.DisplayMember = "Symbol";
            ddlSymbols.SelectedIndex = 0;
            ActiveInstrument = ActiveInstruments[0];
        }

        private string refference()
        {

            string reff = ddlStratagy.SelectedItem.ToString() + ActiveInstrument.Symbol + ddlTF.SelectedItem.ToString(); 

            MessageBox.Show(reff);

            return reff;
        }

        private void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveInstrument = ((Instrument)ddlSymbols.SelectedItem);
            //UpdateCandeles();
        }

        private void ddlStratagy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //refference();
            //MessageBox.Show(refference());
        }

        private void ddlTF_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UpdateCandeles();
        }

        private void UpdateCandeles()
        {
            int AverPricePeriod = Convert.ToInt32(nudAverPricePeriod.Value);
            int PriceChannel = Convert.ToInt32(nudPriceChannel.Value);
            int FirstValidValue = Math.Max(AverPricePeriod, PriceChannel)-1;

            #region Индикаторы

            Candels = bitmex.GetCandleHistory(ActiveInstrument.Symbol, ddlTF.SelectedItem.ToString());

            foreach (Candles c in Candels) // AverPricePeriod
            {
                c.PCC = Candels.Where(a => a.Timestamp < c.Timestamp).Count();

                if (c.PCC > FirstValidValue)
                {
                    c.Up_averPC =
                        Candels.Where(a => a.PCC <= c.PCC-1)
                            .OrderByDescending(a => a.Timestamp)
                            .Take(AverPricePeriod)
                            .Average(a => a.High);

                    c.Down_averPC =
                        Candels.Where(a => a.PCC <= c.PCC-1)
                            .OrderByDescending(a => a.Timestamp)
                            .Take(AverPricePeriod)
                            .Average(a => a.Low);
                }
            }

            foreach (Candles c in Candels)
            {
                if (c.PCC > FirstValidValue)
                {
                    c.UpCh =
                        Candels.Where(a => a.Timestamp <= c.Timestamp)
                            .OrderByDescending(a => a.Timestamp)
                            .Take(PriceChannel)
                            .Max(a => a.Up_averPC);

                    c.DownCh =
                        Candels.Where(a => a.Timestamp <= c.Timestamp)
                            .OrderByDescending(a => a.Timestamp)
                            .Take(PriceChannel)
                            .Min(a => a.Down_averPC);
                }
            }
            #endregion Индикаторы

            dvgCandles.DataSource = Candels;
        }

        private void Trade()
        {
            
            //OpenPositions = bitmex.GetOpenPositions(ActiveInstrument.Symbol);
            int EntryPriceInt = Convert.ToInt32((Candels[0].High + Candels[0].Low + Candels[0].Close * 2) / 4);
            double? EntryPriceDec = ((Candels[0].High + Candels[0].Low + Candels[0].Close * 2) / 4);
            double PrctEquityForSystem = Convert.ToDouble(nudPrctEqForSystem.Value);
            wallet = bitmex.WalletAmmount();
            var summForSystem = wallet.Amount * (PrctEquityForSystem / 100);
            double MpR = Convert.ToDouble(nudMpR.Value);
            int MaxContact = Convert.ToInt32(nudMaxContract.Value);
            
            string reff_time = ddlStratagy.SelectedItem + ActiveInstrument.Symbol + ddlTF.SelectedItem +
                          nudAverPricePeriod.Value + "_" + nudPriceChannel.Value + "_" + DateTime.UtcNow.DayOfYear +
                          DateTime.UtcNow.Hour + DateTime.UtcNow.Minute + DateTime.UtcNow.Second;
            
            string reff = ddlStratagy.SelectedItem + ActiveInstrument.Symbol + ddlTF.SelectedItem +
                          nudAverPricePeriod.Value + "_" + nudPriceChannel.Value;
            
            Orders = bitmex.GetOrders(ActiveInstrument.Symbol, reff);
            int sumPos = (int) Orders.Where(a => a.OrdStatus == "Filled").Sum(a => a.OrderQty);

            //var qwe = Convert.ToInt32(Math.Floor((double)((summForSystem * MpR / 100) / ((Candels[0].UpCh - Candels[0].DownCh) * 100000000))));
            var qwe = (Candels[0].UpCh - Candels[0].DownCh)*100000000;
            txbKontracts.Text = qwe.ToString();

            if (sumPos !=0)
            //if (OpenPositions.Any())
            {
                if (sumPos > 0)
                //if (OpenPositions[0].CurrentQty > 0) // isLong
                {
                    if (Candels[0].Low <= Candels[0].DownCh)
                    {
                        bitmex.MarketOrders(ActiveInstrument.Symbol, "Sell", sumPos, reff_time);
                    }
                }
                else if (sumPos < 0)
                {
                    //if (OpenPositions[0].CurrentQty < 0) //IsShort
                    if (Candels[0].High >= Candels[0].UpCh)
                    {
                            bitmex.MarketOrders(ActiveInstrument.Symbol, "Buy", sumPos, reff_time);
                    }
                }
            }
            else
            {
                if (Candels[0].High > Candels[0].UpCh)
                {
                    int KontractBuy = Convert.ToInt32(Math.Floor((double) ((summForSystem * MpR / 100) / (EntryPriceDec - Candels[0].DownCh))));
                    KontractBuy = Math.Min(KontractBuy, MaxContact);

                    

                    if (KontractBuy > 0)
                    {
                        bitmex.PostLimitOrders(ActiveInstrument.Symbol, "Buy", EntryPriceInt.ToString(), KontractBuy);
                    }
                    else
                    {
                        bitmex.PostLimitOrders(ActiveInstrument.Symbol, "Buy", EntryPriceInt.ToString(), 1);
                    }
                }
                if (Candels[0].Low < Candels[0].DownCh)
                {
                    int KontractShort = Convert.ToInt32(Math.Floor((double)((summForSystem * MpR / 100) / (Candels[0].UpCh - EntryPriceDec))));
                    KontractShort = Math.Min(KontractShort, MaxContact);

                    if (KontractShort > 0)
                    {
                        bitmex.PostLimitOrders(ActiveInstrument.Symbol, "Sell", EntryPriceInt.ToString(), KontractShort);
                    }
                    else
                    {
                        bitmex.PostLimitOrders(ActiveInstrument.Symbol, "Buy", EntryPriceInt.ToString(), 1);
                    }
                }

            }

            
            
            
            //MessageBox.Show(q.ToString()); 
            
           

        }

        private void lblPriceChennel_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateCandels_Click(object sender, EventArgs e)
        {
            UpdateCandeles();
            //Trade();
        }

        private void nudAverPricePeriod_ValueChanged(object sender, EventArgs e)
        {
            //UpdateCandeles();
        }

        private void nudPriceChannel_ValueChanged(object sender, EventArgs e)
        {
            //UpdateCandeles();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Start")
            {
                tmrAutoTrade.Start();
                btnStart.Text = "Stop";
                btnStart.BackColor = Color.Red;
            }
            else
            {
                tmrAutoTrade.Stop();
                btnStart.Text = "Start";
                btnStart.BackColor = Color.LightGreen;
            }
        }

        private void tmrAutoTrade_Tick(object sender, EventArgs e)
        {
            UpdateCandeles();
            Trade();
            /*try
            {
                UpdateCandeles();
                Trade();
            }
            catch (Exception)
            {
                tmrAutoTrade.Stop();
                btnStart.Text = "Start";
                btnStart.BackColor = Color.LightGreen;

                tmrAutoTrade.Start();
                btnStart.Text = "Stop";
                btnStart.BackColor = Color.Red;
                //throw;
            }*/

            /*int k = 0;
            try
            {
                k = +10;
                k = k/Convert.ToInt32(txbKontracts.Text);
                MessageBox.Show(k.ToString());
            }
            catch (Exception)
            {
                tmrAutoTrade.Stop();
                tmrAutoTrade.Start();
                MessageBox.Show("Restart");
                //throw;
                
            }*/

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*int EntryPriceInt = Convert.ToInt32((Candels[1].High + Candels[1].Low + Candels[1].Close * 2) / 4);
            string EntryPrice = EntryPriceInt.ToString() + ".0";
            double? EntryPriceDec = ((Candels[1].High + Candels[1].Low + Candels[1].Close * 2) / 4);
            double PrctEquityForSystem = Convert.ToDouble(nudPrctEqForSystem.Value);
            wallet = bitmex.WalletAmmount();
            var summForSystem = wallet.Amount * (PrctEquityForSystem / 100);
            double MpR = Convert.ToDouble(nudMpR.Value);

            string reff_time = ddlStratagy.SelectedItem + ActiveInstrument.Symbol + ddlTF.SelectedItem +
                          nudAverPricePeriod.Value + "_" + nudPriceChannel.Value + "_" + DateTime.UtcNow.DayOfYear +
                          DateTime.UtcNow.Hour + DateTime.UtcNow.Minute + DateTime.UtcNow.Second;
            string reff = ddlStratagy.SelectedItem + ActiveInstrument.Symbol + ddlTF.SelectedItem +
                          nudAverPricePeriod.Value + "_" + nudPriceChannel.Value;


            bool reff_b = reff_time.Contains(reff);

            Orders = bitmex.GetOrders(ActiveInstrument.Symbol, reff);

            int sum = (int) Orders.Where(a => a.OrdStatus == "Filled").Sum(a => a.OrderQty);
            if (Orders[0].ClOrdId.Contains(reff))
            {
                MessageBox.Show(Orders[0].ClOrdId);
            }
            else
            {
                MessageBox.Show("qwe");
            }

            int KontractBuy = Convert.ToInt32(Math.Floor((double)((summForSystem * MpR / 100) / (EntryPriceDec - Candels[0].DownCh))));
            //bitmex.MarketOrders(ActiveInstrument.Symbol, "Buy", KontractBuy, reff_time);
            txbKontracts.Text = "kontr =" + KontractBuy;
            lblKontracts.Text = "kontr =" + KontractBuy;
            if (sum != 0)
            {
                MessageBox.Show(sum.ToString());
            }
            else
            {
                MessageBox.Show("qwe");
            }*/

           //tmrAutoTrade.Start();
            string reff_time = ddlStratagy.SelectedItem + ActiveInstrument.Symbol + ddlTF.SelectedItem +
                               nudAverPricePeriod.Value + "_" + nudPriceChannel.Value + "_" + nudWidthCh + "_" +
                               DateTime.UtcNow.DayOfYear + DateTime.UtcNow.Hour + DateTime.UtcNow.Minute +
                               DateTime.UtcNow.Second;

            string reff = ddlStratagy.SelectedItem + ActiveInstrument.Symbol + ddlTF.SelectedItem +
                          nudAverPricePeriod.Value + "_" + nudPriceChannel.Value + "_" + nudWidthCh;
            
        }

        private void txbKontracts_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

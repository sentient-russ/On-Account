﻿namespace oa.Models
{
    public class DashboardBundleModel
    {
        public CurrentRaitoModel currentRaitoModel { get; set; }
        public ReturnOnAssetsModel returnOnAssetsModel { get; set; }
        public ReturnOnEquityModel returnOnEquityModel { get; set; }
        public QuickRatioModel quickRatioModel { get; set; }

        public DashboardBundleModel(
            CurrentRaitoModel currentRaitoModel,
            ReturnOnAssetsModel returnOnAssetsModel,
            ReturnOnEquityModel returnOnEquityModel,
            QuickRatioModel quickRatioModel
            )
        {
            this.currentRaitoModel = currentRaitoModel;
            this.returnOnAssetsModel = returnOnAssetsModel;
            this.returnOnEquityModel = returnOnEquityModel;
            this.quickRatioModel = quickRatioModel;
        }
    }
}

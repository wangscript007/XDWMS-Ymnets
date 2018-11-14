﻿using System.Collections.Generic;
using System.Linq;
using Apps.Web.Core;
using Apps.IBLL.Spl;
using Apps.Locale;
using System.Web.Mvc;
using Apps.Common;
using Apps.IBLL;
using Apps.Models.Spl;
using Unity.Attributes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Data;
using Apps.Models.Sys;
using Apps.BLL.Sys;

namespace Apps.Web.Areas.Spl.Controllers
{
    public class WareOutReportController : BaseController
    {
        [Dependency]
        public ISpl_WarehouseWarrantOUTBLL m_BLL { get; set; }
        [Dependency]
        public ISpl_WarehouseBLL m_WarehouseBLL { get; set; }
        ValidationErrors errors = new ValidationErrors();
        private SysConfigModel siteConfig = new SysConfigBLL().loadConfig(Utils.GetXmlMapPath("Configpath"));
        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.TrueName = GetUserTrueName();
            ViewBag.ComName = siteConfig.webcompany;
            return View();
        }

        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult GetWareOutList(string warehouseId, DateTime begin, DateTime end)
        {
            List<Spl_WareOutReportModel> list = m_BLL.GetWareOutList(warehouseId, begin, end);
            GridRows<Spl_WareOutReportModel> grs = new GridRows<Spl_WareOutReportModel>();

            List<Spl_WareOutReportModel> footerList = new List<Spl_WareOutReportModel>();
            footerList.Add(new Spl_WareOutReportModel()
            {
                TotalPrice = list.Sum(a=>a.QuantityTotal),
            });


            grs.rows = list;
            grs.footer = footerList;
            grs.total = list.Count;
            return Json(grs);
        }



        [HttpPost]
        [SupportFilter(ActionName="Index")]
        public JsonResult GetListParent(GridPager pager, string queryStr)
        {
            List<Spl_WarehouseModel> list = m_WarehouseBLL.GetList(ref pager, queryStr, GetUserId());
            GridRows<Spl_WarehouseModel> grs = new GridRows<Spl_WarehouseModel>();
            grs.rows = list;
            grs.total = pager.totalRows;
            return Json(grs);
        }

        
    }
}


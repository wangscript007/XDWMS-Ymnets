﻿using System.Collections.Generic;
using System.Linq;
using Apps.Web.Core;
using Apps.IBLL.WMS;
using Apps.Locale;
using System.Web.Mvc;
using Apps.Common;
using Apps.IBLL;
using Apps.Models.WMS;
using Unity.Attributes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Data;

namespace Apps.Web.Areas.WMS.Controllers
{
    public class SaleOrderController : BaseController
    {
        [Dependency]
        public IWMS_Sale_OrderBLL m_BLL { get; set; }
        [Dependency]
        public IWMS_InvInfoBLL _InvInfoBll { get; set; }

        ValidationErrors errors = new ValidationErrors();
        
        [SupportFilter]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [SupportFilter(ActionName="Index")]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<WMS_Sale_OrderModel> list = m_BLL.GetList(ref pager, queryStr);
            GridRows<WMS_Sale_OrderModel> grs = new GridRows<WMS_Sale_OrderModel>();
            grs.rows = list;
            grs.total = pager.totalRows;
            return Json(grs);
        }
        #region 创建
        [SupportFilter]
        public ActionResult Create()
        {
            ViewBag.Inv = new SelectList(_InvInfoBll.GetList(ref setNoPagerAscById, ""), "Id", "InvName");
            WMS_Sale_OrderModel model = new WMS_Sale_OrderModel()
            {
                SaleBillNum = "XS" + DateTime.Now.ToString("yyyyMMddHHmmssff"),

            };
            return View(model);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(WMS_Sale_OrderModel model)
        {
            model.Id = 0;
            model.CreatePerson = GetUserId();
            model.CreateTime = ResultHelper.NowTime;
            model.PrintStaus = "未打印";
            model.ConfirmStatus = "未确认";
            if (model.Lot == "[空]")
                model.Lot = "";
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",SaleBillNum" + model.SaleBillNum, "成功", "创建", "WMS_Sale_Order");
                    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",SaleBillNum" + model.SaleBillNum + "," + ErrorCol, "失败", "创建", "WMS_Sale_Order");
                    return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.InsertFail));
            }
        }
        #endregion

        #region 打印
        [SupportFilter(ActionName = "Create")]
        public ActionResult Print()
        {
            return View();
        }

        [HttpPost]
        [SupportFilter(ActionName = "Create")]
        [ValidateInput(false)]
        public JsonResult Print(string saleBillNum)
        {
            try
            {
                var SellBillNum = m_BLL.PrintSaleOrder(ref errors, GetUserId(), saleBillNum);
                if (!String.IsNullOrEmpty(SellBillNum))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "打印投料单成功", "成功", "打印", "WMS_Sale_Order");
                    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed, SellBillNum));
                    //return Redirect("~/Report/ReportManager/ShowBill?reportCode=ReturnOrder&billNum=" + returnOrderNum);
                }
                else
                {
                    LogHandler.WriteServiceLog(GetUserId(), errors.Error, "失败", "打印", "WMS_Sale_Order");
                    return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + errors.Error));
                }

            }
            catch (Exception ex)
            {
                LogHandler.WriteServiceLog(GetUserId(), ex.Message, "失败", "打印", "WMS_Sale_Order");
                return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + ex.Message));
            }
        }
        #endregion

        #region 确认
        [SupportFilter(ActionName = "Edit")]
        public ActionResult Confirm()
        {
            return View();
        }

        [HttpPost]
        [SupportFilter(ActionName = "Edit")]
        [ValidateInput(false)]
        public JsonResult Confirm(string sellBillNum)
        {
            try
            {
                if (m_BLL.ConfirmSaleOrder(ref errors, GetUserId(), sellBillNum))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "SellBillNum" + sellBillNum, "成功", "确认", "WMS_Sale_Order");
                    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed, sellBillNum));
                }
                else
                {
                    LogHandler.WriteServiceLog(GetUserId(), "SellBillNum" + sellBillNum + ", " + errors.Error, "失败", "确认", "WMS_Sale_Order");
                    return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + errors.Error));
                }

            }
            catch (Exception ex)
            {
                LogHandler.WriteServiceLog(GetUserId(), "ReturnOrderNum" + sellBillNum + ", " + ex.Message, "失败", "确认", "WMS_Sale_Order");
                return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + ex.Message));
            }
        }
        #endregion

        #region 修改
        [SupportFilter]
        public ActionResult Edit(long id)
        {
            WMS_Sale_OrderModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(WMS_Sale_OrderModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",SaleBillNum" + model.SaleBillNum, "成功", "修改", "WMS_Sale_Order");
                    return Json(JsonHandler.CreateMessage(1, Resource.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",SaleBillNum" + model.SaleBillNum + "," + ErrorCol, "失败", "修改", "WMS_Sale_Order");
                    return Json(JsonHandler.CreateMessage(0, Resource.EditFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.EditFail));
            }
        }
        #endregion

        #region 详细
        [SupportFilter]
        public ActionResult Details(long id)
        {
            WMS_Sale_OrderModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        #endregion

        #region 删除
        [HttpPost]
        [SupportFilter]
        public ActionResult Delete(long id)
        {
            if(id!=0)
            {
                if (m_BLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "WMS_Sale_Order");
                    return Json(JsonHandler.CreateMessage(1, Resource.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "WMS_Sale_Order");
                    return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail));
            }
        }
        #endregion

        #region 导出导入
        [HttpPost]
        [SupportFilter]
        public ActionResult Import(string filePath)
        {
            if (m_BLL.ImportExcelData(GetUserId(), Utils.GetMapPath(filePath), ref errors))
            {
                 LogHandler.WriteImportExcelLog(GetUserId(), "WMS_Sale_Order", filePath.Substring(filePath.LastIndexOf('/') + 1), filePath, "导入成功");
                 return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed, filePath));
            }
            else
            {
                 LogHandler.WriteImportExcelLog(GetUserId(), "WMS_Sale_Order", filePath.Substring(filePath.LastIndexOf('/') + 1), filePath, "导入失败");
                return Json(JsonHandler.CreateMessage(0, Resource.InsertFail, filePath));
            }
        }
        [HttpPost]
        [SupportFilter(ActionName = "Export")]
        public JsonResult CheckExportData(string queryStr)
        {
            List<WMS_Sale_OrderModel> list = m_BLL.GetList(ref setNoPagerAscById, queryStr);
            if (list.Count().Equals(0))
            {
                return Json(JsonHandler.CreateMessage(0, "没有可以导出的数据"));
            }
            else
            {
                return Json(JsonHandler.CreateMessage(1, "可以导出"));
            }
        }
        [SupportFilter]
        public ActionResult Export(string queryStr)
        {
            List<WMS_Sale_OrderModel> list = m_BLL.GetList(ref setNoPagerAscById, queryStr);
            JArray jObjects = new JArray();
                foreach (var item in list)
                {
                    var jo = new JObject();
                    jo.Add("Id", item.Id);
                    jo.Add("销售单号（业务）", item.SaleBillNum);
                    jo.Add("销售单号（系统）", item.SellBillNum);
                    jo.Add("计划发货日期", item.PlanDeliveryDate);
                    jo.Add("客户", item.CustomerId);
                    jo.Add("物料", item.PartId);
                    jo.Add("数量", item.Qty);
                    jo.Add("箱数", item.BoxQty);
                    jo.Add("库存", item.InvId);
                    jo.Add("子库存", item.SubInvId);
                    jo.Add("批次号：YYYYMM", item.Lot);
                    jo.Add("备注", item.Remark);
                    jo.Add("打印状态", item.PrintStaus);
                    jo.Add("打印日期", item.PrintDate);
                    jo.Add("打印人", item.PrintMan);
                    jo.Add("确认状态", item.ConfirmStatus);
                    jo.Add("确认人", item.ConfirmMan);
                    jo.Add("确认时间", item.ConfirmDate);
                    jo.Add("Attr1", item.Attr1);
                    jo.Add("Attr2", item.Attr2);
                    jo.Add("Attr3", item.Attr3);
                    jo.Add("Attr4", item.Attr4);
                    jo.Add("Attr5", item.Attr5);
                    jo.Add("创建人", item.CreatePerson);
                    jo.Add("创建时间", item.CreateTime);
                    jo.Add("修改人", item.ModifyPerson);
                    jo.Add("修改时间", item.ModifyTime);
                    jObjects.Add(jo);
                }
                var dt = JsonConvert.DeserializeObject<DataTable>(jObjects.ToString());
                var exportFileName = string.Concat(
                    RouteData.Values["controller"].ToString() + "_",
                    DateTime.Now.ToString("yyyyMMddHHmmss"),
                    ".xlsx");
                return new ExportExcelResult
                {
                    SheetName = "Sheet1",
                    FileName = exportFileName,
                    ExportData = dt
                };
            }
        [SupportFilter(ActionName = "Export")]
        public ActionResult ExportTemplate()
        {
            JArray jObjects = new JArray();
            var jo = new JObject();
              jo.Add("Id", "");
              jo.Add("销售单号（业务）", "");
              jo.Add("销售单号（系统）", "");
              jo.Add("计划发货日期", "");
              jo.Add("客户", "");
              jo.Add("PartId", "");
              jo.Add("数量", "");
              jo.Add("箱数", "");
              jo.Add("库存", "");
              jo.Add("子库存", "");
              jo.Add("批次号：YYYYMM", "");
              jo.Add("备注", "");
              jo.Add("打印状态", "");
              jo.Add("打印日期", "");
              jo.Add("打印人", "");
              jo.Add("确认状态", "");
              jo.Add("确认人", "");
              jo.Add("确认时间", "");
              jo.Add("Attr1", "");
              jo.Add("Attr2", "");
              jo.Add("Attr3", "");
              jo.Add("Attr4", "");
              jo.Add("Attr5", "");
              jo.Add("创建人", "");
              jo.Add("创建时间", "");
              jo.Add("修改人", "");
              jo.Add("修改时间", "");
            jo.Add("导入的错误信息", "");
            jObjects.Add(jo);
            var dt = JsonConvert.DeserializeObject<DataTable>(jObjects.ToString());
            var exportFileName = string.Concat(
                    RouteData.Values["controller"].ToString() + "_Template",
                    ".xlsx");
                return new ExportExcelResult
                {
                    SheetName = "Sheet1",
                    FileName = exportFileName,
                    ExportData = dt
                };
            }
        #endregion

        #region 选择投料单
        /// <summary>
        /// 弹出选择送检单
        /// </summary>
        /// <param name="mulSelect">是否多选</param>
        /// <returns></returns>
        [SupportFilter(ActionName = "Create")]
        public ActionResult SaleOrderLookUp(string type, bool mulSelect = false)
        {
            ViewBag.Type = type;
            return View();
        }

        [HttpPost]
        [SupportFilter(ActionName = "Create")]
        public JsonResult SaleOrderGetList(GridPager pager, string type, string queryStr)
        {
            List<WMS_Sale_OrderModel> list;

            if (type == "print")
            {
                list = m_BLL.GetListByWhere(ref pager, "PrintStaus == \"未打印\"")
                    .GroupBy(p => new { p.SaleBillNum, p.SellBillNum })
                    .Select(g => g.First())
                    .OrderBy(p => p.SaleBillNum).ToList();
            }
            else
            {
                list = m_BLL.GetListByWhere(ref pager, "PrintStaus == \"已打印\" and ConfirmStatus == \"未确认\"")
                    .GroupBy(p => new { p.SaleBillNum, p.SellBillNum })
                    .Select(g => g.First())
                    .OrderBy(p => p.SaleBillNum).ToList();
            }
            GridRows<WMS_Sale_OrderModel> grs = new GridRows<WMS_Sale_OrderModel>();
            grs.rows = list;
            grs.total = pager.totalRows;
            return Json(grs);
        }

        [HttpPost]
        [SupportFilter(ActionName = "Create")]
        public JsonResult GetSaleOrderByBillNum(GridPager pager, string type, string billNum)
        {
            List<WMS_Sale_OrderModel> list;

            if (type == "print")
            {
                list = m_BLL.GetListByWhere(ref pager, "SaleBillNum = \"" + billNum + "\"");
            }
            else
            {
                list = m_BLL.GetListByWhere(ref pager, "SellBillNum = \"" + billNum + "\"");
            }
            GridRows<WMS_Sale_OrderModel> grs = new GridRows<WMS_Sale_OrderModel>();
            grs.rows = list;
            grs.total = pager.totalRows;
            return Json(grs);
        }
        #endregion
    }
}


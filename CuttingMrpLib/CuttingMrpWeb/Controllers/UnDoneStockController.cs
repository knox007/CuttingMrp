﻿using CsvHelper.Configuration;
using CuttingMrpLib;
using CuttingMrpWeb.Helpers;
using CuttingMrpWeb.Models;
using CuttingMrpWeb.Properties;
using MvcPaging;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuttingMrpWeb.Controllers
{
    public class UnDoneStockController : Controller
    {
        // GET: UnDoneStock
        [CustomAuthorize]
        public ActionResult Index(int? page)
        {
            int pageIndex = PagingHelper.GetPageIndex(page);

            UnDoneStockSearchModel q = new UnDoneStockSearchModel();

            IUnDoneStockService uss = new UnDoneStockService(Settings.Default.db);

            IPagedList<UnDoneStock> undonestocks = uss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;

            SetPartTypeList(null);
            SetUnDoneStockStateList(null);

            return View(undonestocks);
        }

        public ActionResult Search([Bind(Include = "PartNr, SourceType, State")] UnDoneStockSearchModel q)
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString.Get("page"), out pageIndex);
            pageIndex = PagingHelper.GetPageIndex(pageIndex);

            IUnDoneStockService uss = new UnDoneStockService(Settings.Default.db);

            IPagedList<UnDoneStock> undoneStocks = uss.Search(q).ToPagedList(pageIndex, Settings.Default.pageSize);

            ViewBag.Query = q;
            SetPartTypeList(q.SourceType);
            SetUnDoneStockStateList(q.State);

            return View("Index", undoneStocks);
        }

        public ActionResult ImportUnDoneStock(HttpPostedFileBase undoneStock)
        {
            int excelStartFromLine = 9;
            int csvStartFromLine = 13;

            if (undoneStock == null)
            {
                throw new Exception("No file is uploaded to system");
            }

            var appData = Server.MapPath("~/TmpFile/");
            var filename = Path.Combine(appData,
                DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Path.GetFileName(undoneStock.FileName));
            undoneStock.SaveAs(filename);
            string ex = Path.GetExtension(filename);

            List<UnDoneStockImportModel> records = new List<UnDoneStockImportModel>();
            IUnDoneStockService uss = new UnDoneStockService(Settings.Default.db);

            if (Path.GetExtension(filename).Equals(".xlsx"))
            {
                FileInfo file = new FileInfo(filename);
                using (ExcelPackage ep = new ExcelPackage(file))
                {
                    ExcelWorksheet ws = ep.Workbook.Worksheets.First();

                    for (int i = excelStartFromLine; i <= ws.Dimension.End.Row; i++)
                    {
                        int feedback = int.Parse(ws.Cells[i, 17].Value.ToString());
                        if (feedback == 0)
                        {
                            records.Add(new UnDoneStockImportModel()
                            {
                                KanbanNr = ws.Cells[i, 13].Value.ToString(),
                                Quantity = ws.Cells[i, 16].Value.ToString(),
                                SourceType = 1
                            });
                        }
                    }
                }
                uss.SetStateCancel(PartType.BlueCard);
            }
            else if (Path.GetExtension(filename).Equals(".csv"))
            {
                CsvConfiguration configration = new CsvConfiguration();
                configration.Delimiter = Settings.Default.csvDelimiter;
                configration.HasHeaderRecord = true;
                configration.SkipEmptyRecords = true;
                configration.RegisterClassMap<UnDoneStockCsvModelMap>();
                configration.TrimHeaders = true;
                configration.TrimFields = true;

                using ( TextReader treader = System.IO.File.OpenText(filename))
                {
                    for(int i =0; true; i++)
                    {
                        string s = treader.ReadLine();

                        if (i >= csvStartFromLine)
                        {
                            if (string.IsNullOrWhiteSpace(s))
                            {
                                break;
                            }

                            string[] fields = s.Split(char.Parse(Settings.Default.csvDelimiter));

                            try
                            {
                                records.Add(new UnDoneStockImportModel()
                                {
                                    KanbanNr = fields[6],
                                    Quantity = fields[5],
                                    SourceType = 2
                                });
                            }
                            catch (Exception)
                            {
                                break;
                            }
                            
                        }
                    }
                }
                uss.SetStateCancel(PartType.WhiteCard);
            }

            List<UnDoneStockRecord> usr = new List<UnDoneStockRecord>();
            foreach (UnDoneStockImportModel r in records)
            {
                usr.Add(new UnDoneStockRecord()
                {
                    PartNr = r.KanbanNr,
                    KanbanNr = r.KanbanNr,
                    Quantity = r.CutQuantity,
                    SourceType = r.SourceType
                });
            }

            if (records.Count > 0)
            {
                Hashtable results = uss.ValidateUnDoneStock(usr);

                if (results.ContainsKey("SUCCESS"))
                {
                    foreach (UnDoneStockRecord u in results["SUCCESS"] as List<UnDoneStockRecord>)
                    {
                        UnDoneStock uds = new UnDoneStock()
                        {
                            partNr = u.PartNr,
                            quantity = u.Quantity,
                            kanbanNr = u.KanbanNr,
                            sourceType = u.SourceType,
                            state = u.State,
                            createdAt=DateTime.Now,
                            updatedAt=DateTime.Now
                        };

                        uss.Create(uds);
                    }
                }

                if (results.ContainsKey("WARN"))
                {
                    ViewBag.Msg = "Has Warning!";

                    return View(results["WARN"] as List<UnDoneStockRecord>);
                }
                else
                {
                    ViewBag.Msg = "Finish Success!";

                    return View();
                }
            }
            else
            {
                ViewBag.Msg = "No Record!";

                return View();
            }
        }

        [HttpPost]
        public ActionResult CancelAll()
        {
            IUnDoneStockService dss = new UnDoneStockService(Settings.Default.db);

            dss.SetStateCancel(PartType.Product);

            return RedirectToAction("Index");
        }

        private void SetPartTypeList(int? type, bool allowBlank = true)
        {
            List<EnumItem> item = EnumUtility.GetList(typeof(PartType));

            List<SelectListItem> select = new List<SelectListItem>();
            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }
            foreach (var it in item)
            {
                if (type.HasValue && type.ToString().Equals(it.Value))
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = false });
                }
            }
            ViewData["partTypeList"] = select;
        }

        private void SetUnDoneStockStateList(int? state, bool allowBlank = true)
        {
            List<EnumItem> item = EnumUtility.GetList(typeof(StockState));

            List<SelectListItem> select = new List<SelectListItem>();
            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }
            foreach (var it in item)
            {
                if (state.HasValue && state.ToString().Equals(it.Value))
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = false });
                }
            }
            ViewData["stockStateList"] = select;
        }

    }
}
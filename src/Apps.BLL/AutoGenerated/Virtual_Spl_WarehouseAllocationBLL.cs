//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Apps.Models;
using Apps.Common;
using Unity.Attributes;
using System.Transactions;
using Apps.BLL.Core;
using Apps.Locale;
using LinqToExcel;
using System.IO;
using System.Text;
using Apps.IDAL.Spl;
using Apps.Models.Spl;
using Apps.IBLL.Spl;
namespace Apps.BLL.Spl
{
	public partial class Spl_WarehouseAllocationBLL: Virtual_Spl_WarehouseAllocationBLL,ISpl_WarehouseAllocationBLL
	{
        

	}
	public class Virtual_Spl_WarehouseAllocationBLL
	{
        [Dependency]
        public ISpl_WarehouseAllocationRepository m_Rep { get; set; }

		public virtual List<Spl_WarehouseAllocationModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<Spl_WarehouseAllocation> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
								a=>a.Id.Contains(queryStr)
								
								|| a.Handler.Contains(queryStr)
								|| a.Remark.Contains(queryStr)
								
								
								|| a.Checker.Contains(queryStr)
								
								
								|| a.CreatePerson.Contains(queryStr)
								
								|| a.ModifyPerson.Contains(queryStr)
								
								|| a.FromWarehouseId.Contains(queryStr)
								|| a.ToWarehouseId.Contains(queryStr)
								|| a.ContractNumber.Contains(queryStr)
								);
            }
            else
            {
                queryData = m_Rep.GetList();
            }
            pager.totalRows = queryData.Count();
            //排序
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }

		public virtual List<Spl_WarehouseAllocationModel> GetListByUserId(ref GridPager pager, string userId,string queryStr)
		{
			return new List<Spl_WarehouseAllocationModel>();
		}
		
		public virtual List<Spl_WarehouseAllocationModel> GetListByParentId(ref GridPager pager, string queryStr,object parentId)
        {
			return new List<Spl_WarehouseAllocationModel>();
		}

        public virtual List<Spl_WarehouseAllocationModel> CreateModelList(ref IQueryable<Spl_WarehouseAllocation> queryData)
        {

            List<Spl_WarehouseAllocationModel> modelList = (from r in queryData
                                              select new Spl_WarehouseAllocationModel
                                              {
													Id = r.Id,
													InTime = r.InTime,
													Handler = r.Handler,
													Remark = r.Remark,
													PriceTotal = r.PriceTotal,
													State = r.State,
													Checker = r.Checker,
													CheckTime = r.CheckTime,
													CreateTime = r.CreateTime,
													CreatePerson = r.CreatePerson,
													ModifyTime = r.ModifyTime,
													ModifyPerson = r.ModifyPerson,
													Confirmation = r.Confirmation,
													FromWarehouseId = r.FromWarehouseId,
													ToWarehouseId = r.ToWarehouseId,
													ContractNumber = r.ContractNumber,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, Spl_WarehouseAllocationModel model)
        {
            try
            {
                Spl_WarehouseAllocation entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new Spl_WarehouseAllocation();
               				entity.Id = model.Id;
				entity.InTime = model.InTime;
				entity.Handler = model.Handler;
				entity.Remark = model.Remark;
				entity.PriceTotal = model.PriceTotal;
				entity.State = model.State;
				entity.Checker = model.Checker;
				entity.CheckTime = model.CheckTime;
				entity.CreateTime = model.CreateTime;
				entity.CreatePerson = model.CreatePerson;
				entity.ModifyTime = model.ModifyTime;
				entity.ModifyPerson = model.ModifyPerson;
				entity.Confirmation = model.Confirmation;
				entity.FromWarehouseId = model.FromWarehouseId;
				entity.ToWarehouseId = model.ToWarehouseId;
				entity.ContractNumber = model.ContractNumber;
  

                if (m_Rep.Create(entity))
                {
                    return true;
                }
                else
                {
                    errors.Add(Resource.InsertFail);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }



         public virtual bool Delete(ref ValidationErrors errors, object id)
        {
            try
            {
                if (m_Rep.Delete(id) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public virtual bool Delete(ref ValidationErrors errors, object[] deleteCollection)
        {
            try
            {
                if (deleteCollection != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        if (m_Rep.Delete(deleteCollection) == deleteCollection.Length)
                        {
                            transactionScope.Complete();
                            return true;
                        }
                        else
                        {
                            Transaction.Current.Rollback();
                            return false;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

		
       

        public virtual bool Edit(ref ValidationErrors errors, Spl_WarehouseAllocationModel model)
        {
            try
            {
                Spl_WarehouseAllocation entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              				entity.Id = model.Id;
				entity.InTime = model.InTime;
				entity.Handler = model.Handler;
				entity.Remark = model.Remark;
				entity.PriceTotal = model.PriceTotal;
				entity.State = model.State;
				entity.Checker = model.Checker;
				entity.CheckTime = model.CheckTime;
				entity.CreateTime = model.CreateTime;
				entity.CreatePerson = model.CreatePerson;
				entity.ModifyTime = model.ModifyTime;
				entity.ModifyPerson = model.ModifyPerson;
				entity.Confirmation = model.Confirmation;
				entity.FromWarehouseId = model.FromWarehouseId;
				entity.ToWarehouseId = model.ToWarehouseId;
				entity.ContractNumber = model.ContractNumber;
 


                if (m_Rep.Edit(entity))
                {
                    return true;
                }
                else
                {
                    errors.Add(Resource.NoDataChange);
                    return false;
                }

            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

      

        public virtual Spl_WarehouseAllocationModel GetById(object id)
        {
            if (IsExists(id))
            {
                Spl_WarehouseAllocation entity = m_Rep.GetById(id);
                Spl_WarehouseAllocationModel model = new Spl_WarehouseAllocationModel();
                              				model.Id = entity.Id;
				model.InTime = entity.InTime;
				model.Handler = entity.Handler;
				model.Remark = entity.Remark;
				model.PriceTotal = entity.PriceTotal;
				model.State = entity.State;
				model.Checker = entity.Checker;
				model.CheckTime = entity.CheckTime;
				model.CreateTime = entity.CreateTime;
				model.CreatePerson = entity.CreatePerson;
				model.ModifyTime = entity.ModifyTime;
				model.ModifyPerson = entity.ModifyPerson;
				model.Confirmation = entity.Confirmation;
				model.FromWarehouseId = entity.FromWarehouseId;
				model.ToWarehouseId = entity.ToWarehouseId;
				model.ContractNumber = entity.ContractNumber;
 
                return model;
            }
            else
            {
                return null;
            }
        }


		 /// <summary>
        /// 校验Excel数据,这个方法一般用于重写校验逻辑
        /// </summary>
        public virtual bool CheckImportData(string fileName, List<Spl_WarehouseAllocationModel> list,ref ValidationErrors errors )
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors.Add("导入的数据文件不存在");
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
			 				 excelFile.AddMapping<Spl_WarehouseAllocationModel>(x => x.InTime, "InTime");
				 excelFile.AddMapping<Spl_WarehouseAllocationModel>(x => x.Handler, "Handler");
				 excelFile.AddMapping<Spl_WarehouseAllocationModel>(x => x.Remark, "Remark");
				 excelFile.AddMapping<Spl_WarehouseAllocationModel>(x => x.PriceTotal, "PriceTotal");
				 excelFile.AddMapping<Spl_WarehouseAllocationModel>(x => x.State, "State");
				 excelFile.AddMapping<Spl_WarehouseAllocationModel>(x => x.Checker, "Checker");
				 excelFile.AddMapping<Spl_WarehouseAllocationModel>(x => x.CheckTime, "CheckTime");
				 excelFile.AddMapping<Spl_WarehouseAllocationModel>(x => x.CreateTime, "CreateTime");
				 excelFile.AddMapping<Spl_WarehouseAllocationModel>(x => x.CreatePerson, "CreatePerson");
				 excelFile.AddMapping<Spl_WarehouseAllocationModel>(x => x.ModifyTime, "ModifyTime");
				 excelFile.AddMapping<Spl_WarehouseAllocationModel>(x => x.ModifyPerson, "ModifyPerson");
				 excelFile.AddMapping<Spl_WarehouseAllocationModel>(x => x.Confirmation, "Confirmation");
				 excelFile.AddMapping<Spl_WarehouseAllocationModel>(x => x.FromWarehouseId, "FromWarehouseId");
				 excelFile.AddMapping<Spl_WarehouseAllocationModel>(x => x.ToWarehouseId, "ToWarehouseId");
				 excelFile.AddMapping<Spl_WarehouseAllocationModel>(x => x.ContractNumber, "ContractNumber");
 
            //SheetName
            var excelContent = excelFile.Worksheet<Spl_WarehouseAllocationModel>(0);
            int rowIndex = 1;
            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var entity = new Spl_WarehouseAllocationModel();
						 				  entity.Id = row.Id;
				  entity.InTime = row.InTime;
				  entity.Handler = row.Handler;
				  entity.Remark = row.Remark;
				  entity.PriceTotal = row.PriceTotal;
				  entity.State = row.State;
				  entity.Checker = row.Checker;
				  entity.CheckTime = row.CheckTime;
				  entity.CreateTime = row.CreateTime;
				  entity.CreatePerson = row.CreatePerson;
				  entity.ModifyTime = row.ModifyTime;
				  entity.ModifyPerson = row.ModifyPerson;
				  entity.Confirmation = row.Confirmation;
				  entity.FromWarehouseId = row.FromWarehouseId;
				  entity.ToWarehouseId = row.ToWarehouseId;
				  entity.ContractNumber = row.ContractNumber;
 
                //=============================================================================
                if (errorMessage.Length > 0)
                {
                    errors.Add(string.Format(
                        "第 {0} 列发现错误：{1}{2}",
                        rowIndex,
                        errorMessage,
                        "<br/>"));
                }
                list.Add(entity);
                rowIndex += 1;
            }
            if (errors.Count > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public virtual void SaveImportData(IEnumerable<Spl_WarehouseAllocationModel> list)
        {
            try
            {
                using (DBContainer db = new DBContainer())
                {
                    foreach (var model in list)
                    {
                        Spl_WarehouseAllocation entity = new Spl_WarehouseAllocation();
                       						entity.Id = ResultHelper.NewId;
						entity.InTime = model.InTime;
						entity.Handler = model.Handler;
						entity.Remark = model.Remark;
						entity.PriceTotal = model.PriceTotal;
						entity.State = model.State;
						entity.Checker = model.Checker;
						entity.CheckTime = model.CheckTime;
						entity.CreateTime = ResultHelper.NowTime;
						entity.CreatePerson = model.CreatePerson;
						entity.ModifyTime = model.ModifyTime;
						entity.ModifyPerson = model.ModifyPerson;
						entity.Confirmation = model.Confirmation;
						entity.FromWarehouseId = model.FromWarehouseId;
						entity.ToWarehouseId = model.ToWarehouseId;
						entity.ContractNumber = model.ContractNumber;
 
                        db.Spl_WarehouseAllocation.Add(entity);
                    }
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
		public virtual bool Check(ref ValidationErrors errors, object id,int flag)
        {
			return true;
		}

        public virtual bool IsExists(object id)
        {
            return m_Rep.IsExist(id);
        }
		
		public void Dispose()
        { 
            
        }

	}
}
